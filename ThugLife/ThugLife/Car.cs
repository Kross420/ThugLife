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
    class Car
    {
        public Animation CarAnimation; // parast�s ma��nas anim�cija
        public Vector2 Position; // poz�cija
        public bool Active; // st�voklis
        public int Health; // dz�v�bas
        public int Damage;  // cik hp at�em sadursm�s
        public int Value; // punktu skaits, cik tas iedos player
        public float carMoveSpeed; // p�rvieto�an�� �trums
        public bool shot; //vai ir ie�auts

        // ieg�st platumu
        public int Width
        {
            get { return CarAnimation.FrameWidth; }
        }

        // ieg�st garumu
        public int Height
        {
            get { return CarAnimation.FrameHeight; }
        }

        // s�kuma nosac�jumi p�c padotaj�m v�rt�b�m
        public void Initialize(Animation animation, Vector2 position)
        {
            CarAnimation = animation;
            Position = position;
            Active = true;
            Health = 80;
            Damage = 1;
            carMoveSpeed = 2f;
            Value = 100;
            shot = false;
        }

        public void Update(GameTime gameTime)
        {
            Position.X -= carMoveSpeed; //kust�s pa kreisi
            CarAnimation.Position = Position; //anim�cijas poz�cija
            CarAnimation.Update(gameTime); //atjauno anim�ciju

            if (Position.X < -Width || Health <= 0) //ja poz�cija ir �rpus ekr�ja, vai dz�v�bas ir 0
            {
                Active = false; //paliek neakt�va
            }
        }

        //z�m�
        public void Draw(SpriteBatch spriteBatch)
        {
            CarAnimation.Draw(spriteBatch);
        }
    }
}
