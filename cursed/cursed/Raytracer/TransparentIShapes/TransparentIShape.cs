using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace cursed.Raytracer.TransparentIShapes
{
	internal class TransparentIShape
	{
		IShape shape; //!< Pointer to underlying implicit shape.
		Color c;            //!< basic color of the transparent object
		double alpha;       //!< alpha value of transparent object.

		public TransparentIShape(ref IShape shapePtr, Color C, double alpha)
		{
			shape = shapePtr;
			c = C;
			this.alpha = alpha;
		}
	
		public void findClosestIntersection(ref Ray ray, ref TransparentHitRecord hit)
		{
			HitRecord hitRecord = hit;
			shape.findClosestIntersection(ref ray, ref hitRecord);
			if (hit.t != double.MaxValue)
			{
				hit.transColor = c;
				hit.alpha = alpha;
			}
		}

		public static void findIntersection(ref Ray ray, List<TransparentIShape> surfaces, ref TransparentHitRecord theHit) 
		{
			theHit.t = double.MaxValue;
			for (int i = 0; i < surfaces.Count; i++) {
				TransparentHitRecord thisHit = new TransparentHitRecord();
				surfaces[i].findClosestIntersection(ref ray, ref thisHit);
				if (thisHit.t < theHit.t) {
					theHit = thisHit;
				}
		}
}
	}
}
