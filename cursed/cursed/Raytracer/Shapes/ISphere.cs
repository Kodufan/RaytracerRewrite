using cursed.Raytracer.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace cursed.Raytracer.Shapes
{
	internal class ISphere : IQuadricSurface
	{
		public ISphere(Vector3 position, double radius) : base(QuadricParameters.sphereQParams(radius), position) { }

		public override void findClosestIntersection(ref Ray ray, ref HitRecord hit)
		{
			findClosestIntersectionQuadric(ref ray, ref hit);
		}

		public override void getTexCoords(Vector3 pt, out double u, out double v)
		{
			double az, el;
			double R;
			Vector3 delta = pt - center;
			Utilities.computeAzimuthAndElevationFromXYZ(delta, out R, out az, out el);
			u = Utilities.map(az, -Defs.PI, Defs.PI, 0.0, 1.0);
			v = 1.0 - Utilities.map(el, -Defs.PI_2, Defs.PI_2, 0.0, 1.0);
		}
	}
}
