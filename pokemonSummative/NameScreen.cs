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
    public partial class NameScreen : UserControl
    {
        public NameScreen()
        {
            InitializeComponent();
        }

        int rectGap = 3, rectWidth = 60, rectHeight = 7, rectX = 220, rectY = 80, letterIndex = 0, selectRow = 0, selectCol = 0;
        List<Rectangle> nameRects = new List<Rectangle>();

        string name = "";
        const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ x():;[]  -?!  /., ";

        Dictionary<int, string> indexLetterPairs = new Dictionary<int, string>();
        int currentStringIndex = 0;

        private void NameScreen_Load(object sender, EventArgs e)
        {
            for(int i = 0; i < letters.Length; i++)
            {
                indexLetterPairs.Add(i, letters.Substring(i, 1));
            }

            this.Focus();
        }

        private void NameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (currentStringIndex == 44)
                {
                    if (Form1.top5Name && name != "")
                    {
                        int gameSeconds = (11 - MinigameScreen.minTime) * 60 + MinigameScreen.secTime;

                        for (int i = 0; i < Form1.top5Players.Count; i++)
                        {
                            if (MinigameScreen.progress > Form1.top5Players[i].score
                                || MinigameScreen.progress == Form1.top5Players[i].score
                                && gameSeconds < Form1.top5Players[i].min * 60 + Form1.top5Players[i].sec)
                            {
                                for (int x = Form1.top5Players.Count - (i + 1); x > 0; x--)
                                {
                                    Form1.top5Players[i + x] = Form1.top5Players[i + x - 1];
                                }

                                if (MinigameScreen.secTime == 0)
                                {
                                    Form1.top5Players[i] = new MiniGamePlayer(Convert.ToInt32(MinigameScreen.progress), 11 - MinigameScreen.minTime,
                                    0, name);
                                }
                                else if (11- MinigameScreen.minTime < 0)
                                {
                                    Form1.top5Players[i] = new MiniGamePlayer(Convert.ToInt32(MinigameScreen.progress), 12, 0, name);
                                }
                                else
                                {
                                    Form1.top5Players[i] = new MiniGamePlayer(Convert.ToInt32(MinigameScreen.progress), 11 - MinigameScreen.minTime,
                                    60 - MinigameScreen.secTime, name);
                                }
                                break;
                            }
                        }

                        XmlWriter writer = XmlWriter.Create("C:/Users/sambw/source/repos/pokemonSummative/pokemonSummative/HighScores.xml");

                        //Write the root element 
                        writer.WriteStartElement("players");

                        foreach (MiniGamePlayer p in Form1.top5Players)
                        {
                            //Start an element 
                            writer.WriteStartElement("player");

                            //Write sub-elements 
                            writer.WriteElementString("name", p.name);
                            writer.WriteElementString("score", p.score.ToString());
                            writer.WriteElementString("min", p.min.ToString());
                            writer.WriteElementString("sec", p.sec.ToString());

                            // end the element 
                            writer.WriteEndElement();
                        }

                        // end the root element 
                        writer.WriteEndElement();

                        //Write the XML to file and close the writer 
                        writer.Close();

                        Form f = this.FindForm();
                        f.Controls.Remove(this);

                        MenuScreen ms = new MenuScreen();
                        f.Controls.Add(ms);
                        Form1.top5Name = false;
                    }
                    else if(Form1.gameName && name!="")
                    {
                        Form1.playerName = name;
                        Form1.gameName = false;
                        StartScreen.slideIndex = 2;
                        StartScreen.messageLines[20] = "name is " + name + "!";
                        StartScreen.messageLines[30] = name + "!";

                        Form f = this.FindForm();
                        f.Controls.Remove(this);

                        StartScreen ss = new StartScreen();
                        f.Controls.Add(ss);                 
                    }
                    else if (Form1.rName && name != "")
                    {
                        Form1.rivalName = name;
                        Form1.rName = false;
                        StartScreen.slideIndex = 3;
                        StartScreen.messageLines[29] = "name is " + name + "!";

                        Form f = this.FindForm();
                        f.Controls.Remove(this);

                        StartScreen ss = new StartScreen();
                        f.Controls.Add(ss);
                    }
                }
                else
                {
                    name += indexLetterPairs[currentStringIndex];
                    letterIndex++;
                }     
                if (letterIndex == 7)
                {
                    currentStringIndex = 44;
                    selectCol = 8;
                    selectRow = 4;
                }
            }
            else if (e.KeyCode == Keys.Back && name.Length != 0)
            {
                name = name.Substring(0, name.Length - 1);
                letterIndex--;
            }

            if (e.KeyCode == Keys.Right)
            {
                if(selectCol == 8)
                {
                    currentStringIndex -= 8;
                    selectCol = 0;
                }
                else
                {
                    currentStringIndex++;
                    selectCol++;
                }
                
            }
            else if(e.KeyCode == Keys.Left)
            {
                if (selectCol == 0)
                {
                    currentStringIndex+=8;
                    selectCol = 8;
                }
                else
                {
                    currentStringIndex--;
                    selectCol--;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (selectRow == 0)
                {
                    currentStringIndex += 36;
                    selectRow = 4;
                }
                else
                {
                    currentStringIndex -= 9;
                    selectRow--;
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (selectRow == 4)
                {
                    currentStringIndex -= 36;
                    selectRow = 0;
                }
                else
                {
                    currentStringIndex += 9;
                    selectRow++;
                }
            }
            Refresh();
        }

        private void NameScreen_Paint(object sender, PaintEventArgs e)
        {
            rectWidth = Convert.ToInt32(e.Graphics.MeasureString("M", new Font("Pokemon GB", 14)).Width);
            e.Graphics.DrawImage(Properties.Resources.pokemonBlankBoarder, 20, 100, 480, 300);

            nameRects.Clear();
            for (int i = 0; i < 7; i++)
            {
                if (i == letterIndex)
                {
                    nameRects.Add(new Rectangle(rectX + i * rectWidth + i * rectGap, rectY - rectHeight, rectWidth, rectHeight));
                }
                else
                {
                    nameRects.Add(new Rectangle(rectX + i * rectWidth + i * rectGap, rectY, rectWidth, rectHeight));
                }
            }

            foreach(Rectangle r in nameRects)
            {
                e.Graphics.FillRectangle(Brushes.Black, r);
            }

            for (int i = 0; i < name.Length; i++)
            {
                e.Graphics.DrawString(name.Substring(i, 1), new Font("Pokemon GB", 14), Brushes.Black, nameRects[i].X, nameRects[i].Y - 30);
            }

            int letterSpace = 45, letterX = 70, letterY = 140, col = 0, row = 0;

            e.Graphics.DrawImage(Properties.Resources.malePokemon, new PointF(205, 320));
            e.Graphics.DrawImage(Properties.Resources.femalePokemon, new PointF(250, 320));
            e.Graphics.DrawImage(Properties.Resources.donePokemon, new PointF(430, 320));
            e.Graphics.DrawImage(Properties.Resources.pkPokemon, new PointF(385, 275));
            e.Graphics.DrawImage(Properties.Resources.mnPokemon, new PointF(430, 275));

            e.Graphics.DrawImage(Properties.Resources.pokemonSelect, new PointF((letterX-20) + selectCol*letterSpace, 140 + selectRow*letterSpace));
        
            for (int i = 0; i < letters.Length; i++)
            {
                if(i!= 0 && i%9 == 0)
                {
                    row++;
                    col = 0;
                }
                e.Graphics.DrawString(letters.Substring(i, 1), new Font("Pokemon GB", 14), Brushes.Black, new Point(letterX + col * letterSpace, letterY + letterSpace * row));
                col++;
            }
        }
    }
}
