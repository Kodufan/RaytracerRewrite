using System;
using System.Collections.Generic;
using System.Text;
using cursed.Raytracer.Core;

namespace cursed.Raytracer
{
	class PositionalLight : LightSource {
		
		Vector3 pos;                  //!< The position of the light.
		bool attenuationIsTurnedOn; //!< true if attenuation is active.
		bool isTiedToWorld;         //!< true if the position is in world (or eye) coordinates.
		LightATParams atParams;

		public PositionalLight(Vector3 position, Color C)
		{
			pos = position;
			lightColor = C;
			attenuationIsTurnedOn = false;
			isTiedToWorld = true;
			atParams = new LightATParams(0, 0, 0);
		}
		
		public Vector3 actualPosition(ref Frame eyeFrame)
		{
			return isTiedToWorld ? pos : eyeFrame.globalCoordToFrameCoords(pos);
		}

		public override Color Illuminate(Vector3 interceptWorldCoords, Vector3 normal, ref Material material, ref Frame eyeFrame, bool inShadow)
		{
			if (!isOn)
			{
				return new Color(0, 0, 0);
			}
			else if (inShadow)
			{
				return ambientColor(material.ambient, lightColor);
			}
			else
			{
				// Converts the light's position to global coordinates if it is tied to the camera
				Vector3 editedLightPos = actualPosition(ref eyeFrame);
				Vector3 v = Vector3.Normalize(eyeFrame.origin - interceptWorldCoords);
				return totalColor(ref material, lightColor, v, normal, editedLightPos, interceptWorldCoords, attenuationIsTurnedOn, atParams);
			}
		}

		public override Ray getShadowFeeler(Vector3 interceptWorldCoords, Vector3 normal, ref Frame eyeFrame)
		{
			Vector3 origin = interceptWorldCoords + (normal * Defs.EPSILON);
			Vector3 dir = actualPosition(ref eyeFrame) - interceptWorldCoords;
			Ray shadowFeeler = new Ray(origin, dir);
			return shadowFeeler;
		}

		public override bool pointIsInAShadow(Vector3 intercept, Vector3 normal, List<VisibleIShape> objects, ref Frame eyeFrame)
		{
			Ray shadowRay = getShadowFeeler(intercept, normal, ref eyeFrame);
			OpaqueHitRecord hit = new OpaqueHitRecord();
			VisibleIShape.findIntersection(ref shadowRay, objects, ref hit);

			double distance = Vector3.Distance(intercept, actualPosition(ref eyeFrame));

			return hit.t < distance;
		}
	}
}
