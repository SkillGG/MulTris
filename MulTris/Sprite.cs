using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MulTris {
	public class Sprite {

		public override string ToString() {
			return $"Sprite({Texture}): S:{Source}, P:{Position} [{load}";
		}

		private bool load = false;

		private Texture2D texture;
		private Rectangle source;
		private Rectangle position;

		public Texture2D Texture { get => load ? texture : null; }
		public Rectangle Source { get => load ? source : new Rectangle( ); }
		public Rectangle Position { get => load ? position : new Rectangle(); }


		public void Load(Texture2D t, Rectangle? s) {
			try {
				this.texture = t;
				this.source = s ?? new Rectangle(new Point(0), t.Bounds.Size);
				load = true;
			} catch( System.Exception ) {
				load = false;
			}
		}

		public void Load(ContentManager cm, string st, Rectangle? s) {
			try {
				this.texture = cm.Load<Texture2D>(st);
				this.source = s ?? new Rectangle(new Point(0), texture.Bounds.Size);
				load = true;
			} catch( System.Exception ) {
				load = false;
			}
		}

		public void Load(ContentManager cm, string st, Rectangle? s, Rectangle? p) {
			try {
				this.texture = cm.Load<Texture2D>(st);
				this.source = s ?? new Rectangle(new Point(0), texture.Bounds.Size);
				this.position = p ?? new Rectangle(0, 0, 0, 0);
				load = true;
			} catch( System.Exception ) {
				load = false;
			}
		}

		public void Load(Texture2D t, Rectangle? s, Rectangle? p) {
			try {
				this.texture = t;
				this.source = s ?? new Rectangle(new Point(0), texture.Bounds.Size);
				this.position = p ?? new Rectangle(0, 0, 0, 0);
				load = true;
			} catch( System.Exception ) {
				load = false;
			}
		}

		private bool cngS = false;
		private bool cngP = false;

		public void Change(Rectangle? nr) {
			if( cngS && cngP ) {
				new Debug("Sprite#Change", "Couldn't Change!");
				return;
			}
			if( cngS ) {
				this.source = nr ?? source;
			}
			if( cngP ) {
				this.position = nr ?? position;
			}
		}

		public void Change(Point? np) {
			new Debug("Sprite#Change", $"Trying to change Sprite value to: {np} when s:{cngS}, p:{cngP}");
			if( cngS && cngP ) {
				new Debug("Sprite#Change", "Couldn't Change!");
				return;
			}
			if( cngS ) {
				Point p = np ?? source.Location;
				this.source = new Rectangle(p, source.Size);
			}
			if( cngP ) {
				Point p = np ?? position.Location;
				this.position = new Rectangle(p, position.Size);
			}
		}

		public void AllowCP(bool aS) {
			if( aS )
				cngS = aS;
			else
				cngP = !aS;

		}
		public void DisAllow() {
			cngS = false;
			cngP = false;
		}

		public Sprite() { }

		public Sprite(Texture2D t, Rectangle? s) {
			try {
				this.texture = t;
				this.source = s ?? new Rectangle(new Point(0), t.Bounds.Size);
				load = true;
			} catch( System.Exception ) {
				load = false;
			}
		}

		public Sprite(ContentManager cm, string st, Rectangle? s) {
			try {
				this.texture = cm.Load<Texture2D>(st);
				this.source = s ?? new Rectangle(new Point(0), texture.Bounds.Size);
				load = true;
			} catch( System.Exception ) {
				load = false;
			}
		}

		public Sprite(ContentManager cm, string st, Rectangle? s, Rectangle? p) {
			try {
				this.texture = cm.Load<Texture2D>(st);
				this.source = s ?? new Rectangle(new Point(0), texture.Bounds.Size);
				this.position = p ?? new Rectangle(0, 0, 0, 0);
				load = true;
			} catch( System.Exception ) {
				load = false;
			}
		}

		public Sprite(Texture2D t, Rectangle? s, Rectangle? p) {
			try {
				this.texture = t;
				this.source = s ?? new Rectangle(new Point(0), texture.Bounds.Size);
				this.position = p ?? new Rectangle(0, 0, 0, 0);
				load = true;
			} catch( System.Exception ) {
				load = false;
			}
		}

		~Sprite() {
			if( this.texture != null )
				this.texture.Dispose( );
			this.texture = null;
		}

	}
}
