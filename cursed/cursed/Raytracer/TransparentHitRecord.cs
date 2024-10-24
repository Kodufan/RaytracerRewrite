using System;
using System.Collections.Generic;
using System.Text;

namespace cursed.Raytracer
{
	internal class TransparentHitRecord : HitRecord
	{
		public Color transColor;       //!< the color of this transparent material
		public double alpha;           //!< the alpha value for this transparent material
	}
}
