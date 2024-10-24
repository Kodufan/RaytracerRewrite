using System;
using System.Collections.Generic;
using cursed.Raytracer.Core;
using System.Text;

namespace cursed.Raytracer
{
	class OpaqueHitRecord : HitRecord
	{
		public Material material;      //!< the Material value of the object.
		public Image texture;         //!< the texture associated with this object, if any (nullptr when not textured).
		public double u, v;            //!< (u,v) correpsonding to intersection point.

		/**
		 * @fn	static HitRecord getClosest(const vector<HitRecord> &hits)
		 * @brief	Gets a closest, give a vector of hits.
		 * @param	hits	The hits to consider.
		 * @return	The closest hit, that is in front of the camera.
		 */


		public static OpaqueHitRecord GetClosest(ref List<OpaqueHitRecord> hits) 
		{
			OpaqueHitRecord theClosestHit = new OpaqueHitRecord();
			for (int i = 0; i < hits.Count; i++) 
			{
				if (Utilities.inRangeExclusive(hits[i].t, 0, theClosestHit.t)) 
				{
					theClosestHit = hits[i];
				}
			}
			return theClosestHit;
		}
	}
}
