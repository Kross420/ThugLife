using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace ThugLife
{
    class ParallaxingBackground
    {

        Texture2D texture; // fona bilde
        Vector2[] positions; // poz�ciju mas�vs
        int speed; // kust�bas �trums

        //uzst�da s�kuma nosac�jumus p�c patodaj� v�rt�b�m
        public void Initialize(ContentManager content, String texturePath, int screenWidth, int speed)
        {
            texture = content.Load<Texture2D>(texturePath); //iel�d� fona bildi
            this.speed = speed; 
            // izdala ekr�na platumu ar bildes platumu, lai zin�tu, cik bildes reiz� att�lot
            positions = new Vector2[screenWidth / texture.Width + 1]; //pieskaita +1, lai bildes p�rkl�tos bez caurumiem
            
            //uzst�da s�kuma poz�cijas
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = new Vector2(i * texture.Width, 0);// bilde, liek vienu aiz otras
            }
        }

        public void Update()
        {
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i].X += speed; //kust�s
                if (speed <= 0) //ja �trums negat�vs fons kust�s pa kreisi
                {
                    if (positions[i].X <= -texture.Width) // ja bilde ir �rpus loga, t� tiek p�rlikta uz s�kumu
                    {
                        positions[i].X = texture.Width * (positions.Length - 1); 
                    }
                }
                else //ja �trums nav negat�vs fons kust�s pa labi
                {
                    if (positions[i].X >= texture.Width * (positions.Length - 1))  // ja bilde ir �rpus loga, t� tiek p�rlikta uz s�kumu
                    {
                        positions[i].X = -texture.Width;
                    }
                }
            }
        }

        //z�m�
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                spriteBatch.Draw(texture, positions[i], Color.White);
            }
        }
    }
}
