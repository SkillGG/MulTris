using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MulTris {
	public enum TetroType {
		Z = 0,
		S = 1,
		I = 2,
		L = 3,
		J = 4,
		O = 5
	}

	public class Tetromino {

		private Multris Game { get; set; }
		public Board Board { get; private set; }

		private bool falling;
		private uint toGround = 10;
		private TetroType type;

		private TetroBlock[] tblocks = new TetroBlock[4]{
			new TetroBlock(TBT.Center, 50),
			new TetroBlock(TBT.Side, 50),
			new TetroBlock(TBT.Side, 50),
			new TetroBlock(TBT.Side, 50)
		};

		public TetroType Type { get => type; }
		public bool Fall { get => falling; set => falling = value; }
		public TetroBlock CenterBlock { get => tblocks[0]; }
		public byte Rotation { get => rotateState; }
		public Point[] GetRotationOffsetsFor(TetroType t, byte rotS) {

			Point[] ret = new Point[3];

			switch( t ) {
				// TODO: Rotations
				case TetroType.Z:
					switch( rotS ) {
						// One Clockwise(right)
						case 1:
							ret[0] = new Point(0, -1);
							ret[1] = new Point(-1, 0);
							ret[2] = new Point(-1, 1);
							break;
						// Two
						case 2:
							ret[0] = new Point(1, 0);
							ret[1] = new Point(0, -1);
							ret[2] = new Point(-1, -1);
							break;
						// One Counter-clockwise (3x clockwise)
						case 3:
							ret[0] = new Point(0, 1);
							ret[1] = new Point(1, 0);
							ret[2] = new Point(1, -1);
							break;
						default:
							// 0 or 3+
							ret[0] = new Point(-1, 0);
							ret[1] = new Point(0, 1);
							ret[2] = new Point(1, 1);
							break;
					}
					break;
				// TODO: Rotations
				case TetroType.S:
					switch( rotS ) {
						case 1:
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
						case 2:
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
						case 3:
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
						default:
							// 0 or 3+
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
					}
					break;
				// TODO: Rotations
				case TetroType.I:
					switch( rotS ) {
						case 1:
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
						case 2:
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
						case 3:
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
						default:
							// 0 or 3+
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
					}
					break;
				// TODO: Rotations
				case TetroType.L:
					switch( rotS ) {
						case 1:
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
						case 2:
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
						case 3:
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
						default:
							// 0 or 3+
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
					}
					break;
				// TODO: Rotations
				case TetroType.J:
					switch( rotS ) {
						case 1:
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
						case 2:
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
						case 3:
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
						default:
							// 0 or 3+
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
					}
					break;
				// TODO: Rotations
				case TetroType.O:
					switch( rotS ) {
						case 1:
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
						case 2:
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
						case 3:
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
						default:
							// 0 or 3+
							ret[0] = new Point( );
							ret[1] = new Point( );
							ret[2] = new Point( );
							break;
					}
					break;
				default:
					ret[0] = new Point( );
					ret[1] = new Point( );
					ret[2] = new Point( );
					break;
			}

			return ret;
		}

		public void Update(InputState bef) {

			InputState inputs = new InputState( );

			if( inputs.KeyUp(bef, Keys.Up) ) {
				RotateRight( );
			}
			if( inputs.KeyUp(bef, Keys.Right) ) {
				MoveBy(1);
			}
			if( inputs.KeyUp(bef, Keys.Left) ) {
				MoveBy(-1);
			}
			if( inputs.KeyUp(bef, Keys.Down) ) {
				Gravity( );
			}

		}

		public void ToggleDebug() {
			this.CenterBlock.DebugInfo = !CenterBlock.DebugInfo;
		}

		public void Load(ContentManager cm) {
			new Debug("Tetromino#Load", "Loading TetroBlocks via ContentManager");
			foreach( TetroBlock t in tblocks ) {
				t.Load(cm, this.type);
			}
		}

		public void Load(Texture2D txt, Rectangle? source = null) {
			new Debug("Tetromino#Load", "Loading TetroBlocks with Texture2D");
			foreach( TetroBlock t in tblocks ) {
				t.Load(new Sprite(txt, source ?? new Rectangle(0, 0, 50, 50)));
			}
		}

		public void Draw(SpriteBatch sb) {

			foreach( TetroBlock t in tblocks ) {
				t.Draw(sb, this.rotateState, Game);
			}

		}

		public void MoveTo(int x) {
			this.tblocks[0].MoveTo(x, tblocks[0].Position.Y);
		}

		public void MoveBy(int x) {
			this.tblocks[0].MoveBy(x, 0);
		}

		private byte rotateState = 0;
		private readonly byte minRot = 0;
		private readonly byte maxRot = 3;

		private int BlockSize;

		public Tetromino(TetroType t, Multris m, Board board) {
			new Debug("Tetromino#()", "Tetromino(" + t + ") Initialization");
			this.falling = true;
			this.type = t;
			this.Game = m;
			Point[] rBl = GetRotationOffsetsFor(t, rotateState);

			this.Board = board;

			new Debug("Tetromino#()", "Setting proper offsets for given TetroType(" + t + ").");

			this.BlockSize = board.GridSize;

			tblocks[0].Init(null, 0, 0);
			tblocks[1].Init(tblocks[0], rBl[0].X, rBl[0].Y);
			tblocks[2].Init(tblocks[0], rBl[1].X, rBl[1].Y);
			tblocks[3].Init(tblocks[0], rBl[2].X, rBl[2].Y);

			foreach( TetroBlock tb in tblocks ) {
				tb.SetSize(BlockSize);
			}

		}

		public TetroBorder GetBorder() {
			return new TetroBorder(new Rectangle(tblocks[0].Position, tblocks[0].Size),
			new Rectangle(tblocks[1].Position, tblocks[1].Size),
			new Rectangle(tblocks[2].Position, tblocks[2].Size),
			new Rectangle(tblocks[3].Position, tblocks[3].Size));
		}

		public bool Collide(Tetromino t) {
			if( !t.Equals(this) ) {
				if( GetBorder( ).Intersects(t.GetBorder( )) )
					return true;
				return false;
			}
			return true;
		}

		public void RotateLeft() {
			new Debug("Tetromino#RotateLeft", "Rotating Left");
			rotateState = QuickOperations._IRB(true, rotateState, minRot, maxRot);
			if( rotateState != 0 )
				rotateState--;
			else
				rotateState = 3;
			SaveRotate( );
		}

		public void SaveRotate() {
			Point[] rbl = GetRotationOffsetsFor(this.type, rotateState);
			tblocks[0].ResetOffset(0, 0);
			tblocks[1].ResetOffset(rbl[0].X, rbl[0].Y);
			tblocks[2].ResetOffset(rbl[1].X, rbl[1].Y);
			tblocks[3].ResetOffset(rbl[2].X, rbl[2].Y);
		}

		public void RotateRight() {
			new Debug("Tetromino#RotateLeft", "Rotating Right");
			rotateState = QuickOperations._IRB(true, rotateState, minRot, maxRot);
			if( rotateState < 3 )
				rotateState++;
			else
				rotateState = 0;
			SaveRotate( );
		}

		public void Gravity() {
			string pl = "Tetromino#Gravity";
			new Debug(pl, $"Gravity check!");
			if( falling ) {
				new Debug(pl, $"Is falling!");
				if( toGround == 0 ) {
					new Debug(pl, $"Grounding!");
					falling = false;
					return;
				}
				new Debug(pl, $"Falling. {toGround} to ground!");
				toGround--;
				new Debug(pl, $"Before any move: {tblocks[0].Position}");
				tblocks[0].MoveBy(0, 1);
				new Debug(pl, $"After first move: {tblocks[0].Position}");
				foreach( Tetromino t in Board ) {
					new Debug(pl, $"Checking for: {this.Collide(t)}");
					if( this.Collide(t) ) {
						tblocks[0].MoveBy(0, -1);
						falling = false;
					}
				}
			}
		}

	}
}
