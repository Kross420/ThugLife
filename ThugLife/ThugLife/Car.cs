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
        public Animation CarAnimation; // parastâs maðînas animâcija
        public Vector2 Position; // pozîcija
        public bool Active; // stâvoklis
        public int Health; // dzîvîbas
        public int Damage;  // cik hp atòem sadursmçs
        public int Value; // punktu skaits, cik tas iedos player
        public float carMoveSpeed; // pârvietoðanâð âtrums
        public bool shot; //vai ir ieðauts

        // iegûst platumu
        public int Width
        {
            get { return CarAnimation.FrameWidth; }
        }

        // iegûst garumu
        public int Height
        {
            get { return CarAnimation.FrameHeight; }
        }

        // sâkuma nosacîjumi pçc padotajâm vçrtîbâm
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
            Position.X -= carMoveSpeed; //kustâs pa kreisi
            CarAnimation.Position = Position; //animâcijas pozîcija
            CarAnimation.Update(gameTime); //atjauno animâciju

            if (Position.X < -Width || Health <= 0) //ja pozîcija ir ârpus ekrâja, vai dzîvîbas ir 0
            {
                Active = false; //paliek neaktîva
            }
        }

        //zîmç
        public void Draw(SpriteBatch spriteBatch)
        {
            CarAnimation.Draw(spriteBatch);
        }
    }
}
