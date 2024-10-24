using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace cursed.Raytracer.Core
{
	internal class QuadricParameters
	{
		public double A, B, C, D, E, F, G, H, I, J;

		public QuadricParameters() : this(new List<double> { 1, 1, 1, 0, 0, 0, 0, 0, 0, -1 }) { }

		public QuadricParameters(List<double> items) : this(items[0], items[1], items[2], items[3], items[4], items[5], items[6], items[7], items[8], items[9]) { }

		public QuadricParameters(double a, double b, double c, double d, double e, double f,
		double g, double h, double i, double j)
		{
			A = a;
			B = b;
			C = c;
			D = d;
			E = e;
			F = f;
			G = g;
			H = h;
			I = i;
			J = j;
		}

		public static QuadricParameters cylinderXQParams(double r)
		{
			double R2 = r * r;
			return new QuadricParameters(0.0, 1.0 / R2, 1.0 / R2, 0, 0, 0, 0, 0, 0, -1);
		}

		public static QuadricParameters cylinderYQParams(double r)
		{
			double R2 = r * r;
			return new QuadricParameters(1.0 / R2, 0.0, 1.0 / R2, 0, 0, 0, 0, 0, 0, -1);
		}

		public static QuadricParameters cylinderZQParams(double r)
		{
			double R2 = r * r;
			return new QuadricParameters(1.0 / R2, 1.0 / R2, 0.0, 0, 0, 0, 0, 0, 0, -1);
		}
		
		static QuadricParameters coneYQParams(double R, double H)
		{
			double R2 = Math.Pow(H, 2.0) / Math.Pow(R, 2.0);
			return new QuadricParameters(R2, -1.0, R2, 0, 0, 0, 0, 0, 0, 0);
		}
		
		public static QuadricParameters sphereQParams(double R)
		{
			double R2 = R * R;
			return new QuadricParameters(1, 1, 1, 0, 0, 0, 0, 0, 0, -R2);
		}
		
		static QuadricParameters ellipsoidQParams(Vector3 sz)
		{
			Vector3 size = sz * sz;
			return new QuadricParameters(1.0 / size.x, 1.0 / size.y, 1.0 / size.z, 0, 0, 0, 0, 0, 0, -1);
		}
			
	}
}
