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
    public partial class IntroScreen : UserControl
    {
        public IntroScreen()
        {
            InitializeComponent();
        }

        int introCounter = 0;

        private void introTimer_Tick(object sender, EventArgs e)
        {
            introCounter++;

            if (introCounter == 23)
            {
                introPlayer.Ctlcontrols.stop();
                introPlayer.Visible = false;
                introPlayer.Ctlenabled = false;
            }
            Refresh();
        }

        private void IntroScreen_Paint(object sender, PaintEventArgs e)
        {
            if(introCounter%2 == 0)
            {
                e.Graphics.DrawString("PRESS SPACE", new Font("Pokemon GB", 20, FontStyle.Bold), Brushes.White, 110, 470);
            }
        }

        private void IntroScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Space && introPlayer.Visible == false)
            {
                introTimer.Stop();
                Form f = this.FindForm();
                MenuScreen ms = new MenuScreen();
                f.Controls.Remove(this);
                f.Controls.Add(ms);
            }
        }
    }
}
