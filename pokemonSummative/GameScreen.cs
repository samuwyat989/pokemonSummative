using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace pokemonSummative
{
    public partial class GameScreen : UserControl
    {
        public GameScreen()
        {
            InitializeComponent();
        }

        bool leftDown, rightDown, upDown, downDown, move, checkBound = true;
        public static int screenX = 5, screenY = 5, tileSize, moveSpeed = 2;
        string direction, gameRegion = "Lab";

        public static List<int> lineXVals = new List<int>();
        public static List<int> lineYVals = new List<int>();

        Character player;
        List<Boundary> boundaries = new List<Boundary>();

        private void GameScreen_Load(object sender, EventArgs e)
        {
            tileSize = (this.Width - 10)/10;

            for (int i = 0; i < 11; i++)
            {
                lineXVals.Add(screenX + tileSize * i);
                if(i != 10)
                lineYVals.Add(screenY + tileSize * i);
            }

            int playerX = lineXVals[4];
            int playerY = lineYVals[4];
            int playerSize = lineXVals[4] - lineXVals[3];

            player = new Character(playerX, playerY, playerSize);

            LoadRoom();

            Refresh();
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    rightDown = false;
                    RoundTile("Left");
                    break;
                case Keys.Right:
                    leftDown = false;
                    RoundTile("Right");
                    break;
                case Keys.Up:
                    downDown = false;
                    RoundTile("Up");
                    break;
                case Keys.Down:
                    upDown = false;
                    RoundTile("Down");
                    break;
            }
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 11; i++)
            {
                lineXVals[i] = screenX + tileSize * i;               
                e.Graphics.DrawLine(Pens.Red, new Point(lineXVals[i], screenY), new Point(lineXVals[i], screenY + tileSize*9));//verticle
                if (i != 10)
                {
                    lineYVals[i] = screenY + tileSize * i;
                    e.Graphics.DrawLine(Pens.Red, new Point(screenX, lineYVals[i]), new Point(screenX + tileSize*10, lineYVals[i]));//horizontal
                }
            }

            e.Graphics.FillRectangle(Brushes.Green, player.x, player.y, player.size, player.size);

            foreach(Boundary b in boundaries)
            {
                e.Graphics.DrawRectangle(Pens.Blue, lineXVals[b.xTileIndex], lineYVals[b.yTileIndex],
                tileSize * b.tileWidth, tileSize * b.tileHeight);
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            move = true;

            if (leftDown)
            {
                direction = "Left";
            }
            else if (rightDown)
            {
                direction = "Right";
            }
            else if (upDown)
            {
                direction = "Up";
            }
            else if (downDown)
            {
                direction = "Down";
            }
            else
            {
                direction = "none";
            }
            
            foreach(Boundary b in boundaries)
            {
                if (b.Intersect(player, direction, checkBound))
                {
                    move = false;
                    checkBound = true;
                    break;
                }
                if(checkBound)
                {
                    checkBound = false;
                }
            }

            if (move)
            {
                player.Move(direction, moveSpeed);
                UpdateBoundaries();
            }

            Refresh();
        }
     
        public void UpdateBoundaries()
        {
            foreach(Boundary b in boundaries)
            {
                b.Move(direction, moveSpeed);
            }
        }

        public void LoadRoom()
        {
            boundaries.Clear();
            switch (gameRegion)
            {
                case "playerRoom":
                    boundaries.Add(new Boundary(0, 0, 8, 8));//Boarder
                    boundaries.Add(new Boundary(0, 0, 8, 1));//wall
                    boundaries.Add(new Boundary(3, 4, 1, 1));//tv
                    boundaries.Add(new Boundary(3, 5, 1, 1));//snes
                    boundaries.Add(new Boundary(0, 6, 1, 2));//bed
                    boundaries.Add(new Boundary(6, 6, 1, 2));//plant
                    boundaries.Add(new Boundary(0, 1, 3, 1));//computer/desk
                    break;
                case "playerHouse":
                    boundaries.Add(new Boundary(2, 0, 8, 8));//Boarder
                    boundaries.Add(new Boundary(2, 0, 8, 1));//wall
                    boundaries.Add(new Boundary(2, 1, 2, 1));//books
                    boundaries.Add(new Boundary(5, 1, 1, 1));//tv
                    boundaries.Add(new Boundary(5, 4, 2, 2));//table
                    break;
                case "rivalHouse":
                    boundaries.Add(new Boundary(2, 0, 8, 8));//Boarder
                    boundaries.Add(new Boundary(2, 0, 8, 1));//wall
                    boundaries.Add(new Boundary(2, 1, 2, 1));//books
                    boundaries.Add(new Boundary(9, 1, 1, 1));//books
                    boundaries.Add(new Boundary(5, 3, 2, 2));//table
                    boundaries.Add(new Boundary(2, 6, 1, 2));//plant
                    boundaries.Add(new Boundary(9, 6, 1, 2));//plant
                    break;
                case "Outside":
                    break;
                case "Lab":
                    boundaries.Add(new Boundary(0, 0, 10, 12));//Boarder
                    boundaries.Add(new Boundary(0, 0, 10, 1));//wall
                    boundaries.Add(new Boundary(0, 1, 4, 1));//tables
                    boundaries.Add(new Boundary(6, 1, 4, 1));//books
                    boundaries.Add(new Boundary(6, 3, 3, 1));//tabl
                    boundaries.Add(new Boundary(0, 6, 4, 2));//books
                    boundaries.Add(new Boundary(6, 6, 4, 2));//wall
                    break;
            }
        }

        public void RoundTile(string direction)
        {
            int length = 0, closeX1, closeX2, closeY1, closeY2;

            switch (direction)
            {
                case "Left":
                    closeX1 = lineXVals.OrderBy(item => Math.Abs(player.x - item)).First();
                    closeX2 = lineXVals.OrderBy(item => Math.Abs(player.x - item)).ElementAt(1);

                    if(player.x < closeX1)
                    {
                        length = player.x - lineXVals[lineXVals.IndexOf(closeX1) - 1];
                        for(int i = 0; i < length; i++)
                        {
                            screenX++;
                            UpdateBoundaries();
                            Refresh();
                        }
                    }
                    else if(player.x > closeX1)
                    {
                        length = player.x - lineXVals[lineXVals.IndexOf(closeX1)];
                        for (int i = 0; i < length; i++)
                        {
                            screenX++;
                            UpdateBoundaries();
                            Refresh();
                        }
                    }
                    break;
                case "Right":
                    closeX1 = lineXVals.OrderBy(item => Math.Abs(player.x + player.size - item)).First();
                    closeX2 = lineXVals.OrderBy(item => Math.Abs(player.x + player.size - item)).ElementAt(1);

                    if (player.x + player.size < closeX1)
                    {
                        length = lineXVals[lineXVals.IndexOf(closeX1)] - (player.x+player.size);
                        for (int i = 0; i < length; i++)
                        {
                            screenX--;
                            UpdateBoundaries();
                            Refresh();
                        }
                    }
                    else if (player.x + player.size > closeX1)
                    {
                        length = lineXVals[lineXVals.IndexOf(closeX1)] - player.x;
                        for (int i = 0; i < length; i++)
                        {
                            screenX--;
                            UpdateBoundaries();
                            Refresh();
                        }
                    }
                    break;
                case "Up":
                    closeY1 = lineYVals.OrderBy(item => Math.Abs(player.y - item)).First();
                    closeY2 = lineYVals.OrderBy(item => Math.Abs(player.y - item)).ElementAt(1);

                    if (player.y < closeY1)
                    {
                        length = player.y - lineYVals[lineYVals.IndexOf(closeY1) - 1];
                        for (int i = 0; i < length; i++)
                        {
                            screenY++;
                            UpdateBoundaries();
                            Refresh();
                        }
                    }
                    else if (player.y > closeY1)
                    {
                        length = player.y - lineYVals[lineYVals.IndexOf(closeY1)];
                        for (int i = 0; i < length; i++)
                        {
                            screenY++;
                            UpdateBoundaries();
                            Refresh();
                        }
                    }
                    break;
                case "Down":
                    closeY1 = lineYVals.OrderBy(item => Math.Abs(player.y + player.size - item)).First();
                    closeY2 = lineYVals.OrderBy(item => Math.Abs(player.y + player.size - item)).ElementAt(1);

                    if (player.y + player.size < closeY1)
                    {
                        length = lineYVals[lineYVals.IndexOf(closeY1)] - (player.y + player.size);
                        for (int i = 0; i < length; i++)
                        {
                            screenY--;
                            UpdateBoundaries();
                            Refresh();
                        }
                    }
                    else if (player.y + player.size > closeY1)
                    {
                        length = lineYVals[lineYVals.IndexOf(closeY1)] - player.y;
                        for (int i = 0; i < length; i++)
                        {
                            screenY--;
                            UpdateBoundaries();
                            Refresh();
                        }
                    }
                    break; 
            }          
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    rightDown = true;
                    break;
                case Keys.Right:
                    leftDown = true;
                    break;
                case Keys.Up:
                    downDown = true;
                    break;
                case Keys.Down:
                    upDown = true;
                    break;
            }           
        }
    }
}
