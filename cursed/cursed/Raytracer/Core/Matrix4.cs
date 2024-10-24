using System;
using System.Collections.Generic;
using System.Text;

namespace cursed.Raytracer.Core
{
	internal class Matrix4
	{
		public Vector4 Column0
		{
			get
			{
				return new Vector4(m[0, 0], m[0, 1], m[0, 2], m[0, 3]);
			}
			
			set
			{
				m[0, 0] = value.x;
				m[0, 1] = value.y;
				m[0, 2] = value.z;
				m[0, 3] = value.w;
			}
		}

		public Vector4 Column1
		{
			get
			{
				return new Vector4(m[1, 0], m[1, 1], m[1, 2], m[1, 3]);
			}

			set
			{
				m[1, 0] = value.x;
				m[1, 1] = value.y;
				m[1, 2] = value.z;
				m[1, 3] = value.w;
			}
		}

		public Vector4 Column2
		{
			get
			{
				return new Vector4(m[2, 0], m[2, 1], m[2, 2], m[2, 3]);
			}

			set
			{
				m[2, 0] = value.x;
				m[2, 1] = value.y;
				m[2, 2] = value.z;
				m[2, 3] = value.w;
			}
		}

		public Vector4 Column3
		{
			get
			{
				return new Vector4(m[3, 0], m[3, 1], m[3, 2], m[3, 3]);
			}

			set
			{
				m[3, 0] = value.x;
				m[3, 1] = value.y;
				m[3, 2] = value.z;
				m[3, 3] = value.w;
			}
		}

		public double[,] m;
		public Matrix4()
		{
			m = new double[4, 4];
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					m[i, j] = 0;
				}
			}
		}
		
		public Matrix4(double[,] m)
		{
			this.m = m;
		}

		public Matrix4(double xx, double xy, double xz, double xw,
			double yx, double yy, double yz, double yw,
			double zx, double zy, double zz, double zw,
			double wx, double wy, double wz, double ww)
		{
			m = new double[4, 4];
			m[0, 0] = xx; m[0, 1] = xy; m[0, 2] = xz; m[0, 3] = xw;
			m[1, 0] = yx; m[1, 1] = yy; m[1, 2] = yz; m[1, 3] = yw;
			m[2, 0] = zx; m[2, 1] = zy; m[2, 2] = zz; m[2, 3] = zw;
			m[3, 0] = wx; m[3, 1] = wy; m[3, 2] = wz; m[3, 3] = ww;
		}
		
		public static Matrix4 Inverse(Matrix4 m)
		{
			double[,] inv = new double[4, 4];
			inv[0, 0] = m.m[3, 3] * m.m[2, 2] - m.m[3, 2] * m.m[2, 3];
			inv[0, 1] = -m.m[3, 3] * m.m[2, 1] + m.m[3, 1] * m.m[2, 3];
			inv[0, 2] = m.m[3, 3] * m.m[2, 1] - m.m[3, 1] * m.m[2, 3];
			inv[0, 3] = -m.m[3, 3] * m.m[2, 2] + m.m[3, 2] * m.m[2, 3];
			inv[1, 0] = -m.m[3, 2] * m.m[1, 2] + m.m[3, 1] * m.m[1, 3];
			inv[1, 1] = m.m[3, 3] * m.m[1, 2] - m.m[3, 1] * m.m[1, 3];
			inv[1, 2] = -m.m[3, 3] * m.m[1, 1] + m.m[3, 1] * m.m[1, 3];
			inv[1, 3] = m.m[3, 2] * m.m[1, 1] - m.m[3, 1] * m.m[1, 2];
			inv[2, 0] = m.m[2, 3] * m.m[1, 2] - m.m[2, 2] * m.m[1, 3];
			inv[2, 1] = -m.m[2, 3] * m.m[1, 1] + m.m[2, 1] * m.m[1, 3];
			inv[2, 2] = m.m[2, 3] * m.m[1, 1] - m.m[2, 1] * m.m[1, 3];
			inv[2, 3] = -m.m[2, 2] * m.m[1, 1] + m.m[2, 1] * m.m[1, 2];
			inv[3, 0] = -m.m[2, 3] * m.m[0, 2] + m.m[2, 2] * m.m[0, 3];
			inv[3, 1] = m.m[2, 3] * m.m[0, 1] - m.m[2, 1] * m.m[0, 3];
			inv[3, 2] = -m.m[2, 2] * m.m[0, 1] + m.m[2, 1] * m.m[0, 2];
			inv[3, 3] = m.m[2, 3] * m.m[0, 2] - m.m[2, 2] * m.m[0, 3];
			double det = m.m[0, 0] * inv[0, 0] + m.m[0, 1] * inv[1, 0] + m.m[0, 2] * inv[2, 0] + m.m[0, 3] * inv[3, 0];
			if (det == 0)
			{
				return new Matrix4();
			}
			det = 1.0f / det;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					inv[i, j] *= det;
				}
			}
			return new Matrix4(inv);
		}

		public static Matrix4 CreateTranslation(double dx, double dy, double dz)
		{
			return new Matrix4(
				1, 0, 0, dx,
				0, 1, 0, dy,
				0, 0, 1, dz,
				0, 0, 0, 1);
		}

		public static Matrix4 CreateScale(double sx, double sy, double sz)
		{
			return new Matrix4(
				sx, 0, 0, 0,
				0, sy, 0, 0,
				0, 0, sz, 0,
				0, 0, 0, 1);
		}

		public static Matrix4 CreateRotationX(double theta)
		{
			double cos = Math.Cos(theta);
			double sin = Math.Sin(theta);
			return new Matrix4(
				1, 0, 0, 0,
				0, cos, -sin, 0,
				0, sin, cos, 0,
				0, 0, 0, 1);
		}

		public static Matrix4 CreateRotationY(double theta)
		{
			double cos = Math.Cos(theta);
			double sin = Math.Sin(theta);
			return new Matrix4(
				cos, 0, sin, 0,
				0, 1, 0, 0,
				-sin, 0, cos, 0,
				0, 0, 0, 1);
		}

		public static Matrix4 CreateRotationZ(double theta)
		{
			double cos = Math.Cos(theta);
			double sin = Math.Sin(theta);
			return new Matrix4(
				cos, -sin, 0, 0,
				sin, cos, 0, 0,
				0, 0, 1, 0,
				0, 0, 0, 1);
		}

		public static Vector4 operator *(Matrix4 a, Vector4 v)
		{
			return new Vector4(
				a.m[0, 0] * v.x + a.m[0, 1] * v.y + a.m[0, 2] * v.z + a.m[0, 3] * v.w,
				a.m[1, 0] * v.x + a.m[1, 1] * v.y + a.m[1, 2] * v.z + a.m[1, 3] * v.w,
				a.m[2, 0] * v.x + a.m[2, 1] * v.y + a.m[2, 2] * v.z + a.m[2, 3] * v.w,
				a.m[3, 0] * v.x + a.m[3, 1] * v.y + a.m[3, 2] * v.z + a.m[3, 3] * v.w);
		}
	}
}
