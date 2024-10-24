using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace cursed.Raytracer
{
    internal class VisibleIShape
    {
        public Material material;
        public IShape shape;
        public Image texture;

        public VisibleIShape(ref IShape shape, Material mat, Image image = null)
        {
			this.shape = shape;
            material = mat;
            texture = image;
        }

        public static void findIntersection(ref Ray ray, List<VisibleIShape> surfaces, ref OpaqueHitRecord theHit)
        {
            theHit.t = double.MaxValue;
			foreach (VisibleIShape shape in surfaces)
			{
				OpaqueHitRecord thisHit = new OpaqueHitRecord();
				shape.findClosestIntersection(ref ray, ref thisHit);
				if (thisHit.t < theHit.t)
				{
					theHit = thisHit;
				}
			}
        }
		
        public void findClosestIntersection(ref Ray ray, ref OpaqueHitRecord hit)
        {
            HitRecord hitRecord = hit;
            shape.findClosestIntersection(ref ray, ref hitRecord);
			if (hitRecord.t != double.MaxValue)
			{
				hit.t = hitRecord.t;
				hit.material = material;
				hit.texture = texture;
				if (hit.texture != null)
				{
					shape.getTexCoords(hit.interceptPt, out hit.u, out hit.v);
				}
			}
		}
	}
}
