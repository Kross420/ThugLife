using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ThugLife
{
    class Animation
    {
        Texture2D spriteStrip; //animacijas strip bilde
        float scale; // mçrogs
        int elapsedTime; // laiks, kas pagâjis, kopð pçdçja update
        int frameTime; // laiks, cik ilgi attçlo vienu frame pirms nâkoðâ
        int frameCount; // animacijas frame skaits
        int currentFrame; // pashreizejaa frame index
        Color color; // frame krâsa
        Rectangle sourceRect = new Rectangle(); // bildes laukums, kuru attçlo
        Rectangle destinationRect = new Rectangle(); // laukums, kur attçlot bildi
        public int FrameWidth; // frame platums
        public int FrameHeight; // frame garums
        public bool Active; // frame stâvoklis
        public bool Looping; // nosaka, vai animâcija atkârtosies
        public Vector2 Position; // pozîcija

        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frametime, Color color, float scale, bool looping)
        {
            // lokâla kopija no padotajiem
            this.color = color;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frametime;
            this.scale = scale;

            Looping = looping;
            Position = position;
            spriteStrip = texture;

            // uzstâda laiku 0
            elapsedTime = 0;
            currentFrame = 0;

            Active = true; // sâkumâ pçc noklusçjuma animâcija ir aktîva
        }

        public void Update(GameTime gameTime)
        {
            // nav update, ja animacija ir neaktiva
            if (Active == false)
                return;

            // pagâjuðâ laika update
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime > frameTime) // ja pagâjuðais laiks ir lielâks par 1 frame laiku
            {
                currentFrame++; //nomaina frame un nâkoðo
                if (currentFrame == frameCount)// ja paðreizçjais frame index ir vienâds pçdçjâ frame index
                {
                    currentFrame = 0; //uzliek atkal pirmo frame
                    if (Looping == false) //ja loop ir false
                        Active = false; //animâcija paliek neaktîva un neatkârtojas
                }
                elapsedTime = 0; // uzstâda pagâjuðo laiku atkal uz 0
            }


            sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight); // paòem pareizo strip bildes gabalu
            destinationRect = new Rectangle((int)Position.X - (int)(FrameWidth * scale) / 2, // uztaisa laukumu, kur ielikt bildes gabalu
            (int)Position.Y - (int)(FrameHeight * scale) / 2,
            (int)(FrameWidth * scale),
            (int)(FrameHeight * scale));
        }

        //zîmç
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active) // tikai, tad, ja ir aktîva animâcija
            {
                spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, color);
            }
        }
    }
}
