using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace pokemonSummative
{
    public partial class DexScreen : UserControl
    {
        public static string pokemon;
        int sceneCounter = 0;
        int pokeIndex = 0;
        bool startUp = true;
        string[] dexNumber = new []{ "004", "007", "001" };
        string[] heights = new[] { "2'00''", "1'08''", "2'04\"" };
        string[] weights = new[] { "19.0", "20.0", "15.0" };
        string[] types = new[] { "LIZARD", "TINYTURTLE", "SEED" };
        Image[] sprites = new[] {Properties.Resources.blueSpriteIntro};
        string[] dex = new[] { "Obvioulsy prefers", "hot places. When", "it rains, steam", "is said to spout", "from the tip of", "its tail.",
            "After birth, its", "back swells and", "hardens into a", "shell. Powerfully", "sprays foam from", "its mouth.",
            "A strange seed was", "planted on its", "back as birth.", "The plant sprouts", "and grows with", "this POKEMON." };
        Font pokeFont = new Font("Pokemon GB", 12);

        public DexScreen()
        {
            InitializeComponent();
            OnStart();
        }

        public void OnStart()
        {
            if(pokemon == "CHARMANDER")
            {
                pokeIndex = 0;
            }
            else if(pokemon == "SQUIRTLE")
            {
                pokeIndex = 1;
            }
            else if (pokemon == "BULBASAUR")
            {
                pokeIndex = 2;
            }            
        }

        private void DexScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                if (sceneCounter == 3)
                {
                    //GameScreen.gameTimer.Start();
                    //Form f = this.FindForm();
                    //GameScreen gs = new GameScreen();
                    //f.Controls.Remove(this);
                    //f.Controls.Add(gs);
                    this.Parent.Controls.Remove(this);

                    //f.ActiveMdiChild = GameScreen;
                   // f.ActiveMdiChild.;
                    //returnToGame = true;
                }
                else
                {
                    sceneCounter+=3;
                    Refresh();
                }

            }
        }

        public void timer()
        {
            Refresh();
        }

        private void DexScreen_Paint(object sender, PaintEventArgs e)
        {
            if (startUp)
            {
                e.Graphics.DrawString(pokemon, pokeFont, Brushes.Black, new Point(100, 50));
                e.Graphics.DrawString(types[pokeIndex], pokeFont, Brushes.Black, new Point(100, 70));
                e.Graphics.DrawString("No. " + dexNumber[pokeIndex], pokeFont, Brushes.Black, new Point(100, 90));
                e.Graphics.DrawString("HT   ?'??''", pokeFont, Brushes.Black, new Point(100, 110));
                e.Graphics.DrawString("WT   ???lb", pokeFont, Brushes.Black, new Point(100, 130));
                startUp = false;
            }
            else
            {
                e.Graphics.DrawString(pokemon, pokeFont, Brushes.Black, new Point(100, 50));
                e.Graphics.DrawString(types[pokeIndex], pokeFont, Brushes.Black, new Point(100, 70));
                e.Graphics.DrawString("No. " + dexNumber[pokeIndex], pokeFont, Brushes.Black, new Point(100, 90));
                e.Graphics.DrawString("HT  " + heights[pokeIndex], pokeFont, Brushes.Black, new Point(100, 110));
                e.Graphics.DrawString("WT  " + weights[pokeIndex] + "lb", pokeFont, Brushes.Black, new Point(100, 130));
                if (pokemon == "CHARMANDER")
                {
                    e.Graphics.DrawString(dex[sceneCounter], pokeFont, Brushes.Black, new Point(100, 150));
                    e.Graphics.DrawString(dex[sceneCounter+1], pokeFont, Brushes.Black, new Point(100, 170));
                    e.Graphics.DrawString(dex[sceneCounter+2], pokeFont, Brushes.Black, new Point(100, 190));
                }
                else if (pokemon == "SQUIRTLE")
                {
                    e.Graphics.DrawString(dex[sceneCounter+6], pokeFont, Brushes.Black, new Point(100, 150));
                    e.Graphics.DrawString(dex[sceneCounter + 7], pokeFont, Brushes.Black, new Point(100, 170));
                    e.Graphics.DrawString(dex[sceneCounter + 8], pokeFont, Brushes.Black, new Point(100, 190));
                }
                else
                {
                    e.Graphics.DrawString(dex[sceneCounter+12], pokeFont, Brushes.Black, new Point(100, 150));
                    e.Graphics.DrawString(dex[sceneCounter + 13], pokeFont, Brushes.Black, new Point(100, 170));
                    e.Graphics.DrawString(dex[sceneCounter + 14], pokeFont, Brushes.Black, new Point(100, 190));
                }
            }

            int dexSize = 36, lineWidth = 10;

            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(0, 300 + dexSize/2 - lineWidth/2, this.Width, lineWidth));

            e.Graphics.DrawImage(Properties.Resources.dexSquare, 50, 300, dexSize, dexSize);
            e.Graphics.DrawImage(Properties.Resources.dexSquare, 100, 300, dexSize, dexSize);
            e.Graphics.DrawImage(Properties.Resources.dexSquare, 150, 300, dexSize, dexSize);
            e.Graphics.DrawImage(Properties.Resources.dexSquare, 200, 300, dexSize, dexSize);

            e.Graphics.DrawImage(Properties.Resources.dexSquare, 250, 300, dexSize, dexSize);
            e.Graphics.DrawImage(Properties.Resources.dexSquare, 300, 300, dexSize, dexSize);
            e.Graphics.DrawImage(Properties.Resources.dexSquare, 350, 300, dexSize, dexSize);
            e.Graphics.DrawImage(Properties.Resources.dexSquare, 400, 300, dexSize, dexSize);
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
