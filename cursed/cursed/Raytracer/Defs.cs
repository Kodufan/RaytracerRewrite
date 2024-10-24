using System;
using System.Collections.Generic;
using System.Text;
using cursed.Raytracer.Core;

namespace cursed.Raytracer
{
	internal class Defs
	{
		public const string username = "duvalljc";
		public const double EPSILON = .001;      //!< default value used for "SMALL" tolerances.

		public const int TIME_INTERVAL = 100;      //!< default time interval used timers.
		public const int WINDOW_WIDTH = 500;       //!< default window width.
		public const int WINDOW_HEIGHT = 250;      //!< default window height.
		public const char ESCAPE = (char)27;    //!< escape key.
		public const int DEFAULT_SLICES = 8;               //!< default value used when slicing up a curved object.

		public const double PI = 3.14159265358979323846264338327950288419716939937510582097494;    //!< pi
		public const double TWO_PI = 2 * PI;       //!< 2pi	(360 degrees)
		public const double PI_2 = PI / 2.0;       //!< pi/2	(90 degrees)
		public const double PI_3 = PI / 3.0;       //!< pi/3	(60 degrees)
		public const double PI_4 = PI / 4.0;       //!< pi/4	(45 degrees)
		public const double PI_6 = PI / 6.0;       //!< pi/6	(30 degrees)

		public static readonly Vector3 ORIGIN3D = new Vector3(0, 0, 0);            //!< (0, 0, 0)
		public static readonly Vector2 ORIGIN2D = new Vector2(0, 0);                 //!< (0, 0)
		
		public static readonly Vector3 ZEROVEC = new Vector3(0, 0, 0);         //!< <0, 0, 0>
		public static readonly Vector3 X_AXIS = new Vector3(1, 0, 0);          //!< <1, 0, 0>
		public static readonly Vector3 Y_AXIS = new Vector3(0, 1, 0);          //!< <0, 1, 0>
		public static readonly Vector3 Z_AXIS = new Vector3(0, 0, 1);          //!< <0, 0, 1>

		
	}
	
	/**
	* @class	BoundingBoxi
	* @brief	A bounding box in 2D, with integer positions and widths.
	*/
	class BoundingBoxi
	{
		int lx;         //!< lower left corner's x value
		int width;      //!< width of box
		int ly;         //!< lower left corner's y value
		int height;     //!< height of box
		BoundingBoxi(int left, int width, int bottom, int height)
		{
			lx = left;
			this.width = width;
			ly = bottom;
			this.height = height;
		}

		public double aspectRatio()
		{
			return (double)width / (double)height;
		}
	}

	/**
	 * @struct	Frame
	 * @brief	Represents a coordinate frame
	 */
	class Frame
	{
		public Vector3 u;        //!< frame's "x" axis
		public Vector3 v;        //!< frame's "y" axis
		public Vector3 w;        //!< frame's "z" axis
		public Vector3 origin;   //!< location of frame's origin
		Matrix4 inverse;  //!< The inverse of the frame's transformation

		public Frame() { }
		public Frame(Vector3 O, Vector3 U, Vector3 V, Vector3 W) 
		{
			origin = O;
			u = U;
			v = V;
			w = W;
			setInverse();
		}

		public Vector3 globalCoordToFrameCoords(Vector3 pt)
		{
			Core.Vector4 returnVec = inverse * new Core.Vector4(pt.x, pt.y, pt.z, 1);
			return new Vector3(returnVec.x, returnVec.y, returnVec.z);
		}
		Vector3 frameCoordsToGlobalCoords(Vector3 pt)
		{
			return origin + pt.x * u + pt.y * v + pt.z * w;
		}
		
		Vector3 globalVectorToFrameVector(Vector3 V)
		{
			Vector3 A = globalCoordToFrameCoords(V);
			Vector3 B = globalCoordToFrameCoords(Defs.ORIGIN3D);
			return A - B;
		}
		
		Vector3 frameVectorToWorldVector(Vector3 V)
		{
			Vector3 vectorHead = origin + u * V.x + v * V.y + w * V.z;
			Vector3 vectorTail = origin;
			return vectorHead - vectorTail;
		}

		static Frame createOrthoNormalBasis(Vector3 pos, Vector3 w, Vector3 up)
		{
			Frame frame = new Frame();
			frame.origin = pos;
			frame.w = Vector3.Normalize(w);
			frame.u = Vector3.Normalize(Vector3.Cross(up, w));
			frame.v = Vector3.Normalize(Vector3.Cross(frame.w, frame.u));
			frame.setInverse();
			return frame;
		}

		static Frame createOrthoNormalBasis(Vector3 pos, Vector3 w)
		{
			Vector3 wNormed = Vector3.Normalize(w);

			Vector3 fakeUp = wNormed;
			double minNum = Utilities.min(w.x, w.y, w.z);
			if (w.x == minNum) { fakeUp.x = 1; }
			else if (w.y == minNum) { fakeUp.y = 1; }
			else { fakeUp.z = 1; }

			return createOrthoNormalBasis(pos, w, fakeUp);
		}

		static Frame createOrthoNormalBasis(Matrix4 viewingMatrix)
		{
			return null;
			//Matrix4 vmInverse = Matrix4.Invert(viewingMatrix);
			//Vector3 u = new Vector3(vmInverse[0]);
			//dvec3 v(vmInverse[1]);
			//dvec3 w(vmInverse[2]);
			//dvec3 eye(vmInverse[3]);
			//return Frame(eye, u, v, w);
		}

		Matrix4 toViewingMatrix()
		{
			return Matrix4.Inverse(
				new Matrix4(u.x, u.y, u.z, 0,
				v.x, v.y, v.z, 0,
				w.x, w.y, w.z, 0,
				origin.x, origin.y, origin.z, 1));
		}

		void setInverse()
		{
			Matrix4 T = new Matrix4();
			T.Column0 = new Core.Vector4(u.x, u.y, u.z, T.Column0.w);
			T.Column1 = new Core.Vector4(v.x, v.y, v.z, T.Column1.w);
			T.Column2 = new Core.Vector4(w.x, w.y, w.z, T.Column2.w);
			T.Column3 = new Core.Vector4(origin.x, origin.y, origin.z, T.Column3.w);

			inverse = Matrix4.Inverse(T);
		}
	}
}
