using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;

namespace MulTris {
	public class Tetris {

		private GameOption<Point> boardSize;
		private GameOption<bool> _3k;
		private GameOption<bool> _4k;
		private GameOption<bool> _5k;
		private GameOption<bool> _6k;

		private bool init = false;
		private bool load = false;


		public GameOption<Point> BoardSize { get { return boardSize; } }
		public GameOption<bool>[] UsedBlocks {
			get { return new GameOption<bool>[4] { _3k, _4k, _5k, _6k }; }
		}

		public void Load(ContentManager cm){
			try {
				// LOAD TXTs and SFXs
				this.load = true;
			}catch(Exception)
			{
				this.load = false;
			}
		}

		public Tetris() { }

		public void Initialize(GameOption<Point> size, GameOption<bool>[] blocks) {
			try {
				// INIT (Clicked PLAY)
				this.boardSize = size;
				this._3k = blocks[0];
				this._4k = blocks[1];
				this._5k = blocks[2];
				this._6k = blocks[3];
				this.init = true;
			}catch(Exception){
				this.init = false;
			}
		}

		public void Draw(SpriteBatch sb) {
			if(init && load){
				// DRAW

			}
		}

		public void Update(){
			if(init){
				// UPDATE
			}
		}

	}
}
