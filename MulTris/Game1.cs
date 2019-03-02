using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MulTris {
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game {

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public enum GameState {
			MENU,
			T4,
			T5,
			T6,
			T7,
			T8,
			OPTIONS
		}

		public GameState gamestate = GameState.MENU;

		public Point ScreenCentre(int W) {
			return new Point(( GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - W ) / 2, 0);
		}

		public Tile tile1;

		public static int WIDTH = 600, HEIGHT = 700;

		public Game1() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";


			// Show the mouse
			this.IsMouseVisible = true;

			tile1 = new Tile(null, Color.White);

			// Setting default resolution settings
			this.graphics.PreferredBackBufferHeight = HEIGHT;
			this.graphics.PreferredBackBufferWidth = WIDTH;
			this.graphics.IsFullScreen = false;
			Window.Position = ScreenCentre(WIDTH);
			Window.IsBorderless = true;


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

			base.Initialize( );
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent() {
			// Create a new SpriteBatch, which can be used to draw textures.
			this.spriteBatch = new SpriteBatch(GraphicsDevice);

			this.tile1.Load(Content, "T5/TileTXT", new Rectangle(0, 0, 50, 50));

			// TODO: use this.Content to load your game content here
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
			if( GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState( ).IsKeyDown(Keys.Escape) )
				Exit( );

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here

			// Start collecting .Draw(...) calls 
			this.spriteBatch.Begin( );

			if( this.gamestate == GameState.MENU ) {
				// RENDER ALL MENU SPRITES
				this.tile1.Draw(this.spriteBatch);

			}
			// Render everything on screen
			this.spriteBatch.End( );

			base.Draw(gameTime);
		}
	}
}
