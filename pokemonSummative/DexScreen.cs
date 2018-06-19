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
        public static string pokemon = "BULBASAUR";
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
        Font pokeFont = new Font("Pokemon GB", 23);
        Font textFont = new Font("Pokemon GB", 22);
        Point[] clickPoints = new[] { new Point(490, 420), new Point(490, 400) };
        int clickIndex = 0;

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
                    GameScreen.gotStarter = true;
                    GameScreen.publicTimer.Start();
                    this.Parent.Controls.Remove(this);
                }
                else
                {
                    sceneCounter+=3;
                    if (clickIndex == 2)
                    {
                        clickIndex = 0;
                    }
                    Refresh();
                }

            }
        }

        private void DexScreen_Paint(object sender, PaintEventArgs e)
        {
            if (startUp)
            {
                e.Graphics.DrawString(pokemon, pokeFont, Brushes.Black, new Point(200, 50));
                e.Graphics.DrawString(types[pokeIndex], pokeFont, Brushes.Black, new Point(200, 90));
                e.Graphics.DrawString("No." + dexNumber[pokeIndex], new Font("Pokemon GB", 15), Brushes.Black, new Point(50, 205));
                e.Graphics.DrawString("HT   ?'??''", pokeFont, Brushes.Black, new Point(200, 130));
                e.Graphics.DrawString("WT   ???lb", pokeFont, Brushes.Black, new Point(200, 170));
                startUp = false;
            }
            else
            {
                e.Graphics.DrawString(pokemon, pokeFont, Brushes.Black, new Point(200, 50));
                e.Graphics.DrawString(types[pokeIndex], pokeFont, Brushes.Black, new Point(200, 90));
                e.Graphics.DrawString("No." + dexNumber[pokeIndex], new Font("Pokemon GB", 15), Brushes.Black, new Point(50, 205));
                e.Graphics.DrawString("HT  " + heights[pokeIndex], pokeFont, Brushes.Black, new Point(200, 130));
                e.Graphics.DrawString("WT  " + weights[pokeIndex] + "lb", pokeFont, Brushes.Black, new Point(200, 170));
                e.Graphics.DrawImage(Properties.Resources.nextTextPokemon, clickPoints[clickIndex]);
                if (pokemon == "CHARMANDER")
                {
                    e.Graphics.DrawImage(Properties.Resources.charmanderDexSprite, 40, 50, 141, 156);//times 1.25
                    e.Graphics.DrawString(dex[sceneCounter], textFont, Brushes.Black, new Point(5, 270));
                    e.Graphics.DrawString(dex[sceneCounter+1], textFont, Brushes.Black, new Point(5, 320));
                    e.Graphics.DrawString(dex[sceneCounter+2], textFont, Brushes.Black, new Point(5, 370));
                }
                else if (pokemon == "SQUIRTLE")
                {
                    e.Graphics.DrawImage(Properties.Resources.squirtleDexSprite, 25, 50, 165, 152);
                    e.Graphics.DrawString(dex[sceneCounter+6], textFont, Brushes.Black, new Point(5, 270));
                    e.Graphics.DrawString(dex[sceneCounter + 7], textFont, Brushes.Black, new Point(5, 320));
                    e.Graphics.DrawString(dex[sceneCounter + 8], textFont, Brushes.Black, new Point(5, 370));
                }
                else
                {
                    e.Graphics.DrawImage(Properties.Resources.bulbasaurDexSprite, 25, 30, 160, 170);
                    e.Graphics.DrawString(dex[sceneCounter+12], textFont, Brushes.Black, new Point(5, 270));
                    e.Graphics.DrawString(dex[sceneCounter + 13], textFont, Brushes.Black, new Point(5, 320));
                    e.Graphics.DrawString(dex[sceneCounter + 14], textFont, Brushes.Black, new Point(5, 370));
                }
            }

            int dexSize = 18, lineWidth = 8;
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(0, this.Height/2 - lineWidth/2, this.Width, lineWidth));

            e.Graphics.DrawImage(Properties.Resources.dexSquare, 30, this.Height/2 - dexSize / 2, dexSize, dexSize);
            e.Graphics.DrawImage(Properties.Resources.dexSquare, 80, this.Height/2 - dexSize/2, dexSize, dexSize);
            e.Graphics.DrawImage(Properties.Resources.dexSquare, 130, this.Height/2 - dexSize/2, dexSize, dexSize);
            e.Graphics.DrawImage(Properties.Resources.dexSquare, 180, this.Height/2 - dexSize/2, dexSize, dexSize);

            e.Graphics.DrawImage(Properties.Resources.dexSquare, 320, this.Height/2 - dexSize/2, dexSize, dexSize);
            e.Graphics.DrawImage(Properties.Resources.dexSquare, 370, this.Height/2 - dexSize/2, dexSize, dexSize);
            e.Graphics.DrawImage(Properties.Resources.dexSquare, 420, this.Height/2 - dexSize/2, dexSize, dexSize);
            e.Graphics.DrawImage(Properties.Resources.dexSquare, 470, this.Height/2 - dexSize/2, dexSize, dexSize);
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if(clickIndex == 2)
            {
                clickIndex = 0;
            }
            Refresh();
            clickIndex++;
        }
    }
}
