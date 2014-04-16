using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SnakesOnAGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        List<Vector2> snake = new List<Vector2>();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D snakeTexture;
        Vector2 pellet = new Vector2(1, 2);
        Random rand = new Random();
        Texture2D pelletTexture;
        Vector2 velocity = new Vector2(0, -1);
        Vector2 location = new Vector2(1, 1);
        float snakeMovementTimer = 0f;
        float snakeMovementTime = 50f;
        int PlayerScore = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            snakeTexture = Content.Load<Texture2D>(@"Snake");
            pelletTexture = Content.Load<Texture2D>(@"PELLET");
            snake.Add(new Vector2(40, 24));
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            if (snake[0].X < 0 || snake[0].X > 80 || snake[0].Y < 0 || snake[0].Y > 47)
            {
                this.Exit();
            }
            base.Update(gameTime);
            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Up) && velocity.Y != 1)
            {
                velocity.X = 0;
                velocity.Y = -1;
                
            }
            if (kb.IsKeyDown(Keys.Down) && velocity.Y != -1)
            {
                velocity.X = 0;
                velocity.Y = 1;
                
            }
            if (kb.IsKeyDown(Keys.Left) && velocity.X != 1)
            {
                velocity.X = -1;
                velocity.Y = 0;
                
            }
            if (kb.IsKeyDown(Keys.Right) && velocity.X != -1)
            {
                velocity.X = 1;
                velocity.Y = 0;
                
            }

            snakeMovementTimer += (float)gameTime.ElapsedGameTime.Milliseconds;

            if (snakeMovementTimer > snakeMovementTime)
            {
                for (int i = snake.Count - 1; i > 0; i--)
                {
                    snake[i] = snake[i - 1];
                }
                snake[0] += velocity;

                snakeMovementTimer = 0f;
                for (int i = 1; i < snake.Count; i++)
                {
                    if (snake[0] == snake[i])
                    {
                        this.Exit();
                    }
                }
            }

            if (snake[0] == pellet)
            {
                pellet.X = rand.Next(5, 35);
                pellet.Y = rand.Next(5, 35);
                snake.Add(new Vector2(snake[0].X, snake[0].Y));
                PlayerScore++;
                this.Window.Title = "Score : " + PlayerScore.ToString();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here

            base.Draw(gameTime);
            
            spriteBatch.Begin();

            for (int i = 0; i < snake.Count; i++)
            {
                spriteBatch.Draw(snakeTexture, snake[i] * 10, Color.MidnightBlue);
                spriteBatch.Draw(pelletTexture, pellet * 10, Color.DarkTurquoise);
            }
            GraphicsDevice.Clear(Color.Orange);

            spriteBatch.End();

            
        }
    }
}
