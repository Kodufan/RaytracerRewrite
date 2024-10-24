using System;
using System.Collections.Generic;
using System.Text;
using cursed.Raytracer.Core;

namespace cursed.Raytracer
{
	internal class Utilities
	{
		public static bool DEBUG_PIXEL = false;
		public static int xDebug = 245, yDebug = 27;

		void swap(double a, double b)
		{
			double temp = a;
			a = b;
			b = temp;
		}

		/**
		 * @fn	bool approximatelyEqual(double a, double b)
		 * @brief	Determines if two values are approximately equal.
		 * 			That is, their values within EPSILON of each other.
		 * Programming constraint: Use EPSILON defined in defs.h
		 * @param	a	The first value.
		 * @param	b	The second value.
		 * @return	true iff (a-b) is in [-EPSILON, EPSILON].
		 * @test	approximatelyEqual(3.000000, 3.0000000001) --> true
		 * @test	approximatelyEqual(3.000000, 3.1) --> false
		*/

		bool approximatelyEqual(double a, double b)
		{
			return (Math.Abs(a - b) <= Defs.EPSILON);
		}

		/**
		 * @fn	bool approximatelyZero(double a)
		 * @brief	Determines if a value is approximately zero.
		 * 			That is, the value is within EPSILON of zero.
		 * Programming constraint: Use EPSILON defined in defs.h
		 * @param	a	The value.
		 * @return	true iff a is in [-EPSILON, EPSILON].
		 * @test	approximatelyZero(0.0000001) --> true
		 * @test	approximatelyZero(0.1) --> false
		 */

		bool approximatelyZero(double a)
		{
			return approximatelyEqual(a, 0);
		}

		/**
		 * @fn	double normalizeDegrees(double degrees)
		 * @brief	Converts an arbitrary number of degrees to an equivalent
		 * 			number of degrees in the range [0, 360). Loops should NOT
		 *          be used in this function. Recursion should also not be used.
		 * Programming constraint: Do not use recursion or loops. Consider using glm::mod.
		 * @param	degrees	The degrees.
		 * @return	Normalized degrees in the range [0, 360).
		 * @test	normalizeDegrees(0) --> 0
		 * @test	normalizeDegrees(1.75) --> 1.75
		 * @test	normalizeDegrees(-1) --> 359
		 * @test	normalizeDegrees(-721) --> 359
		 */

		double normalizeDegrees(double degrees)
		{
			return degrees % 360.0;
		}

		/**
		 * @fn	double normalizeRadians(double rads)
		 * @brief	Converts an arbitrary number of radians to an equivalent
		 * 			number of radians in the range [0, 2*PI). Loops should NOT
		 *          be used in this function.
		 * Programming constraint: Do not use recursion or loops.
		 * @param	rads	The radians.
		 * @return	Normalized radians in the range [0, 2*PI).
		 * @test	normalizeRadians(0) --> 0
		 * @test	normalizeRadians(1) --> 1
		 * @test	normalizeRadians(3*PI) --> 3.141.....
		 * @test	normalizeRadians(-31*PI) --> 3.141.....
		 */

		double normalizeRadians(double rads)
		{
			return rads % (2.0 * Defs.PI);
		}

		/**
		 * @fn	double rad2deg(double rads)
		 * @brief	Converts radians to degrees.  This function behaves like glm::degrees,
		 * without using glm::degrees.
		 * Programming constraint: Do not glm::degrees.
		 * @param	rads	The radians.
		 * @return	Degrees.
		 */

		double rad2deg(double rads)
		{
			return (rads * (180 / Defs.PI));
		}

		/**
		 * @fn	double deg2rad(double degs)
		 * @brief	Converts degrees to radians. This function behaves like glm::radians,
		 * without using glm::radians.
		 * Programming constraint: Do not use glm::radians.
		 * @param	degs	The degrees.
		 * @return	Radians.
		 */

		double deg2rad(double degs)
		{
			return (degs * (Defs.PI / 180));
		}

		/**
		* @fn	double min(double A, double B, double C)
		* @brief	Determines the minimum of three values, using glm::min.
		* Programming constraint: Use glm::min, which provides the minimum of two numbers
		* @param	A	First value.
		* @param	B	Second value
		* @param	C	Third value.
		* @return	The minimum value.
		*/

		public static double min(double A, double B, double C)
		{
			return Math.Min(A, Math.Min(B, C));
		}

		/**
		* @fn	double max(double A, double B, double C)
		* @brief	Determines the maximum of three values, using glm::max.
		* Programming constraint: Use glm::max
		* @param	A	First value.
		* @param	B	Second value
		* @param	C	Third value.
		* @return	The maximum value.
		*/

		double max(double A, double B, double C)
		{
			return Math.Max(A, Math.Max(B, C));
		}

		/**
		* @fn	distanceFromOrigin(double x, double y)
		* @brief	Determines the distance of the point (x, y) to (0, 0).
		* The distance is defined by sqrt(x^2 + y^2). Note: ^ is not how
		* C++ does exponentiation; you can use glm::pow instead.
		* @param	x	The x coordinate
		* @param	y	The 7 coordinate.
		* @return	The distance of (x, y) to the origin.
		* @test	distanceFromOrigin(0, 1) --> 1.0
		* @test	distanceFromOrigin(1, 0) --> 1.0
		* @test	distanceFromOrigin(1, 1) --> 1.41421356237309514547
		* @test	distanceFromOrigin(-10, 30) --> 31.62277660168379256334
		*/

		double distanceFromOrigin(double x, double y)
		{
			return distanceBetween(x, y, 0, 0);
		}

		/**
		* @fn	distanceBetween(double x1, double y1, double x2, double y2)
		* @brief	Determines the distance of the point (x1, y1) to (x2, y2)
		* The distance is defined by sqrt((x1-x2)^2 + (y1-y2)^2). Note: ^ is not how
		* C++ does exponentiation; you can use glm::pow instead.
		* @param	x1	The first x coordinate
		* @param	y1	The first y coordinate.
		* @param	x2	The second x coordinate
		* @param	y2	The second y coordinate.
		* @return	The distance between (x1, y1) and (x2, y2).
		* @test	distanceBetween(0, 0, 1, 1) --> 1.41421356237309514547
		* @test	distanceBetween(1, 1, 0, 0) --> 1.41421356237309514547
		* @test	distanceBetween(10, 10, 11, 11) --> 1.41421356237309514547
		* @test	distanceBetween(100, 100, 99, 99) --> 1.41421356237309514547
		* @test	distanceBetween(54, 1, -34, -99) --> 133.2066064427737
		*/

		double distanceBetween(double x1, double y1, double x2, double y2)
		{
			return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
		}

		/**
		 * @fn	double areaOfTriangle(double a, double b, double c)
		 * @brief	Computes the area of triangle using Heron's formula.
		 * @param	a length of first side.
		 * @param	b length of second side.
		 * @param	c length of third side.
		 * @return	Area of triangle. Returns -1.0 if the triangle is illegal (i.e.
		 * negative lengths). Legal values will yield v > 0.
		 * @test	areaOfTriangle(3, 4, 5) --> 6
		 * @test	areaOfTriangle(-3, 4, 5) --> -1
		 * @test	areaOfTriangle(3, 4, 50) --> -1
		 */

		double areaOfTriangle(double a, double b, double c)
		{
			double s = (a + b + c) / 2.0;
			return ((a <= 0 || b <= 0 || c <= 0) ? -1.0 : (double.IsNaN(Math.Sqrt(s * (s - a) * (s - b) * (s - c))) ? -1.0 : Math.Sqrt(s * (s - a) * (s - b) * (s - c))));
		}

		/**
		 * @fn	double areaOfTriangle(double x1, double y1, double x2, double y2, double x3, double y3)
		 * @brief	Computes the area of triangle formed by the three vertices (x1, y1), (x2, y2), and
		 * (x3, y3). You can assume all vertices are distinct.
		 * @param	x1 the x value of the first vertice
		 * @param	y1 the y value of the first vertice
		 * @param	x2 the x value of the second vertice
		 * @param	y2 the y value of the second vertice
		 * @param	x3 the x value of the third vertice
		 * @param	y3 the y value of the third vertice
		 * @return	Area of triangle.
		 * @test	areaOfTriangle(0, 0, 3, 0, 0, 4) --> 6
		 */

		double areaOfTriangle(double x1, double y1, double x2, double y2, double x3, double y3)
		{
			return areaOfTriangle(distanceBetween(x1, y1, x2, y2), distanceBetween(x2, y2, x3, y3), distanceBetween(x3, y3, x1, y1));
		}
		/**
		 * @fn	void pointOnUnitCircle(double angleRads, double &x, double &y)
		 * @brief	Determines the (x,y) position of a point on the standard
		 * 			unit circle.
		 * @param 		  	angleRads	The angle in radians.
		 * @param [in,out]	x		 	A double to process.
		 * @param [in,out]	y		 	A double to process.
		 */

		void pointOnUnitCircle(double angleRads, out double x, out double y)
		{
			Vector2 point = pointOnCircle(new Vector2(0, 0), 1, angleRads);
			x = point.x;
			y = point.x;
		}

		/**
		* @fn	dvec2 pointOnCircle(const dvec2 &center, double R, double angleRads)
		* @brief	Computes the (x,y) value of a point on the circle centered on 'center',
		* 			having radius R. The point is determined by sweeping an arc 'angleRads'.
		* @param	center   	The center of the circle
		* @param	R		 	Radius of circle.
		* @param	angleRads	The angle in radians.
		* @return	The point on the circle.
		*/

		Vector2 pointOnCircle(Vector2 center, double R, double angleRads) {
			return new Vector2(R * Math.Cos(angleRads) + center.x,R * Math.Sin(angleRads) + center.y);
		}

		/**
		* @fn	double directionInRadians(const dvec2 &referencePt, const dvec2 &targetPt)
		* @brief	Compute the direction/heading of 'targetPt', relative
		* 			to referencePt. The return angle should be in [0, 2PI)
		* @param	referencePt	Reference point.
		* @param	targetPt	Target point point.
		* @return	A double.
		* @test	directionInRadians((0,0), (2,2)) --> 0.7853981634
		* @test	directionInRadians((2,10), (3,11)) --> 0.7853981634
		* @test	directionInRadians((2,2), (2,0)) --> 4.7123889804
		* @test directionInRadians((1,-1), (1.3420000000000000817, -0.65799999999999991829)) --> 5.061454830783555181
		*/

		double directionInRadians(Vector2 referencePt, Vector2 targetPt)
		{
			double angle = Math.Atan2(targetPt.y - referencePt.y, targetPt.x - referencePt.x);
			return normalizeRadians(angle);
		}

		/**
		* @fn	double directionInRadians(const dvec2 &targetPt)
		* @brief	Compute the direction/heading of 'targetPt', relative
		* 			to the origin. The return angle should be in [0, 2PI)
		* @param	targetPt	Target point point.
		* @return	A double.
		* @test	directionInRadians((2,2)) --> 0.7853981634
		* @test	directionInRadians((0,-2)) --> 4.7123889804
		*/
		double directionInRadians(Vector2 targetPt)
		{
			return directionInRadians(new Vector2(0, 0), targetPt);
		}

		/**
		* @fn	double directionInRadians(double x1, double y1, double x2, double y2)
		* @brief	Compute the direction/heading of (x2, y2), relative
		* 			to (x1, y1). The return angle should be in [0, 2PI)
		* @param	x1  x coordinate of the reference point.
		* @param	y1	y coordinate of the reference point.
		* @param  x2    x coordinate of the target point.
		* @param  y2    y coordinate of the target point.
		* @return	A double.
		* @test	directionInRadians(0,0,2,2) --> 0.7853981634
		* @test	directionInRadians(2,10,3,11) --> 0.7853981634
		* @test	directionInRadians(2,2,2,0) --> 4.7123889804
		*/
		double directionInRadians(double x1, double y1, double x2, double y2)
		{
			return directionInRadians(new Vector2(x1, y1), new Vector2(x2, y2));
		}

		/**
		* @fn	dvec2 doubleIt(const dvec2 &V)
		* @brief	Computes 2*V
		* @param	V	The vector.
		* @return	2*V.
		*/

		Vector2 doubleIt(Vector2 V)
		{
			return new Vector2(2, 2) * V;
		}

		/**
		* @fn	new Vector3 myNormalize(const new Vector3 &V)
		* @brief	Computes the normalization of V, without calling glm::normalize.
		*           The input vector is not be the zero vector.
		* Programming constraint: Do NOT use glm::normalize
		* @param	V	The vector.
		* @return	Normalized vector V.
		*/

		Vector3 myNormalize(Vector3 V)
		{
			return V / V.Length();
		}

		/**
		* @fn	bool isOrthogonal(const new Vector3 &a, const new Vector3 &b)
		* @brief	Determines if two vectors are orthogonal, or nearly orthogonal. The inputs are non-zero vectors.
		* Two vectors are nearly orthogonal if the cosine of the angle formed by these
		* two vectors is approximatelyZero().
		* @param	a	The first vector.
		* @param	b	The second vector.
		* @return	True iff the two vector are orthogonal.
		*/

		bool isOrthogonal(Vector3 a, Vector3 b)
		{
			return approximatelyZero(Vector3.Dot(a, b));
		}

		/**
		* @fn	bool formAcuteAngle(const new Vector3 &a, const new Vector3 &b)
		* @brief	Determines if two vectors form an angle that is < 90 degrees. The inputs are non-zero vectors.
		* Programming constraint: Do NOT use acos, atan, or asin (you CAN use dot, cos, etc)
		* @param	a	The first vector.
		* @param	b	The second vector.
		* @return	True iff the two vectors form an acute angle.
		*/

		bool formAcuteAngle(Vector3 a, Vector3 b)
		{
			return Vector3.Dot(a, b) > 0;
		}

		/**
		 * @fn	double cosBetween(const dvec2 &v1, const dvec2 &v2)
		 * @brief	Cosine between v1 and v2. The inputs are non-zero vectors.
		 * @param	v1	The first vector.
		 * @param	v2	The second vector.
		 * @test	cosBetween(dvec2(1.0, 0.0), dvec2(1.0, 0.0)) --> 1.0
		 * @test	cosBetween(dvec2(1.0, 0.0), dvec2(1.0, 1.0)) --> 0.707107
		 * @test	cosBetween(dvec2(-1.0, glm::sqrt(3.0)), dvec2(-1.0, 0.0)) --> 0.5
		 * @test	cosBetween(dvec2(-1.0, glm::sqrt(3.0)), dvec2(1.0, glm::sqrt(3.0))) --> 0.5
		 * @return	The cosine between v1 and v2.
		 */

		double cosBetween(Vector2 v1, Vector2 v2)
		{
			return cosBetween(new Vector4(v1.x, v1.y, 0, 0), new Vector4(v2.x, v2.y, 0, 0));
		}

		/**
		 * @fn	double cosBetween(const new Vector3 &v1, const new Vector3 &v2)
		 * @brief	Computes the cosine between v1 and v2.
		 * @param	v1	The first vector.
		 * @param	v2	The second vector.
		 * @return	A double.
		 */

		double cosBetween(Vector3 v1, Vector3 v2)
		{
			return cosBetween(new Vector4(v1.x, v1.y, v1.z, 0), new Vector4(v2.x, v2.y, v1.z, 0));
		}

		/**
		 * @fn	double cosBetween(const dvec4 &v1, const dvec4 &v2)
		 * @brief	Computes the cosine between v1 and v2.
		 * @param	v1	The first vector.
		 * @param	v2	The second vector.
		 * @return	A double.
		 */
		
		double cosBetween(Vector4 v1, Vector4 v2)
		{
			return Vector4.Dot(v1, v2) / (v1.Length() * v2.Length());
		}

		/**
		 * @fn	double areaOfParallelogram(const new Vector3 &v1, const new Vector3 &v2)
		 * @brief	Computes the area of parallelogram, given two vectors eminating
		 * 			from the same corner of the parallelogram.
		 * @param	v1	The first vector.
		 * @param	v2	The second vector.
		 * @test	areaOfParallelogram(new Vector3(1.0, 0.0, 0.0), new Vector3(0.0, 1.0, 0.0)) --> 1.0
		 * @test	areaOfParallelogram(new Vector3(1.0, 1.0, 1.0), new Vector3(1.0, 0.0, 1.0)) --> 1.41421
		 * @return	Area of parallelogram.
		 */

		double areaOfParallelogram(Vector3 v1, Vector3 v2)
		{
			return Vector3.Cross(v1, v2).Length();
		}

		/**
		 * @fn	double areaOfTriangle(const new Vector3 &pt1, const new Vector3 &pt2, const new Vector3 &pt3)
		 * @brief	Computes the area of triangle.
		 * Programming constraint: use areaOfParalellogram to solve this one.
		 * @param	pt1	The first point.
		 * @param	pt2	The second point.
		 * @param	pt3	The third point.
		 * @test	areaOfTriangle(new Vector3(0.0, 0.0, 0.0), new Vector3(1.0, 0.0, 0.0), new Vector3(0.0, 1.0, 0.0)) --> 0.5
		 * @test	areaOfTriangle(new Vector3(-10.0, -10.0, -10.0), new Vector3(-11.0, -10.0, -10.0), new Vector3(-10.0, -11.0, -10.0)) --> 0.5
		 * @return	Area of triangle.
		 */

		double areaOfTriangle(Vector3 pt1, Vector3 pt2, Vector3 pt3)
		{
			return 0.5 * Vector3.Cross(pt3 - pt1, pt2 - pt1).Length();
		}

		/**
		* @fn	new Vector3 pointingVector(const new Vector3 &pt1, const new Vector3 &pt2)
		* @brief	Computes unit-length pointing vector.
		* @param	pt1	The first point.
		* @param	pt2	The second point.
		* @return	Unit length vector that points from pt1 toward pt2
		*/

		Vector3 pointingVector(Vector3 pt1, Vector3 pt2)
		{
			return Vector3.Normalize(pt2 - pt1);
		}
		
		/**
		 * @fn	double map(double x, double fromLo, double fromHi, double toLow, double toHigh)
		 * @brief	Linearly map a value from one interval to another.
		 * @param 		  	x	 	x value.
		 * @param 		  	fromLo 	The low value of the x range.
		 * @param 		  	fromHi	The high value of the x range.
		 * @param 		  	toLow 	The low value of the new range.
		 * @param 		  	toHigh	The high value of the new range.
		 * @test	map(2, 0, 5, 10, 11) --> 10.4
		 */

		public static double map(double x, double fromLo, double fromHi, double toLow, double toHigh)
		{
			return (x - fromLo) / (fromHi - fromLo) * (toHigh - toLow) + toLow;
		}

		/**
		 * @fn	vector<double> quadratic(double A, double B, double C)
		 * @brief	Solves the quadratic equation, given A, B, and C.
		 * 			0, 1, or 2 roots are inserted into the vector and returned.
		 * 			The roots are placed into the vector sorted in ascending order.
		 *          vector is somewhat like Java's ArrayList. Do a little research on
		 *          it. The length of the vector will correspond to the number of roots.
		 * @param	A	A.
		 * @param	B	B.
		 * @param	C	C.
		 * @return	Vector containing the real roots.
		 * @test	quadratic(1,4,3) --> [-3,-1]
		 * @test	quadratic(1,0,0) --> [0]
		 * @test	quadratic(-4, -2, -1) --> []
		 */

		public static List<double> quadratic(double A, double B, double C)
		{
			List<double> result = new List<double>();  // put only the roots in here
			double discriminant = (B * B) - (4.0 * A * C);

			if (discriminant < 0)
			{
				return result;
			}

			double multiple = (A < 0) ? -1 : 1;

			// This next line will run as if the discriminant is not less than 0, it must have
			// one root. multiple allows for sorting without sorting, as whether A is positive
			// or negative will dictate whether the numbers are swapped or not.
			result.Add((-B - (multiple * Math.Sqrt(discriminant))) / (2.0 * A));

			if (discriminant > 0)
			{
				result.Add((-B + (multiple * Math.Sqrt(discriminant))) / (2.0 * A));
			}

			return result;
		}

		/**
		 * @fn	int quadratic(double A, double B, double C, double roots[2])
		 * @brief	Solves the quadratic equation, given A, B, and C.
		 * 			0, 1, or 2 roots are inserted into the array 'roots'.
		 * 			The roots are sorted in ascending order.
		 * Here is an example of how this is to be used:
		 *
		 * 	double roots[2];
		 *	int numRoots = quadratic(1, 2, -3, roots);
		 *	if (numRoots == 0) {
		 *		cout << "There are no real roots" << endl;
		 *	} else if (numRoots == 1) {
		 *		cout << "Only one root: " << roots[0] << endl;
		 *	} else if (numRoots == 2) {
		 *      if (roots[0] > roots[1])
		 *			cout << "Something is wrong. This should not happen" << endl;
		 *		else
		 *			cout << "Two roots: " << roots[0] << " and " << roots[1] << endl;
		 *	} else {
		 *		cout << "Something is wrong. This should not happen" << endl;
		 *	}
		 *
		 * @param	A	 	A.
		 * @param	B	 	B.
		 * @param	C	 	C.
		 * @param	roots	The real roots.
		 * @test	quadratic(1, 4, 3, ary) --> returns 2 and fills in ary with: [-3,-1]
		 * @test	quadratic(1 ,0, 0, ary) --> returns 1 and fills in ary with: [0]
		 * @test	quadratic(-4, -2, -1, ary) --> returns 0 and does not modify ary.
		 * @return	The number of real roots put into the array 'roots'
		*/

		public static int quadratic(double A, double B, double C, out double[] roots)
		{
			roots = new double[2];
			List<double> calcRoots = quadratic(A, B, C);
			int rootCnt = calcRoots.Count;
			roots[0] = (rootCnt >= 1) ? calcRoots[0] : roots[0];
			roots[1] = (rootCnt == 2) ? calcRoots[1] : roots[1];
			return rootCnt;
		}

		/**
		* @fn	new Vector3 getRow(const dmat3 &mat, int row)
		* @brief	Retrieves a particular row out of a matrix.
		* @param	mat	The matrix.
		* @param	row	The row.
		* @return	The extracted row.
		*/
		
		Vector3 getRow(Matrix3 mat, int row)
		{
			switch (row)
			{
				case 0:
					return mat.Row0;
				case 1:
					return mat.Row1;
				case 2:
					return mat.Row2;
				default:
					return new Vector3(0, 0, 0);
			}
		}

		/**
		 * @fn	new Vector3 getCol(const dmat3 &mat, int col)
		 * @brief	Retrieves a particular column out of a matrix.
		 * @param	mat	The matrix.
		 * @param	col	The column.
		 * @return	The extracted column.
		 */

		Vector3 getCol(Matrix3 mat, int col)
		{
			switch (col)
			{
				case 0:
					return mat.Column0;
				case 1:
					return mat.Column1;
				case 2:
					return mat.Column2;
				default:
					return new Vector3(0, 0, 0);
			}
		}

		/**
		 * @fn	bool isInvertible(const dmat3 &mat)
		 * @brief	Determines if mat is invertible. A matrix is invertible if
		 *			its determinant is not 0.
		 * @param	mat	The matrix.
		 * @return	true if invertible, false if not.
		 */

		bool isInvertible(Matrix3 mat)
		{
			return mat.Determinant != 0;
		}

		/**
		 * @fn	dmat3 addMatrices(const vector<dmat3> &M)
		 * @brief	Adds the several matrices together. Assume the vector has length > 0.
		 * @param	M	Vector of matrices.
		 * @return	M[0]+M[1]+...+M[m-1]
		 */
		
		Matrix3 addMatrices(List<Matrix3> M)
		{
			return new Matrix3();
		}

		/**
		* @fn	dmat3 T(double dx, double dy)
		* @brief	Creates the 3x3 translation matrix for 2D systems.
		* @param	dx	x translation factor.
		* @param	dy	y translation factor.
		* @return	The scaling matrix.
		*/

		Matrix3 T(double dx, double dy)
		{
			/* CSE 386 - todo  */
			return new Matrix3(1, 0, 0, 0, 1, 0, dx, dy, 1);
		}

		/**
		 * @fn	dmat3 S(double sx, double sy)
		 * @brief	Creates the 3x3 scaling matrix for 2D systems.
		 * @param	sx	x scale factor.
		 * @param	sy	y scale factor.
		 * @return	The scaling matrix.
		 */

		Matrix3 S(double sx, double sy)
		{
			/* CSE 386 - todo  */
			return new Matrix3(sx, 0, 0, 0, sy, 0, 0, 0, 1);
		}
		
		/**
		 * @fn	dmat3 R(double rads)
		 * @brief	Returns 3x3 rotation matrix for 2D rotations.
		 * @param	rads	Radians to rotate.
		 * @return	The rotation matrix.
		 */
		
		Matrix3 R(double rads)
		{
			/* CSE 386 - todo  */
			return new Matrix3(Math.Cos(rads), Math.Sin(rads), 0, -Math.Sin(rads), Math.Cos(rads), 0, 0, 0, 1);
		}

		/**
		 * @fn	dmat3 horzShear(double a)
		 * @brief	Horizontal shear.
		 * @param	a	Shearing parameter.
		 * @return	The 3x3 shearing matrix.
		 */

		Matrix3 horzShear(double a)
		{
			/* CSE 386 - todo  */
			return new Matrix3(1, 0, 0, a, 1, 0, 0, 0, 1);
		}

		/**
		 * @fn	dmat3 vertShear(double a)
		 * @brief	Vertical shear.
		 * @param	a	Shearing parameter.
		 * @return	The 3x3 shearing matrix.
		 */

		Matrix3 vertShear(double a)
		{
			/* CSE 386 - todo  */
			return new Matrix3(1, a, 0, 0, 1, 0, 0, 0, 1);
		}

		/**
		* @fn	dmat4 T(double dx, double dy, double dz)
		* @brief	Creates the 4x4 translation matrix for 3D systems.
		* @param	dx	The x translation factor.
		* @param	dy	The y translation factor.
		* @param	dz	The z translation factor.
		* @return	The 4x4 translation matrix for 3D systems.
		*/

		Matrix4 T(double dx, double dy, double dz)
		{
			return Matrix4.CreateTranslation(dx, dy, dz);
		}

		/**
		* @fn	dmat4 S(double sx, double sy, double sz)
		* @brief	Creates the 4x4 scaling matrix for 3D systems.
		* @param	sx	The x scaling factor.
		* @param	sy	The y scaling factor.
		* @param	sz	The z scaling factor.
		* @return	The 4x4 scaling matrix for 3D systems.
		*/

		Matrix4 S(double sx, double sy, double sz)
		{
			return Matrix4.CreateScale(sx, sy, sz);
		}

		/**
		* @fn	dmat4 S(double scale)
		* @brief	Creates the 4x4 scaling matrix for 3D systems.
		* @param	scale	The scale.
		* @return	The 4x4 [uniform] scaling matrix for 3D systems.
		*/

		Matrix4 S(double scale)
		{
			return S(scale, scale, scale);
		}

		/**
		* @fn	dmat3 Rx(double rads)
		* @brief	Creates the 4x4 rotation matrix for 3D systems.
		* @param	rads	Rotation amount, in radians.
		* @return	The 4x4 matrix for rotation about the +x axis.
		*/

		Matrix4 Rx(double rads)
		{
			return Matrix4.CreateRotationX(rads);
		}

		/**
		* @fn	dmat3 Ry(double rads)
		* @brief	Creates the 4x4 rotation matrix for 3D systems.
		* @param	rads	Rotation amount, in radians.
		* @return	The 4x4 matrix for rotation about the +y axis.
		*/

		Matrix4 Ry(double rads)
		{
			return Matrix4.CreateRotationY(rads);
		}

		/**
		* @fn	dmat3 Rz(double rads)
		* @brief	Creates the 4x4 rotation matrix for 3D systems.
		* @param	rads	Rotation amount, in radians.
		* @return	The 4x4 matrix for rotation about the +z axis.
		*/

		Matrix4 Rz(double rads)
		{
			return Matrix4.CreateRotationZ(rads);
		}

		/**
		* @fn	void computeXYZFromAzimuthAndElevation(double R, double az, double el,
		*												double &x, double &y, double &z)
		* @brief	Computes (x,y,z), given a specific azimuth/elevation angles.
		* @param 		  	R 	The radius of the sphere.
		* @param 		  	az	Azimuth
		* @param 		  	el	Elevation.
		* @param [in,out]	x 	A double to process.
		* @param [in,out]	y 	A double to process.
		* @param [in,out]	z 	A double to process.
		*/

		void computeXYZFromAzimuthAndElevation(double R,
			double az, double el,
			out double x, out double y, out double z)
		{
			z = R * Math.Cos(el) * Math.Cos(az);
			x = R * Math.Cos(el) * Math.Sin(az);
			y = R * Math.Sin(el);
		}

		/**
		* @fn	void computeAzimuthAndElevationFromXYZ(double x, double y, double z,
		*												double &R, double &az, double &el)
		* @brief	Calculates the azimuth and elevation from xyz
		* @param 		  	x 	The x coordinate.
		* @param 		  	y 	The y coordinate.
		* @param 		  	z 	The z coordinate.
		* @param [in,out]	R 	The radius of the sphere.
		* @param [in,out]	az	Azimuth.
		* @param [in,out]	el	Elevation.
		*/

		public static void computeAzimuthAndElevationFromXYZ(double x, double y, double z,
			out double R, out double az, out double el)
		{
			R = new Vector3(x, y, z).Length();
			az = Math.Atan2(x, z);
			el = Math.Atan2(y, new Vector2(x, z).Length());
		}

		/**
		* @fn	void computeAzimuthAndElevationFromXYZ(const new Vector3 &pt, double &R, double &az, double &el)
		* @brief	Compute the azimuth/elevation (relative to the origin) of the point (x,y,z)
		* @param 		  	pt	The point - (x,y,z).
		* @param [in,out]	R 	The radius of the sphere.
		* @param [in,out]	az	Azimuth.
		* @param [in,out]	el	Elevation.
		*/
		
		public static void computeAzimuthAndElevationFromXYZ(Vector3 pt,
			out double R, out double az, out double el)
		{
			computeAzimuthAndElevationFromXYZ(pt.x, pt.y, pt.z, out double outR, out double outaz, out double outel);
			R = outR;
			az = outaz;
			el = outel;
		}

		// 386 Delete
		bool inRangeInclusive(double val, double lo, double hi)
		{
			return val >= lo && val <= hi;
		}
		
		// 386 Delete
		public static bool inRangeExclusive(double val, double lo, double hi)
		{
			return val > lo && val < hi;
		}

		/**
		* @fn	bool inRectangle(double x, double y, double left, double bottom, double right, double top)
		* @brief	Determines if (x,y) is inside (or on) a rectangle.
		* @param	x	  	The x coordinate.
		* @param	y	  	The y coordinate.
		* @param	left  	The left edge of rectangle.
		* @param	bottom	The bottom edge of rectangle.
		* @param	right 	The right edge of rectangle.
		* @param	top   	The top edge of rectangle.
		* @return	true iff (x,y) is in/on the rectangle.
		*/

		bool inRectangle(double x, double y, double left, double bottom, double right, double top)
		{
			return inRangeInclusive(x, left, right) &&
				inRangeInclusive(y, bottom, top);
		}

		/**
		* @fn	bool inRectangle(const dvec2 &pt, const dvec2 &lowerLeft, const dvec2 &upperRight)
		* @brief	Determines if (x,y) is inside (or on) a rectangle.
		* @param	pt		  	The point - (x,y)
		* @param	lowerLeft 	The lower left corner of the rectangle - (left, bottom).
		* @param	upperRight	The upper right corner of the rectangle - (right, top).
		* @return	true iff (x,y) is in/on the rectangle.
		*/

		bool inRectangle(Vector2 pt, Vector2 lowerLeft, Vector2 upperRight)
		{
			return inRangeInclusive(pt.x, lowerLeft.x, upperRight.x) &&
				inRangeInclusive(pt.y, lowerLeft.y, upperRight.y);
		}
	}
}
