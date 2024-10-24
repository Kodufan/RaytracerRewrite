using System;
using System.Collections.Generic;
using cursed.Raytracer.Core;
using System.Text;
using Xamarin.Forms.PlatformConfiguration;

namespace cursed.Raytracer.Cameras
{
    abstract class RaytracingCamera
    {
        public Frame cameraFrame;                  //!< The camera's frame
        public int nx, ny;                         //!< Window size
        public double left, right, bottom, top;    //!< The camera's vertical field of view

        public RaytracingCamera(Vector3 viewingPos, Vector3 lookAtPt, Vector3 up, int width, int height)
		{
			setupFrame(viewingPos, lookAtPt, up);
		}
        public abstract Ray getRay(double x, double y);

        public Frame getFrame() { return cameraFrame; }
        public int getNX() { return nx; }
        public int getNY() { return ny; }
        public double getLeft() { return left; }
        public double getRight() { return right; }
        public double getBottom() { return bottom; }
        public double getTop() { return top; }

        void setupFrame(Vector3 viewingPos, Vector3 lookAtPt, Vector3 up)
        {
			Vector3 viewingDirection = lookAtPt - viewingPos;
			Vector3 w = Vector3.Normalize(-viewingDirection);
			Vector3 u = Vector3.Normalize(Vector3.Cross(up, w));
			Vector3 v = Vector3.Normalize(Vector3.Cross(w, u));
			cameraFrame = new Frame(viewingPos, u, v, w);
		}

		public abstract void setupViewingParameters(int width, int height);

        public Vector2 getProjectionPlaneCoordinates(double x, double y)
        {
			Vector2 s = new Vector2();
			s.x = Utilities.map(x + 0.5, 0, nx, left, right);
			s.y = Utilities.map(y + 0.5, 0, ny, bottom, top);
			return s;
		}
    }
}
