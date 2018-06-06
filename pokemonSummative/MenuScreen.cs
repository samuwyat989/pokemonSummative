using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pokemonSummative
{
    public partial class MenuScreen : UserControl
    {
        public MenuScreen()
        {
            InitializeComponent();
            this.Focus();
        }

        int playIndex = 0;

        Image[] playImages = new[]
        {
            Properties.Resources.pokemonmenu,
            Properties.Resources.pokemonmenu2,
            Properties.Resources.pokemonmenu3,
            Properties.Resources.pokemonmenu4
        };

        private void MenuScreen_Paint(object sender, PaintEventArgs e)
        {
            this.Focus();
            e.Graphics.DrawImage(playImages[playIndex], 0, 0);
        }

        private void MenuScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (playIndex == 3)
                {
                    playIndex = 0;
                }
                else
                {
                    playIndex++;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (playIndex == 0)
                {
                    playIndex = 3;
                }
                else
                {
                    playIndex--;
                }
            }
            else if (e.KeyCode == Keys.Space)
            {
                Form f = this.FindForm();
                f.Controls.Remove(this);

                 switch (playIndex)
                {
                    case 0:
                        StartScreen ss = new StartScreen();
                        f.Controls.Add(ss);
                        break;
                    case 1:
                        MinigameScreen ms = new MinigameScreen();
                        f.Height += 50;
                        f.Controls.Add(ms);
                        break;
                    case 2:
                        HighScoreScreen hs = new HighScoreScreen();               
                        f.Controls.Add(hs);
                        break;
                    case 3:
                        IntroScreen ns = new IntroScreen();
                        f.Controls.Add(ns);
                        break;
                }
            }
            Refresh();
        }
    }
}
