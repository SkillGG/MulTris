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
		private TetroType type;

		private TetroBlock[] tblocks = new TetroBlock[4]{
			new TetroBlock(TBT.Center, 50, null, 0),
			new TetroBlock(TBT.Side, 50, "1", 1),
			new TetroBlock(TBT.Side, 50, "2", 2),
			new TetroBlock(TBT.Side, 50, "3", 3)
		};

		public void ShowTBData() {
			foreach( TetroBlock t in tblocks )
				t.ShowData( );
		}

		public TetroType Type { get => type; }
		public bool Fall { get => falling; set => falling = value; }
		public TetroBlock CenterBlock { get => tblocks[0]; }
		public byte Rotation { get => rotateState; }
		public Point[] GetRotationOffsetsFor(TetroType t, byte rotS) {

			Point[] ret = new Point[4];
			ret[3] = new Point(0);

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
							ret[3] = new Point(0, -1);
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
							ret[0] = new Point(0, -1);
							ret[1] = new Point(1, 0);
							ret[2] = new Point(1, 1);
							break;
						case 2:
							ret[0] = new Point(1, -1);
							ret[1] = new Point(0, -1);
							ret[2] = new Point(-1, 0);
							ret[3] = new Point(0, 1);
							break;
						case 3:
							ret[0] = new Point(-1, -1);
							ret[1] = new Point(-1, 0);
							ret[2] = new Point(0, 1);
							break;
						default:
							// 0 or 3+
							ret[0] = new Point(1, 0);
							ret[1] = new Point(0, 1);
							ret[2] = new Point(-1, 1);
							break;
					}
					break;
				// TODO: Rotations
				case TetroType.I:
					switch( rotS ) {
						case 1:
							ret[0] = new Point(0, -1);
							ret[1] = new Point(0, 1);
							ret[2] = new Point(0, 2);
							break;
						case 2:
							ret[0] = new Point(-1, 0);
							ret[1] = new Point(-2, 0);
							ret[2] = new Point(1, 0);
							break;
						case 3:
							ret[0] = new Point(0, -2);
							ret[1] = new Point(0, -1);
							ret[2] = new Point(0, 1);
							break;
						default:
							// 0 or 3+
							ret[0] = new Point(-1, 0);
							ret[1] = new Point(2, 0);
							ret[2] = new Point(1, 0);
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
			if( inputs.KeyUp(bef, Keys.D) ) {
				if( !tblocks[0].Destroyed )
					Destroy(0);
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

		public void OneUp() {
			this.tblocks[0].MoveBy(0, -1);
		}

		private void MoveTo(int x, int y) {
			this.tblocks[0].MoveTo(x, y);
		}

		public void MoveTo(int x) {
			this.tblocks[0].MoveTo(x, tblocks[0].Position.Y);
		}

		private void MoveBy(int x, int y) {
			this.tblocks[0].MoveBy(x, 0);
			if( Board.Length( ) > 0 ) {
				foreach( Tetromino t in Board ) {
					if( t != this ) {
						if( !t.Fall ) {
							if( AllCollides(t) ) {
								tblocks[0].MoveBy(-x, -y);
							}
						}
					} else {
						if( AllCollides(null) ) {
							tblocks[0].MoveBy(-x, -y);
						}
					}

				}
			}
		}

		public void MoveBy(int x) {
			if( Fall ) {
				this.tblocks[0].MoveBy(x, 0);
				if( Board.Length( ) > 0 ) {
					foreach( Tetromino t in Board ) {
						if( t != this ) {
							if( !t.Fall ) {
								if( AllCollides(t) ) {
									tblocks[0].MoveBy(-x, 0);
								}
							}
						} else {
							if( AllCollides(null) ) {
								tblocks[0].MoveBy(-x, 0);

							}
						}
					}
				}
			}
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

			new Debug("Tetromino#()", $"Setting proper offsets for given TetroType( { t } ) : ${ string.Join(",", rBl) }.");

			this.BlockSize = board.GridSize;

			tblocks[0].Init(null, 0, 0);
			tblocks[1].Init(tblocks[0], rBl[0].X, rBl[0].Y);
			tblocks[2].Init(tblocks[0], rBl[1].X, rBl[1].Y);
			tblocks[3].Init(tblocks[0], rBl[2].X, rBl[2].Y);
			this.MoveTo(rBl[3].X, rBl[3].Y);
			int tbid = Board.Length( ) * 4;
			foreach( TetroBlock tb in tblocks ) {
				tb.SetId(tbid);
				tb.SetSize(BlockSize);
				tbid++;
			}
		}

		public MulBorder GetBorder() {
			List<Rectangle> lor = new List<Rectangle>( );
			foreach( TetroBlock t in tblocks ) {
				if( !t.Destroyed ) {
					lor.Add(new Rectangle(t.ScreenPosition, t.Size));
				}
			}
			return new MulBorder(lor);
		}

		public bool AllCollides(Tetromino t) {
			if( CollideWithGround( ) || CollideWithWall( ) )
				return true;
			if( t != null ) {
				if( Collide(t) )
					return true;
			}
			return false;
		}

		public bool Collide(Tetromino t) {
			if( !t.Equals(this) ) {
				if( GetBorder( ).Intersects(t.GetBorder( )) )
					return true;
				return false;
			}
			return false;
		}

		public bool CollideWithGround() {
			if( GetBorder( ).Intersects(new Rectangle(0, this.BlockSize * Board.Size.Y, Game.WIDTH, Game.HEIGHT)) ) {
				return true;
			}
			return false;
		}

		public bool CollideWithWall() {
			if( GetBorder( ).Intersects(new Rectangle(Board.LeftWall.X - 100, -100, 99, Game.HEIGHT + 100)) ) { // Left wall
				return true;
			}
			if( GetBorder( ).Intersects(new Rectangle(Board.RightWall.X, -100, 10, Game.HEIGHT + 100)) ) {  // Right wall
				return true;
			}
			return false;
		}

		public void RotateLeft() {
			if( Fall ) {
				new Debug("Tetromino#RotateLeft", "Rotating Left");
				byte nrs = QuickOperations._IRB(true, rotateState, minRot, maxRot);
				byte ors = QuickOperations._IRB(true, rotateState, minRot, maxRot);
				if( nrs != 0 )
					nrs--;
				else
					nrs = 3;
				rotateState = nrs;
				SaveRotate( );
				if( Board.Length( ) > 0 ) {
					foreach( Tetromino t in Board ) {
						if( t != this ) {
							if( !t.Fall ) {
								if( AllCollides(t) ) {
									rotateState = ors;
									SaveRotate( );
								}
							}
						} else {
							if( AllCollides(null) ) {
								rotateState = ors;
								SaveRotate( );
							}
						}
					}
				}
			}

		}

		public void SaveRotate() {
			Point[] rbl = GetRotationOffsetsFor(this.type, rotateState);
			tblocks[0].ResetOffset(0, 0);
			tblocks[1].ResetOffset(rbl[0].X, rbl[0].Y);
			tblocks[2].ResetOffset(rbl[1].X, rbl[1].Y);
			tblocks[3].ResetOffset(rbl[2].X, rbl[2].Y);
		}

		public void RotateRight() {
			if( Fall ) {
				new Debug("Tetromino#RotateRight", "Rotating Right");
				byte nrs = QuickOperations._IRB(true, rotateState, minRot, maxRot);
				byte ors = QuickOperations._IRB(true, rotateState, minRot, maxRot);
				if( nrs < 3 )
					nrs++;
				else
					nrs = 0;
				rotateState = nrs;
				SaveRotate( );
				if( Board.Length( ) > 0 ) {
					foreach( Tetromino t in Board ) {
						if( t != this ) {
							if( !t.Fall ) {
								if( AllCollides(t) ) {
									rotateState = ors;
									SaveRotate( );
								}
							}
						} else {
							if( AllCollides(null) ) {
								rotateState = ors;
								SaveRotate( );
							}
						}
					}
				}
			}
		}

		public bool IsOnLine(int y) {
			foreach( TetroBlock t in tblocks ) {
				if( t.GridPosition.Y == y && !t.Destroyed )
					return true;
			}
			return false;
		}

		public void DropIfNotUnder(int y) {
			TetroBlock lowest = tblocks[0];
			foreach( TetroBlock t in tblocks ) {
				if( t.GridPosition.Y > lowest.GridPosition.Y ) {
					lowest = t;
				}
			}
			if( lowest.GridPosition.Y < y ) {
				CenterBlock.MoveBy(0, 1);
			}
		}

		public void DropBlocksOver(int y) {
			foreach( TetroBlock t in tblocks ) {
				if( t.GridPosition.Y <= y && !t.Destroyed ) {
					t.DDDown( );
				}
			}
		}

		public List<TetroBlock> OnLine(int y) {
			List<TetroBlock> ol = new List<TetroBlock>( );
			foreach( TetroBlock t in tblocks ) {
				if( t.GridPosition.Y == y && !t.Destroyed )
					ol.Add(t);
			}
			return ol;
		}

		public void Destroy(int i) {
			tblocks[QuickOperations.InRangeBoundInclusive(i, 0, tblocks.Length - 1)].Destroy( );
		}

		public void Gravity() {
			string pl = "Tetromino#Gravity";
			new Debug(pl, $"Gravity check!");
			if( falling ) {
				new Debug(pl, $"Is falling!");
				new Debug(pl, $"Before any move: {tblocks[0].Position}");
				tblocks[0].MoveBy(0, 1);
				new Debug(pl, $"After first move: {tblocks[0].Position}");

				/**
				 Collision checking
				 */
				if( this.CollideWithGround( ) ) {
					if( this.Fall ) {
						tblocks[0].MoveBy(0, -1);
						falling = false;
					}
				} else {
					if( Board.Length( ) > 0 ) {
						foreach( Tetromino t in Board ) {
							if( t != this ) {
								if( !t.Fall && this.Fall ) {
									if( AllCollides(t) ) {
										tblocks[0].MoveBy(0, -1);
										falling = false;
									}
								}
							} else {
								if( Fall ) {
									if( AllCollides(null) ) {
										tblocks[0].MoveBy(0, -1);
										falling = false;
									}
								}
							}
						}
					}
				}
				/**
				 End of Collision Checking 
				 */
			}
		}
	}
}
