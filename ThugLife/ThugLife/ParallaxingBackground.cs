using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace ThugLife
{
    class ParallaxingBackground
    {

        Texture2D texture; // fona bilde
        Vector2[] positions; // pozîciju masîvs
        int speed; // kustîbas âtrums

        //uzstâda sâkuma nosacîjumus pçc patodajâ vçrtîbâm
        public void Initialize(ContentManager content, String texturePath, int screenWidth, int speed)
        {
            texture = content.Load<Texture2D>(texturePath); //ielâdç fona bildi
            this.speed = speed; 
            // izdala ekrâna platumu ar bildes platumu, lai zinâtu, cik bildes reizç attçlot
            positions = new Vector2[screenWidth / texture.Width + 1]; //pieskaita +1, lai bildes pârklâtos bez caurumiem
            
            //uzstâda sâkuma pozîcijas
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = new Vector2(i * texture.Width, 0);// bilde, liek vienu aiz otras
            }
        }

        public void Update()
        {
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i].X += speed; //kustâs
                if (speed <= 0) //ja âtrums negatîvs fons kustâs pa kreisi
                {
                    if (positions[i].X <= -texture.Width) // ja bilde ir ârpus loga, tâ tiek pârlikta uz sâkumu
                    {
                        positions[i].X = texture.Width * (positions.Length - 1); 
                    }
                }
                else //ja âtrums nav negatîvs fons kustâs pa labi
                {
                    if (positions[i].X >= texture.Width * (positions.Length - 1))  // ja bilde ir ârpus loga, tâ tiek pârlikta uz sâkumu
                    {
                        positions[i].X = -texture.Width;
                    }
                }
            }
        }

        //zîmç
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                spriteBatch.Draw(texture, positions[i], Color.White);
            }
        }
    }
}
