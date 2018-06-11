using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace pokemonSummative
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            OnStart();
        }

        public static List<MiniGamePlayer> top5Players = new List<MiniGamePlayer>();
        public static string playerName = "RED", rivalName = "BLUE";
        public static bool top5Name, gameName, rName, pokemonName;
        public static int messageIndex = 0;

        public void OnStart()
        {
            GetScores();
            //IntroScreen ns = new IntroScreen();
            //this.Controls.Add(ns);

            GameScreen gs = new GameScreen();
            this.Controls.Add(gs);
            this.Height -= 43;
            

            //MenuScreen ms = new MenuScreen();
            //this.Controls.Add(ms);
            //StartScreen ss = new StartScreen();
            //this.Controls.Add(ss);
            //ViewScoreScreen vs = new ViewScoreScreen();
            //this.Controls.Add(vs);
            //NameScreen ns = new NameScreen();
            //this.Controls.Add(ns);
        }

        public void GetScores()
        {
            XmlReader reader = XmlReader.Create("C:/Users/sambw/source/repos/pokemonSummative/pokemonSummative/HighScores.xml");

            string newName;
            int newScore, newMin, newSec;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                    newName = reader.ReadContentAsString();

                    reader.ReadToNextSibling("score");
                    newScore = Convert.ToInt32(reader.ReadElementContentAsString());

                    reader.ReadToNextSibling("min");
                    newMin = Convert.ToInt32(reader.ReadElementContentAsString());

                    reader.ReadToNextSibling("sec");
                    newSec = Convert.ToInt32(reader.ReadElementContentAsString());

                    MiniGamePlayer p = new MiniGamePlayer(newScore,newMin, newSec, newName);
                    top5Players.Add(p);
                }
            }
        }
    }
}
