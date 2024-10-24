using System;
using System.Collections.Generic;
using System.Text;
using cursed.Raytracer.Core;

namespace cursed.Raytracer
{
	class HitRecord
	{
		public double t;               //!< the t value where the intersection took place.
		public Vector3 interceptPt;      //!< the (x,y,z) value where the intersection took place.
		public Vector3 normal;           //!< the normal vector at the intersection point.

		public HitRecord()
		{
			t = double.MaxValue;
			interceptPt = new Vector3();
			normal = new Vector3();
		}
	}
}
