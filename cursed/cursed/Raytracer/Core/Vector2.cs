using System;
using System.Collections.Generic;
using System.Text;

namespace cursed.Raytracer.Core
{
	internal class Vector2
	{
		public double x, y;
		public Vector2()
		{
			x = 0;
			y = 0;
		}

		public Vector2(double x, double y)
		{
			this.x = x;
			this.y = y;
		}

		public static double Distance(Vector2 v1, Vector2 v2)
		{
			return Math.Sqrt(Math.Pow(v1.x - v2.x, 2) + Math.Pow(v1.y - v2.y, 2));
		}

		public double Length()
		{
			return Distance(this, new Vector2());
		}

		public static Vector2 operator *(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.x * v2.x, v1.y * v2.y);
		}
	}
}
