using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ThugLife
{
    class Animation
    {
        Texture2D spriteStrip; //animacijas strip bilde
        float scale; // m�rogs
        int elapsedTime; // laiks, kas pag�jis, kop� p�d�ja update
        int frameTime; // laiks, cik ilgi att�lo vienu frame pirms n�ko��
        int frameCount; // animacijas frame skaits
        int currentFrame; // pashreizejaa frame index
        Color color; // frame kr�sa
        Rectangle sourceRect = new Rectangle(); // bildes laukums, kuru att�lo
        Rectangle destinationRect = new Rectangle(); // laukums, kur att�lot bildi
        public int FrameWidth; // frame platums
        public int FrameHeight; // frame garums
        public bool Active; // frame st�voklis
        public bool Looping; // nosaka, vai anim�cija atk�rtosies
        public Vector2 Position; // poz�cija

        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frametime, Color color, float scale, bool looping)
        {
            // lok�la kopija no padotajiem
            this.color = color;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frametime;
            this.scale = scale;

            Looping = looping;
            Position = position;
            spriteStrip = texture;

            // uzst�da laiku 0
            elapsedTime = 0;
            currentFrame = 0;

            Active = true; // s�kum� p�c noklus�juma anim�cija ir akt�va
        }

        public void Update(GameTime gameTime)
        {
            // nav update, ja animacija ir neaktiva
            if (Active == false)
                return;

            // pag�ju�� laika update
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime > frameTime) // ja pag�ju�ais laiks ir liel�ks par 1 frame laiku
            {
                currentFrame++; //nomaina frame un n�ko�o
                if (currentFrame == frameCount)// ja pa�reiz�jais frame index ir vien�ds p�d�j� frame index
                {
                    currentFrame = 0; //uzliek atkal pirmo frame
                    if (Looping == false) //ja loop ir false
                        Active = false; //anim�cija paliek neakt�va un neatk�rtojas
                }
                elapsedTime = 0; // uzst�da pag�ju�o laiku atkal uz 0
            }


            sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight); // pa�em pareizo strip bildes gabalu
            destinationRect = new Rectangle((int)Position.X - (int)(FrameWidth * scale) / 2, // uztaisa laukumu, kur ielikt bildes gabalu
            (int)Position.Y - (int)(FrameHeight * scale) / 2,
            (int)(FrameWidth * scale),
            (int)(FrameHeight * scale));
        }

        //z�m�
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active) // tikai, tad, ja ir akt�va anim�cija
            {
                spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, color);
            }
        }
    }
}
