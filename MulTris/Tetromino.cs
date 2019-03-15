﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
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

		public Point[] GetRotationOffsetsFor(TetroType t, byte rotS) {

			Point[] ret = new Point[3];

			switch( t ) {
				// TODO: Rotations
				case TetroType.Z:
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

		public void MoveTo(int x) {
			this.tblocks[0].MoveTo(x, tblocks[0].Position.Y);
		}

		public void MoveBy(int x) {
			this.tblocks[0].MoveBy(x, 0);
		}

		private byte rotateState = 0;
		private readonly byte minRot = 0;
		private readonly byte maxRot = 3;

		public Tetromino(TetroType t) {
			this.falling = true;
			this.Type = t;

			Point[] rBl = GetRotationOffsetsFor(t, 0);

			tblocks[0].Init(null, 0, 0);
			tblocks[1].Init(tblocks[0], rBl[0].X, rBl[0].Y);
			tblocks[2].Init(tblocks[0], rBl[1].X, rBl[1].Y);
			tblocks[3].Init(tblocks[0], rBl[2].X, rBl[2].Y);
		}

		public void RotateLeft() {

			rotateState = QuickOperations._IRB(true, rotateState, minRot, maxRot);
			if( rotateState != 0 )
				rotateState--;
			else
				rotateState = 3;

			Point[] rbl = GetRotationOffsetsFor(this.Type, rotateState);
			tblocks[0].ResetOffset(0, 0);
			tblocks[1].ResetOffset(rbl[0].X, rbl[0].Y);
			tblocks[2].ResetOffset(rbl[1].X, rbl[1].Y);
			tblocks[3].ResetOffset(rbl[3].X, rbl[2].Y);
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