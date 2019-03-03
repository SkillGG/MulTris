
namespace MulTris {
	public class QuickMaths {

		private QuickMaths() { }

		public static int _IRD(bool i, int v, int n, int x, int? d) {
			if( i )
				return _IRDI(v, n, x, d);
			return _IRDE(v, n, x, d);
		}

		public static int _IRB(bool i, int v, int n, int x) {
			if( i )
				return _IRBI(v, n, x);
			return _IRBE(v, n, x);
		}

		public static int _IRDE(int v, int n, int x, int? d) {
			return InRangeDefaultExclusive(v, n, x, d);
		}

		public static int _IRDI(int v, int n, int x, int? d) {
			return InRangeDefaultInclusive(v, n, x, d);
		}

		public static int _IRBE(int v, int n, int m) {
			return InRangeBoundExclusive(v, n, m);
		}

		public static int _IRBI(int v, int n, int m) {
			return InRangeBoundInclusive(v, n, m);
		}

		public static int InRangeDefaultInclusive(int v, int min, int max, int? d) {
			return ( v >= min && v <= max ) ? v : ( d ?? 0 );
		}

		public static int InRangeDefaultExclusive(int v, int min, int max, int? d) {
			return ( v > min && v < max ) ? v : ( d ?? 0 );
		}

		public static int InRangeBoundExclusive(int v, int min, int max) {
			return ( v < min ) ? min : ( v > max ) ? max : v;
		}

		public static int InRangeBoundInclusive(int v, int min, int max) {
			return ( v <= min ) ? min : ( v >= max ) ? max : v;
		}

	}
}