using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ThugLife
{
    class Police
    {
        public Animation PoliceAnimation;  // policijas maðînas animâcija
        public Vector2 Position; // pozîcija
        public bool Active; // stâvoklis
        public int Health; // dzîvîbas
        public int Damage; // cik dzîvîas atòem sadursmçs
        public int Value; // puntku skaits, ko var iegût spçlçtâjs
        public float MoveSpeed; //kustîbas âtrums

        // iegûst animâcijas platumu
        public int Width
        {
            get { return PoliceAnimation.FrameWidth; }
        }
        // iegûst animâcijas garumu
        public int Height
        {
            get { return PoliceAnimation.FrameHeight; }
        }

        //Uzstâda sâkuma nosacîjumus
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

        //atjauno animâcijas pozîciju
        public void Update(GameTime gameTime)
        {
            Position.X += MoveSpeed;
            PoliceAnimation.Position = Position;
            PoliceAnimation.Update(gameTime);
            if (Position.X > 1024+Width || Health <= 0) //ja ir ârpus râmja vai hp ir 0
            {
                Active = false; //paliek neaktîvs
            }
        }

        //zîmç
        public void Draw(SpriteBatch spriteBatch)
        {
            PoliceAnimation.Draw(spriteBatch);
        }
    }
}
