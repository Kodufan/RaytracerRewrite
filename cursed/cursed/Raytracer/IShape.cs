using cursed.Raytracer.Core;

namespace cursed.Raytracer
{
	abstract class IShape
	{
		public IShape() { }
		public abstract void findClosestIntersection(ref Ray ray, ref HitRecord hit);
		public abstract void getTexCoords(Vector3 pt, out double u, out double v);
		public static Vector3 movePointOffSurface(Vector3 pt, Vector3 n)
		{
			Vector3 diff = n * new Vector3(Defs.EPSILON, Defs.EPSILON, Defs.EPSILON);
			return pt + diff;
		}
	}
}
