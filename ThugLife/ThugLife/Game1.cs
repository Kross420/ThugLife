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

        // Police
        Texture2D policeTexture;
        List<Police> police;

        // Cars
        Texture2D carTexture;
        Texture2D carTexture2;
        List<Car> cars;

        // The rate at which the police appear
        TimeSpan policeSpawnTime;
        TimeSpan policePreviousSpawnTime;
        // A random number generator
        Random random;

        // The rate at which the cars appear
        TimeSpan carsSpawnTime;
        TimeSpan carsPreviousSpawnTime;


        

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

            // Initialize the police list
            police = new List<Police>();

            // Set the time keepers to zero
            policePreviousSpawnTime = TimeSpan.Zero;

            // Used to determine how fast enemy respawns
            policeSpawnTime = TimeSpan.FromSeconds(5.0f);

            // Initialize our random number generator
            random = new Random();

            // Initialize the enemies list
            cars = new List<Car>();

            // Set the time keepers to zero
            carsPreviousSpawnTime = TimeSpan.Zero;

            // Used to determine how fast enemy respawns
            carsSpawnTime = TimeSpan.FromSeconds(8.0f);

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
            skiesLayer2.Initialize(Content, "bg3", GraphicsDevice.Viewport.Width, -1);
            ground.Initialize(Content, "ground", GraphicsDevice.Viewport.Width, -16);
            buildings.Initialize(Content, "buildings", GraphicsDevice.Viewport.Width, -4);
            road.Initialize(Content, "road", GraphicsDevice.Viewport.Width, -16);
            barrier.Initialize(Content, "barrier", GraphicsDevice.Viewport.Width, -16);

            skies = Content.Load<Texture2D>("bg1");

            //Police
            policeTexture = Content.Load<Texture2D>("popo");

            //Cars
            carTexture = Content.Load<Texture2D>("car1");
            carTexture2 = Content.Load<Texture2D>("car2");

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

            //Update police
            UpdatePolice(gameTime);

            //Update cars
            UpdateCars(gameTime);

            UpdateCollision();
            base.Update(gameTime);
        }

        //
        private void UpdatePlayer(GameTime gameTime)
        {

            player.Update(gameTime);

            // Use the Keyboard / Dpad
            if (currentKeyboardState.IsKeyDown(Keys.A) ||
            currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                player.Position.X -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.D) ||
            currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                player.Position.X += playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.W) ||
            currentGamePadState.DPad.Up == ButtonState.Pressed)
            {
                player.Position.Y -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.S) ||
            currentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                player.Position.Y += playerMoveSpeed;
            }

            // Make sure that the player does not go out of bounds
            player.Position.X = MathHelper.Clamp(player.Position.X, player.Width / 2, GraphicsDevice.Viewport.Width - player.Width / 2);
            player.Position.Y = MathHelper.Clamp(player.Position.Y, 340, 605);
        }

        // Update police
        private void AddPolice()
        {
            // Create the animation object
            Animation policeAnimation = new Animation();

            // Initialize the animation with the correct animation information
            policeAnimation.Initialize(policeTexture, Vector2.Zero, 336, 119, 3, 30, Color.White, 1f, true);

            // Randomly generate the position of the enemy
            Vector2 position = new Vector2(-policeTexture.Width / 2, random.Next(340, GraphicsDevice.Viewport.Height - 140));

            // Create an enemy
            Police policeCar = new Police();

            // Initialize the enemy
            policeCar.Initialize(policeAnimation, position);

            // Add the enemy to the active enemies list
            police.Add(policeCar);
        }

        //Update police
        private void UpdatePolice(GameTime gameTime)
        {
            // Spawn a new enemy enemy every 1.5 seconds
            if (gameTime.TotalGameTime - policePreviousSpawnTime > policeSpawnTime)
            {
                policePreviousSpawnTime = gameTime.TotalGameTime;

                // Add an Enemy
                AddPolice();
            }

            // Update the Enemies
            for (int i = police.Count - 1; i >= 0; i--)
            {
                police[i].Update(gameTime);

                if (police[i].Active == false)
                {
                    police.RemoveAt(i);
                }
            }
        }

        // Update police
        private void AddCars()
        {
            // Create the animation object
            Animation carsAnimation = new Animation();
            Animation carsAnimation2 = new Animation();

            // Initialize the animation with the correct animation information
            carsAnimation.Initialize(carTexture, Vector2.Zero, 297, 103, 3, 30, Color.White, 1f, true);
            carsAnimation2.Initialize(carTexture2, Vector2.Zero, 341, 119, 3, 30, Color.White, 1f, true);

            // Randomly generate the position of the enemy
            Vector2 position = new Vector2(GraphicsDevice.Viewport.Width + carTexture.Width / 2, random.Next(340, GraphicsDevice.Viewport.Height - 140));
            //Vector2 position2 = new Vector2(-carTexture2.Width / 2, random.Next(340, GraphicsDevice.Viewport.Height - 140));

            // Create an enemy
            Car car = new Car();

            // Initialize the enemy
            int sk = random.Next(1, 3);
            if (sk == 1) car.Initialize(carsAnimation, position);
            else car.Initialize(carsAnimation2, position);
            

            // Add the enemy to the active enemies list
            cars.Add(car);
        }

        private void UpdateCars(GameTime gameTime)
        {
            // Spawn a new enemy enemy every 1.5 seconds
            if (gameTime.TotalGameTime - carsPreviousSpawnTime > carsSpawnTime)
            {
                carsPreviousSpawnTime = gameTime.TotalGameTime;

                // Add a Car
                AddCars();
            }

            // Update the Enemies
            for (int i = cars.Count - 1; i >= 0; i--)
            {
                cars[i].Update(gameTime);

                if (cars[i].Active == false)
                {
                    cars.RemoveAt(i);
                }
            }
        }

        private void UpdateCollision()
        {
            // Use the Rectangle's built-in intersect function to 
            // determine if two objects are overlapping
            Rectangle rectanglePlayer;
            Rectangle rectanglePolice;
            Rectangle rectangleCar;

            // Only create the rectangle once for the player
            rectanglePlayer = new Rectangle((int)player.Position.X,
            (int)player.Position.Y+50,
            player.Width,
            player.Height-90);

            // Do the collision between the player and the enemies
            for (int i = 0; i < police.Count; i++)
            {
                rectanglePolice = new Rectangle((int)police[i].Position.X,
                (int)police[i].Position.Y,
                police[i].Width,
                police[i].Height);

                // Determine if the two objects collided with each
                // other
                if (rectanglePlayer.Intersects(rectanglePolice))
                {
                    // Subtract the health from the player based on
                    // the enemy damage
                    player.Health -= police[i].Damage;
                    if (police[i].Position.Y > player.Position.Y) police[i].Position.Y +=  2;
                    else police[i].Position.Y -= 2;
                    // Since the enemy collided with the player
                    // destroy it
                    //police[i].Health -= 1;
                    //police[i].Position.X = player.Position.X - police[i].Width;
                    police[i].Position.X += -10;
                    //police[i].Position.Y += 1;
                    // If the player health is less than zero we died
                    if (player.Health <= 0)
                        player.Active = false;
                }

                for (int j = 0; j < cars.Count; j++)
                {
                    rectangleCar = new Rectangle((int)cars[j].Position.X,
                    (int)cars[j].Position.Y,
                    cars[j].Width,
                    cars[j].Height);

                    if (rectangleCar.Intersects(rectanglePolice))
                    {
                        if (police[i].Position.Y >= cars[j].Position.Y) police[i].Position.Y += 2;
                        else police[i].Position.Y -= 2;
                    }
                }

                

            }
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
            //Draw police
            // Draw the Enemies
            for (int i = 0; i < police.Count; i++)
            {
                police[i].Draw(spriteBatch);

            }

            for (int i = 0; i < cars.Count; i++)
            {
                cars[i].Draw(spriteBatch);

            }
            barrier.Draw(spriteBatch);
            spriteBatch.End(); // Stop drawing

            base.Draw(gameTime);
        }
    }
}
