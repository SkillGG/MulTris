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

		private void ExitConfirm() { new Debug("Menu#ExitConfirm", "State of menu changed to EXIT. (Showing exit confiramtion window)", Debug.Importance.NOTIFICATION); this.State = MenuState.EXIT; }

		private Sprite[] sprites = new Sprite[7] {
			new Sprite(),
			new Sprite(),
			new Sprite(),
			new Sprite(),
			new Sprite(),
			new Sprite(),
			new Sprite()
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

		public void SetSpritePosFor(uint b, Rectangle? r) {
			bool sp = true;
			if( b > sprites.Length - 1 )
				return;
			Rectangle sr = r ?? sprites[b].Source;
			switch( b ) {
				case 0: // GameLogo
					GameLogo.AllowCP(sp);
					GameLogo.Change(sr);
					GameLogo.DisAllow( );
					break;
				case 1: // PlayButton
					PlayButton.AllowCP(sp);
					PlayButton.Change(sr);
					PlayButton.DisAllow( );
					break;
				case 2: // OptButton
					OptButton.AllowCP(sp);
					OptButton.Change(sr);
					OptButton.DisAllow( );
					break;
				case 3: // ExitButton
					ExitButton.AllowCP(sp);
					ExitButton.Change(sr);
					ExitButton.DisAllow( );
					break;
				case 4: // CloseWindow
					CloseWindow.AllowCP(sp);
					CloseWindow.Change(sr);
					CloseWindow.DisAllow( );
					break;
				case 5: // CloseCloseWindow
					CloseCloseWindow.AllowCP(sp);
					CloseCloseWindow.Change(sr);
					CloseCloseWindow.DisAllow( );
					break;
				case 6: // ConfirmCloseButton
					ConfirmCloseButton.AllowCP(sp);
					ConfirmCloseButton.Change(sr);
					ConfirmCloseButton.DisAllow( );
					break;
			}
		}

		public void SetPositionFor(uint b, Rectangle? r) {
			new Debug("Menu#SetPositionFor", "Trying to set " + b + "'s position to " + r + ".", Debug.Importance.NOTIFICATION);
			bool sp = false;
			if( b > sprites.Length - 1 )
				return;
			Rectangle sr = r ?? sprites[b].Position;
			switch( b ) {
				case 0: // GameLogo
					GameLogo.AllowCP(sp);
					GameLogo.Change(sr);
					GameLogo.DisAllow( );
					break;
				case 1: // PlayButton
					PlayButton.AllowCP(sp);
					PlayButton.Change(sr);
					PlayButton.DisAllow( );
					break;
				case 2: // OptButton
					OptButton.AllowCP(sp);
					OptButton.Change(sr);
					OptButton.DisAllow( );
					break;
				case 3: // ExitButton
					ExitButton.AllowCP(sp);
					ExitButton.Change(sr);
					ExitButton.DisAllow( );
					break;
				case 4: // CloseWindow
					CloseWindow.AllowCP(sp);
					CloseWindow.Change(sr);
					CloseWindow.DisAllow( );
					break;
				case 5: // CloseCloseWindow
					CloseCloseWindow.AllowCP(sp);
					CloseCloseWindow.Change(sr);
					CloseCloseWindow.DisAllow( );
					break;
				case 6: // ConfirmCloseButton
					ConfirmCloseButton.AllowCP(sp);
					ConfirmCloseButton.Change(sr);
					ConfirmCloseButton.DisAllow( );
					break;
			}
			new Debug("Menu#SetPositionFor", "Successfully changed " + b + "'s position!", Debug.Importance.NOTIFICATION);
		}

		/// <summary>
		/// A Menu Logo Parameter.
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 0 as first parameter.
		/// </summary>
		public Sprite GameLogo {
			get {
				return sprites[0];
			}
		}

		/// <summary>
		/// A Play Button Parameter.
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 1 as first parameter.
		/// </summary>
		public Sprite PlayButton {
			get {
				return sprites[1];
			}
		}

		/// <summary>
		/// An Option Button Parameter.
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 2 as first parameter.
		/// </summary>
		public Sprite OptButton {
			get {
				return sprites[2];
			}
		}

		/// <summary>
		/// An Exit Button Parameter.
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 3 as first parameter.
		/// </summary>
		public Sprite ExitButton {
			get {
				return sprites[3];
			}
		}

		/// <summary>
		/// An Exit Button Parameter.
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 3 as first parameter.
		/// </summary>
		public Sprite CloseWindow {
			get {
				return sprites[4];
			}
		}

		/// <summary>
		/// An Close CloseWindow Parameter.
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 3 as first parameter.
		/// </summary>
		public Sprite CloseCloseWindow {
			get {
				return sprites[5];
			}
		}

		/// <summary>
		/// An Confirm CloseWindow Parameter.
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 3 as first parameter.
		/// </summary>
		public Sprite ConfirmCloseButton {
			get {
				return sprites[6];
			}
		}

		public Menu(Multris g) {

			string pl = "Menu#()";

			new Debug(pl, "Menu Initialization", Debug.Importance.INIT_INFO);

			this.game = g;

			// ExitTextRectangle = new Rectangle(xCW + marginCW, yCW + marginCW, widthCW - marginCW * 2, heightCW - marginCW * 2);
		}

		public void Load(ContentManager cm) {
			try {
				string pl = "Menu#Load";
				new Debug(pl, "Loading 'MainMenu/mainMenu' texture", Debug.Importance.INIT_INFO);
				Texture2D tx = cm.Load<Texture2D>("MainMenu/mainMenu");
				sprites[0].Load(tx, new Rectangle(0, 100, 900, 180));   // Game Logo
				sprites[1].Load(tx, new Rectangle(0, 0, 240, 100));     // Play Button
				sprites[2].Load(tx, new Rectangle(240, 0, 240, 100));   // Options Button
				sprites[3].Load(tx, new Rectangle(480, 0, 240, 100));   // Exit Button
				sprites[4].Load(tx, new Rectangle(0, 280, 800, 600));   // Close Window
				sprites[5].Load(tx, new Rectangle(720, 0, 40, 40));     // Close Close Window
				sprites[6].Load(tx, new Rectangle(0, 880, 240, 100));   // Confirm Close Button

				// Compute sprite scales for lower resolutions!
				// Compute CloseWindow for lower resolutions
				new Debug(pl, "Doing graphics calculations", Debug.Importance.NOTIFICATION);
				int marginCW = 50;
				int widthCW = QuickOperations._IRB(false, CloseWindow.Source.Width, 0, game.WIDTH - ( 2 * marginCW ));
				int heightCW = widthCW * CloseWindow.Source.Height / CloseWindow.Source.Width;
				int xCW = ( game.WIDTH - widthCW ) / 2;
				int yCW = ( game.HEIGHT - heightCW ) / 2;
				int widthCCW = CloseCloseWindow.Source.Width * widthCW / CloseWindow.Source.Width;
				int heightCCW = CloseCloseWindow.Source.Height * heightCW / CloseWindow.Source.Height;
				int xCCW = xCW + widthCW - widthCCW;
				int yCCW = yCW;
				int xcCW = xCW + ( ( widthCW - ConfirmCloseButton.Source.Width ) / 2 );
				int ycCW = yCW + heightCW - ConfirmCloseButton.Source.Height - 50;

				// Compute Logo for lower resolutions
				int widthL = QuickOperations._IRB(true, GameLogo.Source.Width, 0, game.WIDTH - 100);
				int heightL = ( QuickOperations._IRB(true, GameLogo.Source.Width, 0, game.WIDTH - 100) * GameLogo.Source.Height ) / GameLogo.Source.Width;
				int xL = ( game.WIDTH - QuickOperations._IRB(true, GameLogo.Source.Width, 0, game.WIDTH - 100) ) / 2;
				int logo_bottom = 50 + heightL;


				new Debug(pl, "Changing positions of menu elements.", Debug.Importance.INIT_INFO);
				this.SetPositionFor(0, new Rectangle(xL, 50, widthL, heightL));                 // Set position for LOGO
				this.SetPositionFor(1, new Rectangle(                                           // Set position for PlayButton
					( game.WIDTH - PlayButton.Source.Width ) / 2,                               // Middle of Window
					logo_bottom + 50,                                                           // 50px under LOGO
					PlayButton.Source.Width,                                                    // Original width
					PlayButton.Source.Height                                                    // Original height
				));
				this.SetPositionFor(2, new Rectangle(                                           // Set position for OptButton
					( game.WIDTH - OptButton.Source.Width ) / 2,                                // Middle of Window
					logo_bottom + 100 + 50 + 20,                                                // 20px under previous button
					OptButton.Source.Width,                                                     // Original width
					OptButton.Source.Height                                                     // Original height
				));
				this.SetPositionFor(3, new Rectangle(                                           // Set position for ExitButton
					( game.WIDTH - ExitButton.Source.Width ) / 2,                               // Middle of Window
					logo_bottom + 50 + 240,                                                     // 20px under previous button
					ExitButton.Source.Width,                                                    // Original width
					ExitButton.Source.Height                                                    // Original height
				));
				this.SetPositionFor(4, new Rectangle(xCW, yCW, widthCW, heightCW));     // Set position for CloseWindow
				this.SetPositionFor(5, new Rectangle(xCCW, yCCW, widthCCW, heightCCW)); // Set position for CloseCloseWindow
																						// Set position for ConfirmCloseWindow
				this.SetPositionFor(6, new Rectangle(xcCW, ycCW, ConfirmCloseButton.Source.Width, ConfirmCloseButton.Source.Height));

				new Debug("Menu#Load", "Loaded successfully", Debug.Importance.NOTIFICATION);
				this.loaded = true;
			} catch( Exception e ) {
				loaded = false;
				new Debug("Menu#Load", "ERR: " + e, Debug.Importance.ERROR);
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

				sb.Draw(GameLogo.Texture, GameLogo.Position, GameLogo.Source, colors[0] * opaque);            // Draw GameLogo
				sb.Draw(PlayButton.Texture, PlayButton.Position, PlayButton.Source, colors[1] * opaque);        // Draw PlayButton
				sb.Draw(OptButton.Texture, OptButton.Position, OptButton.Source, colors[2] * opaque);          // Draw OptionButton
				sb.Draw(ExitButton.Texture, ExitButton.Position, ExitButton.Source, colors[3] * opaque);        // Draw ExitButton

				if( State == MenuState.EXIT ) {

					sb.Draw(CloseWindow.Texture, CloseWindow.Position, CloseWindow.Source, colors[4]);                   // Draw CloseWindow
					sb.Draw(CloseCloseWindow.Texture, CloseCloseWindow.Position, CloseCloseWindow.Source, colors[5]);         // Draw Close CloseWindow Button
					sb.Draw(ConfirmCloseButton.Texture, ConfirmCloseButton.Position, ConfirmCloseButton.Source, colors[6]);     // Draw Confirm CloseWindow Button

					SpriteFont fira = this.game.FiraLight24;                            // Save locally font

					String ays = "Are you sure?";                                       // Store String to show
					Vector2 aysPoint =                                                  // Calculate position of string
					new Vector2(CloseWindow.Position.X + ( CloseWindow.Position.Width - fira.MeasureString(ays).X ) / 2, CloseWindow.Position.Y + ( CloseWindow.Position.Height - fira.MeasureString(ays).Y ) / 2);
					sb.DrawString(fira, ays, aysPoint, Color.White);                    //	Draw string

					String yes = "Yes";                                                  // Store String to show
					Vector2 yesPoint =                                                  // Calculate position of string
					new Vector2(ConfirmCloseButton.Position.X + ( ConfirmCloseButton.Position.Width - fira.MeasureString(yes).X ) / 2, ConfirmCloseButton.Position.Y + ( ConfirmCloseButton.Position.Height - fira.MeasureString(yes).Y ) / 2);
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
				if( inputs.MouseRectangle.Intersects(PlayButton.Position) ) {
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
				if( inputs.MouseRectangle.Intersects(OptButton.Position) ) {
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
				if( inputs.MouseRectangle.Intersects(ExitButton.Position) ) {
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
				if( inputs.MouseRectangle.Intersects(CloseCloseWindow.Position) ) {
					// ::hover
					if( mr.button == MouseButton.LEFT ) {
						// MouseReleased
						this.State = Menu.MenuState.NORMAL;
					}
				}

				// Confirm CloseWindow Button
				if( inputs.MouseRectangle.Intersects(ConfirmCloseButton.Position) ) {
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

	}
}
