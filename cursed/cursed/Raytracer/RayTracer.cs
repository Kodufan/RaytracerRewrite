using cursed.Raytracer.Cameras;
using System;
using System.Collections.Generic;
using cursed.Raytracer.Core;
using System.Text;
using static Xamarin.Essentials.Permissions;
using System.Threading.Tasks;
using System.Linq;

namespace cursed.Raytracer
{
	internal class RayTracer
	{
		Color defaultColor;
		IScene theScene;
		public static int X;
		public static int Y;
		/**
		 * @fn	RayTracer::RayTracer(const color &defa)
		 * @brief	Constructs a raytracers.
		 * @param	defa	The clear color.
		 */

		public RayTracer(Color defaultColor, ref IScene theScene)
		{
			this.defaultColor = defaultColor;
			this.theScene = theScene;
		}

		public void raytraceScene(ref FrameBuffer frameBuffer, int depth, int aaFactor) {
			RaytracingCamera camera = theScene.camera;
			List<VisibleIShape> objs = theScene.opaqueObjs;
			List<TransparentIShapes.TransparentIShape> transparentObjs = theScene.transparentObjs;
			List<PositionalLight> lights = theScene.lights;


			for (int y = 0; y < frameBuffer.HEIGHT; ++y)
			{ 
				for (int x = 0; x < frameBuffer.WIDTH; ++x) 
				{
					X = x;
					Y = y;
					Utilities.DEBUG_PIXEL = (x == Utilities.xDebug && y == Utilities.yDebug);
					if (Utilities.DEBUG_PIXEL) 
					{
						Console.WriteLine();
					}
					
					frameBuffer.SetColor(x, y, DrawPixel(theScene.camera.getRay(x, y), depth, aaFactor, x, y));
				}
			}
		}

		/**
		 * @fn	color RayTracer::traceIndividualRay(const Ray &ray,
		 *											const IScene &theScene,
		 *											int recursionLevel) const
		 * @brief	Trace an individual ray.
		 * @param	ray			  	The ray.
		 * @param	theScene	  	The scene.
		 * @param	recursionLevel	The recursion level.
		 * @return	The color to be displayed as a result of this ray.
		 */
		Color DrawPixel(Ray ray, int recursionLevel, int aaFactor, int x, int y)
		{
			bool defaultAA = (aaFactor == 1);

			Color totalColor = new Color(0, 0, 0);
			for (int i = 1; i <= aaFactor; i++)
			{
				for (int j = 1; j <= aaFactor; j++)
				{
					if (!defaultAA)
					{
						ray = theScene.camera.getRay(x + (1.0 / (2.0 * i)), y + (1.0 / (2.0 * j)));
					}
					totalColor += traceIndividualRay(ray, recursionLevel + 1);
				}
			}

			return totalColor / (aaFactor * aaFactor);
		}
		
		Color traceIndividualRay(Ray ray, int recursionLevel) 
		{
			if (recursionLevel <= 0) 
			{
				return new Color(0, 0, 0, 0);
			}
			
			OpaqueHitRecord opaqueHit = new OpaqueHitRecord();
			VisibleIShape.findIntersection(ref ray, theScene.opaqueObjs, ref opaqueHit);
			TransparentHitRecord transparentHit = new TransparentHitRecord();
			TransparentIShapes.TransparentIShape.findIntersection(ref ray, theScene.transparentObjs, ref transparentHit);

			bool hitOpaque = opaqueHit.t != double.MaxValue;
			bool hitTransparent = transparentHit.t != double.MaxValue;
			bool hitOpaqueFirst = opaqueHit.t < transparentHit.t;
			bool hitTransparentFirst = transparentHit.t < opaqueHit.t;
			bool isTextured = opaqueHit.texture != null;
			double reflectionVal = (recursionLevel == 1) ? 0.0 : 0.3;

			Color result = new Color(0, 0, 0, 0);
			
			if (hitOpaque) 
			{
				if (isTextured) 
				{
					Color texel = opaqueHit.texture.getPixelUV(opaqueHit.u, opaqueHit.v);
					result += texel * 0.5;
					//frameBuffer.setColor(x, y, texel);
				}
				for (int i = 0; i < theScene.lights.Count; i++)
				{
					Frame frame = theScene.camera.getFrame();
					bool inShadow = theScene.lights[i].pointIsInAShadow(opaqueHit.interceptPt, opaqueHit.normal, theScene.opaqueObjs, ref frame);
					if (!Config.hasShadows)
					{
						inShadow = false;
					}
					bool backface = Vector3.Dot(opaqueHit.normal, ray.dir) > 0;
					if (backface)
					{
						opaqueHit.normal = -opaqueHit.normal;
					}
					Color tempColor = theScene.lights[i].Illuminate(opaqueHit.interceptPt, opaqueHit.normal, ref opaqueHit.material, ref frame, inShadow);
					result += (isTextured) ? tempColor * .5 : tempColor;
				}
			}

		if (hitTransparentFirst && hitOpaque)
		{
			result = transparentHit.transColor * transparentHit.alpha + (result * (1 - transparentHit.alpha));
		}

		if (hitTransparentFirst && !hitOpaque)
		{
			return transparentHit.transColor * transparentHit.alpha + (defaultColor * (1 - transparentHit.alpha));
		}

		if (!hitOpaque && !hitTransparent)
		{
			return defaultColor;
		}
		
		Vector3 newIntercept = opaqueHit.interceptPt + (opaqueHit.normal * new Vector3(Defs.EPSILON, Defs.EPSILON, Defs.EPSILON));
		Ray reflectionRay = new Ray(newIntercept, Vector3.Reflect(ray.dir, opaqueHit.normal));
		return Color.Clamp((result * (1 - reflectionVal)) + (traceIndividualRay(reflectionRay, recursionLevel - 1) * reflectionVal), 0, 1);
		}
	}
}
