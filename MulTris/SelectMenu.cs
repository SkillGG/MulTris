using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace MulTris {
	public class GameOption<T> where T : struct {
		private T v;
		private string n;
		private T? d;
		public string Name { get => this.n; }
		public T Value { get => this.v; }
		public GameOption(T o, string name, T? def) {
			this.n = name;
			this.v = o;
			this.d = def;
		}
		public void ChangeValue(T o) {
			this.v = o;
		}
		public void ChangeToDefault() {
			this.v = this.d ?? this.v;
		}
	}

	public class GameOption {
		private string v;
		private string n;
		private string d;
		public string Name { get => this.n; }
		public string Value { get => this.v; }
		public GameOption(string o, string name, string def) {
			this.n = name;
			this.v = o;
			this.d = def;
		}
		public void ChangeValue(string o) {
			this.v = o;
		}
		public void ChangeToDefault() {
			this.v = this.d ?? this.v;
		}
	}

	public class SelectMenu {

		private Multris game;


		public enum SelectState {
			TYPE,
			OPTIONS
		}

		public GameOption<bool>[] boolOptions;
		public GameOption<string>[] stringOptions;
		public GameOption<uint>[] uintOptions;
		public GameOption<int>[] intOptions;

		public SelectState State = SelectState.TYPE;

		public void GoBack() {
			if( this.State == SelectState.OPTIONS ) {

				this.State = SelectState.TYPE;
				MovePointerTo(0);

			} else
				this.game.State = Multris.GameState.MENU;
		}

		private Texture2D texture;

		/// <summary>
		/// <para>This Rectangle Array contains source placements of given elements from MainMenu/mainMenu texture.</para>
		/// <para>0 - Pointer</para>
		/// </summary>
		private Rectangle[] sprites = new Rectangle[1] {
			new Rectangle(0, 0, 60, 90)		// Pointer
		};

		private bool changeBTNPos = false;
		private bool changeBTNSprite = false;

		/// <summary>
		/// <para>This Rectangle Array contains all placements of buttons on-screen. Where they are drawn</para>
		/// <para>0 - Pointer</para>
		/// </summary>
		private Rectangle[] positions = new Rectangle[1] {
			new Rectangle(0, 0, 0, 0)		// Pointer
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

		public void SetSpritePosFor(uint b, Nullable<Rectangle> r) {
			changeBTNSprite = true;
			if( b > sprites.Length - 1 )
				return;
			Rectangle sr = r ?? sprites[b];
			switch( b ) {
				case 0:
					Pointer = new Rectangle[2] { sr, sr };
					break;
			}
			changeBTNSprite = false;
		}

		public void SetSpritePosFor(uint b, Point? p) {
			changeBTNSprite = true;
			if( b > positions.Length - 1 )
				return;
			Point sp = p ?? positions[b].Location;
			switch( b ) {
				case 0:
					Pointer = new Rectangle[2] { new Rectangle(sp, Pointer[0].Size), new Rectangle(sp, Pointer[0].Size) };
					break;
			}
			changeBTNSprite = false;
		}

		public void SetPositionFor(uint b, Nullable<Rectangle> r) {
			changeBTNPos = true;
			if( b > positions.Length - 1 )
				return;
			Rectangle sr = r ?? positions[b];
			switch( b ) {
				case 0:
					Pointer = new Rectangle[2] { sr, sr };
					break;
			}
			changeBTNPos = false;
		}

		public void DefaultPointerPositionSpriteSize() {
			changeBTNPos = true;
			Pointer = new Rectangle[2] { new Rectangle(Pointer[0].Location, Pointer[1].Size), new Rectangle( ) };
			changeBTNPos = false;
		}

		public void SetPositionFor(uint b, Point? p) {
			changeBTNPos = true;
			if( b > positions.Length - 1 )
				return;
			Point sp = p ?? positions[b].Location;
			Console.WriteLine(sp + ":Pointer[0]:" + p);
			switch( b ) {
				case 0:
					Pointer = new Rectangle[2] { new Rectangle(sp, Pointer[0].Size), new Rectangle(sp, Pointer[0].Size) };
					break;
			}
			changeBTNPos = false;
		}

		/// <summary>
		/// A Slect Menu Pointer Parameter.
		/// It is an Array containing two Rectangles:
		/// <para>0 - position on screen</para>
		/// <para>1 - position on sprite</para>
		/// To change it's position or sprite you have to use <see cref="Menu.SetSpritePosFor(int,Rectangle)"/>/<see cref="Menu.SetPositionFor(int,Rectangle)"/> with 0 as first parameter.
		/// </summary>
		public Rectangle[] Pointer {
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

		public Rectangle OfflineLabel;

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
					case 0:                     // T3 blocka
					case 1:                     // T4 blocks
					case 2:                     // T5 blocks
					case 3:                     // T6 blocks
						PointerLocation = (byte) ( PointerLocation + by );
						if( PointerLocation > PointerOptionLocations.Length - 1 )
							PointerLocation = 0;
						break;
					case 4:                     // Play Button
						PointerLocation = 0;    // Go to first element
						break;
				}
				this.SetPositionFor(0, PointerOptionLocations[PointerLocation]);
			}
		}

		public void MovePointerTo(byte position) {
			PointerLocation = position;
			if( this.State == SelectState.TYPE )
				this.SetPositionFor(0, PointerTypeLocations[PointerLocation]);
			else if( this.State == SelectState.OPTIONS ) {
				this.SetPositionFor(0, PointerOptionLocations[PointerLocation]);
			}
		}

		public SelectMenu(Multris g) {
			this.game = g;

			this.SetPointerLocations(
				new Point[3] {
					// Offline
					new Point(150,150),
					new Point(150, 300),
					new Point(350, 600)
				},
				new Point[5] {
					Point.Zero,
					Point.Zero,
					Point.Zero,
					Point.Zero,
					// Play
					Point.Zero // Changed in Load()
				}

			);

			this.MovePointerTo(0);
			DefaultPointerPositionSpriteSize( );

			// Option init
			this.uintOptions = new GameOption<uint>[2] { new GameOption<uint>(10, "Board Width", 10), new GameOption<uint>(24, "Board Height", 24) };

		}

		public void Load(ContentManager cm) {
			try {
				texture = cm.Load<Texture2D>("MainMenu/selectMenu");
				this.OfflineLabel = new Rectangle(Pointer[0].X, Pointer[0].Y, Pointer[0].Width + ( (int) this.game.FiraLight20.MeasureString("Offline").X ), Pointer[0].Height);

				// Change Play location
				this.PointerOptionLocations[4] = new Point((int) ( ( this.game.WIDTH / 2 ) - ( this.game.FiraLight20.MeasureString("Play").X * 4 ) ), this.game.HEIGHT - 100);

				loaded = true;
			} catch( Exception e ) {
				loaded = false;
				Console.WriteLine(e);
			}
		}

		public void Draw(SpriteBatch sb) {
			if( loaded ) {

				if( this.State == SelectState.TYPE ) {

					sb.Draw(this.texture, Pointer[0], Pointer[1], colors[0]);

					switch( this.PointerLocation ) {
						case 0:
							/// Offline
							sb.DrawString(
								this.game.FiraLight20,
								"Offline",
								new Vector2(
									PointerTypeLocations[0].X + Pointer[1].Width + 50,
									PointerTypeLocations[0].Y + ( Pointer[1].Height / 2 - this.game.FiraLight20.MeasureString("Offline").Y / 2 )
								),
								Color.White
							);
							break;
					}


				} else if( this.State == SelectState.OPTIONS ) {
					// Draw options
					sb.Draw(this.texture, Pointer[0], Pointer[1], colors[0]);

					sb.DrawString(
						this.game.FiraLight20,
						"Play",
						new Vector2(
							PointerOptionLocations[4].X + Pointer[0].Width + 50,
							PointerOptionLocations[4].Y + ( Pointer[0].Height / 2 - this.game.FiraLight20.MeasureString("Play").Y / 2 )
						),
						Color.White
					);
				}
			}
		}

		public void ShowOptions() {

			this.State = SelectState.OPTIONS;
			MovePointerTo(4);

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
						( OfflineLabel.Intersects(new Rectangle(mr.point, new Point(1))) && mr.button == MouseButton.LEFT ) ) { // If clicked LMB over Offline
						this.ShowOptions( );
					}
				}

			} else if( this.State == SelectState.OPTIONS ) {
				// Handle selecting options for on/offline mode
				if( inputs.ButtonUpAny(bef, Buttons.Back) || inputs.KeyUp(bef, Keys.Escape) )
					this.GoBack( );

				if( inputs.ButtonUpAny(bef, Buttons.A) || inputs.KeyUp(bef, Keys.Enter) ) {
					this.game.InitializeGame( );
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
