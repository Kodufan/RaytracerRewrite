using cursed.Raytracer.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms;

namespace cursed.Raytracer.Shapes
{
	internal class IPlane : IShape
	{
		Vector3 point;
		Vector3 normal;
		double radius;

		public IPlane() : this(Defs.ORIGIN3D, Defs.Z_AXIS) { }

		/**
		 * @fn	IPlane::IPlane(const new Vector3 &pos, const new Vector3 &normal, double rad)
		 * @brief	Implicit representation of an implicit disk.
		 * @param	pos   	Center of disk.
		 * @param	normal	Normal vector of disk.
		 * @param	rad   	Radius of disk.
		 */
		public IPlane(Vector3 pos, Vector3 normal) : base()
		{
			point = pos;
			this.normal = Vector3.Normalize(normal);
		}

		public override void findClosestIntersection(ref Ray ray, ref HitRecord hit)
		{
			double denom = Vector3.Dot(ray.dir, normal);

			if (denom == 0)
			{
				hit.t = double.MaxValue;
			} 
			else
			{
				double numer = Vector3.Dot(point - ray.origin, normal);
				double t = numer / denom;
				if (t < 0)
				{
					hit.t = double.MaxValue;
				}
				else
				{
					hit.t = t;
					hit.interceptPt = ray.getPoint(t);
					hit.normal = normal;
				}
			}
		}

		public override void getTexCoords(Vector3 pt, out double u, out double v)
		{
			throw new NotImplementedException();
		}
	}
}
