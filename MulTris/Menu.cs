using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace MulTris {
	class Menu {

		private Texture2D menuTexture;

		private bool loaded = false;

		public Menu() { }

		public void Load(ContentManager cm) {
			// TODO: Load sprites for menu
			menuTexture = cm.Load<Texture2D>("mainMenu");
			loaded = true;
		}

		public void Draw(SpriteBatch sb) {
			if( loaded )
				sb.Draw(menuTexture, new Rectangle(0, 0, Game1.WIDTH, Game1.HEIGHT), Color.White);
		}

		public void HandleClicks() {
			//	Mouse position/buttons/scroll...
			MouseState mouse = Mouse.GetState( );



		}

		~Menu() {
			if( menuTexture != null ) {
				menuTexture.Dispose( );
			}
			menuTexture = null;
		}

	}
}
