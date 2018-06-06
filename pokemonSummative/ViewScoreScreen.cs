using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace pokemonSummative
{
    public partial class ViewScoreScreen : UserControl
    {
        public ViewScoreScreen()
        {
            InitializeComponent();
        }

        int minTime = 11 - MinigameScreen.minTime, secTime = 60 - MinigameScreen.secTime, selectIndex = 0;
        bool top5 = false;

        Point[] selectPoints = new[] { new Point(10, 300), new Point(235, 300) };

        private void ViewScoreScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (top5)
            {
                if (e.KeyCode == Keys.Right && selectIndex == 0)
                {
                    selectIndex++;
                }
                else if (e.KeyCode == Keys.Left && selectIndex == 1)
                {
                    selectIndex--;
                }
                Refresh();
            }
            if(e.KeyCode == Keys.Space)
            {
                if(selectIndex == 0)
                {
                    Form f = this.FindForm();
                    f.Controls.Remove(this);

                    MenuScreen ms = new MenuScreen();
                    f.Controls.Add(ms);

                    Form1.top5Name = false;
                }
                else
                {
                    Form f = this.FindForm();
                    f.Controls.Remove(this);

                    NameScreen ns = new NameScreen();
                    f.Controls.Add(ns);
                    Form1.top5Name = true;
                }
            }
        }

        private void ViewScoreScreen_Paint(object sender, PaintEventArgs e)
        {
            this.Focus();
            e.Graphics.DrawString("Congratulations!\n\n\n\n  Your score was: " + MinigameScreen.progress.ToString() +
                "\n  Your time was: " + minTime.ToString("00") + ":" + secTime.ToString("00")
                , new Font("Pokemon GB", 15), Brushes.Black, 20, 100);

            e.Graphics.DrawString("MENU", new Font("Pokemon GB", 15), Brushes.Black, 30, 300);

            if (top5)
            {
                e.Graphics.DrawString("You Made Top 5!", new Font("Pokemon GB", 15), Brushes.Black, 20, 130);
                e.Graphics.DrawString("ENTER NAME", new Font("Pokemon GB", 15), Brushes.Black, 250, 300);
            }

            e.Graphics.DrawImage(Properties.Resources.pokemonSelect, selectPoints[selectIndex]);
        }

        private void ViewScoreScreen_Load(object sender, EventArgs e)
        {
            foreach (MiniGamePlayer mp in Form1.top5Players)
            {
                if (MinigameScreen.progress > mp.score)
                {
                    top5 = true;
                    Form1.pokemonName = true;
                }
                else if (MinigameScreen.progress == mp.score && minTime < mp.min)
                {
                    top5 = true;
                    Form1.pokemonName = true;
                }
                else if (MinigameScreen.progress == mp.score && minTime == mp.min && secTime < mp.sec)
                {
                    top5 = true;
                    Form1.pokemonName = true;
                }
            }
        }
    }
}
