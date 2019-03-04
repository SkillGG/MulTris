using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace MulTris {

	public enum MouseButton {
		RIGHT,
		LEFT,
		MIDDLE,
		NONE
	}

	public class MouseClick {

		private MouseButton btn;
		private Point pnt;

		public MouseButton button { get { return this.btn; } }
		public Point point { get { return this.pnt; } }

		private MouseClick() { }

		public MouseClick(Point p, MouseButton b) {
			this.pnt = p;
			this.btn = b;
		}

		public MouseClick(int x, int y, MouseButton b) {
			this.pnt = new Point(x, y);
			this.btn = b;
		}

	}

	public class InputState {

		public MouseState mouse;
		public KeyboardState keyboard;
		public GamePadState gamePad1;
		public GamePadState gamePad2;

		public InputState() {
			this.mouse = Mouse.GetState( );
			this.keyboard = Keyboard.GetState( );
			this.gamePad1 = GamePad.GetState(0);
			this.gamePad2 = GamePad.GetState(1);
		}

		public void Update() {
			this.mouse = Mouse.GetState( );
			this.keyboard = Keyboard.GetState( );
			this.gamePad1 = GamePad.GetState(0);
			this.gamePad2 = GamePad.GetState(1);
		}

		public bool StateChangeDown(InputState b, Keys k) {
			this.Update( );
			if( b.keyboard.IsKeyDown(k) != this.keyboard.IsKeyDown(k) )
				return true;
			return false;
		}

		/// <summary>
		/// A function that given previous InputState checks if either Right/Left/Middle mouse Button was clicked.
		/// </summary>
		/// <param name="b">Previous InputState (before .Update())</param>
		/// <returns>
		/// MouseClick object that contains every information about given click.
		/// </returns>
		public MouseClick MouseClicked(InputState b) {
			this.Update( );
			if( b.mouse.LeftButton == ButtonState.Released && this.mouse.LeftButton == ButtonState.Pressed )
				return new MouseClick(mouse.X, mouse.Y, MouseButton.LEFT);
			if( b.mouse.RightButton == ButtonState.Released && this.mouse.RightButton == ButtonState.Pressed )
				return new MouseClick(mouse.X, mouse.Y, MouseButton.RIGHT);
			if( b.mouse.MiddleButton == ButtonState.Released && this.mouse.MiddleButton == ButtonState.Pressed )
				return new MouseClick(mouse.X, mouse.Y, MouseButton.RIGHT);
			return new MouseClick(0, 0, MouseButton.NONE); // No change in state appeared
		}

	}
}