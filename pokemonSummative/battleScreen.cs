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
    public partial class BattleScreen : UserControl
    {
        public BattleScreen()
        {
            InitializeComponent();
        }

        string[] movesPlayer, movesRival, menu = new[] { "FIGHT", "ITEM", "PkMn", "RUN" };

        string pokemon = "SQUIRTLE", rivalPokemon, rivalName = Form1.rivalName, attackName;

        bool startBattle, fightScene;

        int menuScene, upDownIndex = 0, leftRightIndex = 0, startPlayerX = 400, startPlayerY = 280, startRivalX = 10, rivalSpeed,
            startRivalY = 100, playerHp, rivalHp, playerSpeed, maxPlayerHp, maxRivalHp, playerMove1pp, playerMove2pp, rivalMove1pp, rivalMove2pp;

       

        Font pokeFont = new Font("Pokemon GB", 16);

        Image blueSprite = Properties.Resources.blueSpriteIntro, 
            redSprite = Properties.Resources.playerBattleSprite;

        private void BattleScreen_Load(object sender, EventArgs e)
        {
            switch (pokemon)
            {
                case "SQUIRTLE":
                    movesPlayer = new[] { "TACKLE", "TAIL WHIP" };
                    playerHp = 24;
                    maxPlayerHp = 24;
                    playerSpeed = 13;
                    playerMove1pp = 35;//tackle
                    playerMove2pp = 30;//tail whip

                    rivalPokemon = "BULBASAUR";
                    movesRival = new[] { "TACKLE", "GROWL" };
                    rivalHp = 24;
                    maxRivalHp = 24;
                    playerSpeed = 14;
                    rivalMove1pp = 35;//tackle
                    rivalMove2pp = 40;//growl
                    break;
                case "BULBASAUR":
                    movesPlayer = new[] { "TACKLE", "GROWL" };
                    playerHp = 24;
                    maxPlayerHp = 24;
                    playerSpeed = 14;
                    playerMove1pp = 35;//tackle
                    playerMove2pp = 40;//growl

                    rivalPokemon = "CHARMANDER";
                    movesRival = new[] { "SCRATCH", "GROWL" };
                    rivalHp = 23;
                    maxRivalHp = 23;
                    rivalSpeed = 16;
                    rivalMove1pp = 35;
                    rivalMove2pp = 40;
                    break;
                case "CHARMANDER":
                    movesPlayer = new[] { "SCRATCH", "GROWL" };
                    playerHp = 23;
                    maxPlayerHp = 23;
                    playerSpeed = 16;
                    playerMove1pp = 35;//scratch
                    playerMove2pp = 40;

                    rivalPokemon = "SQUIRTLE";
                    movesRival = new[] { "TACKLE", "TAIL WHIP" };
                    rivalHp = 24;
                    maxRivalHp = 24;
                    rivalSpeed = 13;
                    rivalMove1pp = 35;
                    rivalMove2pp = 30;
                    break;
                default:
                    break;
            }

            startBattle = true;
            screenTimer.Start();
            Refresh();
        }

        private void BattleScreen_Paint(object sender, PaintEventArgs e)
        {
            if(startBattle)
            {
                e.Graphics.DrawImage(blueSprite, startRivalX, startRivalY, 58, 112);
                e.Graphics.DrawImage(redSprite, startPlayerX, startPlayerY, 112, 112);
            }
            else if(fightScene)
            {
                int yOffset = 30;
                foreach (string move in movesPlayer)
                {
                    e.Graphics.DrawString(move, pokeFont, Brushes.Black, new Point(100, 200 + yOffset));
                    yOffset += 30;
                }
            }
            else
            {
                e.Graphics.DrawString(menu[0], pokeFont, Brushes.Black, new Point(100, 300));
                e.Graphics.DrawString(menu[1], pokeFont, Brushes.Black, new Point(100, 400));
                e.Graphics.DrawString(menu[2], pokeFont, Brushes.Black, new Point(400, 300));
                e.Graphics.DrawString(menu[3], pokeFont, Brushes.Black, new Point(400, 400));

                e.Graphics.DrawImage(Properties.Resources.pokemonSelect, new Point(85 + 300 * leftRightIndex, 300 + 100 * upDownIndex));
            }
        }

        private void screenTimer_Tick(object sender, EventArgs e)
        {
            if (startBattle)
            {
                startPlayerX -= 16;
                startRivalX += 16;

                if (startPlayerX < 100)
                {
                    startBattle = false;
                }
            }
            Refresh();
        }

        private void BattleScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Left:
                    if(leftRightIndex == 0)
                    {
                        leftRightIndex = 1;
                    }
                    else
                    {
                        leftRightIndex = 0;
                    }
                    break;
                case Keys.Right:
                    if (leftRightIndex == 0)
                    {
                        leftRightIndex = 1;
                    }
                    else
                    {
                        leftRightIndex = 0;
                    }
                    break;
                case Keys.Up:
                    if (upDownIndex == 0)
                    {
                        upDownIndex = 1;
                    }
                    else
                    {
                        upDownIndex = 0;
                    }
                    break;
                case Keys.Down:
                    if (upDownIndex == 0)
                    {
                        upDownIndex = 1;
                    }
                    else
                    {
                        upDownIndex = 0;
                    }
                    break;
                case Keys.Space:
                    if (upDownIndex == 0 && leftRightIndex == 0)
                    {
                        fightScene = true;
                        //fight
                    }
                    else if (upDownIndex == 0 && leftRightIndex == 1)
                    {
                        //item
                    }
                    else if (upDownIndex == 1 && leftRightIndex == 0)
                    {
                        //item
                    }
                    else
                    {
                        //run
                    }
                    break;
            }
        }
    }
}
