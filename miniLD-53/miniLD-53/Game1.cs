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

namespace miniLD_53
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        
        enum GameState
        {
            mainMenu,
            options,
            Playing,
        }
        GameState currentGameState = GameState.mainMenu;

        int screenWidth = 800, screenHeight = 600;
        int Score;
        Rectangle bullet1 = new Rectangle(-40, -40, 8, 3);
        Rectangle bullet2 = new Rectangle(-40, -40, 8, 3);

        Button button;
        Map map;
        Player player1;
        Player player2;

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
            map = new Map();
            player1 = new Player(new Vector2(128, 32), Content.Load<Texture2D>("Bullet"), bullet1);
            player2 = new Player(new Vector2(128 * 4, 32), Content.Load<Texture2D>("Bullet"), bullet2);

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

            font = Content.Load<SpriteFont>("Font");

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();
            IsMouseVisible = true;

            button = new Button(Content.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            button.setPosition(new Vector2(350, 300));

            Tiles.Content = Content;

            map.Generate(new int[,]{
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,3},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0},
{0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0},
{0,0,0,0,3,3,3,3,0,0,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0},
{3,0,0,0,4,2,2,2,0,0,4,2,2,4,4,0,0,0,0,0,0,3,3,3,3},
{2,3,0,0,0,2,2,4,0,0,4,4,4,4,0,0,0,0,0,0,0,2,2,2,2},
{2,2,0,0,0,4,4,0,0,0,0,4,4,0,0,0,0,0,0,0,2,2,2,1,4},
{2,2,3,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4,4},
{2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4},
{2,2,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,4,4},
{2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,3,2,0,0,0,0,0,4},
{2,2,2,2,3,0,0,0,0,0,0,0,0,0,0,0,3,2,2,3,0,0,0,0,0},
{2,2,2,2,2,3,3,3,3,0,0,0,0,0,0,3,2,2,2,2,0,0,0,0,0},
{2,2,2,2,2,2,4,2,2,3,0,0,0,0,2,2,2,1,4,2,2,0,0,0,3},
{1,2,2,2,2,4,4,4,2,2,2,0,0,2,4,4,4,4,4,2,2,0,0,0,2},
{4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4},

            }, 32);
            player1.Load(Content, 1);
            player2.Load(Content, 2);
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
            MouseState mouse = Mouse.GetState();

            switch (currentGameState)
            {
                case GameState.mainMenu:
                    if (button.isClicked == true) currentGameState = GameState.Playing;
                    button.Update(gameTime);
                    break;

                case GameState.Playing:
                    player1.Update(gameTime, 1);
                    player2.Update(gameTime, 2);
                    break;
            }

            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                player1.Collision(tile.Rectangle, map.Width, map.Height);
                player2.Collision(tile.Rectangle, map.Width, map.Height);
            }

            Score++;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
                        Vector2 text = new Vector2(10, 10);

            spriteBatch.Begin();
            switch (currentGameState)
            {
                case GameState.mainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("MainMenu"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    button.Draw(spriteBatch);
                    break;

                case GameState.Playing:
                    spriteBatch.Draw(Content.Load<Texture2D>("Background"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    map.Draw(spriteBatch);
                    spriteBatch.DrawString(font, "Score: " + Score, text, Color.White);
                    player1.draw(spriteBatch);
                    player2.draw(spriteBatch);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
