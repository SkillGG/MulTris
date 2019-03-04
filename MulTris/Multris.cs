﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MulTris {
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Multris : Game {

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public enum GameState {
			MENU,
			SELECT,
			OPTIONS,
			T3,
			T4,
			T5,
			T6
		}

		// FONTS
		public SpriteFont FiraLight24;
		// FONTS
		

		private GameState gamestate = GameState.MENU;

		public GameState State { get { return this.gamestate; } set { this.gamestate = value; } }

		public static Point ScreenCentre(int W, int H) {
			return new Point(
				( GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - W ) / 2,
				( GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - H ) / 2
			);
		}

		public Point WindowCentre() {
			return new Rectangle(0, 0, WIDTH, HEIGHT).Center;
		}

		private const int DEFRES = 800;
		private int[] useRes = new int[2] { DEFRES, DEFRES / 12 * 9 };
		private bool fullScreen = false;
		private bool borderLess = true;

		private bool changeRes = false;
		public int WIDTH { get { return this.useRes[0]; } set { if( changeRes ) this.useRes[0] = value; } }
		public int HEIGHT { get { return this.useRes[1]; } set { if( changeRes ) this.useRes[1] = value; } }
		public bool FULLSCREEN { get { return this.fullScreen; } set { if( changeRes ) this.fullScreen = value; } }
		public bool BORDERLESS { get { return this.borderLess; } set { if( changeRes ) this.borderLess = value; } }

		public void ChangeGameResolution(int? w, int? h, bool? FS, bool? BL) {
			this.changeRes = true;
			this.WIDTH = w ?? this.WIDTH;
			this.HEIGHT = h ?? this.HEIGHT;
			this.FULLSCREEN = FS ?? this.FULLSCREEN;
			this.BORDERLESS = BL ?? this.BORDERLESS;
			this.changeRes = false;

			// Save game res
			this.graphics.PreferredBackBufferHeight = HEIGHT;
			this.graphics.PreferredBackBufferWidth = WIDTH;
			this.graphics.IsFullScreen = FULLSCREEN;
			Window.IsBorderless = BORDERLESS;
			this.graphics.ApplyChanges( );
		}

		private InputState inputs;

		public Menu menu;

		public Multris() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			// Show the mouse
			this.IsMouseVisible = true;

			// Setting default resolution settings
			this.graphics.PreferredBackBufferHeight = HEIGHT;
			this.graphics.PreferredBackBufferWidth = WIDTH;
			this.graphics.IsFullScreen = FULLSCREEN;
			Window.IsBorderless = BORDERLESS;
			Window.Position = new Point(ScreenCentre(WIDTH, 0).X, 0);



			// Setting default FPS settings
			this.IsFixedTimeStep = true;
			this.graphics.SynchronizeWithVerticalRetrace = true;
			this.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 33); // Play at ~30.3FPS

		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize() {
			// TODO: Add your initialization logic here

			this.inputs = new InputState( );

			this.menu = new Menu(this);

			base.Initialize( );
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent() {
			// Create a new SpriteBatch, which can be used to draw textures.
			this.spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here

			this.FiraLight24 = this.Content.Load<SpriteFont>("Fira");

			menu.Load(this.Content);

		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent() {
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime) {

			/*if( GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState( ).IsKeyDown(Keys.Escape) )
					Exit( );*/

			if( State == GameState.MENU )
				this.menu.Update(inputs);
			

			base.Update(gameTime);
			inputs.Update( );
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.Black);

			// TODO: Add your drawing code here

			// Start collecting .Draw(...) calls 
			this.spriteBatch.Begin( );

			if( this.State == GameState.MENU ) {
				// RENDER ALL MENU SPRITES
				this.menu.Draw(this.spriteBatch);

			}
			// Render everything on screen
			this.spriteBatch.End( );

			base.Draw(gameTime);
		}
	}
}