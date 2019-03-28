using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MulTris {
	public class Board {

		// Main game
		private Multris Game;

		// Flags
		private bool load = false;
		private bool init = false;

		// Textures and SR/PR
		private Texture2D boardTXT;
		private Texture2D blockTXT;

		private enum TxtIndexes {
			NONE = -1
		}

		// CONSTR=>LOAD=>INIT=>DRAW/UPDATE

		// SR
		private Rectangle[] sources = new Rectangle[0];

		// PR
		private Rectangle[] positions = new Rectangle[0];

		public TetroBlock this[ushort x, ushort y] {
			get {
				return null;
			}
		}

		public Tetromino this[ushort i] {
			get {
				return tetrominoes[i];
			}
		}

		public IEnumerator<Tetromino> GetEnumerator() {
			return tetrominoes.GetEnumerator( );
		}

		public ushort Length() {
			return (ushort) tetrominoes.Count;
		}

		private List<Tetromino> tetrominoes;
		public List<Tetromino> Tetrominoes { get => tetrominoes; }
		public Tetromino LastTetro { get => tetrominoes.Last( ); }


		// Size
		private Point size;
		/// <summary>
		/// Size of the board in blocks
		/// </summary>
		public Point Size { get => size; }

		/// <summary>
		/// Width of playable area in pixels
		/// </summary>
		public int Width { get => GridSize * ( Size.X + 1 ); }
		/// <summary>
		/// Position of left-top corner of left wall of the board
		/// </summary>
		public Point LeftWall { get => new Point(Game.WIDTH / 2 - Width / 2, 0); }
		/// <summary>
		/// Position of left-top corner of right wall of the board
		/// </summary>
		public Point RightWall { get => new Point(Game.WIDTH / 2 + Width / 2, 0); }

		// Grid
		private bool grid = true;
		public bool IsShowingGrid { get => grid; }
		public void ShowGrid() { grid = true; }
		public void HideGrid() { grid = false; }

		/// <summary>
		/// Size of one block
		/// </summary>
		public int GridSize {
			get {
				int ret = QuickOperations.InRangeBoundInclusive(Game.HEIGHT / size.Y, 0, 50);
				new Debug("Board#GridSize.get", "Grid Size calculated to be: " + ( Game.HEIGHT / size.Y ) + " (Bound to: " + ret + ")");
				return ret;
			}
		}

		public Board(Multris game) {
			new Debug("Board#()", "Board Initialization");
			this.Game = game;
		}

		public void Init(Point bs) {
			new Debug("Board#Init", $"Initialization with GameOptions: BS:{bs}");
			try {
				this.size = new Point(bs.X, bs.Y);
				this.tetrominoes = new List<Tetromino>( );
				AddTetromino(TetroType.S);
				this.init = true;
			} catch( System.Exception e ) {
				this.init = false;
				new Debug("Board#Init", "ERR: " + e);
			}
		}

		public bool CheckIfFullLine(int y) {
			int onthisline = 0;
			for( int i = 0; i < tetrominoes.Count - 1; i++ ) {
				Tetromino t = tetrominoes[i];
				if( !t.Fall )
					onthisline += t.OnLine(y).Count;
			}
			/*
			if( onthisline != 0 )
				new Debug("", $"On line {y} found {onthisline} blocks!", Debug.Importance.IMPORTANT_INFO);
			*/
			if( onthisline >= Size.X ) {
				return true;
			}
			return false;
		}

		public void Load(ContentManager cm) {
			try {
				new Debug("Board#Load", "Loading Board textures");
				this.boardTXT = cm.Load<Texture2D>("Game/Board");
				this.blockTXT = cm.Load<Texture2D>("Game/blocks");
				load = true;
			} catch( System.Exception e ) {
				load = false;
				new Debug("Board#Load", "ERR: " + e);
			}
		}

		public void Draw(SpriteBatch sb) {
			if( init && load ) {
				// Draw
				foreach( Tetromino t in tetrominoes ) {
					t.Draw(sb);
				}
				//sb.Draw(this.boardTXT, new Rectangle(LeftWall.X - 4, 0, Width + 1, Game.HEIGHT + 10), new Rectangle(0, 5, 50, 45), Color.White);
			}
		}

		public void DestroyLine(int y) {
			foreach( Tetromino t in tetrominoes ) {
				if( !t.Fall ) {
					if( t.IsOnLine(y) ) {
						t.OnLine(y).ForEach((x) => x.Destroy( ));
						t.DropBlocksOver(y);
					} else {
						t.DropIfNotUnder(y);
					}
				}
			}
		}

		public void Update(InputState bef) {
			if( init ) {
				Tetromino lastTetro = tetrominoes.Last( );
				InputState inputs = new InputState( );
				if( inputs.KeyUp(bef, Keys.F3) ) {
					foreach( Tetromino t in tetrominoes ) {
						t.ToggleDebug( );
					}
				}
				lastTetro.Update(bef);
			}
		}

		private readonly List<TetroType> UsedTT = new List<TetroType> { TetroType.Z, TetroType.S, TetroType.I };

		public void AddTetromino(TetroType t) {

			new Debug("Board#AddTetromino", $"Adding new Tetromino {t.ToString( )}!", Debug.Importance.IMPORTANT_INFO);

			var nt = new Tetromino(t, this.Game, this) {
				Fall = true
			};

			nt.MoveTo((int) ( this.Size.X / 2 + this.LeftWall.X / GridSize ));
			nt.OneUp( );

			new Debug("Board#AddTetromino", "Centering new Tetromino.");


			Tetromino lt = null;
			if( tetrominoes.Count != 0 )
				lt = tetrominoes[tetrominoes.Count - 1];


			switch( t ) {
				case TetroType.Z:
					nt.Load(blockTXT, new Rectangle(0, 0, 50, 50));
					break;
				case TetroType.S:
					nt.Load(blockTXT, new Rectangle(50, 0, 50, 50));
					break;
				case TetroType.I:
					nt.Load(blockTXT, new Rectangle(100, 0, 50, 50));
					break;
				default:
					nt.Load(this.Game.Content);
					break;
			}

			if( lt != null ) {
				nt.CenterBlock.DebugInfo = lt.CenterBlock.DebugInfo;
			}

			this.tetrominoes.Add(nt);
			new Debug("Board#AddTetromino", "Added new Tetromino #" + tetrominoes.Count);
		}

		/* DEBUG SET YOUR RNG
		private int rngid = 0;
		private List<TetroType> GMN = new List<TetroType> {
			TetroType.S,
			TetroType.Z,
			TetroType.Z,
			TetroType.Z,
			TetroType.Z,
			TetroType.S,
			TetroType.S,
			TetroType.S,
			TetroType.Z,
			TetroType.I
		};
		*/

		public TetroType RandomTT(List<TetroType> l) {
			// RNG
			int rn = new Random(DateTime.Now.Millisecond).Next(0, l.Count * 10);
			new Debug("Board#RandomTT", $"CAP: 0 - {l.Count * 10 - 1}. #: {rn}.");
			return l[(int) ( rn / 10 )];

			/* DEBUG SET YOUR RNG
			rngid++;
			var r = GMN[rngid];
			if( r == TetroType.I ) {
				rngid = 0;
				r = TetroType.S;
			}
			return r;
			*/
		}

		private bool sped = false;

		public void ShowAllTBData() {
			foreach( Tetromino t in this ) {
				t.ShowTBData( );
			}
		}

		public void FallUpdate() {
			if( init && load ) {
				if( !sped ) {
					if( tetrominoes.Count > 5 ) {
						new Debug("FU", "Speed up!", Debug.Importance.IMPORTANT_INFO);
						this.Game.tetris.SpeedUp(4);
						sped = true;
					}
				}
				// Every second
				new Debug("Board#FixedUpdateS", $"Fall should occur!");
				Tetromino lastTetro = tetrominoes.Last( );
				for( int i = Size.Y; i > 0; i-- ) {
					if( CheckIfFullLine(i) ) {
						new Debug("", $"Line {i} is full. Destroying!", Debug.Importance.IMPORTANT_INFO);
						DestroyLine(i);
					}
				}
				if( !lastTetro.Fall ) {
					AddTetromino(RandomTT(UsedTT));
					return;
				} else {
					lastTetro.Gravity( );
				}
			}
		}

		public void FixedUpdateS() {

		}

	}
}
