using cursed.Raytracer.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms;

namespace cursed.Raytracer.Shapes
{
	internal class IDisk : IShape
	{
		Vector3 center;
		Vector3 normal;
		double radius;
		
		IDisk() : this(Defs.ORIGIN3D, Defs.Y_AXIS, 1.0) { }

		/**
		 * @fn	IDisk::IDisk(const new Vector3 &pos, const new Vector3 &normal, double rad)
		 * @brief	Implicit representation of an implicit disk.
		 * @param	pos   	Center of disk.
		 * @param	normal	Normal vector of disk.
		 * @param	rad   	Radius of disk.
		 */

		public IDisk(Vector3 pos, Vector3 normal, double rad) : base()
		{
			center = pos;
			this.normal = Vector3.Normalize(normal);
			radius = rad;
		}

		public override void findClosestIntersection(ref Ray ray, ref HitRecord hit)
		{
			IPlane p = new IPlane(center, normal);
			p.findClosestIntersection(ref ray, ref hit);

			if (hit.t == double.MaxValue)
			{
				return;
			}

			hit.t = (Vector3.Distance(center, hit.interceptPt) > radius) ? double.MaxValue : hit.t;
		}

		public override void getTexCoords(Vector3 pt, out double u, out double v)
		{
			throw new NotImplementedException();
		}
	}
}
