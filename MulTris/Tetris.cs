using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;

namespace MulTris {

	public enum GameOptionType {
		B3,
		B4,
		B5,
		B6,
		BS
	}

	public class GameOption<T> where T : struct {
		private T v;
		private readonly string n;
		private readonly T? d;
		private readonly GameOptionType type;

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
		private readonly string n;
		private readonly string d;
		private readonly GameOptionType type;

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

		private Multris Game;

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

		public void Load(ContentManager cm) {
			try {
				// LOAD TXTs and SFXs
				this.board.Load(cm);
				this.load = true;
			} catch( Exception e ) {
				this.load = false;
				Console.Error.WriteLine(e);
			}
		}

		public Tetris(Multris game) {
			this.Game = game;
			this.board = new Board(game);
		}

		public Board board;

		public void FixedUpdateS() {
			if( init )
				this.board.FixedUpdateS( );
		}

		public void Initialize(GameOption<Point> size, GameOption<bool>[] blocks) {
			try {
				// INIT (Clicked PLAY)
				if( size.Type == GameOptionType.BS )
					this.boardSize = size;
				foreach( GameOption<bool> go in blocks ) {
					if( go.Type == GameOptionType.B3 )
						this._3k = go;
					if( go.Type == GameOptionType.B4 )
						this._4k = go;
					if( go.Type == GameOptionType.B5 )
						this._5k = go;
					if( go.Type == GameOptionType.B6 )
						this._6k = go;
				}
				this.board.Init(this.boardSize.Value);
				this.init = true;
			} catch( Exception e ) {
				this.init = false;
				Console.Error.WriteLine(e);
			}
		}

		public void Draw(SpriteBatch sb) {
			if( init && load ) {
				// DRAW
				this.board.Draw(sb);
			}
		}

		public void Update(InputState bef) {
			if( init ) {
				// UPDATE
				this.board.Update( );
			}
		}

	}
}
