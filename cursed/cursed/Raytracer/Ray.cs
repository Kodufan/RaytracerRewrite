using System;
using System.Collections.Generic;
using System.Text;
using cursed.Raytracer.Core;

namespace cursed.Raytracer
{
	class Ray
	{
		public Vector3 origin;       //!< starting point for this ray
		public Vector3 dir;          //!< direction for this ray, given it's origin
		
		public Ray(Vector3 rayOrigin, Vector3 rayDirection)
		{
			origin = rayOrigin;
			dir = Vector3.Normalize(rayDirection);
		}
		
		public Vector3 getPoint(double t) 
		{
			Vector3 temp = new Vector3(t + dir.x, t + dir.y, t + dir.z);
			return origin + t * dir;
		}
	}
}
