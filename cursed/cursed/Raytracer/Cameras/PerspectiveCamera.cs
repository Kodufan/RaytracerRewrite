using System;
using System.Collections.Generic;
using System.Text;
using cursed.Raytracer.Core;

namespace cursed.Raytracer.Cameras
{
	class PerspectiveCamera : RaytracingCamera
	{
		double fov;                     //!< The camera's field of view
		double distToPlane;             //!< Distance to image plane

		public PerspectiveCamera(Vector3 pos, Vector3 lookAtPt, Vector3 up, double FOVRads, int width, int height) : base(pos, lookAtPt, up, width, height)
		{
			fov = FOVRads;
			setupViewingParameters(width, height);
		}

		public override Ray getRay(double x, double y)
		{
			Vector2 uv = getProjectionPlaneCoordinates(x, y);
			Vector3 rayDirection = Vector3.Normalize(new Vector3(-distToPlane, -distToPlane, -distToPlane) 
				* cameraFrame.w + uv.x * cameraFrame.u + uv.y * cameraFrame.v);
			return new Ray(cameraFrame.origin, rayDirection);
		}

		public double getDistToPlane() { return distToPlane; }
		
		public override void setupViewingParameters(int width, int height)
		{
			nx = width;
			ny = height;

			double fov_2 = fov / 2.0;
			distToPlane = 1.0 / Math.Tan(fov_2);

			top = 1.0;
			bottom = -top;

			right = top * ((double)nx / ny);
			left = -right;
		}
	}
}
