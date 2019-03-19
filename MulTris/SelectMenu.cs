using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace MulTris {

	public class SelectMenu {

		private Multris game;


		public enum OPTPLValues {
			Width = 0,
			Height = 1,
			Play = 6,
			B3 = 2,
			B4 = 3,
			B5 = 4,
			B6 = 5
		}

		public enum SelectState {
			TYPE,
			OPTIONS
		}

		public GameOption<bool>[] blockOptions;
		public GameOption<Point> borderSizeOption;

		public SelectState State = SelectState.TYPE;

		public void GoBack() {
			new Debug("SelectMenu#GoBack", "Trying to go back one state ( " + this.State + ")");
			if( this.State == SelectState.OPTIONS ) {

				this.State = SelectState.TYPE;
				MovePointerTo(0);
				new Debug("SelectMenu#GoBack", "Went back to TYPE");

			} else {
				this.game.State = Multris.GameState.MENU;
				new Debug("SelectMenu#GoBack", "Went back to MENU");
			}
		}

		private Texture2D texture;

		private Sprite[] sprites = new Sprite[1] {
			new Sprite()		// Pointer
		};

		/// <summary>
		/// <para>This Color Array contains all Colors of buttons on-screen. What Color will they be tined in <see cref="SpriteBatch.Draw(Texture2D, Rectangle, Rectangle?, Color)"/></para>
		/// <para>0 - Pointer</para>
		/// </summary>
		private Color[] colors = new Color[1]{
			Color.White				// Pointer
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
			if( b > sprites.Length - 1 )
				return;
			Rectangle sr = r ?? sprites[b].Source;
			switch( b ) {
				case 0:
					Pointer.AllowCP(true);
					Pointer.Change(sr);
					Pointer.DisAllow( );
					break;
			}
		}

		public void SetSpritePosFor(uint b, Point? p) {
			if( b > sprites.Length - 1 )
				return;
			Point sp = p ?? sprites[b].Position.Location;
			switch( b ) {
				case 0:
					Pointer.AllowCP(true);
					Pointer.Change(sp);
					Pointer.DisAllow( );
					break;
			}
		}

		public void SetPositionFor(uint b, Rectangle? r) {
			new Debug("SelectMenu#SetPositionFor", "Trying to set " + b + "'s position to " + r + ".");
			if( b > sprites.Length - 1 )
				return;
			Rectangle sp = r ?? sprites[b].Position;
			switch( b ) {
				case 0:
					Pointer.AllowCP(false);
					Pointer.Change(sp);
					Pointer.DisAllow( );
					break;
			}
			new Debug("SelectMenu#SetPositionFor", "Successfully changed " + b + "'s position!");
		}

		public void DefaultPointerPositionSpriteSize() {
			Pointer.AllowCP(false);
			Pointer.Change(new Rectangle(Pointer.Position.Location, Pointer.Source.Size));
			Pointer.DisAllow( );
		}

		public void SetPositionFor(uint b, Point? p) {
			if( b > sprites.Length - 1 )
				return;
			Point sp = p ?? sprites[b].Position.Location;
			switch( b ) {
				case 0:
					Pointer.AllowCP(false);
					Pointer.Change(sp);
					Pointer.DisAllow( );
					break;
			}
			new Debug("SelectMenu#SetPositionFor", "Successfully changed " + b + "'s position!");
		}

		/// <summary>
		/// A Slect Menu Pointer Parameter.
		/// It is an Array containing two Rectangles:
		/// <para>0 - position on screen</para>
		/// <para>1 - position on sprite</para>
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 0 as first parameter.
		/// </summary>
		public Sprite Pointer {
			get {
				return sprites[0];
			}
		}

		public Rectangle OfflineLabel;
		public Rectangle PlayLabel;

		private void SetPointerLocations(Point[] tl, Point[] ol) {
			this.PointerOptionLocations = ol;
			this.PointerTypeLocations = tl;
		}

		private Point[] PointerOptionLocations;
		private Point[] PointerTypeLocations;

		private byte PointerLocation = 0;

		public void MovePointer(sbyte by) {
			if( this.State == SelectState.TYPE ) {
				switch( PointerLocation ) {
					case 0:                     // Offline
					case 1:                     // Online
						PointerLocation = (byte) ( PointerLocation + by );
						if( PointerLocation > PointerTypeLocations.Length - 1 )
							PointerLocation = 0;
						break;
					case 2:                     // Back button
						PointerLocation = 0;
						break;
				}
				this.SetPositionFor(0, PointerTypeLocations[PointerLocation]);
			} else if( this.State == SelectState.OPTIONS ) {
				switch( PointerLocation ) {
					case 0:                     // Width
					case 1:                     // Height
					case 2:                     // T3 blocka
					case 3:                     // T4 blocks
					case 4:                     // T5 blocks
					case 5:                     // T6 blocks
						PointerLocation = (byte) ( PointerLocation + by );
						if( PointerLocation > PointerOptionLocations.Length - 1 )
							PointerLocation = 0;
						break;
					case 6:                     // Play Button
						PointerLocation = 0;    // Go to first element
						break;
				}
				this.SetPositionFor(0, PointerOptionLocations[PointerLocation]);
			}
		}

		public void MovePointerTo(byte position) {
			new Debug("SelectMenu#MovePointerTo", "Moving Pointer to position " + position + " (" + this.State + ")");
			PointerLocation = position;
			if( this.State == SelectState.TYPE )
				this.SetPositionFor(0, PointerTypeLocations[PointerLocation]);
			else if( this.State == SelectState.OPTIONS ) {
				this.SetPositionFor(0, PointerOptionLocations[PointerLocation]);
			}
		}

		public SelectMenu(Multris g) {
			string pl = "SelectMenu#()";

			new Debug(pl, "SelectMenu Initializing");
			this.game = g;

			new Debug(pl, "Setting all static Pointer positions");
			this.SetPointerLocations(
				new Point[3] {
					// Offline
					new Point(150,150),
					new Point(150, 300),
					new Point(350, 600)
				},
				new Point[7] {
					Point.Zero,		// Board Width
					Point.Zero,		// Board Height
					Point.Zero,		// B3
					Point.Zero,		// B4
					Point.Zero,		// B5
					Point.Zero,		// B6
					// Play
					Point.Zero // Changed in Load()
				}
			);

			sprites[0] = new Sprite( );
			this.MovePointerTo(0);

			new Debug(pl, "Initializing GameOptions.");
			// Option init
			this.borderSizeOption = new GameOption<Point>(new Point(10, 24), GameOptionType.BS, "Board Size", new Point(10, 24));
			this.blockOptions = new GameOption<bool>[4] {
				new GameOption<bool>(false, GameOptionType.B3, "Use 3minos", false),
				new GameOption<bool>(true, GameOptionType.B4, "Use tetrominos", true),
				new GameOption<bool>(false, GameOptionType.B5, "Use 5minos", false),
				new GameOption<bool>(false, GameOptionType.B6, "Use 6minos", false)
			};

		}

		public void Load(ContentManager cm) {
			string pl = "SelectMenu#Load";
			try {
				new Debug(pl, "Loading SelectMenu");
				sprites[0].Load(cm.Load<Texture2D>("MainMenu/selectMenu"), null);

				new Debug(pl, "Loaded Sprite: " + Pointer.ToString());
				DefaultPointerPositionSpriteSize( );
				new Debug(pl, "Calculating texture-based sizes and positions.");

				this.OfflineLabel = new Rectangle(PointerTypeLocations[0].X, PointerTypeLocations[0].Y, Pointer.Source.Width + ( (int) this.game.FiraLight20.MeasureString("Offline").X ) + 50, Pointer.Source.Height);

				// Change Play location
				this.PointerOptionLocations[(int) OPTPLValues.Play] = new Point((int) ( ( this.game.WIDTH / 2 ) - ( this.game.FiraLight20.MeasureString("Play").X * 4 ) ), this.game.HEIGHT - 100);
				this.PlayLabel = new Rectangle(PointerOptionLocations[(int) OPTPLValues.Play].X, PointerOptionLocations[(int) OPTPLValues.Play].Y, Pointer.Source.Width + 50 + ( (int) this.game.FiraLight20.MeasureString("Play").X ), Pointer.Source.Height);

				new Debug(pl, "Loading complete!");

				loaded = true;
			} catch( Exception e ) {
				loaded = false;
				new Debug(pl, "ERR: " + e);
			}
		}

		public void Draw(SpriteBatch sb) {
			if( loaded ) {
				if( this.State == SelectState.TYPE ) {

					sb.Draw(Pointer.Texture, Pointer.Position, Pointer.Source, colors[0]);

					switch( this.PointerLocation ) {
						case 0:
							/// Offline
							sb.DrawString(
								this.game.FiraLight20,
								"Offline",
								new Vector2(
									PointerTypeLocations[0].X + Pointer.Source.Width + 50,
									PointerTypeLocations[0].Y + ( Pointer.Source.Height / 2 - this.game.FiraLight20.MeasureString("Offline").Y / 2 )
								),
								Color.White
							);
							break;
					}


				} else if( this.State == SelectState.OPTIONS ) {
					// Draw options
					sb.Draw(Pointer.Texture, Pointer.Position, Pointer.Source, colors[0]);

					sb.DrawString(
						this.game.FiraLight20,
						"Play",
						new Vector2(
							PointerOptionLocations[(int) OPTPLValues.Play].X + Pointer.Position.Width + 50,
							PointerOptionLocations[(int) OPTPLValues.Play].Y + ( Pointer.Position.Height / 2 - this.game.FiraLight20.MeasureString("Play").Y / 2 )
						),
						Color.White
					);
				}
			}
		}

		public void ShowOptions() {

			new Debug("SelectMenu#ShowOptions", "Showing GameOptions.");

			this.State = SelectState.OPTIONS;
			MovePointerTo((int) OPTPLValues.Play);

			new Debug("SelectMenu#ShowOptions", "State changed to OPTIONS");

		}

		/// <summary>
		/// Checking for any mouse/keyboard/gamepad input.
		/// <para>Animating anything.</para>
		/// </summary>
		public InputState Update(InputState bef) {

			InputState inputs = new InputState( );
			MouseClick mc = inputs.MouseClicked(bef);
			MouseClick mr = inputs.MouseReleased(bef);

			if( this.State == SelectState.TYPE ) {

				if( inputs.ButtonUpAny(bef, Buttons.Back) || inputs.KeyUp(bef, Keys.Escape) )
					this.GoBack( );

				if( PointerLocation == 0 ) {
					if( inputs.ButtonDownAny(bef, Buttons.A) || // If pushed A
						inputs.KeyUp(bef, Keys.Enter) ||        // If clicked Enter
						( OfflineLabel.Intersects(inputs.MouseRectangle) && mr.button == MouseButton.LEFT ) ) { // If clicked LMB over Offline
						this.ShowOptions( );
					}
				}

			} else if( this.State == SelectState.OPTIONS ) {
				// Handle selecting options for on/offline mode
				if( inputs.ButtonUpAny(bef, Buttons.Back) || inputs.KeyUp(bef, Keys.Escape) )
					this.GoBack( );

				if( this.PointerLocation == (byte) OPTPLValues.Play ) {
					if( inputs.ButtonUpAny(bef, Buttons.A) || inputs.KeyUp(bef, Keys.Enter) ||
						( PlayLabel.Intersects(inputs.MouseRectangle) && mr.button == MouseButton.LEFT )
					) {
						this.game.InitializeGame(
						borderSizeOption,
						new GameOption<bool>[4] {
							blockOptions[0],
							blockOptions[1],
							blockOptions[2],
							blockOptions[3]
							}
						);
					}
				}

			}

			return inputs;

		}

		~SelectMenu() {
			if( texture != null ) {
				texture.Dispose( );
			}
			texture = null;
		}

	}
}
