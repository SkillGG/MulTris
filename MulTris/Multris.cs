using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;

namespace MulTris {

	public class Debug {

		private Debug() { }

		public enum Importance {
			INIT_INFO = 0,
			DRAW_INFO = 1,
			VALUE_INFO = 2,
			NOTIFICATION = 3,
			IMPORTANT_INFO = 4,
			WARN = 5,
			hid = 6,
			ERROR = 7
		}

		private static Importance Minimp = Importance.INIT_INFO;
		public static Importance MinImp { get => Minimp; set => Minimp = value; }

		public Debug(string invoker, object msg, Importance importance = (Importance) 0) {
			uint imp = (uint) importance;
			if( imp >= (uint) MinImp )
				Console.WriteLine($"[{importance.ToString( )}]( " + invoker + " ) " + msg.ToString( ));
		}

		public Debug(string invoker, string msg, Importance importance = (Importance) 0) {
			uint imp = (uint) importance;
			if( imp >= (uint) MinImp )
				Console.WriteLine($"[{importance.ToString( )}]( " + invoker + " ) " + msg);
		}
	}

	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Multris : Game {

		/* STRUCTS */

		public enum GameState {
			MENU,
			SELECT,
			OPTIONS,
			GAME
		}

		public static readonly string VERSION_STRING = "Pre-Alpha v0.2";

		/* STRUCTS */

		/* VARIABLES */

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		// STATES
		private GameState gamestate = GameState.MENU;
		// STATES

		// GAMEPLAY
		private InputState inputs;
		public Menu menu;
		public Tetris tetris;
		public SelectMenu selectmenu;
		// GAMEPLAY

		// SCREEN
		private const int DEFRES = 800;
		private int[] useRes = new int[2] { DEFRES, DEFRES / 12 * 9 };
		private bool fullScreen = false;
		private bool borderLess = true;
		private bool changeRes = false;
		// SCREEN

		// FIXED_UPDATE
		private Thread thread;
		private Timer futim;
		private Timer sectim;
		private UInt32 FixedRate = 1000;
		// FIXED_UPDATE

		// FONTS
		public SpriteFont FiraLight24;
		public SpriteFont FiraLight20;
		public SpriteFont FiraLight10;
		// FONTS

		/* VARIABLES */

		/* PROPERTIES */

		public GameState State { get { return this.gamestate; } set { this.gamestate = value; } }

		public UInt32 Rate { get => FixedRate; set => ChangeFixedRate(value); }

		// SCREEN
		public int WIDTH { get { return this.useRes[0]; } set { if( changeRes ) this.useRes[0] = value; } }
		public int HEIGHT { get { return this.useRes[1]; } set { if( changeRes ) this.useRes[1] = value; } }
		public bool FULLSCREEN { get { return this.fullScreen; } set { if( changeRes ) this.fullScreen = value; } }
		public bool BORDERLESS { get { return this.borderLess; } set { if( changeRes ) this.borderLess = value; } }
		// SCREEN

		/* PROPERTIES  */

		/* 
		 XNA
		 FUNCTIONS
		*/

		public Multris() {
			Debug.MinImp = Debug.Importance.IMPORTANT_INFO;
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			new Debug("Multris#()", "App Initialization.", Debug.Importance.INIT_INFO);

			// Show the mouse
			this.IsMouseVisible = true;

			// Setting default resolution settings
			this.graphics.PreferredBackBufferHeight = HEIGHT;
			this.graphics.PreferredBackBufferWidth = WIDTH;
			this.graphics.IsFullScreen = FULLSCREEN;
			Window.IsBorderless = BORDERLESS;
			Window.Position = new Point(ScreenCentre(WIDTH, 0).X, 0);

			new Debug("Multris#()", "Correctly setted up default screen parameters.", Debug.Importance.NOTIFICATION);

			// Setting default FPS settings
			this.IsFixedTimeStep = true;
			this.graphics.SynchronizeWithVerticalRetrace = true;
			this.TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 33); // Play at ~30.3FPS

			new Debug("Multris#()", "Setted up framerate.", Debug.Importance.NOTIFICATION);

		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize() {
			// TODO: Add your initialization logic here

			string pl = "Multris#Initialize";

			new Debug(pl, "Initializing variables", Debug.Importance.INIT_INFO);

			this.inputs = new InputState( );
			this.menu = new Menu(this);
			this.selectmenu = new SelectMenu(this);
			this.tetris = new Tetris(this);

			new Debug(pl, "DONE: inputs, menu, selectmenu, tetris", Debug.Importance.NOTIFICATION);

			new Debug(pl, "Initializing second(timer-handling) thread!", Debug.Importance.INIT_INFO);

			try {
				this.thread = new Thread(new ThreadStart(STF));
				thread.Start( );
			} catch( Exception e ) {
				new Debug(pl, " ERR: " + e, Debug.Importance.ERROR);
			}

			new Debug(pl, "DONE: thread (" + thread.ManagedThreadId + ")", Debug.Importance.NOTIFICATION);

			base.Initialize( );
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent() {

			string pl = "Multris#LoadContent";

			// Create a new SpriteBatch, which can be used to draw textures.
			this.spriteBatch = new SpriteBatch(GraphicsDevice);

			new Debug(pl, "Loading Content!", Debug.Importance.INIT_INFO);

			// TODO: use this.Content to load your game content here

			new Debug(pl, "Fonts:", Debug.Importance.INIT_INFO);

			new Debug(pl, "FiraLight", Debug.Importance.INIT_INFO);
			this.FiraLight24 = this.Content.Load<SpriteFont>("Fonts/Fira24");
			this.FiraLight20 = this.Content.Load<SpriteFont>("Fonts/Fira20");
			this.FiraLight10 = this.Content.Load<SpriteFont>("Fonts/Fira10");
			new Debug(pl, "DONE: FiraLight24, FiraLight20, FiraLight10", Debug.Importance.NOTIFICATION);


			new Debug(pl, "Loading sub-classes", Debug.Importance.INIT_INFO);
			tetris.Load(Content);
			new Debug(pl, "DONE: tetris", Debug.Importance.NOTIFICATION);
			menu.Load(this.Content);
			new Debug(pl, "DONE: menu", Debug.Importance.NOTIFICATION);
			selectmenu.Load(Content);
			new Debug(pl, "DONE: selectmenu", Debug.Importance.NOTIFICATION);

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
			if( new InputState( ).KeyUp(inputs, Keys.Escape) )
				Console.WriteLine("KeyUp!");
			if( State == GameState.MENU )
				this.menu.Update(inputs);
			if( State == GameState.SELECT )
				this.selectmenu.Update(inputs);
			if( State == GameState.GAME ) {
				this.tetris.Update(inputs);
			}


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

			this.spriteBatch.DrawString(FiraLight10, $"Version: {VERSION_STRING}", new Vector2(WIDTH - FiraLight10.MeasureString("Version: Pre-Alpha v0.1").X, HEIGHT - FiraLight10.MeasureString("Version: Pre-Alpha v0.1").Y), Color.Red);

			if( this.State == GameState.MENU ) {
				// RENDER ALL MENU SPRITES
				this.spriteBatch.DrawString(FiraLight10, ( "M: " + this.State + " S: " + this.menu.State ), new Vector2(0, 0), Color.Yellow);

				this.menu.Draw(this.spriteBatch);
			}
			if( this.State == GameState.SELECT ) {
				this.spriteBatch.DrawString(FiraLight10, ( "M: " + this.State + " S: " + this.selectmenu.State ), new Vector2(0, 0), Color.Yellow);

				this.selectmenu.Draw(spriteBatch);
			}
			if( this.State == GameState.OPTIONS ) {
				this.spriteBatch.DrawString(FiraLight10, ( "M: " + this.State + " S: " ), new Vector2(0, 0), Color.Yellow);
				// DRAW OPTIONS
			}
			if( this.State == GameState.GAME ) {
				this.spriteBatch.DrawString(FiraLight10, ( $"M: {this.State} S: {tetris.board.Size.X}/{tetris.board.Size.Y}" ), new Vector2(0, 0), Color.Yellow);
				// DRAW GAME
				this.tetris.Draw(this.spriteBatch);
			}

			// Render everything on screen
			this.spriteBatch.End( );

			base.Draw(gameTime);
		}

		/* 
		 XNA
		 FUNCTIONS
		*/

		/* MULTRIS FUNCTIONS */

		public void InitializeGame(GameOption<Point> size, GameOption<bool>[] bck) {
			// INITIALIZE GAME WITH GIVEN OPTIONS
			new Debug("Multris#InitializeGame", "Initializing new Tetris game with: " + size + ", [" + string.Join(", ", (object[]) bck) + "]", Debug.Importance.INIT_INFO);
			this.tetris.Initialize(size, bck);
			this.State = GameState.GAME;
			new Debug("Multris#InitializeGame", "New Tetris Game has been initialized with: " + size + ", [" + string.Join(", ", (object[]) bck) + "]", Debug.Importance.NOTIFICATION);
		}

		public void Terminate() {
			this.Exit( );
		}

		/* MULTRIS FUNCTIONS */

		/* SCREEN FUNCTIONS */

		public static Point ScreenCentre(int W, int H) {
			return new Point(
				( GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - W ) / 2,
				( GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - H ) / 2
			);
		}

		public Point WindowCentre() {
			return new Rectangle(0, 0, WIDTH, HEIGHT).Center;
		}

		public void ChangeGameResolution(int? w, int? h, bool? FS, bool? BL) {
			string pl = "Multris#ChangeGameResolution";
			new Debug(pl, "Changing Resolution with: {w:" + w + ",h:" + h + ",FS:" + FS + ",BL:" + BL + "}", Debug.Importance.IMPORTANT_INFO);
			this.changeRes = true;
			this.WIDTH = w ?? this.WIDTH;
			this.HEIGHT = h ?? this.HEIGHT;
			this.FULLSCREEN = FS ?? this.FULLSCREEN;
			this.BORDERLESS = BL ?? this.BORDERLESS;
			this.changeRes = false;
			new Debug("Multris#ChangeGameResolution", "Successfully changed resolution", Debug.Importance.NOTIFICATION);

			new Debug(pl, "Saving changes", Debug.Importance.INIT_INFO);
			// Save game res
			this.graphics.PreferredBackBufferHeight = HEIGHT;
			this.graphics.PreferredBackBufferWidth = WIDTH;
			this.graphics.IsFullScreen = FULLSCREEN;
			Window.IsBorderless = BORDERLESS;
			this.graphics.ApplyChanges( );
			new Debug(pl, "Successfully saved new resolution", Debug.Importance.NOTIFICATION);
		}

		/* SCREEN FUNCTIONS */

		/* TIMER FUNCTIONS */

		private void STF() {
			try {
				new Debug("Multris#STF:2", "Initializing timers.");
				futim = new Timer(FixedUpdate, null, 0, (int) Rate);
				sectim = new Timer(FixedUpdateS, null, 0, 1000);
				tetris.secondInit( );
			} catch( Exception ) {
				// log errors
			}
		}

		public void ChangeFixedRate(uint nfr) {
			this.FixedRate = nfr;
			futim.Change(0, this.FixedRate);
		}

		/// <summary>
		/// This method is called once every <see cref="Rate"/>ms.
		/// </summary>
		/// <param name="sinf"></param>
		protected void FixedUpdate(object sinf) {



		}

		/// <summary>
		/// This method is called once every 1s.
		/// </summary>
		/// <param name="sinf"></param>
		protected void FixedUpdateS(object sinf) {

			if( this.State == GameState.GAME )
				this.tetris.FixedUpdateS( );

		}

		/* TIMER FUNCTIONS */

		/*~Multris() {
			Dispose(false);
			thread.Abort( );
			thread = null;
			futim.Dispose( );
			futim = null;
			sectim.Dispose( );
			sectim = null;
			return;
		}*/
	}
}
