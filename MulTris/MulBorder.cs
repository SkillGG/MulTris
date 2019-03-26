using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MulTris {

	public class MulBorder {

		public override string ToString() {
			return $"[TetroBorder] Contains:[{string.Join(", ", rs)}]";
		}

		public MulBorder(List<Rectangle> l) {
			this.Rectangles = l;
		}

		public bool Intersects(Rectangle pr) {
			foreach( Rectangle r in Rectangles ) {
				if( r.Intersects(pr) )
					return true;
			}
			return false;
		}

		public bool Intersects(MulBorder tb) {
			if( tb == null )
				return false;
			new Debug("TetroBorder#Interects", "Checking if " + this.ToString( ) + " intersects with: " + tb.ToString( ), Debug.Importance.VALUE_INFO);
			foreach( Rectangle r in Rectangles ) {
				foreach( Rectangle pr in tb.Rectangles ) {
					if( pr.Intersects(r) ) {
						return true;
					}
				}
			}
			return false;
		}

		protected List<Rectangle> rs;
		public List<Rectangle> Rectangles { get => rs; private set => rs = value; }

	}
}