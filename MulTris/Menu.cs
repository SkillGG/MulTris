using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace MulTris {
	public class Menu {

		private Multris game;

		private Texture2D menuTexture;

		/// <summary>
		/// <para>This Rectangle Array contains source placements of given elements from MainMenu/mainMenu texture.</para>
		/// <para>0 - Game Logo</para>
		/// <para>1 - Play Button</para>
		/// <para>2 - Options Button</para>
		/// <para>3 - Exit Button</para>
		/// </summary>
		private Rectangle[] menuSprites = new Rectangle[4] {
			new Rectangle(0, 0, 300, 60),		// Game Logo
			new Rectangle(0, 180, 240, 100),	// Play Button
			new Rectangle(0, 70, 240, 100),		// Options Button
			new Rectangle(0, 290, 240, 100)		// Exit Button
		};

		private bool changeBTNPos = false;
		private bool changeBTNSprite = false;

		/// <summary>
		/// <para>This Rectangle Array contains all placements of buttons on-screen. Where they are drawn</para>
		/// <para>0 - Game Logo</para>
		/// <para>1 - Play Button</para>
		/// <para>2 - Options Button</para>
		/// <para>3 - Exit Button</para>
		/// </summary>
		private Rectangle[] menuPositions = new Rectangle[4] {
			new Rectangle(0, 0, 0, 0),		// Game Logo
			new Rectangle(0, 0, 0, 0),		// Play Button
			new Rectangle(0, 0, 0, 0),		// Options Button
			new Rectangle(0, 0, 0, 0)		// Exit Button
		};

		private bool loaded = false;

		public void setSpritePosFor(uint b, Nullable<Rectangle> r) {
			changeBTNSprite = true;
			if( b > menuSprites.Length - 1 )
				return;
			Rectangle sr = r ?? menuSprites[b];
			switch( b ) {
				case 0:
					GameLogo = new Rectangle[2] { sr, sr };
					break;
				case 1:
					PlayButton = new Rectangle[2] { sr, sr };
					break;
				case 2:
					OptButton = new Rectangle[2] { sr, sr };
					break;
				case 3:
					ExitButton = new Rectangle[2] { sr, sr };
					break;
			}
			changeBTNSprite = false;
		}

		public void setPositionFor(uint b, Nullable<Rectangle> r) {
			changeBTNPos = true;
			if( b > menuPositions.Length - 1 )
				return;
			Rectangle sr = r ?? menuPositions[b];
			switch( b ) {
				case 0:
					GameLogo = new Rectangle[2] { sr, sr };
					break;
				case 1:
					PlayButton = new Rectangle[2] { sr, sr };
					break;
				case 2:
					OptButton = new Rectangle[2] { sr, sr };
					break;
				case 3:
					ExitButton = new Rectangle[2] { sr, sr };
					break;
			}
			changeBTNPos = false;
		}

		/// <summary>
		/// A Menu Logo Parameter.
		/// It is an Array containing two Rectangles:
		/// <para>0 - buttonPosition</para>
		/// <para>1 - buttonSprite</para>
		/// To change it's position or sprite you have to use <see cref="Menu.setSpritePosFor(int,Rectangle)"/>/<see cref="Menu.setPositionFor(int,Rectangle)"/>
		/// </summary>
		public Rectangle[] GameLogo {
			get {
				return new Rectangle[2] { menuPositions[0], menuSprites[0] };
			}
			set {
				if( changeBTNPos && changeBTNSprite ) {
					changeBTNPos = false;
					changeBTNSprite = false;
					throw new InvalidOperationException("You should not be able to do that operation!");
				}
				if( changeBTNPos )
					menuPositions[0] = value[0];
				if( changeBTNSprite )
					menuSprites[0] = value[0];
			}
		}

		/// <summary>
		/// A Play Button Parameter.
		/// It is an Array containing two Rectangles:
		/// <para>0 - buttonPosition</para>
		/// <para>1 - buttonSprite</para>
		/// To change it's position or sprite you have to use <see cref="Menu.setSpritePosFor(int,Rectangle)"/>/<see cref="Menu.setPositionFor(int,Rectangle)"/>
		/// </summary>
		public Rectangle[] PlayButton {
			get {
				return new Rectangle[2] { menuPositions[1], menuSprites[1] };
			}
			set {
				if( changeBTNPos && changeBTNSprite ) {
					changeBTNPos = false;
					changeBTNSprite = false;
					throw new InvalidOperationException("You should not be able to do that operation!");
				}
				if( changeBTNPos )
					menuPositions[1] = value[0];
				if( changeBTNSprite )
					menuSprites[1] = value[0];
			}
		}

		/// <summary>
		/// An Option Button Parameter.
		/// It is an Array containing two Rectangles:
		/// <para>0 - buttonPosition</para>
		/// <para>1 - buttonSprite</para>
		/// To change it's position or sprite you have to use <see cref="Menu.setSpritePosFor(int,Rectangle)"/>/<see cref="Menu.setPositionFor(int,Rectangle)"/>
		/// </summary>
		public Rectangle[] OptButton {
			get {
				return new Rectangle[2] { menuPositions[2], menuSprites[2] };
			}
			set {
				if( changeBTNPos && changeBTNSprite ) {
					changeBTNPos = false;
					changeBTNSprite = false;
					throw new InvalidOperationException("You should not be able to do that operation!");
				}
				if( changeBTNPos )
					menuPositions[2] = value[0];
				if( changeBTNSprite )
					menuSprites[2] = value[0];
			}
		}

		/// <summary>
		/// An Exit Button Parameter.
		/// It is an Array containing two Rectangles:
		/// <para>0 - buttonPosition</para>
		/// <para>1 - buttonSprite</para>
		/// To change it's position or sprite you have to use <see cref="Menu.setSpritePosFor(int,Rectangle)"/>/<see cref="Menu.setPositionFor(int,Rectangle)"/>
		/// </summary>
		public Rectangle[] ExitButton {
			get {
				return new Rectangle[2] { menuPositions[3], menuSprites[3] };
			}
			set {
				if( changeBTNPos && changeBTNSprite ) {
					changeBTNPos = false;
					changeBTNSprite = false;
					throw new InvalidOperationException("You should not be able to do that operation!");
				}
				if( changeBTNPos )
					menuPositions[3] = value[0];
				if( changeBTNSprite )
					menuSprites[3] = value[0];
			}
		}

		public Menu(Multris g) {
			this.game = g;
			this.setPositionFor(0, new Rectangle(
				( g.WIDTH - GameLogo[1].Width ) / 2,
				0,
				GameLogo[1].Width,
				GameLogo[1].Height
			));
			this.setPositionFor(1, new Rectangle(
				( g.WIDTH - GameLogo[1].Width ) / 2,
				120,
				PlayButton[1].Width,
				PlayButton[1].Height
			));
			this.setPositionFor(2, new Rectangle(
				( g.WIDTH - GameLogo[1].Width ) / 2,
				240,
				OptButton[1].Width,
				OptButton[1].Height
			));
			this.setPositionFor(3, new Rectangle(
				( g.WIDTH - GameLogo[1].Width ) / 2,
				460,
				ExitButton[1].Width,
				ExitButton[1].Height
			));
		}

		public void Load(ContentManager cm) {
			menuTexture = cm.Load<Texture2D>("MainMenu/mainMenu");
			loaded = true;
		}

		public void Draw(SpriteBatch sb) {
			if( loaded ) {
				sb.Draw(this.menuTexture, GameLogo[0], GameLogo[1], Color.White);
			}
		}

		public void HandleClicks() {
			//	Mouse position/buttons/scroll...
			MouseState mouse = Mouse.GetState( );

			// TODO: Proper handling mouse presses

		}

		public void HandleKeyboard() {
			// Keyboard info
			KeyboardState keyboard = Keyboard.GetState( );

			// TODO: Proper handling key presses

		}

		~Menu() {
			if( menuTexture != null ) {
				menuTexture.Dispose( );
			}
			menuTexture = null;
		}

	}
}
