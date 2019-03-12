using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;

namespace MulTris {

	public enum GameOptionType{
		B3,
		B4,
		B5,
		B6,
		BS
	}

	public class GameOption<T> where T : struct {
		private T v;
		private string n;
		private T? d;
		private GameOptionType type;

		public string Name { get => this.n; }
		public T Value { get => this.v; }
		public GameOptionType Type { get => this.type; }

		public GameOption(T o, GameOptionType got, string name, T? def) {
			this.n = name;
			this.v = o;
			this.type = got;
			this.d = def;
		}
		public void ChangeValue(T o) {
			this.v = o;
		}
		public void ChangeToDefault() {
			this.v = this.d ?? this.v;
		}
	}

	public class GameOption {
		private string v;
		private string n;
		private string d;
		private GameOptionType type;

		public string Name { get => this.n; }
		public string Value { get => this.v; }
		public GameOptionType Type { get => this.type; }

		public GameOption(string o, GameOptionType got, string name, string def) {
			this.n = name;
			this.v = o;
			this.type = got;
			this.d = def;
		}
		public void ChangeValue(string o) {
			this.v = o;
		}
		public void ChangeToDefault() {
			this.v = this.d ?? this.v;
		}
	}

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
				if( size.Type == GameOptionType.BS )
					this.boardSize = size;
				foreach(GameOption<bool> go in blocks){
					if( go.Type == GameOptionType.B3 )
						this._3k = go;
					if( go.Type == GameOptionType.B4 )
						this._4k = go;
					if( go.Type == GameOptionType.B5 )
						this._5k = go;
					if( go.Type == GameOptionType.B6 )
						this._6k = go;
				}
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

		~Tetris(){

		}

	}
}
