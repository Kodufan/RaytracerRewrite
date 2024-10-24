using System;
using System.Collections.Generic;
using cursed.Raytracer.Core;
using System.Text;

namespace cursed.Raytracer.Shapes
{
	abstract class IQuadricSurface : IShape
	{
		public Vector3 center;   //!< center of quadric
		public QuadricParameters qParams;      //!< The parameters that make up the quadric
		public double twoA;                    //!< 2*A
		public double twoB;                    //!< 2*B
		public double twoC;                    //!< 2*C

		

		public IQuadricSurface(List<double> parameters, Vector3 position) : this(new QuadricParameters(parameters), position) { }

		public IQuadricSurface(Vector3 position) : this(new QuadricParameters(), position) { }

		public IQuadricSurface(QuadricParameters parameters, Vector3 position) : base()
		{
			qParams = parameters;
			center = position;
			twoA = 2 * parameters.A;
			twoB = 2 * parameters.B;
			twoC = 2 * parameters.C;
		}

		public void findClosestIntersectionQuadric(ref Ray ray, ref HitRecord hit)
		{
			hit.t = double.MaxValue;

			int numIntercepts = findIntersections(ref ray, out HitRecord[] hits);
			if (numIntercepts == 1 && hits[0].t > 0)
			{
				hit.t = hits[0].t;
				hit.interceptPt = hits[0].interceptPt;
				hit.normal = normal(hit.interceptPt);
			}
			else if (numIntercepts == 2)
			{
				if (hits[0].t > 0)
				{
					hit.t = hits[0].t;
					hit.interceptPt = hits[0].interceptPt;
					hit.normal = normal(hit.interceptPt);
				}
				else if (hits[1].t > 0)
				{
					hit.t = hits[1].t;
					hit.interceptPt = hits[1].interceptPt;
					hit.normal = normal(hit.interceptPt);
				}
			}
		}

		public int findIntersections(ref Ray ray, out HitRecord[] hits)
		{
			hits = new HitRecord[2];
			hits[0] = new HitRecord();
			hits[1] = new HitRecord();

			double Aq = -1, Bq = -1, Cq = -1;
			computeAqBqCq(ref ray, ref Aq, ref Bq, ref Cq);

			int numRoots = Utilities.quadratic(Aq, Bq, Cq, out double[] roots);
			int numIntersections = 0;

			for (int i = 0; i < numRoots; i++)
			{
				if (roots[i] > 0)
				{	
					double t = roots[i];
					hits[numIntersections].t = t;
					hits[numIntersections].interceptPt = ray.origin + t * ray.dir;
					Vector3 intercept = hits[numIntersections].interceptPt;
					hits[numIntersections].normal = normal(intercept);
					numIntersections++;
				} 
			}

			return numIntersections;
		}

		Vector3 normal(Vector3 P) 
		{
			double A = qParams.A;
			double B = qParams.B;
			double C = qParams.C;
			double D = qParams.D;
			double E = qParams.E;
			double F = qParams.F;
			double G = qParams.G;
			double H = qParams.H;
			double I = qParams.I;
			double J = qParams.J;
			Vector3 pt = P - center;
			Vector3 normal = new Vector3(twoA * pt.x + D * pt.y + E * pt.z + G,
				twoB * pt.y + D * pt.x + F * pt.z + H,
				twoC * pt.z + E * pt.x + F * pt.y + I);
			return Vector3.Normalize(normal);
		}

		void computeAqBqCq(ref Ray ray, ref double Aq, ref double Bq, ref double Cq)
		{
			Vector3 Ro = ray.origin - center;
			Vector3 Rd = ray.dir;
			double A = qParams.A;
			double B = qParams.B;
			double C = qParams.C;
			double D = qParams.D;
			double E = qParams.E;
			double F = qParams.F;
			double G = qParams.G;
			double H = qParams.H;
			double I = qParams.I;
			double J = qParams.J;
			Aq = A * (Rd.x * Rd.x) +
				B * (Rd.y * Rd.y) +
				C * (Rd.z * Rd.z) +
				D * (Rd.x * Rd.y) +
				E * (Rd.x * Rd.z) +
				F * (Rd.y * Rd.z);

			Bq = twoA * Ro.x * Rd.x +
				twoB * Ro.y * Rd.y +
				twoC * Ro.z * Rd.z +
				D * (Ro.x * Rd.y + Ro.y * Rd.x) +
				E * (Ro.x * Rd.z + Ro.z * Rd.x) +
				F * (Ro.y * Rd.z + Ro.z * Rd.y) +
				G * Rd.x + H * Rd.y + I * Rd.z;

			Cq = A * (Ro.x * Ro.x) +
				B * (Ro.y * Ro.y) +
				C * (Ro.z * Ro.z) +
				D * (Ro.x * Ro.y) +
				E * (Ro.x * Ro.z) +
				F * (Ro.y * Ro.z) +
				G * Ro.x +
				H * Ro.y +
				I * Ro.z + J;
		}
	}
}
