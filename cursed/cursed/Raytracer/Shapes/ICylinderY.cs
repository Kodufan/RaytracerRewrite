using cursed.Raytracer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace cursed.Raytracer.Shapes
{
	internal class ICylinderY : ICylinder
	{
		public ICylinderY() : this(Defs.ORIGIN3D, 1, 1) { }

		public ICylinderY(Vector3 pos, double radius, double height) : base(QuadricParameters.cylinderYQParams(radius), pos)
		{
			this.radius = radius;
			this.height = height;
		}

		public override void findClosestIntersection(ref Ray ray, ref HitRecord hit)
		{
			int numHits = base.findIntersections(ref ray, out HitRecord[] hits);

			double topY = center.y + height / 2;
			double bottomY = center.y - height / 2;

			for (int i = 0; i < numHits; i++)
			{
				if (hits[i].interceptPt.y >= bottomY && hits[i].interceptPt.y <= topY)
				{
					hit = hits[i];
					return;
				}
			}
			hit.t = double.MaxValue;
		}

		public override void getTexCoords(Vector3 pt, out double u, out double v)
		{
			throw new NotImplementedException();
		}
	}

}
