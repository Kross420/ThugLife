using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThugLife
{
    class PlayerCar
    {
        public Animation PlayerAnimation; //Spēlētāja bilde
        public Vector2 Position; // pozīcija
        public bool Active; // stāvoklis
        public int Health; // dzīvības
        public int score; //punkti
        public int bulletCount; //ložu skaits

        // iegūst bildes izmērus
        public int Width
        {
            get { return PlayerAnimation.FrameWidth; }
        }
        public int Height
        {
            get { return PlayerAnimation.FrameHeight; }
        }


        //uzstāda sākuma parametrus
        public void Initialize(Animation animation, Vector2 position)
        {
            PlayerAnimation = animation;
            Position = position;
            Active = true;
            Health = 100;
            bulletCount = 50;
            score = 0;
        }

        //atjauno player animācijas pozīciju
        public void Update(GameTime gameTime)
        {
            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime);
        }

        //zīmē player animāciju
        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerAnimation.Draw(spriteBatch);
        }
    }
}
