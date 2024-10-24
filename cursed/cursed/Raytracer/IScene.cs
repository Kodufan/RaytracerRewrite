using System;
using System.Collections.Generic;
using cursed.Raytracer.Core;
using System.Text;
using cursed.Raytracer.Cameras;

namespace cursed.Raytracer
{
	class IScene
	{
		public List<PositionalLight> lights;              //!< All the lights in the scene
		public List<VisibleIShape> opaqueObjs;            //!< All the visible objects in the scene
		public List<TransparentIShapes.TransparentIShape> transparentObjs;   //!< All the transparent objects in the scene
		public RaytracingCamera camera;                       //!< The one camera in the scene

		public IScene()
		{
			lights = new List<PositionalLight>();
			opaqueObjs = new List<VisibleIShape>();
			transparentObjs = new List<TransparentIShapes.TransparentIShape>();
			camera = new PerspectiveCamera(new Vector3(0, 0, 0), new Vector3(0, 0, -1), new Vector3(0, 1, 0), Math.PI / 2, 1, 1);
		}

		/**
		 * @fn	void IScene::addOpaqueObject(const VisibleIShapePtr obj)
		 * @brief	Adds an visible object to the scene
		 * @param	obj	The object to be added.
		 */
		public void addOpaqueObject(VisibleIShape obj) {
			opaqueObjs.Add(obj);
		}

		/**
		 * @fn	void IScene::addTransparentObject(const TransparentIShapePtr obj, double alpha)
		 * @brief	Adds a transparent object to the scene
		 * @param	obj  	The transparent object to be added.
		 */

		public void addTransparentObject(TransparentIShapes.TransparentIShape obj)
		{
			transparentObjs.Add(obj);
		}

		/**
		 * @fn	void IScene::addLight(const PositionalLightPtr light)
		 * @brief	Adds a positional light to the scene.
		 * @param	light	The light to be added.
		 */

		public void addLight(PositionalLight light)
		{
			lights.Add(light);
		} 
	}

}
