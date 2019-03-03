using Microsoft.Xna.Framework.Input;

namespace MulTris {
	public class InputState {

		public MouseState mouse;
		public KeyboardState keyboard;
		public GamePadState gamePad1;
		public GamePadState gamePad2;

		public InputState() {
			this.mouse = Mouse.GetState();
			this.keyboard = Keyboard.GetState();
			this.gamePad1 = GamePad.GetState(0);
			this.gamePad2 = GamePad.GetState(1);
		}

		public void Update(){
			this.mouse = Mouse.GetState( );
			this.keyboard = Keyboard.GetState( );
			this.gamePad1 = GamePad.GetState(0);
			this.gamePad2 = GamePad.GetState(1);
		}

		public bool StateChangeDown(InputState b, Keys k){
			this.Update( );
			if( b.keyboard.IsKeyDown(k) != this.keyboard.IsKeyDown(k) )
				return true;
			return false;
		}

	}
}