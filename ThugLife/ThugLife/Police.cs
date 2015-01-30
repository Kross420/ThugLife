using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ThugLife
{
    class Police
    {
        public Animation PoliceAnimation;  // policijas ma��nas anim�cija
        public Vector2 Position; // poz�cija
        public bool Active; // st�voklis
        public int Health; // dz�v�bas
        public int Damage; // cik dz�v�as at�em sadursm�s
        public int Value; // puntku skaits, ko var ieg�t sp�l�t�js
        public float MoveSpeed; //kust�bas �trums

        // ieg�st anim�cijas platumu
        public int Width
        {
            get { return PoliceAnimation.FrameWidth; }
        }
        // ieg�st anim�cijas garumu
        public int Height
        {
            get { return PoliceAnimation.FrameHeight; }
        }

        //Uzst�da s�kuma nosac�jumus
        public void Initialize(Animation animation, Vector2 position)
        {
            PoliceAnimation = animation;
            Position = position;
            Active = true;
            Health = 100;
            Damage = 1;
            MoveSpeed = 3f;
            Value = 100;
        }

        //atjauno anim�cijas poz�ciju
        public void Update(GameTime gameTime)
        {
            Position.X += MoveSpeed;
            PoliceAnimation.Position = Position;
            PoliceAnimation.Update(gameTime);
            if (Position.X > 1024+Width || Health <= 0) //ja ir �rpus r�mja vai hp ir 0
            {
                Active = false; //paliek neakt�vs
            }
        }

        //z�m�
        public void Draw(SpriteBatch spriteBatch)
        {
            PoliceAnimation.Draw(spriteBatch);
        }
    }
}
