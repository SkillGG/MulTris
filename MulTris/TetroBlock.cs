using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace MulTris {

	public enum TBT {
		Center,
		Side
	}

	public class TetroBlock {

		private bool load = false;
		private bool init = false;

		private TetroBlock centerPiece;
		public TetroBlock CenterPiece { get => centerPiece; }

		private Point offset;
		public Point Offset { get => offset; }

		private Sprite sprite;

		private int x, y;
		public Point Position { get => new Point(x, y); }

		private int square;
		public int Side { get => square; }
		public Point Size { get => new Point(square); }

		public void SetSize(int side) {
			square = side;
		}

		private TBT type;
		public TBT Type { get => type; }

		public TetroBlock(TBT tp, int size) {
			new Debug("TetroBlock#()", "TetroBlock Initialization");
			this.type = tp;
			this.square = size;
		}

		public void Load(ContentManager cm, TetroType TetraLetter) {

			try {
				new Debug("TetroBlock#Load", "Loading TetroBlock sprite via ContentManager.");
				this.sprite = new Sprite(cm.Load<Texture2D>("Game/blocks"), new Rectangle(new Point(0, 50 * (int) TetraLetter), new Point(50, 50)));
				this.load = true;
			} catch( Exception e ) {
				this.load = false;
				new Debug("TetroBlock#Load", "ERR:" + e);
			}

		}

		public void Load(Texture2D s, TetroType TetraLetter) {
			try {
				new Debug("TetroBlock#Load", "Loading TetroBlock sprite with Texture2D.");
				this.sprite = new Sprite(s, new Rectangle(new Point(0, 50 * (int) TetraLetter), new Point(50, 50)));
				this.load = true;
			} catch( Exception e ) {
				this.load = false;
				new Debug("TetroBlock#Load", "ERR:" + e);
			}
		}

		public void Load(Sprite sp) {
			try {
				new Debug("TetroBlock#Load", "Loading TetroBlock sprite with Sprite.");
				this.sprite = sp;
				this.load = true;

			} catch( Exception e ) {
				this.load = false;
				new Debug("TetroBlock#Load", "ERR:" + e);
			}
		}

		public void Init(TetroBlock center, int xOffset, int yOffset) {
			new Debug("TetroBlock#Init", "Initializing Offset and CenterPiece.");
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

		public bool DebugInfo = false;

		public void Draw(SpriteBatch sb, byte Rotate, Multris m) {

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
					sb.Draw(sprite.Texture, pos, sprite.Source, Color.White);
					if( centerPiece.DebugInfo )
						sb.DrawString(m.FiraLight10, "O" + Offset.X + ":" + Offset.Y, new Vector2(pos.X + 5, pos.Y + 5), Color.White);
				} else {
					//CENTER PIECE
					Rectangle pos = new Rectangle(new Point(Position.X * square, Position.Y * square), Size);
					sb.Draw(sprite.Texture, pos, sprite.Source, new Color(255, 255, 255) * 0.5f);
					if( this.DebugInfo )
						sb.DrawString(m.FiraLight10, "R" + Rotate.ToString( ), new Vector2(pos.X + 5, pos.Y + 5), Color.Red);
				}
			}

		}

		public void ResetOffset(int x, int y) {
			new Debug("TetroBlock#ResetOffset", "Resetting Offset to ( " + x + ", " + y + " ).");
			this.offset = new Point(x, y);
		}

	}
}
