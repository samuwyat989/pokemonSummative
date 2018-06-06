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
    public partial class HighScoreScreen : UserControl
    {
        public HighScoreScreen()
        {
            InitializeComponent();
        }

        float percentScore = 0;

        Font placeFont = new Font("Pokemon GB", 15);
        Font titleFont = new Font("Pokemon GB", 12);
        Font drawFont = new Font("Pokemon GB", 11);

        private void HighScoreScreen_Paint(object sender, PaintEventArgs e)
        {
            this.Focus();
            e.Graphics.DrawString("MENU", titleFont, Brushes.Black, new Point(28, 406));
            e.Graphics.DrawImage(Properties.Resources.pokemonSelect, new Point(5, 405));
            e.Graphics.DrawString("High Scores", new Font("Pokemon GB", 18), Brushes.Black, new Point(5, 40));
            e.Graphics.DrawString("Place", titleFont, Brushes.Black, new Point(5, 90));
            e.Graphics.DrawString("Name", titleFont, Brushes.Black, new Point(105, 90));
            e.Graphics.DrawString("Score", titleFont, Brushes.Black, new Point(205, 90));
            e.Graphics.DrawString("Time", titleFont, Brushes.Black, new Point(305, 90));
            e.Graphics.DrawString("Percent", titleFont, Brushes.Black, new Point(405, 90));

            int counter = 0, place = 1;

            foreach (MiniGamePlayer p in Form1.top5Players)
            {
                percentScore = p.score * 100 / 151F;

                if (place == 1)
                {
                    e.Graphics.DrawString("#" + place.ToString() + ": ", placeFont, Brushes.Goldenrod, new Point(10, 130 + 40 * counter));
                }
                else if (place == 2)
                {
                    e.Graphics.DrawString("#" + place.ToString() + ": ", placeFont, Brushes.Silver, new Point(10, 130 + 40 * counter));
                }
                else if (place == 3)
                {
                    e.Graphics.DrawString("#" + place.ToString() + ": ", placeFont, Brushes.RosyBrown, new Point(10, 130 + 40 * counter));
                }
                else
                {
                    e.Graphics.DrawString("#" + place.ToString() + ": ", placeFont, Brushes.Black, new Point(10, 130 + 40 * counter));

                }
                place++;
                e.Graphics.DrawString(p.name, drawFont, Brushes.Black, new Point(100, 130 + 40 * counter));
                e.Graphics.DrawString(p.min.ToString("00") + ":" + p.sec.ToString("00"), drawFont, Brushes.Black, new Point(305, 130 + 40 * counter));
                e.Graphics.DrawString(p.score.ToString(), drawFont, Brushes.Black, new Point(220, 130 + 40 * counter));
                e.Graphics.DrawString(percentScore.ToString("0.0") + "%", drawFont, Brushes.Black, new Point(405, 130 + 40 * counter));
                counter++;
            }
        }

        private void HighScoreScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                Form f = this.FindForm();
                MenuScreen ms = new MenuScreen();
                f.Controls.Remove(this);
                f.Controls.Add(ms);
            }
        }
    }
}

