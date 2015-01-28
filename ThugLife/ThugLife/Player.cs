using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThugLife
{
    class Player
    {

        // Animation representing the player
        public Animation PlayerAnimation; //Spēlētāja bilde
        public Vector2 Position; // pozīcija
        public bool Active; // stāvoklis
        public int Health; // dzīvības
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
        }

        // Update the player animation
        public void Update(GameTime gameTime)
        {
            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerAnimation.Draw(spriteBatch);
        }
    }
}
