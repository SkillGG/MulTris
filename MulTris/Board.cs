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
			this.Game = game;
		}

		public void Init(Point bs) {
			Console.WriteLine("(Board) Init Start!");
			try {
				this.size = bs;
				this.tetrominoes = new List<Tetromino>( );
				AddTetromino(TetroType.Z);
				this.init = true;
			} catch( Exception e ) {
				this.init = false;
				Console.Error.WriteLine(e.Message);
			}
		}

		public void Load(ContentManager cm) {
			try {
				this.boardTXT = cm.Load<Texture2D>("Game/Board");
				this.zTXT = cm.Load<Texture2D>("Game/blockZ");
				load = true;
			} catch( Exception e ) {
				load = false;
				Console.Error.WriteLine(e);
			}
		}

		public void Draw(SpriteBatch sb) {
			if( init && load ) {
				// Draw
				if( grid ) {
					
				}
			} else {
				Console.Error.WriteLine("(Board) Not Init/Load yet!");
			}
		}

		public void Update() {
			if( init ) {

			}
		}

		public void AddTetromino(TetroType t){

			var nt = new Tetromino(t) {
				Fall = true
			};

			switch( t ) {
				case TetroType.Z:
					nt.Load(zTXT);
					break;
				default:
					nt.Load(this.Game.Content);
					break;
			}

			this.tetrominoes.Add(nt);
			Console.WriteLine("New Tetromino added! #{0}", tetrominoes.Count);
		}

		public void FixedUpdateS(){

			// Every second

			Tetromino lastTetro = tetrominoes.Last( );

			if(!lastTetro.Fall){
				AddTetromino(TetroType.Z);
				return;
			}
			else{
				lastTetro.Gravity( );
			}

		}

	}
}
