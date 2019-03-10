using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace MulTris {
	public class AnimatedSprite {

		private Texture2D txt;
		private Rectangle[] frames;
		private uint frame;
		private bool animatable = false;


		public AnimatedSprite(Texture2D texture, Rectangle[] frames) {
			try {
				if( frames.Length > 0 ) // If not empty
					frame = 0;          // Start counting
				this.txt = texture;
				this.frames = frames;
				this.animatable = true;
			} catch( Exception ) {
				this.animatable = false;
			}
		}

		public AnimatedSprite(ContentManager cm, String s, Rectangle[] frames) {
			try {
				if( frames.Length > 0 ) // If not empty
					frame = 0;          // Start counting

				this.txt = cm.Load<Texture2D>(s);
				this.frames = frames;
				this.animatable = true;
			} catch( Exception ) {
				this.animatable = false;
			}
		}


		/// <summary>
		/// Draws actual frame of this animation.
		/// </summary>
		/// <param name="sb">SpriteBatch to draw inside</param>
		/// <param name="position">Where to put your animation. (If NULL selects X:0,Y:0 and size of current frame)</param>
		/// <param name="c">Color to add to this frame</param>
		public void Draw(SpriteBatch sb, Rectangle? position, Color? c) {
			if( this.animatable )
				sb.Draw(this.txt, position ?? new Rectangle(0, 0, this.frames[this.frame].Width, this.frames[this.frame].Height), this.frames[this.frame], c ?? Color.White);
		}

		/// <summary>
		/// Draws actual frame of this animation
		/// </summary>
		/// <param name="sb">SpriteBatch to draw inside</param>
		/// <param name="position">Position of the left-top corner of animation (width and height will be taken from current frame)</param>
		/// <param name="c">Color to add to this frame</param>
		public void Draw(SpriteBatch sb, Point? position, Color? c) {
			if( this.animatable )
				this.Draw(sb, new Rectangle?(new Rectangle(position ?? new Point(0), new Point(this.frames[this.frame].Width, this.frames[this.frame].Height))), c);
		}

		/// <summary>
		/// Switch to next frame
		/// </summary>
		public void Next() {
			this.frame++;
			if( this.frame > this.frames.Length - 1 )
				this.frame = 0;
		}

		/// <summary>
		/// It switches to next frame, then draws it.
		/// <para>Short for <see cref="AnimatedSprite.Next()"/> then <see cref="AnimatedSprite.Draw(SpriteBatch, Rectangle?, Color?)"/></para>
		/// </summary>
		/// <param name="sb">SpriteBatch to invoke <see cref="Draw(SpriteBatch, Rectangle?, Color?)"/> with.</param>
		/// <param name="pos">Rectangle to invoke <see cref="Draw(SpriteBatch, Rectangle?, Color?)"/> with.</param>
		/// <param name="c">Color to invoke <see cref="Draw(SpriteBatch, Rectangle?, Color?)"/> with.</param>
		public void DrawNext(SpriteBatch sb, Rectangle? pos, Color? c) {
			this.Next( );
			this.Draw(sb, pos, c);
		}

		/// <summary>
		/// It switches to next frame, then draws it.
		/// <para>Short for <see cref="AnimatedSprite.Next()"/> then <see cref="AnimatedSprite.Draw(SpriteBatch, Point?, Color?)"/></para>
		/// </summary>
		/// <param name="sb">SpriteBatch to invoke <see cref="Draw(SpriteBatch, Point?, Color?)"/> with.</param>
		/// <param name="pos">Point to invoke <see cref="Draw(SpriteBatch, Point?, Color?)"/> with.</param>
		/// <param name="c">Color to invoke <see cref="Draw(SpriteBatch, Point?, Color?)"/> with.</param>
		public void DrawNext(SpriteBatch sb, Point? pos, Color? c) {
			this.Next( );
			this.Draw(sb, pos, c);
		}

	}
}
