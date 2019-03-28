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

		public void ShowData() {
			new Debug($"TetroBlock({Tetrid})", $"D:{Destroyed}, pY:{Position.Y}, ddY:{DropDownOffset.Y}, oY:{Offset.Y}, gpY:{GridPosition.Y}", Debug.Importance.IMPORTANT_INFO);
		}

		private TetroBlock centerPiece;
		public TetroBlock CenterPiece { get => centerPiece; }

		private Point offset;
		public Point Offset { get => offset; }

		public Point ScreenPosition {
			get {
				if( this.CenterPiece == null ) {
					return new Point(Side * ( Position.X + DropDownOffset.X ), Side * ( Position.Y + DropDownOffset.Y ));
				} else {
					return new Point(
						Side * ( centerPiece.Position.X + Offset.X + DropDownOffset.X ),
						Side * ( centerPiece.Position.Y + Offset.Y + DropDownOffset.Y )
					);
				}
			}
		}

		private Sprite sprite;

		private int Tetrid;

		public void SetId(int id) {
			Tetrid = id;
		}

		private Point DropDownOffset = new Point(0);

		public void DDDown() {
			new Debug($"TetroBlock({Tetrid}, {Destroyed})", $"Droping down. {DropDownOffset.Y} => {DropDownOffset.Y + 1}", Debug.Importance.IMPORTANT_INFO);
			DropDownOffset = new Point(0, DropDownOffset.Y + 1);
		}

		private int x, y;
		public Point Position { get => new Point(x, y); }
		public Point GridPosition { get => new Point(( ScreenPosition.X / Side ) + 1, ( ScreenPosition.Y / Side ) + 1); }


		private int square;
		public int Side { get => square; }
		public Point Size { get => new Point(square); }

		public void SetSize(int side) {
			square = side;
		}

		private TBT type;
		public TBT Type { get => type; }

		private string SPS_DEBUG = "";

		public TetroBlock(TBT tp, int size, string pos, int id) {
			this.Tetrid = id;
			new Debug($"TetroBlock#({Tetrid})", "TetroBlock Initialization");
			this.type = tp;
			this.square = size;
			this.SPS_DEBUG = pos ?? "0";
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

		private bool dst = false;
		public bool Destroyed { get => dst; private set => dst = value; }

		public void Destroy() {
			new Debug($"TetroBlock({Tetrid}, {Destroyed})", "Destroying tblock!", Debug.Importance.IMPORTANT_INFO);
			Destroyed = true;
		}

		public void Load(Texture2D s, TetroType TetraLetter) {
			try {
				new Debug("TetroBlock#Load", "Loading TetroBlock sprite with Texture2D.");
				this.sprite = new Sprite(s, new Rectangle(new Point(50 * (int) TetraLetter, 0), new Point(50, 50)));
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

			if( load && init && !Destroyed ) {
				if( centerPiece != null ) {
					// OFFSET PIECE
					Rectangle pos = new Rectangle(this.ScreenPosition, Size);
					sb.Draw(sprite.Texture, pos, sprite.Source, Color.White);
					if( centerPiece.DebugInfo )
						//sb.DrawString(m.FiraLight10, $"{GridPosition.Y}", new Vector2(pos.X + 5, pos.Y + 5), Color.White);
						sb.DrawString(m.FiraLight10, $"{Tetrid}", new Vector2(pos.X + 5, pos.Y + 5), Color.White);
				} else {
					//CENTER PIECE
					Rectangle pos = new Rectangle(this.ScreenPosition, Size);
					sb.Draw(sprite.Texture, pos, sprite.Source, new Color(255, 255, 255) * 0.5f);
					if( this.DebugInfo )
						//sb.DrawString(m.FiraLight10, $"{GridPosition.Y}", new Vector2(pos.X + 5, pos.Y + 5), Color.Red);
						sb.DrawString(m.FiraLight10, $"{Tetrid}", new Vector2(pos.X + 5, pos.Y + 5), Color.Red);
				}
			}

		}

		public void ResetOffset(int x, int y) {
			new Debug("TetroBlock#ResetOffset", "Resetting Offset to ( " + x + ", " + y + " ).");
			this.offset = new Point(x, y);
		}

	}
}
