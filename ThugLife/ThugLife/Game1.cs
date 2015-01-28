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

namespace ThugLife
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;

        // Keyboard states used to determine key presses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        // Gamepad states used to determine button presses
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        // A movement speed for the player
        float playerMoveSpeed;

        // Image used to display the static background
        Texture2D skies;

        // Parallaxing Layers
        ParallaxingBackground skiesLayer1;
        ParallaxingBackground skiesLayer2;
        ParallaxingBackground ground;
        ParallaxingBackground buildings;
        ParallaxingBackground road;
        ParallaxingBackground barrier;

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;
            Content.RootDirectory = "Content";
        }

        //
        protected override void Initialize()
        {
            player = new Player();
            playerMoveSpeed = 8.0f;

            skiesLayer1 = new ParallaxingBackground();
            skiesLayer2 = new ParallaxingBackground();
            ground = new ParallaxingBackground();
            buildings = new ParallaxingBackground();
            road = new ParallaxingBackground();
            barrier = new ParallaxingBackground();

            base.Initialize();
        }

        //
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Animation playerAnimation = new Animation();
            Texture2D playerTexture = Content.Load<Texture2D>("playercar");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 331, 99, 4, 30, Color.White, 1f, true);

            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y
            + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(playerAnimation, playerPosition);

            // Load the parallaxing background
            skiesLayer1.Initialize(Content, "bg2", GraphicsDevice.Viewport.Width, -1);
            skiesLayer2.Initialize(Content, "bg3", GraphicsDevice.Viewport.Width, -2);
            ground.Initialize(Content, "ground", GraphicsDevice.Viewport.Width, -8);
            buildings.Initialize(Content, "buildings", GraphicsDevice.Viewport.Width, -4);
            road.Initialize(Content, "road", GraphicsDevice.Viewport.Width, -8);
            barrier.Initialize(Content, "barrier", GraphicsDevice.Viewport.Width, -8);

            skies = Content.Load<Texture2D>("bg1");

        }

        //
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        //
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)

            // Save the previous state of the keyboard and game pad so we can determinesingle key/button presses
            previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;

            // Read the current state of the keyboard and gamepad and store it
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);


            //Update the player
            UpdatePlayer(gameTime);

            skiesLayer1.Update();
            skiesLayer2.Update();
            ground.Update();
            buildings.Update();
            road.Update();
            barrier.Update();

            base.Update(gameTime);
        }

        //
        private void UpdatePlayer(GameTime gameTime)
        {

            player.Update(gameTime);

            // Get Thumbstick Controls
            player.Position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;
            player.Position.Y -= currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;

            // Use the Keyboard / Dpad
            if (currentKeyboardState.IsKeyDown(Keys.Left) ||
            currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                player.Position.X -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) ||
            currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                player.Position.X += playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) ||
            currentGamePadState.DPad.Up == ButtonState.Pressed)
            {
                player.Position.Y -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down) ||
            currentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                player.Position.Y += playerMoveSpeed;
            }

            // Make sure that the player does not go out of bounds
            player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width);
            player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height);
        }

        //
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(); // Start drawing

            spriteBatch.Draw(skies, Vector2.Zero, Color.White);

            // Draw the moving background
            skiesLayer1.Draw(spriteBatch);
            skiesLayer2.Draw(spriteBatch);
            buildings.Draw(spriteBatch);
            road.Draw(spriteBatch);
            ground.Draw(spriteBatch);
            // Draw the Player
            player.Draw(spriteBatch);
            barrier.Draw(spriteBatch);
            spriteBatch.End(); // Stop drawing

            base.Draw(gameTime);
        }
    }
}
