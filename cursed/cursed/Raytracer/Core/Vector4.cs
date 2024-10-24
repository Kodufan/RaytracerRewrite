using System;
using System.Collections.Generic;
using System.Text;

namespace cursed.Raytracer.Core
{
	internal class Vector4
	{
		public double x, y, z, w;
		public Vector4()
		{
			x = 0;
			y = 0;
			z = 0;
			w = 0;
		}
		
		public Vector4(double x, double y, double z, double w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}
		
		public static double Dot(Vector4 v1, Vector4 v2)
		{
			return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z + v1.w * v2.w;
		}

		public static double Distance(Vector4 v1, Vector4 v2)
		{
			return Math.Sqrt(Math.Pow(v1.x - v2.x, 2) + Math.Pow(v1.y - v2.y, 2) + Math.Pow(v1.z - v2.z, 2) + Math.Pow(v1.w - v2.w, 2));
		}

		public double Length()
		{
			return Distance(this, new Vector4());
		}

		public static Vector4 operator /(Vector4 v, double d)
		{
			return new Vector4(v.x / d, v.y / d, v.z / d, v.w / d);
		}
	}
}
