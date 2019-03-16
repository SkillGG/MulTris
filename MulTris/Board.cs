using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MulTris {
	public class Board {

		// Main game
		private Multris Game;

		// Flags
		private bool load = false;
		private bool init = false;

		// Textures and SR/PR
		private Texture2D boardTXT;
		private Texture2D zTXT;

		private enum TxtIndexes {
			NONE = -1
		}

		// CONSTR=>LOAD=>INIT=>DRAW/UPDATE

		// SR
		private Rectangle[] sources = new Rectangle[0];

		// PR
		private Rectangle[] positions = new Rectangle[0];

		private List<Tetromino> tetrominoes;

		// Size
		private Point size;
		public Point Size { get => size; }

		// Grid
		private bool grid = true;
		public bool IsShowingGrid { get => grid; }
		public void ShowGrid() { grid = true; }
		public void HideGrid() { grid = false; }

		public Board(Multris game) {
			new Debug("Board#()", "Board Initialization");
			this.Game = game;
		}

		public void Init(Point bs) {
			new Debug("Board#Init", "Initialization with GameOptions");
			try {
				this.size = bs;
				this.tetrominoes = new List<Tetromino>( );
				AddTetromino(TetroType.Z);
				this.init = true;
			} catch( Exception e ) {
				this.init = false;
				new Debug("Board#Init", "ERR: " + e);
			}
		}

		public void Load(ContentManager cm) {
			try {
				new Debug("Board#Load", "Loading Board textures");
				this.boardTXT = cm.Load<Texture2D>("Game/Board");
				this.zTXT = cm.Load<Texture2D>("Game/blockZ");
				load = true;
			} catch( Exception e ) {
				load = false;
				new Debug("Board#Load", "ERR: " + e);
			}
		}

		public void Draw(SpriteBatch sb) {
			if( init && load ) {
				// Draw
				foreach( Tetromino t in tetrominoes ) {
					t.Draw(sb, this.Game);
				}
				if( grid ) {

				}
			}
		}

		public void Update(InputState bef) {
			if( init ) {
				Tetromino lastTetro = tetrominoes.Last( );

				if( !lastTetro.Fall ) {
					AddTetromino(TetroType.Z);
					return;
				} else {
					lastTetro.Update(bef);
				}
			}
		}

		public void AddTetromino(TetroType t) {

			new Debug("Board#AddTetromino", "Adding new Tetromino!");

			var nt = new Tetromino(t) {
				Fall = true
			};

			nt.MoveTo((int) ( this.Size.X / 2 ));

			new Debug("Board#AddTetromino", "Centering new Tetromino.");

			switch( t ) {
				case TetroType.Z:
					nt.Load(zTXT);
					break;
				default:
					nt.Load(this.Game.Content);
					break;
			}

			this.tetrominoes.Add(nt);
			new Debug("Board#AddTetromino", "Added new Tetromino #" + tetrominoes.Count);
		}

		public void FixedUpdateS() {
			if( init && load ) {
				// Every second

				Tetromino lastTetro = tetrominoes.Last( );

				if( !lastTetro.Fall ) {
					AddTetromino(TetroType.Z);
					return;
				} else {
					lastTetro.Gravity( );
				}

			}
		}

	}
}
