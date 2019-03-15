using System;
using Microsoft.Xna.Framework;

namespace MulTris {
	public static class QuickOperations {

		/// <summary>
		/// Moves <paramref name="f"/>'s location BY xy
		/// </summary>
		/// <param name="f">Rectangle to move</param>
		/// <param name="xy">Point to move BY</param>
		/// <returns>A new moved Rectangle</returns>
		internal static Rectangle MoveRect(Rectangle f, Point xy) => new Rectangle(f.X + xy.X, f.Y + xy.Y, f.Width, f.Height);

		/// <summary>
		/// Stretches <paramref name="f"/>'s size BY wh
		/// </summary>
		/// <param name="f">Rectangle to stretch</param>
		/// <param name="wh">Point to stretch by</param>
		/// <returns></returns>
		internal static Rectangle StretchRect(Rectangle f, Point wh) => new Rectangle(f.X, f.Y, f.Width + wh.X, f.Height + wh.Y);

		/// <summary>
		/// Converts Point to Vector2
		/// </summary>
		/// <param name="p">Point to convert</param>
		/// <returns>New Vector2 converted from Point</returns>
		internal static Vector2 Point2Vec(Point p) => new Vector2(p.X, p.Y);

		/// <summary>
		/// Converts Vector2 to Point
		/// </summary>
		/// <param name="p">Vector2 to convert</param>
		/// <returns>New Point converted from Vector2</returns>
		internal static Point Vec2Point(Point p) => new Point(p.X, p.Y);

		internal static int _IRD(bool i, int v, int n, int x, int? d) => ( ( i ) ? _IRDI(v, n, x, d) : _IRDE(v, n, x, d) );
		internal static byte _IRD(bool i, byte v, byte n, byte x, byte? d) => ( ( i ) ? _IRDI(v, n, x, d) : _IRDE(v, n, x, d) );

		internal static int _IRB(bool i, int v, int n, int x) => ( ( i ) ? _IRBI(v, n, x) : _IRBE(v, n, x) );
		internal static byte _IRB(bool i, byte v, byte n, byte x) => ( ( i ) ? _IRBI(v, n, x) : _IRBE(v, n, x) );

		internal static int _IRDE(int v, int n, int x, int? d) => InRangeDefaultExclusive(v, n, x, d);
		internal static byte _IRDE(byte v, byte n, byte x, byte? d) => InRangeDefaultExclusive(v, n, x, d);

		internal static int _IRDI(int v, int n, int x, int? d) => InRangeDefaultInclusive(v, n, x, d);
		internal static byte _IRDI(byte v, byte n, byte x, byte? d) => InRangeDefaultInclusive(v, n, x, d);

		internal static int _IRBE(int v, int n, int m) => InRangeBoundExclusive(v, n, m);
		internal static byte _IRBE(byte v, byte n, byte m) => InRangeBoundExclusive(v, n, m);

		internal static int _IRBI(int v, int n, int m) => InRangeBoundInclusive(v, n, m);
		internal static byte _IRBI(byte v, byte n, byte m) => InRangeBoundInclusive(v, n, m);

		internal static int InRangeDefaultInclusive(int v, int min, int max, int? d) => ( v >= min && v <= max ) ? v : ( d ?? 0 );
		internal static byte InRangeDefaultInclusive(byte v, byte min, byte max, byte? d) => ( v >= min && v <= max ) ? v : ( d ?? 0 );

		internal static int InRangeDefaultExclusive(int v, int min, int max, int? d) => ( v > min && v < max ) ? v : ( d ?? 0 );
		internal static byte InRangeDefaultExclusive(byte v, byte min, byte max, byte? d) => ( v > min && v < max ) ? v : ( d ?? 0 );

		internal static int InRangeBoundExclusive(int v, int min, int max) => ( v < min ) ? min : ( v > max ) ? max : v;
		internal static byte InRangeBoundExclusive(byte v, byte min, byte max) => ( v < min ) ? min : ( v > max ) ? max : v;

		internal static int InRangeBoundInclusive(int v, int min, int max) => ( v <= min ) ? min : ( v >= max ) ? max : v;
		internal static byte InRangeBoundInclusive(byte v, byte min, byte max) => ( v <= min ) ? min : ( v >= max ) ? max : v;

	}
}
