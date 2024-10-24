using System;
using System.Collections.Generic;
using System.Text;

namespace cursed.Raytracer.Core
{
	internal class Vector3
	{
		public double x, y, z;
		public Vector3()
		{
			x = 0;
			y = 0;
			z = 0;
		}

		public Vector3(double x, double y, double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public double Length()
		{
			return Distance(this, new Vector3());
		}

		public static double Distance(Vector3 v1, Vector3 v2)
		{
			return Math.Sqrt(Math.Pow(v1.x - v2.x, 2) + Math.Pow(v1.y - v2.y, 2) + Math.Pow(v1.z - v2.z, 2));
		}

		public static double Dot(Vector3 v1, Vector3 v2)
		{
			return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
		}

		public static Vector3 Cross(Vector3 v1, Vector3 v2)
		{
			double x, y, z;
			x = v1.y * v2.z - v2.y * v1.z;
			y = (v1.x * v2.z - v2.x * v1.z) * -1;
			z = v1.x * v2.y - v2.x * v1.y;

			Vector3 output = new Vector3(x, y, z);
			return output;
		}

		public static Vector3 Reflect(Vector3 v, Vector3 axis)
		{
			Vector3 output = new Vector3();
			output.x = v.x - 2 * axis.x * Dot(v, axis);
			output.y = v.y - 2 * axis.y * Dot(v, axis);
			output.z = v.z - 2 * axis.z * Dot(v, axis);
			return output;
		}

		public static Vector3 Normalize(Vector3 v)
		{
			return v / v.Length();
		}

		public static Vector3 operator +(Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
		}

		public static Vector3 operator -(Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
		}

		public static Vector3 operator -(Vector3 v)
		{
			return new Vector3(-v.x, -v.y, -v.z);
		}

		public static Vector3 operator *(Vector3 v, double d)
		{
			return new Vector3(v.x * d, v.y * d, v.z * d);
		}

		public static Vector3 operator *(double d, Vector3 v)
		{
			return new Vector3(v.x * d, v.y * d, v.z * d);
		}

		public static Vector3 operator *(Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
		}
		
		public static Vector3 operator /(Vector3 v, double d)
		{
			return new Vector3(v.x / d, v.y / d, v.z / d);
		}
	}
}
