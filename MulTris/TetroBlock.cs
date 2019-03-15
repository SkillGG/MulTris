using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace MulTris {

	public enum TBT {
		Center,
		Side
	}

	class TetroBlock {

		private bool load = false;
		private bool init = false;

		private TetroBlock centerPiece;
		public TetroBlock CenterPiece { get => centerPiece; }

		private Point offset;
		public Point Offset { get => offset; }

		private Texture2D block;
		private Rectangle source;

		private int x, y;
		public Point Position { get => new Point(x, y); }

		private int square;
		public int Side { get => square; }
		public Point Size { get => new Point(square); }

		private TBT type;
		public TBT Type { get => type; }

		public TetroBlock(TBT tp, int size) {
			this.type = tp;
			this.square = size;
		}

		public void Load(ContentManager cm, TetroType TetraLetter) {

			try {
				this.block = cm.Load<Texture2D>("Game/block" + TetraLetter.ToString( ).ToUpper( ));
				this.source = new Rectangle(new Point(0), Size);
				this.load = true;

			} catch( Exception ) {
				this.load = false;
			}

		}

		public void Load(Texture2D t) {
			try {
				this.block = t;
				this.source = new Rectangle(new Point(0), Size);
				this.load = true;

			} catch( Exception ) {
				this.load = false;
			}
		}

		public void Init(TetroBlock center, int xOffset, int yOffset) {
			this.offset = new Point(xOffset, yOffset);
			this.centerPiece = center;
			this.init = true;
		}

		public void MoveTo(int x, int y) {
			if( this.Type == TBT.Center ) {
				this.x = x;
				this.y = y;
			}
		}

		public void MoveBy(int x, int y) {
			if( this.Type == TBT.Center ) {
				this.x += x;
				this.y += y;
			}
		}

		public void Draw(SpriteBatch sb) {

			if( load && init ) {
				if( centerPiece != null ) {
					// OFFSET PIECE
					Rectangle pos =
					new Rectangle(
						new Point(
							CenterPiece.Position.X * CenterPiece.Side + Offset.X * Side,
							CenterPiece.Position.Y * CenterPiece.Side + Offset.Y * Side
						),
					Size);
					Console.WriteLine("Offsetted Piece! O:{X:" + Offset.X + ",Y:" + Offset.Y + "}, R:" + pos);
					sb.Draw(this.block, pos, source, Color.White);
				} else {
					//CENTER PIECE
					Rectangle pos = new Rectangle(new Point(Position.X * square, Position.Y * square), Size);
					sb.Draw(this.block, pos, source, new Color(255, 255, 255) * 0.5f);
				}
			}

		}

		public void ResetOffset(int x, int y) {
			this.offset = new Point(x, y);
		}

	}
}
