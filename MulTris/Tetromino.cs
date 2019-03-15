using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MulTris {
	public enum TetroType {
		Z,
		S,
		I,
		L,
		J,
		O
	}

	public class Tetromino {

		private bool falling;
		private uint toGround = 10;
		public bool Fall { get => falling; set => falling = value; }

		public TetroType Type;

		private TetroBlock[] tblocks = new TetroBlock[4]{
			new TetroBlock(TBT.Center, 50),
			new TetroBlock(TBT.Side, 50),
			new TetroBlock(TBT.Side, 50),
			new TetroBlock(TBT.Side, 50)
		};

		public void Load(ContentManager cm) {

			foreach( TetroBlock t in tblocks ) {
				t.Load(cm, this.Type);
			}

		}

		public void Load(Texture2D txt) {
			foreach( TetroBlock t in tblocks ) {
				t.Load(txt);
			}
		}

		public void Draw(SpriteBatch sb) {

			foreach( TetroBlock t in tblocks ) {
				t.Draw(sb);
			}

		}

		public Tetromino(TetroType t) {
			this.falling = true;
			this.Type = t;
			tblocks[0].Init(null, 0, 0);
			tblocks[1].Init(tblocks[0], 1, 0);
			tblocks[1].Init(tblocks[0], 1, 1);
			tblocks[1].Init(tblocks[0], 1, 2);
		}

		public void Gravity() {
			if( toGround == 0 )
				falling = false;
			if( falling ) {
				toGround--;
				tblocks[0].MoveBy(0, 1);
			} else
				Console.WriteLine("Dude. Imma on ground!");
		}

	}
}
