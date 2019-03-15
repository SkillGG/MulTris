using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace MulTris {
	public class Menu {

		private Multris game;

		public enum MenuState {
			NORMAL,
			EXIT
		}

		public MenuState State { set; get; } = MenuState.NORMAL;

		private void ExitConfirm() { new Debug("Menu#ExitConfirm", "State of menu changed to EXIT. (Showing exit confiramtion window)"); this.State = MenuState.EXIT; }

		private Texture2D texture;

		/// <summary>
		/// <para>This Rectangle Array contains source placements of given elements from MainMenu/mainMenu texture.</para>
		/// <para>0 - Game Logo</para>
		/// <para>1 - Play Button</para>
		/// <para>2 - Options Button</para>
		/// <para>3 - Exit Button</para>
		/// <para>4 - Close Window</para>
		/// <para>5 - Close Close Window</para>
		/// </summary>
		private Rectangle[] sprites = new Rectangle[7] {
			new Rectangle(0, 100, 900, 180),	// Game Logo
			new Rectangle(0, 0, 240, 100),		// Play Button
			new Rectangle(240, 0, 240, 100),	// Options Button
			new Rectangle(480, 0, 240, 100),	// Exit Button
			new Rectangle(0, 280, 800, 600),	// Close Window
			new Rectangle(720, 0, 40, 40),		// Close Close Window
			new Rectangle(0, 880, 240, 100)		// Confirm Close Button
		};

		private bool changeBTNPos = false;
		private bool changeBTNSprite = false;

		/// <summary>
		/// <para>This Rectangle Array contains all placements of buttons on-screen. Where they are drawn</para>
		/// <para>0 - Game Logo</para>
		/// <para>1 - Play Button</para>
		/// <para>2 - Options Button</para>
		/// <para>3 - Exit Button</para>
		/// <para>4 - Close Window</para>
		/// <para>5 - Close Close Window</para>
		/// </summary>
		private Rectangle[] positions = new Rectangle[7] {
			new Rectangle(0, 0, 0, 0),		// Game Logo
			new Rectangle(0, 0, 0, 0),		// Play Button
			new Rectangle(0, 0, 0, 0),		// Options Button
			new Rectangle(0, 0, 0, 0),		// Exit Button
			new Rectangle(0, 0, 0, 0),		// Close Window
			new Rectangle(0, 0, 0, 0),		// Close Close Window
			new Rectangle(0, 0, 0, 0)		// Confirm Close Button
		};

		/// <summary>
		/// <para>This Color Array contains all Colors of buttons on-screen. What Color will they be tined in <see cref="SpriteBatch.Draw(Texture2D, Rectangle, Rectangle?, Color)"/></para>
		/// <para>0 - Game Logo</para>
		/// <para>1 - Play Button</para>
		/// <para>2 - Options Button</para>
		/// <para>3 - Exit Button</para>
		/// <para>4 - Close Window</para>
		/// <para>5 - Close Close Window</para>
		/// </summary>
		private Color[] colors = new Color[7]{
			Color.White,				// Game Logo
			new Color(200,200,200),		// Play Button
			new Color(200,200,200),		// Option Button
			new Color(200,200,200),		// Exit Button
			Color.White,				// Close Window
			Color.White,				// Close Close Window
			new Color(200,200,200)		// Confirm Close Button
		};

		private bool loaded = false;

		public void ChangeColorFor(uint b, Color? c) {
			if( b > colors.Length - 1 )
				return;
			Color sc = c ?? colors[b];
			colors[b] = sc;
		}

		public void ChangeColorForNew(uint b, Color? c) {
			if( b > colors.Length - 1 )
				return;
			Color sc = c ?? colors[b];
			if( !sc.Equals(colors[b]) )
				ChangeColorFor(b, c);
		}

		public void SetSpritePosFor(uint b, Nullable<Rectangle> r) {
			changeBTNSprite = true;
			if( b > sprites.Length - 1 )
				return;
			Rectangle sr = r ?? sprites[b];
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
				case 4:
					CloseWindow = new Rectangle[2] { sr, sr };
					break;
				case 5:
					CloseCloseWindow = new Rectangle[2] { sr, sr };
					break;
				case 6:
					ConfirmCloseButton = new Rectangle[2] { sr, sr };
					break;
			}
			changeBTNSprite = false;
		}

		public void SetPositionFor(uint b, Nullable<Rectangle> r) {
			new Debug("Menu#SetPositionFor", "Trying to set " + b + "'s position to " + r + ".");
			changeBTNPos = true;
			if( b > positions.Length - 1 )
				return;
			Rectangle sr = r ?? positions[b];
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
				case 4:
					CloseWindow = new Rectangle[2] { sr, sr };
					break;
				case 5:
					CloseCloseWindow = new Rectangle[2] { sr, sr };
					break;
				case 6:
					ConfirmCloseButton = new Rectangle[2] { sr, sr };
					break;
			}
			changeBTNPos = false;
			new Debug("Menu#SetPositionFor", "Successfully changed " + b + "'s position!");
		}

		/// <summary>
		/// A Menu Logo Parameter.
		/// It is an Array containing two Rectangles:
		/// <para>0 - position on screen</para>
		/// <para>1 - position on sprite</para>
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 0 as first parameter.
		/// </summary>
		public Rectangle[] GameLogo {
			get {
				return new Rectangle[2] { positions[0], sprites[0] };
			}
			set {
				if( changeBTNPos && changeBTNSprite ) {
					changeBTNPos = false;
					changeBTNSprite = false;
					throw new InvalidOperationException("You should not be able to do that operation!");
				}
				if( changeBTNPos )
					positions[0] = value[0];
				if( changeBTNSprite )
					sprites[0] = value[0];
			}
		}

		/// <summary>
		/// A Play Button Parameter.
		/// It is an Array containing two Rectangles:
		/// <para>0 - position on screen</para>
		/// <para>1 - position on sprite</para>
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 1 as first parameter.
		/// </summary>
		public Rectangle[] PlayButton {
			get {
				return new Rectangle[2] { positions[1], sprites[1] };
			}
			set {
				if( changeBTNPos && changeBTNSprite ) {
					changeBTNPos = false;
					changeBTNSprite = false;
					throw new InvalidOperationException("You should not be able to do that operation!");
				}
				if( changeBTNPos )
					positions[1] = value[0];
				if( changeBTNSprite )
					sprites[1] = value[0];
			}
		}

		/// <summary>
		/// An Option Button Parameter.
		/// It is an Array containing two Rectangles:
		/// <para>0 - position on screen</para>
		/// <para>1 - position on sprite</para>
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 2 as first parameter.
		/// </summary>
		public Rectangle[] OptButton {
			get {
				return new Rectangle[2] { positions[2], sprites[2] };
			}
			set {
				if( changeBTNPos && changeBTNSprite ) {
					changeBTNPos = false;
					changeBTNSprite = false;
					throw new InvalidOperationException("You should not be able to do that operation!");
				}
				if( changeBTNPos )
					positions[2] = value[0];
				if( changeBTNSprite )
					sprites[2] = value[0];
			}
		}

		/// <summary>
		/// An Exit Button Parameter.
		/// It is an Array containing two Rectangles:
		/// <para>0 - position on screen</para>
		/// <para>1 - position on sprite</para>
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 3 as first parameter.
		/// </summary>
		public Rectangle[] ExitButton {
			get {
				return new Rectangle[2] { positions[3], sprites[3] };
			}
			set {
				if( changeBTNPos && changeBTNSprite ) {
					changeBTNPos = false;
					changeBTNSprite = false;
					throw new InvalidOperationException("You should not be able to do that operation!");
				}
				if( changeBTNPos )
					positions[3] = value[0];
				if( changeBTNSprite )
					sprites[3] = value[0];
			}
		}

		/// <summary>
		/// An Exit Button Parameter.
		/// It is an Array containing two Rectangles:
		/// <para>0 - position on screen</para>
		/// <para>1 - position on sprite</para>
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 3 as first parameter.
		/// </summary>
		public Rectangle[] CloseWindow {
			get {
				return new Rectangle[2] { positions[4], sprites[4] };
			}
			set {
				if( changeBTNPos && changeBTNSprite ) {
					changeBTNPos = false;
					changeBTNSprite = false;
					throw new InvalidOperationException("You should not be able to do that operation!");
				}
				if( changeBTNPos )
					positions[4] = value[0];
				if( changeBTNSprite )
					sprites[4] = value[0];
			}
		}

		/// <summary>
		/// An Close CloseWindow Parameter.
		/// It is an Array containing two Rectangles:
		/// <para>0 - position on screen</para>
		/// <para>1 - position on sprite</para>
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 3 as first parameter.
		/// </summary>
		public Rectangle[] CloseCloseWindow {
			get {
				return new Rectangle[2] { positions[5], sprites[5] };
			}
			set {
				if( changeBTNPos && changeBTNSprite ) {
					changeBTNPos = false;
					changeBTNSprite = false;
					throw new InvalidOperationException("You should not be able to do that operation!");
				}
				if( changeBTNPos )
					positions[5] = value[0];
				if( changeBTNSprite )
					sprites[5] = value[0];
			}
		}

		/// <summary>
		/// An Confirm CloseWindow Parameter.
		/// It is an Array containing two Rectangles:
		/// <para>0 - position on screen</para>
		/// <para>1 - position on sprite</para>
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 3 as first parameter.
		/// </summary>
		public Rectangle[] ConfirmCloseButton {
			get {
				return new Rectangle[2] { positions[6], sprites[6] };
			}
			set {
				if( changeBTNPos && changeBTNSprite ) {
					changeBTNPos = false;
					changeBTNSprite = false;
					throw new InvalidOperationException("You should not be able to do that operation!");
				}
				if( changeBTNPos )
					positions[6] = value[0];
				if( changeBTNSprite )
					sprites[6] = value[0];
			}
		}

		public Menu(Multris g) {

			string pl = "Menu#()";

			new Debug(pl, "Menu Initialization");

			this.game = g;

			// Compute sprite scales for lower resolutions!

			// Compute CloseWindow for lower resolutions
			new Debug(pl, "Doing graphics calculations");
			int marginCW = 50;
			int widthCW = QuickOperations._IRB(false, CloseWindow[1].Width, 0, g.WIDTH - ( 2 * marginCW ));
			int heightCW = widthCW * CloseWindow[1].Height / CloseWindow[1].Width;
			int xCW = ( g.WIDTH - widthCW ) / 2;
			int yCW = ( g.HEIGHT - heightCW ) / 2;
			int widthCCW = CloseCloseWindow[1].Width * widthCW / CloseWindow[1].Width;
			int heightCCW = CloseCloseWindow[1].Height * heightCW / CloseWindow[1].Height;
			int xCCW = xCW + widthCW - widthCCW;
			int yCCW = yCW;
			int xcCW = xCW + ( ( widthCW - ConfirmCloseButton[1].Width ) / 2 );
			int ycCW = yCW + heightCW - ConfirmCloseButton[1].Height - 50;

			// Compute Logo for lower resolutions
			int widthL = QuickOperations._IRB(true, GameLogo[1].Width, 0, g.WIDTH - 100);
			int heightL = ( QuickOperations._IRB(true, GameLogo[1].Width, 0, g.WIDTH - 100) * GameLogo[1].Height ) / GameLogo[1].Width;
			int xL = ( g.WIDTH - QuickOperations._IRB(true, GameLogo[1].Width, 0, g.WIDTH - 100) ) / 2;
			int logo_bottom = 50 + heightL;


			new Debug(pl, "Changing positions of menu elements." );
			this.SetPositionFor(0, new Rectangle(xL, 50, widthL, heightL));         // Set position for LOGO
			this.SetPositionFor(1, new Rectangle(                                   // Set position for PlayButton
				( g.WIDTH - PlayButton[1].Width ) / 2,                              // Middle of Window
				logo_bottom + 50,                                                   // 50px under LOGO
				PlayButton[1].Width,                                                // Original width
				PlayButton[1].Height                                                // Original height
			));
			this.SetPositionFor(2, new Rectangle(                                   // Set position for OptButton
				( g.WIDTH - OptButton[1].Width ) / 2,                               // Middle of Window
				logo_bottom + 100 + 50 + 20,                                        // 20px under previous button
				OptButton[1].Width,                                                 // Original width
				OptButton[1].Height                                                 // Original height
			));
			this.SetPositionFor(3, new Rectangle(                                   // Set position for ExitButton
				( g.WIDTH - ExitButton[1].Width ) / 2,                              // Middle of Window
				logo_bottom + 50 + 240,                                             // 20px under previous button
				ExitButton[1].Width,                                                // Original width
				ExitButton[1].Height                                                // Original height
			));
			this.SetPositionFor(4, new Rectangle(xCW, yCW, widthCW, heightCW));     // Set position for CloseWindow
			this.SetPositionFor(5, new Rectangle(xCCW, yCCW, widthCCW, heightCCW)); // Set position for CloseCloseWindow
																					// Set position for ConfirmCloseWindow
			this.SetPositionFor(6, new Rectangle(xcCW, ycCW, ConfirmCloseButton[1].Width, ConfirmCloseButton[1].Height));



			// ExitTextRectangle = new Rectangle(xCW + marginCW, yCW + marginCW, widthCW - marginCW * 2, heightCW - marginCW * 2);
		}

		public void Load(ContentManager cm) {
			try {
				new Debug("Menu#Load", "Loading 'MainMenu/mainMenu' texture" );
				texture = cm.Load<Texture2D>("MainMenu/mainMenu");
				new Debug("Menu#Load", "Loaded successfully");
				this.loaded = true;
			} catch( Exception e ) {
				loaded = false;
				new Debug("Menu#Load", "ERR: " + e);
			}
		}

		/* THIS SOLUTION IS CPU HEAVY. USE ONLY WHEN NECESARRY
		public RasterizerState ExitTextRaster = new RasterizerState( ) { ScissorTestEnable = true };
		private Rectangle ExitTextRectangle;
		*/

		public void Draw(SpriteBatch sb) {
			if( loaded ) {
				float opaque = 1.0f;

				if( State == MenuState.EXIT )                                                           // If MenuState.EXIT
					opaque = 0.3f;                                                                      // Change opacity for GL/PB/OB/EB to 0.3

				sb.Draw(this.texture, GameLogo[0], GameLogo[1], colors[0] * opaque);            // Draw GameLogo
				sb.Draw(this.texture, PlayButton[0], PlayButton[1], colors[1] * opaque);        // Draw PlayButton
				sb.Draw(this.texture, OptButton[0], OptButton[1], colors[2] * opaque);          // Draw OptionButton
				sb.Draw(this.texture, ExitButton[0], ExitButton[1], colors[3] * opaque);        // Draw ExitButton

				if( State == MenuState.EXIT ) {

					sb.Draw(this.texture, CloseWindow[0], CloseWindow[1], colors[4]);                   // Draw CloseWindow
					sb.Draw(this.texture, CloseCloseWindow[0], CloseCloseWindow[1], colors[5]);         // Draw Close CloseWindow Button
					sb.Draw(this.texture, ConfirmCloseButton[0], ConfirmCloseButton[1], colors[6]);     // Draw Confirm CloseWindow Button

					SpriteFont fira = this.game.FiraLight24;                            // Save locally font

					String ays = "Are you sure?";                                       // Store String to show
					Vector2 aysPoint =                                                  // Calculate position of string
					new Vector2(CloseWindow[0].X + ( CloseWindow[0].Width - fira.MeasureString(ays).X ) / 2, CloseWindow[0].Y + ( CloseWindow[0].Height - fira.MeasureString(ays).Y ) / 2);
					sb.DrawString(fira, ays, aysPoint, Color.White);                    //	Draw string

					String yes = "Yes";                                                 // Store String to show
					Vector2 yesPoint =                                                  // Calculate position of string
					new Vector2(ConfirmCloseButton[0].X + ( ConfirmCloseButton[0].Width - fira.MeasureString(yes).X ) / 2, ConfirmCloseButton[0].Y + ( ConfirmCloseButton[0].Height - fira.MeasureString(yes).Y ) / 2);
					sb.DrawString(fira, yes, yesPoint, colors[6]);                  // Draw string

					/* THIS IS CPU HEAVY NOT OPTIMAL SOLUTION (Uses second sb.Begin(...))!
					
						sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, ExitTextRaster); 
						Rectangle tempR = sb.GraphicsDevice.ScissorRectangle;		// Save Original ScissorRect
						sb.GraphicsDevice.ScissorRectangle = ExitTextRectangle;		// Set new ScissorRect
						// Draw
						sb.DrawString(this.game.FiraLight24, "Are you sure?", Vector2.Zero, Color.White);
						// Draw
						sb.GraphicsDevice.ScissorRectangle = tempR;					// Reset to Original ScissorRect
						sb.End( );

					*/
				}
			}
		}

		/// <summary>
		/// Checking for any mouse/keyboard/gamepad input.
		/// <para>Animating anything.</para>
		/// </summary>
		public InputState Update(InputState bef) {

			InputState inputs = new InputState( );
			MouseClick mc = inputs.MouseClicked(bef);
			MouseClick mr = inputs.MouseReleased(bef);

			if( State == MenuState.NORMAL ) {

				// If player clicked BACK/ESCAPE
				if( inputs.ButtonUpAny(bef, Buttons.Back) || inputs.KeyUp(bef, Keys.Escape) )
					ExitConfirm( );

				// PlayButton
				if( inputs.MouseRectangle.Intersects(PlayButton[0]) ) {
					// ::hover
					ChangeColorForNew(1, Color.White);
					if( inputs.mouse.LeftButton == ButtonState.Pressed ) {
						ChangeColorForNew(1, Color.White * 0.5f);
					}
					if( mr.button == MouseButton.LEFT ) {
						// Init of select menu
						game.State = Multris.GameState.SELECT;

					}
				} else {
					// ::default
					ChangeColorForNew(1, new Color(200, 200, 200));
				}

				// OptButton
				if( inputs.MouseRectangle.Intersects(OptButton[0]) ) {
					// ::hover
					ChangeColorForNew(2, Color.White);
					if( inputs.mouse.LeftButton == ButtonState.Pressed ) {
						ChangeColorForNew(2, Color.White * 0.5f);
					}
					if( mr.button == MouseButton.LEFT ) {
						// Init of options menu
						game.State = Multris.GameState.OPTIONS;

					}
				} else {
					// ::default
					ChangeColorForNew(2, new Color(200, 200, 200));
				}

				// ExitButton
				if( inputs.MouseRectangle.Intersects(ExitButton[0]) ) {
					// ::hover
					ChangeColorForNew(3, new Color(255, 180, 180));
					if( inputs.mouse.LeftButton == ButtonState.Pressed ) {
						ChangeColorForNew(3, new Color(255, 180, 180) * 0.8f);
					}
					if( mr.button == MouseButton.LEFT ) {
						this.ExitConfirm( );
					}
				} else {
					// ::default
					ChangeColorForNew(3, new Color(200, 200, 200));
				}

			} else {
				// Handle get-out-of-the-game mouse click/press

				// If clicked escape or back: cancel
				if( inputs.KeyUp(bef, Keys.Escape) || inputs.ButtonUpAny(bef, Buttons.Back) ) {
					this.State = Menu.MenuState.NORMAL;
				}

				// If clicked enter or A: exit game
				if( inputs.KeyUp(bef, Keys.Enter) || inputs.ButtonUpAny(bef, Buttons.A) ) {
					game.Terminate( );
				}

				// Close CloseWindow Button
				if( inputs.MouseRectangle.Intersects(CloseCloseWindow[0]) ) {
					// ::hover
					if( mr.button == MouseButton.LEFT ) {
						// MouseReleased
						this.State = Menu.MenuState.NORMAL;
					}
				}

				// Confirm CloseWindow Button
				if( inputs.MouseRectangle.Intersects(ConfirmCloseButton[0]) ) {
					// ::hover
					ChangeColorForNew(6, new Color(255, 180, 180));
					if( inputs.mouse.LeftButton == ButtonState.Pressed ) {
						ChangeColorForNew(6, new Color(255, 180, 180) * 0.8f);
					}
					if( mr.button == MouseButton.LEFT ) {
						game.Terminate( );
					}
				} else {
					// ::default
					ChangeColorForNew(6, new Color(200, 200, 200));
				}


			}
			return inputs;

		}

		~Menu() {
			if( texture != null ) {
				texture.Dispose( );
			}
			texture = null;
		}

	}
}
