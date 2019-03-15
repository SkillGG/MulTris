using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace MulTris {
	class BoardTile {

		public override string ToString() => ( "LOAD:" + load.ToString( ) + " BND:" + Boundaries.ToString( ) );

		private bool load = false;

		private Texture2D txt;
		private Rectangle source;

		private Point position;
		public Point Position { get => position; }
		public Rectangle Boundaries { get => new Rectangle(position, source.Size); }

		// public Tetromino inside;

		public BoardTile() {
		}

		public void Move(Point newp) {
			position = newp;
		}

		public void Move(int x, int y) {
			position = new Point(x, y);
		}

		public void Load(Texture2D t, Rectangle? s) {
			try {
				this.source = s ?? new Rectangle(0, 0, 50, 50);
				this.txt = t;
				this.load = true;
			} catch( Exception e ) {
				Console.Error.WriteLine(e);
				this.load = false;
			}
		}

		public void Draw(SpriteBatch sb) {
			if( load ) {
				sb.Draw(txt, Boundaries, source, Color.White);
			} else {
				Console.Error.WriteLine("(BoardTile) Not Load yet!");
			}
		}

		public void Update() {

		}

	}
}
