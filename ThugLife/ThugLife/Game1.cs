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
        PlayerCar player; //spçlçtâja maðîna
        Player newPlayer; //spçlçtâjs

        // Keyboard states used to determine key presses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        // Gamepad states used to determine button presses
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        float playerMoveSpeed; //spçlçtâja kustîbas âtrums
        bool busted = false;
   
        Texture2D skies; // statiskas debesis

        // fona slâòi
        ParallaxingBackground skiesLayer1;
        ParallaxingBackground skiesLayer2;
        ParallaxingBackground ground;
        ParallaxingBackground buildings;
        ParallaxingBackground road;
        ParallaxingBackground barrier;

        Random random; // random skaitïa ìenerators

        // Police
        Texture2D policeTexture;
        List<Police> police;

        // Cars
        Texture2D carTexture;
        Texture2D carTexture2;
        List<Car> cars;

        // policijas parâdîðanâs bieþums
        TimeSpan policeSpawnTime;
        TimeSpan policePreviousSpawnTime;

        // parasto maðînu parâdîðanâs bieþums
        TimeSpan carsSpawnTime;
        TimeSpan carsPreviousSpawnTime;

        // lodes
        Texture2D bulletTexture;
        List<Bullets> bullets;
        bool shooting;
        Texture2D gunshotTexture;

        // ðavçja bildes
        Texture2D thugTexture;
        Texture2D thugTextureForward;

        // loþu ðauðanas âtrums
        TimeSpan fireTime;
        TimeSpan previousFireTime;

        //beigu ekrâns
        Texture2D gameOver;

        // skaòas
        SoundEffect shootingSound;
        SoundEffect explosionSound;
        SoundEffect tireScreetchSound;
        SoundEffect policeSpawn;
        SoundEffect policeSiren;
        SoundEffect gameover;

        // mûzika un motora skaòa
        Song gameplayMusic;
        Song engine;

        // sprâdzieni
        Texture2D explosionTexture;
        List<Animation> explosions;

        SpriteFont font; //  UI elementu fonts

        public Game1(Player newPlayer)
        {
            this.newPlayer = newPlayer;
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;
            Content.RootDirectory = "Content";
        }

        //uzstâda sâkuma vçrtîbas
        protected override void Initialize()
        {
            player = new PlayerCar();
            playerMoveSpeed = 8.0f;

            skiesLayer1 = new ParallaxingBackground();
            skiesLayer2 = new ParallaxingBackground();
            ground = new ParallaxingBackground();
            buildings = new ParallaxingBackground();
            road = new ParallaxingBackground();
            barrier = new ParallaxingBackground();

            random = new Random(); //random inicializâcija

            police = new List<Police>(); // inicializç policijas maðînu sarakstu
            policePreviousSpawnTime = TimeSpan.Zero; // pçdçjâs policijas masînas parâdîðanâs laiks = 0
            policeSpawnTime = TimeSpan.FromSeconds(5.0f); //policijas masînas parâdîðanâs bieþums

            cars = new List<Car>(); //inicializç parasto maðînu sarakstu
            carsPreviousSpawnTime = TimeSpan.Zero; // pçdçjâs masînas parâdîðanâs laiks = 0
            carsSpawnTime = TimeSpan.FromSeconds(8.0f); // masînas parâdîðanâs bieþums

            //lodes 
            bullets = new List<Bullets>();
            fireTime = TimeSpan.FromSeconds(.5f); //bieþums

            //sprâdzieni
            explosions = new List<Animation>();

            base.Initialize();
        }

        //ielâdç komponentes
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //player
            Animation playerAnimation = new Animation();
            Texture2D playerTexture = Content.Load<Texture2D>("playercar");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 331, 99, 4, 30, Color.White, 1f, true);
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y+ GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            player.Initialize(playerAnimation, playerPosition);

            //fona slâòi
            skiesLayer1.Initialize(Content, "bg2", GraphicsDevice.Viewport.Width, -1);
            skiesLayer2.Initialize(Content, "bg3", GraphicsDevice.Viewport.Width, -1);
            ground.Initialize(Content, "ground", GraphicsDevice.Viewport.Width, -16);
            buildings.Initialize(Content, "buildings", GraphicsDevice.Viewport.Width, -4);
            road.Initialize(Content, "road", GraphicsDevice.Viewport.Width, -16);
            barrier.Initialize(Content, "barrier", GraphicsDevice.Viewport.Width, -16);

            //statiskâs debesis
            skies = Content.Load<Texture2D>("bg1");

            //Police
            policeTexture = Content.Load<Texture2D>("popo");

            //Cars
            carTexture = Content.Load<Texture2D>("car1");
            carTexture2 = Content.Load<Texture2D>("car2");

            //lodes
            bulletTexture = Content.Load<Texture2D>("bullet");
            gunshotTexture = Content.Load<Texture2D>("gunshot");

            //ðâvçjs
            thugTexture = Content.Load<Texture2D>("gangsta");
            thugTextureForward = Content.Load<Texture2D>("gangstaForward");

            //mûzika
            gameplayMusic = Content.Load<Song>("sound/gameplaymusic2");
            //engine = Content.Load<Song>("sound/driving");

            ///skaòas 
            shootingSound = Content.Load<SoundEffect>("sound/gunshot");
            explosionSound = Content.Load<SoundEffect>("sound/explosion");
            tireScreetchSound = Content.Load<SoundEffect>("sound/screetch");
            gameover = Content.Load<SoundEffect>("sound/gameover");
            policeSpawn = Content.Load<SoundEffect>("sound/policeSiren");
            policeSiren = Content.Load<SoundEffect>("sound/policeSiren2");
            explosionTexture = Content.Load<Texture2D>("explosion_anim");
            font = Content.Load<SpriteFont>("gameFont");
            gameOver = Content.Load<Texture2D>("gameOver");

            // mûzika sâk skançt uzreiz
            PlayMusic(engine);
            PlayMusic(gameplayMusic);

        }

        //nav pielietots
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        //atjauno spçles komponeðu parametrus
        protected override void Update(GameTime gameTime)
        {

            // Save the previous state of the keyboard and game pad so we can determinesingle key/button presses
            previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;

            // Read the current state of the keyboard and gamepad and store it
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);


            if (player.Health > 0) //kamçr spçlçtâjs ir aktîvs
            {
                UpdatePlayer(gameTime); //spçlçtâjs

                //fona slâòi
                skiesLayer1.Update(); 
                ground.Update();
                buildings.Update();
                road.Update();
                barrier.Update();

                UpdatePolice(gameTime); 
                UpdateCars(gameTime);
                UpdateExplosions(gameTime);
                UpdateBullets();
                UpdateCollision(); 
            }
            base.Update(gameTime);
        }

        //atjauno player parametrus atkarîbâ no nospiestajâm pogâm
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

            // lai spçlçtâjs nevarçtu ibraukt ârpus ceïa
            player.Position.X = MathHelper.Clamp(player.Position.X, player.Width / 2, GraphicsDevice.Viewport.Width - player.Width / 2);
            player.Position.Y = MathHelper.Clamp(player.Position.Y, 340, 605);


            if (player.Health > 0 && player.bulletCount > 0) // ja spçlçtâjam ir hp un lodes
            {
                if (currentKeyboardState.IsKeyDown(Keys.Left))
                {
                    shooting = false;
                    if (gameTime.TotalGameTime - previousFireTime > fireTime) //ja pagâjis ðauðanas bieþuma laiks
                    {
                        // uzliek pçdçjo ðauðanas laiku, kad izðauj
                        previousFireTime = gameTime.TotalGameTime;
                        shooting = true; //tiek ðauts

                        // izveido lodi, kas lido uz atpakaïu
                        AddBulletsBackward(player.Position + new Vector2(-35, -37));
                        shootingSound.Play();
                        player.bulletCount -= 1;
                    }
                }

                if (currentKeyboardState.IsKeyDown(Keys.Right))
                {
                    shooting = false;
                    if (gameTime.TotalGameTime - previousFireTime > fireTime)
                    {
                        // uzliek pçdçjo ðauðanas laiku, kad izðauj
                        previousFireTime = gameTime.TotalGameTime;
                        shooting = true;

                        // izveido lodi, kas lido uz priekðu
                        AddBulletsForward(player.Position + new Vector2(50, -37));
                        shootingSound.Play();
                        player.bulletCount -= 1;
                    }
                }
            }
            
        }

        // izveido policijas maðînu
        private void AddPolice()
        {
            Animation policeAnimation = new Animation();
            policeAnimation.Initialize(policeTexture, Vector2.Zero, 336, 119, 3, 30, Color.White, 1f, true);

            //random pozîcijas ìenerçðana
            Vector2 position = new Vector2(-policeTexture.Width / 2, random.Next(340, GraphicsDevice.Viewport.Height - 140));

            Police policeCar = new Police();
            policeCar.Initialize(policeAnimation, position);

            police.Add(policeCar);
        }

        //Update police
        private void UpdatePolice(GameTime gameTime)
        {
            if (police.Count < 3) // ja esoðâs policijas maðînas nav vairâk kâ 3
            {
                if (gameTime.TotalGameTime - policePreviousSpawnTime > policeSpawnTime)
                {
                    policePreviousSpawnTime = gameTime.TotalGameTime;

                    AddPolice(); // pievieno policijas maðînu
                    policeSpawn.Play(); //atskaòo sirçnas
                    policeSiren.Play(); //atskaòo sirçnas
                }
            }
            

            // polocista maðînas braukðanas AI
            for (int i = police.Count - 1; i >= 0; i--)
            {
                if (police[i].Position.X > player.Position.X + player.Width - 5) //ja cop ir priekðâ player
                {
                    police[i].MoveSpeed = -1; //cop iet uz atpakaïu

                    if (police[i].Position.Y > player.Position.Y) //ja cop ir zemak par player
                        police[i].Position.Y -= 2; //cop iet uz augshu
                    else //ja cop ir augstak par player
                        police[i].Position.Y += 2; //cop iet uz leju
                }
                else //ja cop nav priekshaa player
                    police[i].MoveSpeed = 3; //tuprina braukt uz priekshu


                police[i].Update(gameTime);

                if (police[i].Active == false) //ja policijas maðîna ir neaktîva
                {
                    if (police[i].Health <= 0)
                    {
                        explosionSound.Play();
                        player.score += police[i].Value;
                        AddExplosion(police[i].Position);
                        AddExplosion(police[i].Position + new Vector2(60, 0));
                        AddExplosion(police[i].Position + new Vector2(-60, 0));
                        AddExplosion(police[i].Position + new Vector2(0, -20));
                        player.bulletCount += 10;
                    }
                    police.RemoveAt(i);
                }
                
            }
        }

        // izveido parasto maðînu
        private void AddCars()
        {
            //2 veidu maðînas
            Animation carsAnimation = new Animation();
            Animation carsAnimation2 = new Animation();

            carsAnimation.Initialize(carTexture, Vector2.Zero, 297, 103, 3, 30, Color.White, 1f, true);
            carsAnimation2.Initialize(carTexture2, Vector2.Zero, 341, 119, 3, 30, Color.White, 1f, true);

            // random pozîcijas ìenerçðana
            Vector2 position = new Vector2(GraphicsDevice.Viewport.Width + carTexture.Width / 2, random.Next(340, GraphicsDevice.Viewport.Height - 140));

            Car car = new Car();

            // randomâ ìenerç vienu no 2 veidu maðînâm 50/50
            int sk = random.Next(1, 3);
            if (sk == 1) car.Initialize(carsAnimation, position);
            else car.Initialize(carsAnimation2, position);
            
            cars.Add(car);
        }


        //update cars
        private void UpdateCars(GameTime gameTime)
        {
            // ìenerç pçc laika
            if (gameTime.TotalGameTime - carsPreviousSpawnTime > carsSpawnTime)
            {
                carsPreviousSpawnTime = gameTime.TotalGameTime;
                AddCars();
            }

            for (int i = cars.Count - 1; i >= 0; i--)
            {
                cars[i].Update(gameTime);

                if (cars[i].Active == false)
                {
                    if (cars[i].Health <= 0)
                    {
                        explosionSound.Play();
                        player.score -= 50;
                        AddExplosion(cars[i].Position);
                        AddExplosion(cars[i].Position + new Vector2(60, 0));
                        AddExplosion(cars[i].Position + new Vector2(-60, 0));
                        AddExplosion(cars[i].Position + new Vector2(0, -20));
                    }
                    cars.RemoveAt(i);
                }
            }
        }

        private void UpdateCollision()
        {

            //objektu râmji
            Rectangle rectanglePlayer;
            Rectangle rectanglePolice;
            Rectangle rectanglePolice2;
            Rectangle rectangleCar;
            Rectangle rectangleBullet;

            //player râmis
            rectanglePlayer = new Rectangle((int)player.Position.X,
            (int)player.Position.Y+50,
            player.Width,
            player.Height-90);

            // player vs cop
            for (int i = 0; i < police.Count; i++)
            {
               
                rectanglePolice = new Rectangle((int)police[i].Position.X,
                (int)police[i].Position.Y,
                police[i].Width,
                police[i].Height);

                if (rectanglePlayer.Intersects(rectanglePolice)) 
                {
                    player.Health -= police[i].Damage;
                    if (police[i].Position.Y > player.Position.Y) // ja cop ir zemak par player
                    {
                        if (police[i].Position.Y + police[i].Height < 710) //ja cop nav parak zemu
                        {
                            police[i].Position.Y += 2; //cop iet uz leju
                        }
                        
                    }
                    else //ja cop augstak par player
                    {
                        if (police[i].Position.Y > 350) //ja cop nav parak augstu
                        {
                            police[i].Position.Y -= 2; //cop iet uz augshu
                        }
                    }
                    if (police[i].Position.X < player.Position.X) //ja cop ir aizmugure player
                    {
                        police[i].Position.X -= 10; // cop iet uz atpakalu
                    }
                    else //ja cop ir prieksha player
                    {
                        police[i].Position.X += 10; //cop iet uz priekshu
                    }


                    if (player.Health <= 0)
                    {
                        player.Active = false;
                        gameover.Play();
                    }
                }


                
                for (int j = 0; j < cars.Count; j++)
                {
                    rectangleCar = new Rectangle((int)cars[j].Position.X,
                    (int)cars[j].Position.Y,
                    cars[j].Width,
                    cars[j].Height);

                    //car vs player
                    if (rectangleCar.Intersects(rectanglePlayer))
                    {
                        player.Health -= cars[j].Damage;
                        if (player.Position.Y >= cars[j].Position.Y) // ja player ir zemak par car
                        {
                            if (cars[j].Position.Y > 350) //car nav parak augstu
                            {
                                cars[j].Position.Y -= 2; //car iet uz augshu
                            }
                            else //ja ir parak augstu
                            {
                                cars[j].Position.Y += 2; //car iet uz leju
                            }
                        }
                        else // ja player ir augstak par car
                        {
                            if (cars[j].Position.Y + cars[j].Height < 710) //car nav parak zemu
                            {
                                cars[j].Position.Y += 2; //car iet uz leju
                            }
                            else //ja ir parak zemu
                            {
                                cars[j].Position.Y -= 2; //car iet uz augðu
                            }
                        }
                    }

                    //car vs cop
                    if (rectangleCar.Intersects(rectanglePolice))
                    {
                        if (police[i].Position.Y >= cars[j].Position.Y) // ja police ir zemak par car
                        {
                            if (police[i].Position.Y + police[i].Height < 710) //police nav parak zemu
                            {
                                police[i].Position.Y += 2; //police iet uz leju
                                if (cars[j].Position.Y > 350) //ja car ir parak zemu
                                    cars[j].Position.Y -= 2; //car iet uz augðu
                            }
                            else //ja police ir parak zemu
                            {
                                police[i].Position.Y -= 2; //police iet uz augshu
                            }
                        }
                        else // ja police ir augstak par car
                        {
                            if (police[i].Position.Y > 350) //ja police nav parak augstu
                            {
                                police[i].Position.Y -= 2; //police iet uz augðu
                                if (cars[j].Position.Y + cars[j].Height < 710) //ja car nav parak zemu
                                    cars[j].Position.Y += 2; //car iet uz leju
                            }
                            else //ja  police ir parak augstu
                            {
                                police[i].Position.Y += 2; //police iet uz leju
                            }
                        }
                    }
                }

                for (int k = 1; k < police.Count; k++)
                {
                    rectanglePolice2 = new Rectangle((int)police[k].Position.X,
                    (int)police[k].Position.Y,
                    police[k].Width,
                    police[k].Height);

                    //cop vs cop
                    if (rectanglePolice2.Intersects(rectanglePolice))
                    {
                        if (police[i].Position.Y >= police[k].Position.Y) // ja police ir zemak par car
                        {
                            if (police[i].Position.Y + police[i].Height < 710) //police nav parak zemu
                            {
                                police[i].Position.Y += 2; //police iet uz leju
                                if (police[k].Position.Y > 350) //ja car ir parak zemu
                                    police[k].Position.Y -= 2; //car iet uz augðu
                            }
                            else //ja police ir parak zemu
                            {
                                police[i].Position.Y -= 2; //police iet uz augshu
                            }
                        }
                        else // ja police ir augstak par car
                        {
                            if (police[i].Position.Y > 350) //ja police nav parak augstu
                            {
                                police[i].Position.Y -= 2; //police iet uz augðu
                                if (police[k].Position.Y + police[k].Height < 710) //ja car nav parak zemu
                                    police[k].Position.Y += 2; //car iet uz leju
                            }
                            else //ja  police ir parak augstu
                            {
                                police[i].Position.Y += 2; //police iet uz leju
                            }
                        }
                    }
                }

                

            }
            for (int i = 0; i < bullets.Count; i++)
            {
                for (int j = 0; j < police.Count; j++)
                {
                    for (int k = 0; k < cars.Count; k++)
                    {

                        // lodes sadursmes
                        rectangleBullet = new Rectangle((int)bullets[i].Position.X -
                        bullets[i].Width / 2, (int)bullets[i].Position.Y -
                        bullets[i].Height / 2, bullets[i].Width, bullets[i].Height);

                        rectanglePolice = new Rectangle((int)police[j].Position.X - police[j].Width / 2,
                        (int)police[j].Position.Y - police[j].Height / 2,
                        police[j].Width, police[j].Height);

                        rectangleCar = new Rectangle((int)cars[k].Position.X -
                        cars[k].Width / 2, (int)cars[k].Position.Y -
                        cars[k].Height / 2, cars[k].Width, cars[k].Height);

                        // lode vs cop
                        if (rectangleBullet.Intersects(rectanglePolice))
                        {
                            police[j].Health -= bullets[i].Damage;
                            bullets[i].Active = false;
                        }

                        //lode vs car
                        if (rectangleBullet.Intersects(rectangleCar))
                        {
                            cars[k].Health -= bullets[i].Damage;
                            cars[k].carMoveSpeed = 12;
                            if (!cars[k].shot)
                            {
                                tireScreetchSound.Play();
                                cars[k].shot = true;
                            }
                            
                            bullets[i].Active = false;
                        }

                    }
                }
            }
        }

        //Add uz atpakaïu
        private void AddBulletsBackward(Vector2 position)
        {
            Bullets bullet = new Bullets();
            bullet.Initialize(GraphicsDevice.Viewport, bulletTexture, position);
            bullets.Add(bullet);
        }

        //Add uz priekðu
        private void AddBulletsForward(Vector2 position)
        {
            Bullets bullet = new Bullets();
            bullet.Initialize(GraphicsDevice.Viewport, bulletTexture, position);
            bullet.bulletMoveSpeed = -20f;
            bullets.Add(bullet);
        }

        // Update lodes
        private void UpdateBullets()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i].Update();

                if (bullets[i].Active == false)
                {
                    bullets.RemoveAt(i);
                }
            }
        }

        // Add sprâdziens
        private void AddExplosion(Vector2 position)
        {
            Animation explosion = new Animation();
            explosion.Initialize(explosionTexture, position, 134, 134, 12, 45, Color.White, 1f, false);
            explosions.Add(explosion);
        }

        //Update sprâdziens
        private void UpdateExplosions(GameTime gameTime)
        {
            for (int i = explosions.Count - 1; i >= 0; i--)
            {
                explosions[i].Update(gameTime);
                if (explosions[i].Active == false)
                {
                    explosions.RemoveAt(i);
                }
            }
        }

        //zîmç ðâvçju uz atpakaïu
        private void DrawGangsta(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(thugTexture, player.Position, null, Color.White, 0f, new Vector2(thugTexture.Width / 2 + 10, thugTexture.Height / 2 + 35), 1f, SpriteEffects.None, 0f);
        }
        //zîmç ðâvçju uz priekðu
        private void DrawGangstaForward(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(thugTextureForward, player.Position, null, Color.White, 0f, new Vector2(thugTextureForward.Width / 2 - 30, thugTextureForward.Height / 2 + 35), 1f, SpriteEffects.None, 0f);
        }
        //zîmç ðâvçju uz atpakaïu
        private void DrawGunshotBackward(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gunshotTexture, player.Position, null, Color.White, 0f, new Vector2(gunshotTexture.Width / 2 + 55, gunshotTexture.Height / 2 + 37), 1f, SpriteEffects.None, 0f);
        }
        //zîmç ðâvçju uz priekðu
        private void DrawGunshotForward(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gunshotTexture, player.Position, null, Color.White, 0f, new Vector2(gunshotTexture.Width / 2 - 73, gunshotTexture.Height / 2 + 37), 1f, SpriteEffects.None, 0f);
        }
        //zîmç beigu ekrânu
        private void DrawGameOver(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameOver, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);
        }

        // atskaòo mûziku
        private void PlayMusic(Song song)
        {
            try
            {
                MediaPlayer.Play(song);
                MediaPlayer.IsRepeating = true;
            }
            catch { }
        }

        // pârstâj atskaòot mûziku
        private void StopMusic(Song song)
        {
            try
            {
                MediaPlayer.Stop();
            }
            catch { }
        }

        // Saglabâ reultâtu DB
        private void SaveScore(Player player, int score)
        {
            busted = false;
            ThugLifeDBEntities5 db = new ThugLifeDBEntities5();

            Score newScore = new Score();
            newScore.ID_Player = player.ID_Player;
            newScore.GameScore = score;

            db.Score.AddObject(newScore);
            db.SaveChanges();
            
        }


        //Zîmç visu
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(); // Sâk zîmçt

            if (player.Health > 0)
            {
                spriteBatch.Draw(skies, Vector2.Zero, Color.White);

                // fona slâòi
                skiesLayer1.Draw(spriteBatch);
                skiesLayer2.Draw(spriteBatch);
                buildings.Draw(spriteBatch);
                road.Draw(spriteBatch);
                ground.Draw(spriteBatch);

                // policija
                for (int i = 0; i < police.Count; i++)
                {
                    police[i].Draw(spriteBatch);

                }

                // parastâs maðînas
                for (int i = 0; i < cars.Count; i++)
                {
                    cars[i].Draw(spriteBatch);
                }

                //player
                player.Draw(spriteBatch);
                
                //lodes
                for (int i = 0; i < bullets.Count; i++)
                {
                    bullets[i].Draw(spriteBatch);
                }

                // ðâvçjs un ðauðanas effekts
                if (currentKeyboardState.IsKeyDown(Keys.Left))
                {
                    DrawGangsta(spriteBatch);
                    if (shooting) DrawGunshotBackward(spriteBatch);
                }
                if (currentKeyboardState.IsKeyDown(Keys.Right))
                {
                    DrawGangstaForward(spriteBatch);
                    if (shooting) DrawGunshotForward(spriteBatch);
                }

                // sprâdzieni
                for (int i = 0; i < explosions.Count; i++)
                {
                    explosions[i].Draw(spriteBatch);
                }

                barrier.Draw(spriteBatch); //zîmç metâla barjeru priekðâ

                // spçles dati stûrî
                spriteBatch.DrawString(font, "Score: " + player.score, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);
                spriteBatch.DrawString(font, "Health: " + player.Health, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + 30), Color.White);
                spriteBatch.DrawString(font, "Bullets: " + player.bulletCount, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + 60), Color.White);
                spriteBatch.DrawString(font, "Name: " + newPlayer.Username.Trim(), new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X+200, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);
            }
            if (player.Health <= 0) //ja spçlçtâja hp=0 zîmç beigu ekrânu
            {
                StopMusic(gameplayMusic);
                DrawGameOver(spriteBatch);
                spriteBatch.DrawString(font, "Score: " + player.score, new Vector2(gameOver.Width / 2 - 100,gameOver.Height / 2), Color.Red);
                spriteBatch.DrawString(font, "Press space to play again", new Vector2(gameOver.Width / 2 - 200, gameOver.Height / 2 + 30), Color.White);
                busted = false;
                //ja nospieþ SPACE atsâk spçli
                if (currentKeyboardState.IsKeyDown(Keys.Space))
                {
                    SaveScore(newPlayer, player.score);
                    player.Health = 100;
                    player.score = 0;
                    player.bulletCount = 50;
                    police.Clear();
                    cars.Clear();
                    bullets.Clear();
                }
            }
            
            spriteBatch.End(); //beidz zîmçt
            base.Draw(gameTime);
        }
    }
}
