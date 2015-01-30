using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThugLife
{
    class Bullets
    {
        public Texture2D Texture; // lodes bilde
        public Vector2 Position; // pozīcija
        public bool Active; // sāvoklis
        public int Damage; // cik hp atņem sadursmēs
        public float bulletMoveSpeed; // lodes kustības ātrums
        Viewport viewport; // redzamais spēles rāmis

        // iegūst platumu
        public int Width
        {
            get { return Texture.Width; }
        }

        // iegūst garumu
        public int Height
        {
            get { return Texture.Height; }
        }

        // sākuma uzstādījumi no padotajiem parametriem
        public void Initialize(Viewport viewport, Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position; 
            Active = true;
            Damage = 20;
            bulletMoveSpeed = 20f;
            this.viewport = viewport;
        }
        public void Update()
        {
            Position.X -= bulletMoveSpeed; //kustās pa kreisi
            if ((Position.X + Texture.Width / 2 > viewport.Width) || (Position.X + Texture.Width / 2 < 0)) //ja lode iziet no spēles rāmja
                Active = false; //lode paliek neaktīva
        }

        //zīmē
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f,new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f);
        }
    }
}
