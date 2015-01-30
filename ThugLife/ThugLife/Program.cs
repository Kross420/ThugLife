using System;
using System.Windows.Forms;

namespace ThugLife
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            XNASignIn signinDialog = new XNASignIn();
            DialogResult result = DialogResult.Abort;

            while (result == DialogResult.Abort)
            {
                result = signinDialog.ShowDialog();

                if (result == DialogResult.Abort)
                    MessageBox.Show("You entered the wrong username and/or password");
            }

            if (result == DialogResult.Cancel)
                MessageBox.Show("You cancelled the login, the game will exit");
            else if (result == DialogResult.OK)
            {
                using (Game1 game = new Game1(signinDialog.newPlayer))
                {
                    game.Run();
                }
            }
        }
    }
#endif
}

