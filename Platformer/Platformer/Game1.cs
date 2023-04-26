using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer;

namespace Platformer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        internal const int Gravity = 8;

        Transform GO_Transform;
        Transform[] CoinTransforms;
        Transform GoalTransform;
        ActorObject player;
        Coin[] coins;
        Goal goal;

        Texture2D playerIdle;
        Texture2D playerRun;
        Texture2D playerJump;
        Texture2D coinSprite;
        Texture2D goalSprite;

        SpriteFont font;
        SpriteFont font2;

        Platform[] p;

        Rectangle playerRect = new Rectangle(0, 0, 48, 48);
        Rectangle coinRect = new Rectangle(0, 0, 48, 48);
        Rectangle goalRect = new Rectangle(0, 0, 96, 96);
        CelAnimationSet playerSet;
        CelAnimationSequence CoinAnim;

        GameState gameState = GameState.Active;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            p = new Platform[] {
                new Platform(new Vector2(120, Window.ClientBounds.Height - 120), new Vector2(100, 25), "Meatball"),
                new Platform(new Vector2(310, Window.ClientBounds.Height - 145), new Vector2(110, 55), "Meatball"),
                new Platform(new Vector2(450, Window.ClientBounds.Height - 170), new Vector2(120, 72), "Meatball"),
                new Platform(new Vector2(630, Window.ClientBounds.Height - 255), new Vector2(100, 150), "Meatball"),
                new Platform(new Vector2(430, Window.ClientBounds.Height - 250), new Vector2(105, 15), "Meatball"),
                new Platform(new Vector2(100, Window.ClientBounds.Height - 275), new Vector2(100, 50), "Meatball")
                    };
            base.Initialize();
            GO_Transform = new Transform(new Vector2(48, 24), 0, 1);
            CoinTransforms = new Transform[]
            {
                new Transform(new Vector2(120, Window.ClientBounds.Height - 168), 0, 1),
                new Transform(new Vector2(310, Window.ClientBounds.Height - 193), 0, 1)
            };
            GoalTransform = new Transform(new Vector2(100, Window.ClientBounds.Height - 371), 0, 1);
            player = new ActorObject(this, GO_Transform, playerRect, playerIdle, playerSet);
            coins = new Coin[]
            {
                new Coin(1, this, CoinTransforms[0], coinRect, coinSprite, CoinAnim),
                new Coin(1, this, CoinTransforms[1], coinRect, coinSprite, CoinAnim)
            };
            goal = new Goal(this, GoalTransform, goalRect, goalSprite);
            Window.Title = "Hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh";
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerIdle = Content.Load<Texture2D>("Character Idle 48x48");
            playerRun = Content.Load<Texture2D>("run cycle 48x48");
            playerJump = Content.Load<Texture2D>("player jump 48x48");
            coinSprite = Content.Load<Texture2D>("cat");
            goalSprite = Content.Load<Texture2D>("Trophy");
            font = Content.Load<SpriteFont>("Font");
            font2 = Content.Load<SpriteFont>("Font2");
            CoinAnim = new CelAnimationSequence(coinSprite, 48, 0.5f);
            playerSet = new CelAnimationSet(playerIdle, playerRun, playerJump);
            foreach (var platform in p)
            {
                platform.LoadContent(Content);
            }

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (gameState == GameState.Active)
            {
                KeyboardState kbState = Keyboard.GetState();
                if (kbState.IsKeyDown(Keys.Right) == kbState.IsKeyDown(Keys.Left))
                {
                    player.HorizontalStop();
                    //player.State = ActorState.Idle;
                }
                else if (kbState.IsKeyDown(Keys.Right))
                {
                    player.MoveHorizontally(1);
                }
                else if (kbState.IsKeyDown(Keys.Left))
                {
                    player.MoveHorizontally(-1);
                }
                if (kbState.IsKeyDown(Keys.Space))
                {
                    player.Jump();
                }
                //player.Update(gameTime);
                foreach (var platform in p)
                {
                    platform.ProcessCollisions(player);
                }

                for (int i = 0; i < coins.Length; i++)
                {
                    if (coins[i] is not null)
                    {
                        coins[i].Update(gameTime);
                        if (coins[i].HandleCollisions(player))
                        {
                            coins[i] = null;
                        }
                    }
                }

                goal.Update(gameTime);
                if(goal.HandleCollisions(player))
                {
                    gameState = GameState.GameOver;
                }

                base.Update(gameTime);
            }
            // TODO: Add your update logic here

            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSalmon); //You know what's better than light salmon? Dark salmon
            _spriteBatch.Begin();
            if (gameState == GameState.Active)
            {
                foreach (var platform in p)
                {
                    platform.Draw(_spriteBatch);
                }
                _spriteBatch.DrawString(font2, "Collect the cats to get the trophy", new Vector2(430, Window.ClientBounds.Height - 300), Color.Black);
                base.Draw(gameTime);
            } else
            {
                _spriteBatch.DrawString(font, "Fin", new Vector2(Window.ClientBounds.Width / 2 - 50, Window.ClientBounds.Height / 2 - 50), Color.Black);
            }
            _spriteBatch.End();
            // TODO: Add your drawing code here

            
        }

        public enum GameState
        {
            Active,
            GameOver
        }
    }
}