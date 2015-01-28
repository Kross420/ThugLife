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

        public Texture2D PlayerImage; //Spēlētāja bilde
        Vector2 Position; // pozīcija
        public bool Active; // stāvoklis
        public int Health; // dzīvības
        // iegūst bildes izmērus
        public int Width
        {
            get { return PlayerImage.Width; }
        }
        public int Height
        {
            get { return PlayerImage.Height; }
        }


        //uzstāda sākuma parametrus
        public void Initialize(Texture2D image, Vector2 position)
        {
            PlayerImage = image;
            Position = position;
            Active = true;
            Health = 100;
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayerImage, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
