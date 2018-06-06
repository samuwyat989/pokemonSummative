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
    public partial class StartScreen : UserControl
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        public static int lift = 50, slide = 0, textCounter = 0, selectY, slideIndex, 
            cornerSize = Properties.Resources.bottomLeft.Width, nameIndex, nameWidth = 250, nameHeight = 300,
            x1 = 0, x2, y1 = 330, y2, verticalWidth = Properties.Resources.vertical.Width, 
            horizontalHeight = Properties.Resources.horizontal.Height;
        bool textUp, chooseName;
        string player;
        Point spriteLocation = new Point();

        Image[] slideImages = new[]
        {
            Properties.Resources.oakIntroImage,
            Properties.Resources.nidoranSprite,
            Properties.Resources.redSpriteIntro,
            Properties.Resources.blueSpriteIntro,
            Properties.Resources.small1,
            Properties.Resources.small2
        };

        Size[] spriteDimensions = new[]
        {
            new Size(90, 170),
            new Size(132, 129),
            new Size(78, 162),
            new Size(87, 168), 
            new Size(44, 80), 
            new Size(28, 46)
        };

        public static string[] messageLines = new[]
        {
            "Hello there!",
            "Welcome to the",
            "world of POKeMON!",
            "My name is OAK!",
            "People call me",
            "the POKeMON PROF!", 
            "This world is",
            "inhabited by",
            "creatures called",
            "POKeMON!",
            "For some people",
            "POKeMON are",
            "pets. Others use",
            "them for fights.",
            "Myself...",
            "I study POKeMON",
            "as a profession.", 
            "First, what is",
            "your name?",
            "Right! So your",
            "name is",
            "This is my grand-",
            "son. He's been",
            "your rival since",
            "you were a baby.",
            "...Erm, what is",
            "his name again?",
            "That's right! I",
            "remember now! His",
            "name is",
            "", 
            "Your very own",
            "POKeMON legend is",
            "about to unfold!",
            "A world of dreams",
            "and adventures",
            "with POKeMON",
            "awaits! Lets go!"
        };

        private void StartScreen_Load(object sender, EventArgs e)
        {
            selectY = this.Height - cornerSize - 30;
            x2 = this.Width - cornerSize;
            y2 = this.Height - cornerSize;

            spriteLocation.X = this.Width / 2 - spriteDimensions[slideIndex].Width / 2 + slide;
            spriteLocation.Y = y1 - spriteDimensions[slideIndex].Height - 20;
        }

        private void StartScreen_Paint(object sender, PaintEventArgs e)
        {
            this.Focus();

            //sprite
            e.Graphics.DrawImage(slideImages[slideIndex], spriteLocation.X, spriteLocation.Y,
                spriteDimensions[slideIndex].Width, spriteDimensions[slideIndex].Height);

            if(chooseName)
            {
                e.Graphics.DrawImage(Properties.Resources.horizontal, 10, 10, nameWidth, horizontalHeight);//top
                e.Graphics.DrawImage(Properties.Resources.horizontal, 10, 10+nameHeight- verticalWidth, 
                    nameWidth+verticalWidth, horizontalHeight);//bottom
                e.Graphics.DrawImage(Properties.Resources.vertical, 10, 10, verticalWidth, nameHeight);//left
                e.Graphics.DrawImage(Properties.Resources.vertical, 10+nameWidth, 10, verticalWidth, nameHeight);//right

                e.Graphics.DrawImage(Properties.Resources.leftTop, 6, 10 - 4, cornerSize, cornerSize);//top left corner
                e.Graphics.DrawImage(Properties.Resources.topRight, nameWidth-2, 10 - 4, cornerSize, cornerSize);//top right corner
                e.Graphics.DrawImage(Properties.Resources.bottomLeft, 6, nameHeight - 10, cornerSize, cornerSize);//bottom left
                e.Graphics.DrawImage(Properties.Resources.bottomRight, nameWidth - 2, nameHeight - 10, cornerSize, cornerSize);//bottom right

                e.Graphics.DrawString("NEW NAME", new Font("Pokemon GB", 18), Brushes.Black, 50, 50);

                if (player == "RED")
                {
                    e.Graphics.DrawString("RED", new Font("Pokemon GB", 18), Brushes.Black, 50, 100);
                    e.Graphics.DrawString("ASH", new Font("Pokemon GB", 18), Brushes.Black, 50, 150);
                    e.Graphics.DrawString("JACK", new Font("Pokemon GB", 18), Brushes.Black, 50, 200);
                }
                else
                {
                    e.Graphics.DrawString("BLUE", new Font("Pokemon GB", 18), Brushes.Black, 50, 100);
                    e.Graphics.DrawString("GARRY", new Font("Pokemon GB", 18), Brushes.Black, 50, 150);
                    e.Graphics.DrawString("JOHN", new Font("Pokemon GB", 18), Brushes.Black, 50, 200);
                }

                e.Graphics.DrawImage(Properties.Resources.pokemonSelect, 30, 50 * nameIndex + 50);
            }

            //Connecting lines
            e.Graphics.DrawImage(Properties.Resources.horizontal, x1 + 5, y1, x2 - x1, horizontalHeight);//top
            e.Graphics.DrawImage(Properties.Resources.horizontal, x1 + 5, y2, x2 - x1, horizontalHeight);//bottom
            e.Graphics.DrawImage(Properties.Resources.vertical, x1 + 9, y1, verticalWidth, y2 - y1);//left
            e.Graphics.DrawImage(Properties.Resources.vertical, x2 + 7, y1, verticalWidth, y2 - y1);//right

            //Corners
            e.Graphics.DrawImage(Properties.Resources.leftTop, x1 + 5, y1 - 4, cornerSize, cornerSize);//top left corner
            e.Graphics.DrawImage(Properties.Resources.topRight, x2 - 5, y1 - 4, cornerSize, cornerSize);//top right corner
            e.Graphics.DrawImage(Properties.Resources.bottomLeft, x1 + 5, y2 - 8, cornerSize, cornerSize);//bottom left
            e.Graphics.DrawImage(Properties.Resources.bottomRight, x2 - 5, y2 - 8, cornerSize, cornerSize);//bottom right

            if (textUp)
            {
                e.Graphics.DrawString(messageLines[Form1.messageIndex], new Font("Pokemon GB", 19), 
                    Brushes.Black, x1 + verticalWidth + 20, y1 + 30 + lift);
                if (lift < 10)
                {
                    e.Graphics.DrawString(messageLines[Form1.messageIndex + 1], 
                        new Font("Pokemon GB", 19), Brushes.Black, x1 + verticalWidth + 20, y1 + 100 + lift);
                }
            }
            else
            {
                e.Graphics.DrawString(messageLines[Form1.messageIndex], new Font("Pokemon GB", 19), 
                    Brushes.Black, x1 + verticalWidth + 20, y1 + 30);
                e.Graphics.DrawString(messageLines[Form1.messageIndex + 1], new Font("Pokemon GB", 19), 
                    Brushes.Black, x1 + verticalWidth + 20, y1 + 100);
                e.Graphics.DrawImage(Properties.Resources.nextTextPokemon, x2 - 30, selectY);
            }
        }

        public void SlideLeft()
        {
            slideIndex++;
            int length = x2 + spriteDimensions[slideIndex].Width - spriteLocation.X;
            spriteLocation.X = x2 + spriteDimensions[slideIndex].Width;
            for (int i = 0; i < length/6; i++)
            {
                spriteLocation.X-=6;
                Refresh();
            }
            Refresh();
            Form1.messageIndex+=2;
        }

        public void SlideText()
        {
            Form1.messageIndex++;
            textUp = true;
            for (int i = 25; i > 0; i--)
            {
                lift -= 2;
                Refresh();
            }
            lift = 50;
            textUp = false;
            Refresh();
        }

        public void SlideBack()
        {
            chooseName = false;

            int length = spriteLocation.X - (this.Width / 2 - spriteDimensions[slideIndex].Width / 2 + slide);
            
            for (int i = 0; i < length / 6; i++)
            {
                spriteLocation.X -= 6;
                Refresh();
            }

            Form1.messageIndex++;
            Refresh();
        }

        public void SlideRight()
        {
            int length = 360 - spriteLocation.X;
            for (int i = 0; i < length / 6; i++)
            {
                spriteLocation.X += 6;
                Refresh();
            }

            Refresh();
            chooseName = true;
        }

        private void StartScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (chooseName)
            {
                if(e.KeyCode == Keys.Down)
                {
                    if(nameIndex!=3)
                    nameIndex++;
                }
                else if(e.KeyCode == Keys.Up)
                {
                    if(nameIndex!=0)
                    nameIndex--;
                }
                else if(e.KeyCode == Keys.Space)
                {
                    switch(nameIndex)
                    {
                        case 0:
                            if(player == "RED")
                            {
                                Form1.gameName = true;
                            }
                            else
                            {
                                Form1.rName = true;
                            }
                            
                            Form f = this.FindForm();
                            NameScreen ns = new NameScreen();
                            f.Controls.Remove(this);
                            f.Controls.Add(ns);
                            break;
                        case 1:
                            if (player == "RED")
                            {
                                Form1.playerName = "RED";
                                messageLines[20] = "name is RED!";
                                messageLines[30] = "RED!";
                            }
                            else
                            {
                                Form1.rivalName = "BLUE";
                                messageLines[29] = "name is BLUE!";
                            }                          
                            break;
                        case 2:
                            if (player == "RED")
                            {
                                Form1.playerName = "ASH";
                                messageLines[20] = "name is ASH!";
                                messageLines[30] = "ASH!";
                            }
                            else
                            {
                                Form1.rivalName = "GARRY";
                                messageLines[29] = "name is GARRY!";
                            }
                            break;
                        case 3:
                            if (player == "RED")
                            {
                                Form1.playerName = "JACK";
                                messageLines[20] = "name is JACK!";
                                messageLines[30] = "JACK!";
                            }
                            else
                            {
                                Form1.rivalName = "JOHN";
                                messageLines[29] = "name is JOHN!";
                            }
                            break;
                    }
                    SlideBack();
                }
                Refresh();
            }
            else
            {
                if (e.KeyCode == Keys.Space)
                {
                    if (Form1.messageIndex == 36)
                    {
                        gameTimer.Stop();
                        Thread.Sleep(500);                     
                        slideIndex =4;
                        spriteLocation.X = this.Width / 2 - spriteDimensions[slideIndex].Width / 2 + slide;
                        spriteLocation.Y = y1 - spriteDimensions[slideIndex].Height - 50;
                        Refresh();
                        Thread.Sleep(500);
                        slideIndex++;
                        spriteLocation.X = this.Width / 2 - spriteDimensions[slideIndex].Width / 2 + slide;
                        spriteLocation.Y = y1 - spriteDimensions[slideIndex].Height - 70;
                        Refresh();
                        Thread.Sleep(500);
                        Form f = this.FindForm();
                        f.Controls.Remove(this);
                        GameScreen gs = new GameScreen();
                        f.Controls.Add(gs);
                    }
                    else if (Form1.messageIndex == 4)
                    {
                        SlideLeft();
                    }
                    else if (Form1.messageIndex == 15)
                    {
                        SlideLeft();
                    }
                    else if (Form1.messageIndex == 17)
                    {
                        SlideRight();
                        player = "RED";
                    }
                    else if (Form1.messageIndex == 20)
                    {
                        slideIndex++;
                        Form1.messageIndex++;
                        spriteLocation.X = this.Width / 2 - spriteDimensions[slideIndex].Width / 2 + slide;
                        spriteLocation.Y = y1 - spriteDimensions[slideIndex].Height - 20;
                        Refresh();
                    }
                    else if (Form1.messageIndex == 25)
                    {
                        SlideRight();
                        player = "BLUE";
                    }
                    else if (Form1.messageIndex == 28)
                    {
                        slideIndex = 2;
                        spriteLocation.X = this.Width / 2 - spriteDimensions[slideIndex].Width / 2 + slide;
                        spriteLocation.Y = y1 - spriteDimensions[slideIndex].Height - 20;
                        Form1.messageIndex+=2;
                        Refresh();
                    }
                    else
                    {
                        SlideText();
                    }
                }
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            textCounter++;
            if (textCounter == 1)
            {
                selectY = this.Height - cornerSize - 30;
                textCounter = -1;
            }
            else
            {
                selectY = this.Height - cornerSize * 2 - 30;
            }
            Refresh();
        }
    }
}
