using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace MulTris {
	public class Menu {

		private Multris game;

		private enum MenuState {
			NORMAL,
			EXIT
		}

		private MenuState State { set; get; } = MenuState.NORMAL;

		private void ExitConfirm() { this.State = MenuState.EXIT; }

		private Texture2D menuTexture;

		/// <summary>
		/// <para>This Rectangle Array contains source placements of given elements from MainMenu/mainMenu texture.</para>
		/// <para>0 - Game Logo</para>
		/// <para>1 - Play Button</para>
		/// <para>2 - Options Button</para>
		/// <para>3 - Exit Button</para>
		/// <para>4 - Close Window</para>
		/// <para>5 - Close Close Window</para>
		/// </summary>
		private Rectangle[] menuSprites = new Rectangle[6] {
			new Rectangle(0, 100, 900, 180),	// Game Logo
			new Rectangle(0, 0, 240, 100),		// Play Button
			new Rectangle(240, 0, 240, 100),	// Options Button
			new Rectangle(480, 0, 240, 100),	// Exit Button
			new Rectangle(0, 280, 800, 600),	// Close Window
			new Rectangle(720, 0, 40, 40)		// Close Close Window
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
		private Rectangle[] menuPositions = new Rectangle[6] {
			new Rectangle(0, 0, 0, 0),		// Game Logo
			new Rectangle(0, 0, 0, 0),		// Play Button
			new Rectangle(0, 0, 0, 0),		// Options Button
			new Rectangle(0, 0, 0, 0),		// Exit Button
			new Rectangle(0, 0, 0, 0),		// Close Window
			new Rectangle(0, 0, 0, 0)		// Close Close Window
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
		private Color[] menuColors = new Color[6]{
			Color.White,				// Game Logo
			new Color(200,200,200),		// Play Button
			new Color(200,200,200),		// Option Button
			new Color(200,200,200),		// Exit Button
			Color.White,				// Close Window
			Color.White					// Close Close Window
		};

		private bool loaded = false;

		public void ChangeColorFor(uint b, Nullable<Color> c) {
			if( b > menuColors.Length - 1 )
				return;
			Color sc = c ?? menuColors[b];
			menuColors[b] = sc;
		}

		public void SetSpritePosFor(uint b, Nullable<Rectangle> r) {
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
				case 4:
					CloseWindow = new Rectangle[2] { sr, sr };
					break;
				case 5:
					CloseCloseWindow = new Rectangle[2] { sr, sr };
					break;
			}
			changeBTNSprite = false;
		}

		public void SetPositionFor(uint b, Nullable<Rectangle> r) {
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
				case 4:
					CloseWindow = new Rectangle[2] { sr, sr };
					break;
				case 5:
					CloseCloseWindow = new Rectangle[2] { sr, sr };
					break;
			}
			changeBTNPos = false;
		}

		/// <summary>
		/// A Menu Logo Parameter.
		/// It is an Array containing two Rectangles:
		/// <para>0 - buttonPosition</para>
		/// <para>1 - buttonSprite</para>
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 0 as first parameter.
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
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 1 as first parameter.
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
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 2 as first parameter.
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
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 3 as first parameter.
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

		/// <summary>
		/// An Exit Button Parameter.
		/// It is an Array containing two Rectangles:
		/// <para>0 - buttonPosition</para>
		/// <para>1 - buttonSprite</para>
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 3 as first parameter.
		/// </summary>
		public Rectangle[] CloseWindow {
			get {
				return new Rectangle[2] { menuPositions[4], menuSprites[4] };
			}
			set {
				if( changeBTNPos && changeBTNSprite ) {
					changeBTNPos = false;
					changeBTNSprite = false;
					throw new InvalidOperationException("You should not be able to do that operation!");
				}
				if( changeBTNPos )
					menuPositions[4] = value[0];
				if( changeBTNSprite )
					menuSprites[4] = value[0];
			}
		}

		/// <summary>
		/// An Close CloseWindow Parameter.
		/// It is an Array containing two Rectangles:
		/// <para>0 - buttonPosition</para>
		/// <para>1 - buttonSprite</para>
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 3 as first parameter.
		/// </summary>
		public Rectangle[] CloseCloseWindow {
			get {
				return new Rectangle[2] { menuPositions[5], menuSprites[5] };
			}
			set {
				if( changeBTNPos && changeBTNSprite ) {
					changeBTNPos = false;
					changeBTNSprite = false;
					throw new InvalidOperationException("You should not be able to do that operation!");
				}
				if( changeBTNPos )
					menuPositions[5] = value[0];
				if( changeBTNSprite )
					menuSprites[5] = value[0];
			}
		}

		public Menu(Multris g) {

			this.game = g;

			// Compute sprite scales for lower resolutions!

			// Compute CloseWindow for lower resolutions
			int marginCW = 50;
			int widthCW = QuickMaths._IRB(false, CloseWindow[1].Width, 0, g.WIDTH - ( 2 * marginCW ));
			int heightCW = widthCW * CloseWindow[1].Height / CloseWindow[1].Width;
			int xCW = ( g.WIDTH - widthCW ) / 2;
			int yCW = ( g.HEIGHT - heightCW ) / 2;
			int widthCCW = CloseCloseWindow[1].Width * widthCW / CloseWindow[1].Width;
			int heightCCW = CloseCloseWindow[1].Height * heightCW / CloseWindow[1].Height;
			int xCCW = xCW + widthCW - widthCCW;
			int yCCW = yCW;

			// Compute Logo for lower resolutions
			int widthL = QuickMaths._IRB(true, GameLogo[1].Width, 0, g.WIDTH - 100);
			int heightL = ( QuickMaths._IRB(true, GameLogo[1].Width, 0, g.WIDTH - 100) * GameLogo[1].Height ) / GameLogo[1].Width;
			int xL = ( g.WIDTH - QuickMaths._IRB(true, GameLogo[1].Width, 0, g.WIDTH - 100) ) / 2;
			int logo_bottom = 50 + heightL;

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
			this.SetPositionFor(5, new Rectangle(xCCW, yCCW, widthCCW, heightCCW)); //Set position for CloseCloseWindow

			// ExitTextRectangle = new Rectangle(xCW + marginCW, yCW + marginCW, widthCW - marginCW * 2, heightCW - marginCW * 2);
		}

		public void Load(ContentManager cm) {
			menuTexture = cm.Load<Texture2D>("MainMenu/mainMenu");
			loaded = true;
		}

		/* THIS SOLUTION IS CPU HEAVY. USE ONLY WHEN NECESARRY
		public RasterizerState ExitTextRaster = new RasterizerState( ) { ScissorTestEnable = true };
		private Rectangle ExitTextRectangle;
		*/

		public void Draw(SpriteBatch sb) {
			if( loaded ) {
				if( State == MenuState.NORMAL ) {

					sb.Draw(this.menuTexture, GameLogo[0], GameLogo[1], menuColors[0]);
					sb.Draw(this.menuTexture, PlayButton[0], PlayButton[1], menuColors[1]);
					sb.Draw(this.menuTexture, OptButton[0], OptButton[1], menuColors[2]);
					sb.Draw(this.menuTexture, ExitButton[0], ExitButton[1], menuColors[3]);

					// TODO: Animate
					// TODO: Particles
					// TODO: Background

				} else {
					float opaque = 0.3f;
					sb.Draw(this.menuTexture, GameLogo[0], GameLogo[1], menuColors[0] * opaque);
					sb.Draw(this.menuTexture, PlayButton[0], PlayButton[1], menuColors[1] * opaque);
					sb.Draw(this.menuTexture, OptButton[0], OptButton[1], menuColors[2] * opaque);
					sb.Draw(this.menuTexture, ExitButton[0], ExitButton[1], menuColors[3] * opaque);
					sb.Draw(this.menuTexture, CloseWindow[0], CloseWindow[1], menuColors[4]);
					sb.Draw(this.menuTexture, CloseCloseWindow[0], CloseCloseWindow[1], menuColors[5]);

					SpriteFont fira = this.game.FiraLight24;
					String s = "Are you sure?";
					Vector2 sWH = fira.MeasureString(s);

					sb.DrawString(this.game.FiraLight24, s, new Vector2(
						CloseWindow[0].X + ( CloseWindow[0].Width - sWH.X ) / 2,
						CloseWindow[0].Y + ( CloseWindow[0].Height - sWH.Y ) / 2
					), Color.White);

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

				if( inputs.gamePad1.Buttons.Back == ButtonState.Pressed || inputs.StateChangeDown(bef, Keys.Escape) )
					ExitConfirm( );

				// PlayButton
				if( inputs.MouseRectangle.Intersects(PlayButton[0]) ) {
					// ::hover
					ChangeColorFor(1, Color.White);
					if( inputs.mouse.LeftButton == ButtonState.Pressed ) {
						ChangeColorFor(1, Color.White * 0.5f);
					}
					if( mr.button == MouseButton.LEFT ) {
						// Init of select menu
						game.State = Multris.GameState.SELECT;

					}
				} else {
					// ::default
					ChangeColorFor(1, new Color(200, 200, 200));
				}

				// OptButton
				if( inputs.MouseRectangle.Intersects(OptButton[0]) ) {
					// ::hover
					ChangeColorFor(2, Color.White);
					if( inputs.mouse.LeftButton == ButtonState.Pressed ) {
						ChangeColorFor(2, Color.White * 0.5f);
					}
					if( mr.button == MouseButton.LEFT ) {
						// Init of options menu
						game.State = Multris.GameState.OPTIONS;

					}
				} else {
					// ::default
					ChangeColorFor(2, new Color(200, 200, 200));
				}

				// ExitButton
				if( inputs.MouseRectangle.Intersects(ExitButton[0]) ) {
					// ::hover
					ChangeColorFor(3, new Color(255, 180, 180));
					if( inputs.mouse.LeftButton == ButtonState.Pressed ) {
						ChangeColorFor(3, new Color(255, 180, 180) * 0.99f);
					}
					if( mr.button == MouseButton.LEFT ) {
						this.ExitConfirm( );
					}
				} else {
					// ::default
					ChangeColorFor(3, new Color(200, 200, 200));
				}

			} else {
				// Handle get-out-of-the-game mouse click/press

			}
			return inputs;

		}

		~Menu() {
			if( menuTexture != null ) {
				menuTexture.Dispose( );
			}
			menuTexture = null;
		}

	}
}
