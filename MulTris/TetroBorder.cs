using Microsoft.Xna.Framework;

namespace MulTris {
	public class TetroBorder {

		public override string ToString() {
			string rss = "";
			foreach(Rectangle r in Rectangles){
				rss += r.ToString( ) + ", ";
			}
			return "[TetroBorder] Contains:[" +rss + "]";
		}

		public Rectangle[] Rectangles { get => rs; }
		private readonly Rectangle[] rs = new Rectangle[4];

		public TetroBorder(Rectangle? r1, Rectangle? r2, Rectangle? r3, Rectangle? r4) {
			rs = new Rectangle[4] {
				r1 ?? new Rectangle(0,0,50,50),
				r2 ?? new Rectangle(50,0,50,50),
				r3 ?? new Rectangle(100,0,50,50),
				r4 ?? new Rectangle(150,0,50,50)
			};
		}

		public bool Intersects(Rectangle pr) {
			foreach( Rectangle r in Rectangles ) {
				if( r.Intersects(pr) )
					return true;
			}
			return false;
		}

		public bool Intersects(TetroBorder tb) {
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

	}
}