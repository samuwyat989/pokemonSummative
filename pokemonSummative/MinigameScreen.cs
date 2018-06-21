using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace pokemonSummative
{
    public partial class MinigameScreen : UserControl
    {
        string[] poke = 
            {"Bulbasaur","Ivysaur","Venusaur","Charmander","Charmeleon","Charizard","Squirtle","Wartortle",
            "Blastoise","Caterpie","Metapod","Butterfree","Weedle","Kakuna","Beedrill","Pidgey","Pidgeotto","Pidgeot",
            "Rattata","Raticate","Spearow","Fearow","Ekans","Arbok","Pikachu","Raichu","Sandshrew","Sandslash","Nidoran♀",
            "Nidorina","Nidoqueen", "Nidoran♂","Nidorino","Nidoking","Clefairy","Clefable","Vulpix","Ninetales",

            "Jigglypuff","Wigglytuff", "Zubat","Golbat","Oddish","Gloom","Vileplume","Paras","Parasect",
            "Venonat","Venomoth","Diglett","Dugtrio","Meowth","Persian","Psyduck","Golduck","Mankey","Primeape",
            "Growlithe","Arcanine","Poliwag","Poliwhirl","Poliwrath","Abra","Kadabra","Alakazam","Machop","Machoke",
            "Machamp","Bellsprout","Weepinbell","Victreebel","Tentacool","Tentacruel","Geodude","Graveler","Golem",

            "Ponyta","Rapidash","Slowpoke","Slowbro","Magnemite","Magneton","Farfetch'd","Doduo","Dodrio",
            "Seel","Dewgong","Grimer","Muk","Shellder","Cloyster","Gastly","Haunter","Gengar","Onix","Drowzee","Hypno",
            "Krabby","Kingler","Voltorb","Electrode","Exeggcute","Exeggutor","Cubone","Marowak","Hitmonlee","Hitmonchan",
            "Lickitung","Koffing","Weezing","Rhyhorn","Rhydon","Chansey","Tangela",

            "Kangaskhan","Horsea","Seadra","Goldeen","Seaking","Staryu","Starmie","Mr. Mime","Scyther","Jynx",
            "Electabuzz","Magmar","Pinsir","Tauros","Magikarp","Gyarados","Lapras","Ditto","Eevee","Vaporeon", "Jolteon",
            "Flareon","Porygon","Omanyte","Omastar","Kabuto","Kabutops","Aerodactyl","Snorlax","Articuno","Zapdos", "Moltres",
            "Dratini","Dragonair","Dragonite","Mewtwo","Mew"};

        List<string> allPokemon = new List<string>();
        List<string> missedPokemon = new List<string>();
        public static int secTime = 0, minTime = 12, progress = 0, selectIndex = 0, letterIndex = 0;
        string inputPokemon, endMessage = "End\nGame";
        Point[] selectPoints = new[] { new Point(5,10), new Point(408, 10)};
        bool exception;
        string[] playLines1 = new string[38], playLines2 = new string[38], playLines3 = new string[38], playLines4 = new string[37];

        public MinigameScreen()
        {
            InitializeComponent();

            for(int i = 0; i < 38; i++) 
            {                
                if (i != 37)
                {
                    rowBox1.Text += Environment.NewLine;
                    rowBox2.Text += Environment.NewLine;
                    rowBox3.Text += Environment.NewLine;
                    rowBox4.Text += Environment.NewLine;
                }
                dexBox1.Items.Add((i + 1).ToString("000"));
                dexBox2.Items.Add((i + 39).ToString("000"));
                dexBox3.Items.Add((i + 77).ToString("000"));
                if(i+115 != 152)
                dexBox4.Items.Add((i + 115).ToString("000"));
            }
            progress = 0;
            secTime = 0;
            minTime = 12;
            allPokemon.AddRange(poke);
            missedPokemon.AddRange(poke);
            scoreLabel.Focus();
        }

        private void scoreLabel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            char key = (char)e.KeyData;

            if (e.KeyCode == Keys.Left && endMessage != "View\nScore")
            {
                selectIndex = 0;
            }
            else if (e.KeyCode == Keys.Right)
            {
                selectIndex = 1;
            }
            else if (e.KeyCode == Keys.Space && selectIndex == 1)
            {
                if (endMessage == "View\nScore")
                {
                    Form f = this.FindForm();
                    ViewScoreScreen vs = new ViewScoreScreen();
                    f.Controls.Remove(this);
                    f.Controls.Add(vs);
                }
                else
                {
                    gameTimer.Stop();
                    ShowMissedPokemon();
                    endMessage = "View\nScore";
                    Refresh();
                }
            }
            else if (e.KeyCode == Keys.Back && selectIndex == 0)
            {
                if (letterIndex != 0)
                {
                    inputPokemon = inputPokemon.Substring(0, inputPokemon.Length - 1);
                    letterIndex--;
                }
            }
            else if(Char.IsLetter(key) || e.KeyCode == Keys.OemQuotes || e.KeyCode == Keys.OemPeriod)
            {
                //Start timer when the text of the input box is initially changed
                if (gameTimer.Enabled == false)
                {
                    gameTimer.Enabled = true;
                }

                if (e.KeyCode == Keys.OemPeriod)
                {
                    inputPokemon += ".";
                }
                else if (e.KeyCode == Keys.OemQuotes)
                {
                    inputPokemon += "'";
                }
                else
                {
                    //Gets current value of text box
                    inputPokemon += e.KeyCode;
                }

                //Converts the first letter to an upper and all others to lower ie. rigHt -> Right
                inputPokemon = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(inputPokemon.ToLower());

                //Checks for exception pokemon
                ExceptionCheck(inputPokemon);

                if (exception == false)
                {
                    foreach (string pokemon in allPokemon)
                    {
                        if (pokemon == inputPokemon && GotPokemon(pokemon) == false)
                        {
                            int dexNumber = allPokemon.IndexOf(inputPokemon);

                            if (dexNumber <= 37)
                            {
                                playLines1[dexNumber] = inputPokemon;
                                rowBox1.Lines = playLines1;
                            }
                            else if (dexNumber > 37 && dexNumber <= 75)
                            {
                                playLines2[dexNumber - 38] = inputPokemon;
                                rowBox2.Lines = playLines2;
                            }
                            else if (dexNumber > 75 && dexNumber <= 113)
                            {
                                playLines3[dexNumber - 76] = inputPokemon;
                                rowBox3.Lines = playLines3;
                            }
                            else if (dexNumber > 113)
                            {
                                playLines4[dexNumber - 114] = inputPokemon;
                                rowBox4.Lines = playLines4;
                            }

                            missedPokemon.Remove(pokemon);
                            progress++;
                            letterIndex = -1;
                            Reset();
                            break;
                        }
                    }
                }
                exception = false;
                letterIndex++;
            }       
            Refresh();
        }

        private void rowBox1_Enter(object sender, EventArgs e)
        {
            scoreLabel.Focus();
        }

        private void dexBox4_Enter(object sender, EventArgs e)
        {
            scoreLabel.Focus();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (secTime == 0)
            {
                secTime = 59;
                minTime--;
            }
            else
            {
                secTime--;
            }

            timeLabel.Text = minTime.ToString("00") + ":" + secTime.ToString("00");

            if(secTime == 0 && minTime == 0 || progress == 151)
            {
                gameTimer.Stop();
                ShowMissedPokemon();            
                endMessage = "View\nScore";
            }
        }

        private void MinigameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Properties.Resources.pokemonSelect, selectPoints[selectIndex]);
            e.Graphics.DrawString(endMessage, new Font("Pokemon GB", 11), Brushes.Black, selectPoints[1].X + 25, selectPoints[1].Y);

            int rectWidth = 15, rectHeight = 5; 

            for (int i = 0; i < 10; i++)
            {
                if(i == letterIndex)
                {
                    e.Graphics.FillRectangle(Brushes.Black, new Rectangle(selectPoints[0].X + 20 + i * rectWidth + i, selectPoints[0].Y + 25 - rectHeight, rectWidth, rectHeight));
                }
                else
                {
                    e.Graphics.FillRectangle(Brushes.Black, new Rectangle(selectPoints[0].X + 20 + i * rectWidth + i, selectPoints[0].Y + 25, rectWidth, rectHeight));
                }
            }
            e.Graphics.DrawString(inputPokemon, new Font("Pokemon GB", 12), Brushes.Black, selectPoints[0].X + 17, selectPoints[0].Y);
            scoreLabel.Focus();
        }

        public void Reset()
        {
            scoreLabel.Text = progress.ToString() + "/151";
            inputPokemon = "";
        }

        public Boolean GotPokemon(string pokemon)
        {
            bool gotPokemon = true;
            if(missedPokemon.Contains(pokemon))
            {
                gotPokemon = false;
            }
            return gotPokemon;
        }

        public void ExceptionCheck(string pokemon)
        {
            if (pokemon == "Nidoran" && GotPokemon(allPokemon[28]) == false)
            {
                playLines1[28] = allPokemon[28];
                playLines1[31] = allPokemon[31];
                rowBox1.Lines = playLines1;
                missedPokemon.Remove(allPokemon[28]);
                missedPokemon.Remove(allPokemon[31]);
                progress += 2;
                exception = true;
                letterIndex = -1;
                Reset();
            }
            else if (pokemon == "Mr.Mime" && GotPokemon(allPokemon[121]) == false)
            {
                playLines4[7] = allPokemon[121];
                rowBox4.Lines = playLines4;
                missedPokemon.Remove(allPokemon[121]);
                progress++;
                exception = true;
                letterIndex = -1;
                Reset();
            }
        }

        public void ShowMissedPokemon()
        {
            //Clear Boxes
            rowBox1.Clear();
            rowBox2.Clear();
            rowBox3.Clear();
            rowBox4.Clear();

            //Fill Boxes
            for(int i = 0; i < 38; i++)
            {
                rowBox1.Text += allPokemon[i];
                rowBox2.Text += allPokemon[i+38];
                rowBox3.Text += allPokemon[i+76];
                try { rowBox4.Text += allPokemon[i+114]; }
                catch { }

                if (i != 37)
                {
                    rowBox1.Text += Environment.NewLine;
                    rowBox2.Text += Environment.NewLine;
                    rowBox3.Text += Environment.NewLine;
                    rowBox4.Text += Environment.NewLine;
                }
            }

            //Paint missed pokemon red
            foreach (string pokemon in missedPokemon)
            {
                int dexNumber = allPokemon.IndexOf(pokemon);

                if(dexNumber == 150)//mew
                {
                    int textPlacement = rowBox4.Text.IndexOf(pokemon)+7;
                    rowBox4.Select(textPlacement, pokemon.Length);
                    rowBox4.SelectionColor = Color.Red;
                }
                else if (dexNumber == 17)//pidgeot
                {
                    int textPlacement = rowBox1.Text.IndexOf(pokemon) + 10;
                    rowBox1.Select(textPlacement, pokemon.Length);
                    rowBox1.SelectionColor = Color.Red;
                }
                else if (dexNumber <= 37)
                {
                    int textPlacement = rowBox1.Text.IndexOf(pokemon);
                    rowBox1.Select(textPlacement, pokemon.Length);
                    rowBox1.SelectionColor = Color.Red;
                }
                else if (dexNumber > 37 && dexNumber <= 75)
                {
                    int textPlacement = rowBox2.Text.IndexOf(pokemon);
                    rowBox2.Select(textPlacement, pokemon.Length);
                    rowBox2.SelectionColor = Color.Red;
                }
                else if (dexNumber > 75 && dexNumber <= 113)
                {
                    int textPlacement = rowBox3.Text.IndexOf(pokemon);
                    rowBox3.Select(textPlacement, pokemon.Length);
                    rowBox3.SelectionColor = Color.Red;
                }
                else if (dexNumber > 113)
                {
                    int textPlacement = rowBox4.Text.IndexOf(pokemon);
                    rowBox4.Select(textPlacement, pokemon.Length);
                    rowBox4.SelectionColor = Color.Red;
                }
            }
        }
    }
}
