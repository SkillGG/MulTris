using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace MulTris {
	public class Tile {
		/// <summary>
		///		Whole Texture Sprite
		/// </summary>
		Texture2D texture;

		/// <summary>
		///		Source Rectangle
		/// </summary>
		Rectangle SR;

		/// <summary>
		///		Tint color for this tile
		/// </summary>
		Color color;

		/// <summary>
		///		Draw boundaries
		/// </summary>
		Rectangle bound;

		/// <summary>
		/// Flag, is texture loaded
		/// </summary>
		private bool loaded;

		/// <summary>
		/// Flag, has error message been showed already. Anti-spam system
		/// </summary>
		private bool ems = false;

		/// <summary>
		/// Blocking a default constructor
		/// </summary>
		private Tile() { }

		public Tile(Nullable<Rectangle> bn, Color inColor) {
			color = inColor;
			bound = bn ?? new Rectangle(0, 0, 50, 50);
			loaded = false;
		}

		/// <summary>
		/// Moves tile to <paramref name="x"/>, <paramref name="y"/> coordinates.
		/// </summary>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		public void Move(int x, int y) {
			bound.X = x;
			bound.Y = y;
		}

		/// <summary>
		/// Moves tile by <paramref name="x"/> in X-axis and <paramref name="y"/> in Y-axis.
		/// </summary>
		/// <param name="x">move in X-axis</param>
		/// <param name="y">move in Y-axis</param>
		public void MoveBy(int x, int y) {
			bound.X += x;
			bound.Y += y;
		}

		/// <summary>
		/// Scales(changes size) of the sprite by a factor of <paramref name="scale"/>. This function scales both dimensions of the sprite.
		/// </summary>
		/// <param name="scale">Factor to scale by</param>
		public void Scale(float scale) {
			bound.Width = (int) ( ( (float) bound.Width ) * scale );
			bound.Height = (int) ( ( (float) bound.Width ) * scale );
		}

		/// <summary>
		/// <para>
		/// Loads a texture from ContentManager via <see cref="ContentManager.Load{T}(string)"/>
		/// </para>
		/// </summary>
		/// <param name="cm"><see cref="ContentManager"/> to Load texture from</param>
		/// <param name="str">A String that will be fed into <see cref="ContentManager.Load{T}(string)"/>.</param>
		/// <param name="source">A part of texture that will be taken from the whole image. Leave <see langword="null"/> if the whole texture should be loaded</param>
		public void Load(ContentManager cm, String str, Nullable<Rectangle> source) {
			texture = cm.Load<Texture2D>(str);
			SR = source ?? texture.Bounds;
			loaded = true;
		}

		/// <summary>
		/// Draws the tile
		/// </summary>
		/// <param name="sb"><see cref="SpriteBatch"/> to draw texture in.</param>
		public void Draw(SpriteBatch sb) {
			try {
				if( loaded )
					sb.Draw(texture, bound, SR, color);
				else
					throw new ArgumentNullException("Tile has not been loaded yet!");
			} catch( ArgumentNullException e ) {
				if( !this.ems )
					Console.WriteLine("NullException: " + e.Message + " @ " + e.StackTrace);
				this.ems = true;
			} catch( Exception e ) {
				if( !this.ems )
					Console.WriteLine("Unexpected Error: " + e.Message + " @ " + e.StackTrace);
				this.ems = true;
			}
		}

		~Tile() {
			if( this.texture != null )
				this.texture.Dispose( );
			this.texture = null;
		}
	}

}
