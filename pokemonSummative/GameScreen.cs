using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace pokemonSummative
{
    public partial class GameScreen : UserControl
    {
        public GameScreen()
        {
            InitializeComponent();
            publicTimer = gameTimer;
        }

        public static System.Windows.Forms.Timer publicTimer;
        public static bool gotStarter;
        bool leftDown, rightDown, upDown, downDown, move, checkBound = true, newGame = true, storyEvent, grassEvent = true, 
            chooseStarter, message, textUp;       
        public static int screenX = 5, screenY = 5, tileSize, moveSpeed = 2, messageIndex = 0, messageBoxHeight = 150, lift = 50, selectY, x2, 
        verticalWidth = Properties.Resources.vertical.Width, 
            horizontalHeight = Properties.Resources.horizontal.Height, nameWidth = 250, nameHeight = 300, x1 = 0, y1 = 330,
            cornerSize = Properties.Resources.bottomLeft.Width;
        public static string direction, gameRegionName = "playerRoom", faceDirection = "Down";

        public static Dictionary<string, Size> gameRegions = new Dictionary<string, Size>();
        string[] screenMessage;
        public static List<int> lineXVals = new List<int>();
        public static List<int> lineYVals = new List<int>();

        Image[] backgroundImages = new Image[] {
            Properties.Resources.playerRoomBack,
            Properties.Resources.playerHouseBack,
            Properties.Resources.outsideBack,
            Properties.Resources.pokemonLabBack,
            Properties.Resources.rivalHouseBack};
        Image pokeBall = Properties.Resources.pokeBall;
        int imageIndex = 0;

        List<string> areaTrackList = new List<string>();
        List<Character> npc = new List<Character>();

        Character player;
        List<Boundary> boundaries = new List<Boundary>();

        private void GameScreen_Load(object sender, EventArgs e)
        {
            selectY = this.Height - cornerSize - 30;
            x2 = this.Width - cornerSize;

            gameRegions.Add("playerRoom", new Size(8, 8));
            gameRegions.Add("playerHouse", new Size(8, 8));
            gameRegions.Add("rivalHouse", new Size(8, 8));
            gameRegions.Add("Lab", new Size(10, 12));
            gameRegions.Add("Outside", new Size(18, 17));

            areaTrackList.Add("playerRoom");
            areaTrackList.Add("playerRoom");

            tileSize = (this.Width - 10)/10;

            for (int i = 0; i < 11; i++)
            {
                lineXVals.Add(screenX + tileSize * i);
                if(i != 10)
                lineYVals.Add(screenY + tileSize * i);
            }

            player = new Character(lineXVals[4], lineYVals[4], tileSize, 4, 4, new[] { "..." }, "Down");
            player.playerImages[0] = Properties.Resources.playerFront;
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
            
            for (int i = 0; i <= gameRegions[gameRegionName].Width; i++)
            {
                lineXVals[i] = screenX + tileSize * i;               
                e.Graphics.DrawLine(Pens.Red, new Point(lineXVals[i], screenY), new Point(lineXVals[i], screenY + tileSize* gameRegions[gameRegionName].Height));//verticle
            }
            for (int i = 0; i <= gameRegions[gameRegionName].Height; i++)
            {
                lineYVals[i] = screenY + tileSize * i;
                e.Graphics.DrawLine(Pens.Red, new Point(screenX, lineYVals[i]), new Point(screenX + tileSize * gameRegions[gameRegionName].Width, lineYVals[i]));//horizontal
            }
            if (imageIndex == 2)
            {
                e.Graphics.DrawImage(backgroundImages[imageIndex], lineXVals[0] - 2 * tileSize, lineYVals[0] - tileSize,
                tileSize * gameRegions[gameRegionName].Width + 4 * tileSize,
                tileSize * gameRegions[gameRegionName].Height + 2 * tileSize);
            }
            else
            {
                e.Graphics.DrawImage(backgroundImages[imageIndex], lineXVals[0], lineYVals[0],
                    tileSize * gameRegions[gameRegionName].Width,
                    tileSize * gameRegions[gameRegionName].Height);
            }
            if (imageIndex == 3)
            {
                e.Graphics.DrawImage(pokeBall, lineXVals[6], lineYVals[3], tileSize, tileSize);
                e.Graphics.DrawImage(pokeBall, lineXVals[7], lineYVals[3], tileSize, tileSize);
                e.Graphics.DrawImage(pokeBall, lineXVals[8], lineYVals[3], tileSize, tileSize);
            }
            e.Graphics.FillRectangle(Brushes.Green, player.x, player.y, player.size, player.size);
            //e.Graphics.DrawImage(player.playerImages[0], player.x, player.x, player.size, player.size);

            switch (faceDirection)
            {
                case "Right":
                    e.Graphics.FillRectangle(Brushes.Orange, player.x+5, player.y + player.size/2 - 3, 6, 6);
                    break;
                case "Left":
                    e.Graphics.FillRectangle(Brushes.Orange, player.x + player.size - 11, player.y + player.size/2 - 3, 6, 6);
                    break;
                case "Down":
                    e.Graphics.FillRectangle(Brushes.Orange, player.x + player.size/2 - 3, player.y +5 , 6, 6);
                    break;
                case "Up":
                    e.Graphics.FillRectangle(Brushes.Orange, player.x + player.size / 2 - 3, player.y + player.size - 11, 6, 6);
                    break;
            }

            foreach(Boundary b in boundaries)
            {
                if(b.messages.Count == 0)
                {
                    e.Graphics.DrawRectangle(Pens.Blue, lineXVals[b.xTileIndex], lineYVals[b.yTileIndex],
                    tileSize * b.tileWidth, tileSize * b.tileHeight);
                }
                else if (b.messages[0] == "Exit")
                {
                    e.Graphics.DrawRectangle(Pens.Purple, lineXVals[b.xTileIndex], lineYVals[b.yTileIndex],
                    tileSize * b.tileWidth, tileSize * b.tileHeight);
                }
                else
                {
                    e.Graphics.DrawRectangle(Pens.Blue, lineXVals[b.xTileIndex], lineYVals[b.yTileIndex],
                    tileSize * b.tileWidth, tileSize * b.tileHeight);
                }
            }
            foreach(Character c in npc)
            {
                if (storyEvent && c.messages[0] == "Oak")
                { }
                else
                {
                    e.Graphics.DrawRectangle(Pens.Yellow, lineXVals[c.xTileIndex], lineYVals[c.yTileIndex], tileSize, tileSize);

                    switch (c.faceDirection)
                    {
                        case "Right":
                            e.Graphics.FillRectangle(Brushes.Orange, lineXVals[c.xTileIndex] + 5, lineYVals[c.yTileIndex] + tileSize / 2 - 3, 6, 6);
                            break;
                        case "Left":
                            e.Graphics.FillRectangle(Brushes.Orange, lineXVals[c.xTileIndex] + tileSize - 11, lineYVals[c.yTileIndex] + tileSize / 2 - 3, 6, 6);
                            break;
                        case "Down":
                            e.Graphics.FillRectangle(Brushes.Orange, lineXVals[c.xTileIndex] + tileSize / 2 - 3, lineYVals[c.yTileIndex] + 5, 6, 6);
                            break;
                        case "Up":
                            e.Graphics.FillRectangle(Brushes.Orange, lineXVals[c.xTileIndex] + tileSize / 2 - 3, lineYVals[c.yTileIndex] + tileSize - 11, 6, 6);
                            break;
                    }
                }
            }

            if(storyEvent)
            {
                if(grassEvent)
                {
                    e.Graphics.DrawRectangle(Pens.Yellow, npc[npc.Count-1].x, npc[npc.Count - 1].y, tileSize, tileSize);

                    switch (npc[npc.Count - 1].faceDirection)
                    {
                        case "Right":
                            e.Graphics.FillRectangle(Brushes.Orange, npc[npc.Count - 1].x + 5, npc[npc.Count - 1].y + tileSize / 2 - 3, 6, 6);
                            break;
                        case "Left":
                            e.Graphics.FillRectangle(Brushes.Orange, npc[npc.Count - 1].x + tileSize - 11, npc[npc.Count - 1].y + tileSize / 2 - 3, 6, 6);
                            break;
                        case "Down":
                            e.Graphics.FillRectangle(Brushes.Orange, npc[npc.Count - 1].x + tileSize / 2 - 3, npc[npc.Count - 1].y + 5, 6, 6);
                            break;
                        case "Up":
                            e.Graphics.FillRectangle(Brushes.Orange, npc[npc.Count - 1].x + tileSize / 2 - 3, npc[npc.Count - 1].y + tileSize - 11, 6, 6);
                            break;
                    }
                }
            }

            if (message)
            {
                e.Graphics.FillRectangle(Brushes.White, new Rectangle(0, this.Height - messageBoxHeight, this.Width, messageBoxHeight));

                e.Graphics.DrawImage(Properties.Resources.horizontal, 8, this.Height - messageBoxHeight + 4, this.Width, horizontalHeight);//top
                e.Graphics.DrawImage(Properties.Resources.horizontal, 8, this.Height - horizontalHeight, this.Width, horizontalHeight);//bottom
                e.Graphics.DrawImage(Properties.Resources.vertical, 14, this.Height - messageBoxHeight, verticalWidth, messageBoxHeight);//left
                e.Graphics.DrawImage(Properties.Resources.vertical, this.Width - verticalWidth - 8, this.Height - messageBoxHeight, verticalWidth, messageBoxHeight);//right

                e.Graphics.DrawImage(Properties.Resources.leftTop, 6, this.Height - messageBoxHeight, cornerSize, cornerSize);//top left corner
                e.Graphics.DrawImage(Properties.Resources.topRight, this.Width - cornerSize, this.Height - messageBoxHeight, cornerSize, cornerSize);//top right corner
                e.Graphics.DrawImage(Properties.Resources.bottomLeft, 6, this.Height - cornerSize, cornerSize, cornerSize);//bottom left
                e.Graphics.DrawImage(Properties.Resources.bottomRight, this.Width - cornerSize, this.Height - cornerSize, cornerSize, cornerSize);//bottom

                if (textUp)
                {
                    e.Graphics.DrawString(screenMessage[messageIndex], new Font("Pokemon GB", 19),
                        Brushes.Black, x1 + verticalWidth + 20, y1 + 30 + lift);
                    if (lift < 15)
                    {
                        e.Graphics.DrawString(screenMessage[messageIndex+1],
                            new Font("Pokemon GB", 19), Brushes.Black, x1 + verticalWidth + 20, y1 + 100 + lift);
                    }
                }
                else
                {
                    e.Graphics.DrawString(screenMessage[messageIndex], new Font("Pokemon GB", 19),
                        Brushes.Black, x1 + verticalWidth + 20, y1 + 30);
                    if (screenMessage.Count() != -1)
                    {
                        e.Graphics.DrawString(screenMessage[messageIndex + 1], new Font("Pokemon GB", 19),
                            Brushes.Black, x1 + verticalWidth + 20, y1 + 100);
                    }
                    e.Graphics.DrawImage(Properties.Resources.nextTextPokemon, x2 - 30, selectY);
                }
            }

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            move = true;

            if (leftDown)
            {
                direction = "Left";
                faceDirection = "Left";
            }
            else if (rightDown)
            {
                direction = "Right";
                faceDirection = "Right";
            }
            else if (upDown)
            {
                direction = "Up";
                faceDirection = "Up";
            }
            else if (downDown)
            {
                direction = "Down";
                faceDirection = "Down";
            }
            else
            {
                direction = "none";
            }

            if (chooseStarter)
            {
                if (player.x == lineXVals[4] && player.y == lineYVals[6] ||
                   player.x == lineXVals[5] && player.y == lineYVals[6])
                {
                    gameTimer.Stop();
                    MessageBox.Show("OAK: Hey! Don't go away yet!");
                    faceDirection = "Down";
                    for (int i = 0; i < tileSize / moveSpeed; i++)
                    {
                        player.Move("Down", moveSpeed);
                        Refresh();
                        Thread.Sleep(gameTimer.Interval);
                    }
                    gameTimer.Start();
                }
            }
            if (gotStarter)
            {


                if (player.x == lineXVals[4] && player.y == lineYVals[6] ||
                   player.x == lineXVals[5] && player.y == lineYVals[6])
                {

                }
            }

            foreach (Boundary b in boundaries)
            {
                if (b.messages.Count == 0)
                {
                    if (b.Intersect(player, direction, checkBound))
                    {
                        move = false;
                        break;
                    }
                    if (checkBound)
                    {
                        checkBound = false;
                    }
                }
                else if (b.messages[0] != "Exit")
                {
                    if (b.Intersect(player, direction, checkBound))
                    {
                        move = false;
                        break;
                    }
                    if (checkBound)
                    {
                        checkBound = false;
                    }
                }
            }
            checkBound = true;

            foreach (Character c in npc)
            {
                if (c.IntersectNPC(player, direction))
                {
                    move = false;
                    break;
                }
            }

            if (move)
            {
                player.Move(direction, moveSpeed);
                UpdateCharacters();
                UpdateBoundaries();
            }

            if (player.CheckExit(boundaries))
            {
                if (gameRegionName == "playerRoom" && faceDirection == "Left" ||
                    gameRegionName == "playerRoom" && faceDirection == "Down")
                {
                    gameRegionName = "playerHouse";
                    faceDirection = "Up";
                    areaTrackList.Add("playerHouse");
                    LoadRoom();
                }
                else if (gameRegionName == "playerHouse")
                {
                    if (player.x == lineXVals[7] && faceDirection == "Down" ||
                        player.x == lineXVals[7] && faceDirection == "Left")
                    {
                        gameRegionName = "playerRoom";
                        faceDirection = "Up";
                        areaTrackList.Add("playerRoom");
                        LoadRoom();
                    }
                    else if (player.x != lineXVals[7] && faceDirection == "Up")
                    {
                        gameRegionName = "Outside";
                        faceDirection = "Up";
                        areaTrackList.Add("Outside");
                        LoadRoom();
                    }
                }
                else if (gameRegionName == "Outside")
                {
                    if (player.x == lineXVals[4] && faceDirection == "Down")
                    {
                        gameRegionName = "playerHouse";
                        faceDirection = "Down";
                        areaTrackList.Add("playerHouse");
                    }
                    else if (player.x == lineXVals[11])
                    {
                        gameRegionName = "Lab";
                        faceDirection = "Down";
                        areaTrackList.Add("Lab");
                    }
                    else if (player.x == lineXVals[12])
                    {
                        gameRegionName = "rivalHouse";
                        faceDirection = "Down";
                        areaTrackList.Add("rivalHouse");
                    }
                    else
                    {
                        gameTimer.Stop();
                        faceDirection = "Up";
                        MessageBox.Show("OAK: Hey! Wait! Don't go out!");
                        npc.Add(new Character(lineXVals[7], lineYVals[4], tileSize, 7, 4, new string[] {"Oak"}, "Right"));
                        npc[npc.Count - 1].faceDirection = "Down";

                        storyEvent = true;
                        for (int i = 0; i < tileSize * 5 / moveSpeed; i++)
                        {
                            if (i < tileSize / moveSpeed)
                            {
                                npc[npc.Count - 1].y -= moveSpeed;
                            }
                            else if (i >= tileSize / moveSpeed && i < tileSize / moveSpeed * 2)
                            {
                                npc[npc.Count - 1].x += moveSpeed;
                                npc[npc.Count - 1].faceDirection = "Left";
                            }
                            else if (i >= tileSize / moveSpeed * 2 && i < tileSize / moveSpeed * 3)
                            {
                                npc[npc.Count - 1].y -= moveSpeed;
                                npc[npc.Count - 1].faceDirection = "Down";
                            }
                            else if (i >= tileSize / moveSpeed * 3 && i < tileSize / moveSpeed * 4)
                            {
                                npc[npc.Count - 1].x += moveSpeed;
                                npc[npc.Count - 1].faceDirection = "Left";
                            }
                            else if (i >= tileSize / moveSpeed * 4)
                            {
                                npc[npc.Count - 1].y -= moveSpeed;
                                npc[npc.Count - 1].faceDirection = "Down";
                            }
                            Refresh();
                            Thread.Sleep(gameTimer.Interval);
                        }
                        npc[npc.Count - 1].faceDirection = "Up";
                        MessageBox.Show("OAK: It's unsafe! Wild POKeMON live in tall grass! You need your own POKeMON for your protection. I know! Here come with me.");

                        for (int i = 0; i < tileSize * 5 / moveSpeed; i++)
                        {
                            player.Move("Up", moveSpeed);
                            npc[npc.Count - 1].y = player.y + tileSize;
                            Refresh();
                            Thread.Sleep(gameTimer.Interval);
                        }

                        npc[npc.Count - 1].faceDirection = "Right";
                        for (int i = 0; i < tileSize / moveSpeed; i++)
                        {
                            player.Move("Up", moveSpeed);
                            npc[npc.Count - 1].y -= moveSpeed;
                            npc[npc.Count - 1].x -= moveSpeed;
                            Refresh();
                            Thread.Sleep(gameTimer.Interval);
                        }

                        npc[npc.Count - 1].faceDirection = "Up";
                        faceDirection = "Right";
                        for (int i = 0; i < tileSize / moveSpeed; i++)
                        {
                            player.Move("Right", moveSpeed);
                            npc[npc.Count - 1].y += moveSpeed;
                            npc[npc.Count - 1].x += moveSpeed;
                            Refresh();
                            Thread.Sleep(gameTimer.Interval);
                        }

                        faceDirection = "Up";
                        for (int i = 0; i < tileSize * 4 / moveSpeed; i++)
                        {
                            player.Move("Up", moveSpeed);
                            npc[npc.Count - 1].y = player.y + tileSize;
                            Refresh();
                            Thread.Sleep(gameTimer.Interval);
                        }

                        faceDirection = "Up";
                        npc[npc.Count - 1].faceDirection = "Left";
                        for (int i = 0; i < tileSize / moveSpeed; i++)
                        {
                            player.Move("Up", moveSpeed);
                            npc[npc.Count - 1].y -= moveSpeed;
                            npc[npc.Count - 1].x += moveSpeed;
                            Refresh();
                            Thread.Sleep(gameTimer.Interval);
                        }

                        faceDirection = "Left";
                        for (int i = 0; i < tileSize * 2 / moveSpeed; i++)
                        {
                            player.Move("Left", moveSpeed);
                            npc[npc.Count - 1].x = player.x + player.size;
                            Refresh();
                            Thread.Sleep(gameTimer.Interval);
                        }

                        npc[npc.Count - 1].faceDirection = "Down";
                        for (int i = 0; i < tileSize / moveSpeed; i++)
                        {
                            player.Move("Left", moveSpeed);
                            npc[npc.Count - 1].y -= moveSpeed;
                            npc[npc.Count - 1].x -= moveSpeed;
                            Refresh();
                            Thread.Sleep(gameTimer.Interval);
                        }

                        faceDirection = "Down";
                        for (int i = 0; i < tileSize / moveSpeed; i++)
                        {
                            npc[npc.Count - 1].y += moveSpeed;
                            player.Move("Down", moveSpeed);
                            Refresh();
                            Thread.Sleep(gameTimer.Interval);
                        }

                        gameRegionName = "Lab";
                        faceDirection = "Down";
                        areaTrackList.Add("Lab");
                        LoadRoom();

                        npc.Add(new Character(lineXVals[5], lineYVals[10], tileSize, 5, 10, new string[] { "Oak" }, "Down"));
                        for (int i = 0; i < tileSize * 3 / moveSpeed; i++)
                        {
                            npc[npc.Count - 1].y -= moveSpeed;
                            Refresh();
                            Thread.Sleep(gameTimer.Interval);
                        }

                        npc[npc.Count - 1].y -= tileSize * 5;
                        npc[npc.Count - 1].faceDirection = "Up";
                        npc[0].faceDirection = "Down";
                        Refresh();

                        for (int i = 0; i < tileSize * 8 / moveSpeed; i++)
                        {
                            player.Move("Down", moveSpeed);
                            npc[npc.Count - 1].y += moveSpeed;
                            Refresh();
                            Thread.Sleep(gameTimer.Interval);
                        }

                        MessageBox.Show("BLUE: Gramps! I'm fed up with waiting!");
                        MessageBox.Show("OAK: BLUE? Let me think... Oh, that's right, I told you to come! Just wait! " +
                            "Here, RED! There are 3 POKeMON here! Haha! They are inside the POKe BALLs. When I was young, " +
                            "I was a serious POKeMON trainer! In my old age, I have only 3 left, but you can have one! Choose!");
                        MessageBox.Show("BLUE: Hey! Gramps! What about me?");
                        MessageBox.Show("OAK: Be patient! BLUE, you can have one too!");

                       // npc[0].message = "BLUE: Heh, I don't need to be greedy like you! Go ahead and choose, RED!";
                       // npc[npc.Count - 1].message = "OAK: Now, RED, which POKeMON do you want?";
                        npc[npc.Count - 1].xTileIndex = 5;
                        npc[npc.Count - 1].yTileIndex = 2;
                        storyEvent = false;
                        chooseStarter = true;
                        gameTimer.Start();
                        return;
                    }
                    LoadRoom();
                }
                else if (gameRegionName == "rivalHouse" && faceDirection == "Up")
                {
                    gameRegionName = "Outside";
                    faceDirection = "Up";
                    areaTrackList.Add("Ouside");
                    LoadRoom();
                }
                else if (gameRegionName == "Lab" && faceDirection == "Up")
                {
                    gameRegionName = "Outside";
                    faceDirection = "Up";
                    areaTrackList.Add("Outside");

                    LoadRoom();
                }
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

        public void UpdateCharacters()
        {
            foreach (Character c in npc)
            {
                c.MoveNPC(direction, moveSpeed);
            }
        }

        public void AddMessage(Boundary b, string[] messages)
        {
            b.messages.AddRange(messages);
        }

        public void LoadRoom()
        {
            boundaries.Clear();
            npc.Clear();

            if (newGame)
            {
                screenY -= tileSize * 2;
                screenX += tileSize;

                boundaries.Add(new Boundary(0, 0, 8, 8));//Boarder
                boundaries.Add(new Boundary(0, 0, 8, 1));//wall
                boundaries.Add(new Boundary(3, 4, 1, 1));//tv
                boundaries.Add(new Boundary(3, 5, 1, 1));//snes
                AddMessage(boundaries[3], 
                    new string[] { Form1.playerName + " is", "playing the SNES!", "...Okay!", "It's time to go!" });
                boundaries.Add(new Boundary(0, 6, 1, 2));//bed
                boundaries.Add(new Boundary(6, 6, 1, 2));//plant
                boundaries.Add(new Boundary(0, 1, 3, 1));//computer/desk
                boundaries.Add(new Boundary(7, 1, 1, 1));//exit to house
                AddMessage(boundaries[7], new string[] { "Exit" });

                Refresh();
                newGame = false;
            }
            else
            {
                switch (gameRegionName)
                {
                    case "playerRoom":
                        imageIndex = 0;
                        boundaries.Add(new Boundary(0, 0, 8, 8));//Boarder
                        boundaries.Add(new Boundary(0, 0, 8, 1));//wall
                        boundaries.Add(new Boundary(3, 4, 1, 1));//tv
                        boundaries.Add(new Boundary(3, 5, 1, 1));//snes
                        AddMessage(boundaries[3],
                            new string[] { Form1.playerName + " is", "playing the SNES!", "...Okay!", "It's time to go!" });
                        boundaries.Add(new Boundary(0, 6, 1, 2));//bed
                        boundaries.Add(new Boundary(6, 6, 1, 2));//plant
                        boundaries.Add(new Boundary(0, 1, 3, 1));//computer/desk
                        boundaries.Add(new Boundary(7, 1, 1, 1));//exit to house
                        AddMessage(boundaries[7], new string[] { "Exit" });

                        lineXVals.Clear();
                        lineYVals.Clear();
                        screenX = 5;
                        screenY = 5;

                        screenY += tileSize * 3;
                        screenX -= tileSize * 3;

                        for (int i = 0; i < 11; i++)
                        {
                            lineXVals.Add(screenX + tileSize * i);
                            if (i != 10)
                                lineYVals.Add(screenY + tileSize * i);
                        }
                        Refresh();
                        break;
                    case "playerHouse":
                        imageIndex = 1;
                        boundaries.Add(new Boundary(0, 0, 8, 8));//Boarder
                        boundaries.Add(new Boundary(0, 0, 8, 1));//wall
                        boundaries.Add(new Boundary(0, 1, 2, 1));//books
                        AddMessage(boundaries[2], new string[] { "Crammed full of",  "POKéMON books!" });
                        boundaries.Add(new Boundary(3, 1, 1, 1));//tv
                        AddMessage(boundaries[3], 
                            new string[] { "There's a movie", "on TV. Four boys", "are walking on", "railroad tracks.", "I better go too.",
                                "Oops, wrong side" });
                        boundaries.Add(new Boundary(3, 4, 2, 2));//table
                        boundaries.Add(new Boundary(2, 7, 2, 1));//exit to outside
                        AddMessage(boundaries[5], new string[] { "Exit" });
                        boundaries.Add(new Boundary(7, 1, 1, 1));//exit to room
                        AddMessage(boundaries[6], new string[] { "Exit" });

                        npc.Clear();
                        lineXVals.Clear();
                        lineYVals.Clear();
                        screenX = 5;
                        screenY = 5;

                        if (areaTrackList[areaTrackList.Count - 2] == "playerRoom")
                        {
                            screenY += tileSize * 3;
                            screenX -= tileSize * 3;
                        }
                        else
                        {
                            screenY -= tileSize * 3;
                            screenX += tileSize * 2;
                        }

                        for (int i = 0; i < 11; i++)
                        {
                            lineXVals.Add(screenX + tileSize * i);
                            if (i != 10)
                                lineYVals.Add(screenY + tileSize * i);
                        }

                        npc.Add(new Character(lineXVals[5], lineYVals[4], tileSize, 5, 4,
                            new string[] { "MOM: Right.", "All boys leave", "home some day.", "It said so on TV.", "PROF.OAK, next", "door, is looking", "for you." }, "Right"));

                       Refresh();
                        break;
                    case "rivalHouse":
                        imageIndex = 4;
                        boundaries.Add(new Boundary(0, 0, 8, 8));//Boarder
                        boundaries.Add(new Boundary(0, 0, 8, 1));//wall
                        boundaries.Add(new Boundary(0, 1, 2, 1));//books
                        AddMessage(boundaries[2], new string[] { "Crammed full of", "POKéMON books!" });
                        boundaries.Add(new Boundary(7, 1, 1, 1));//books
                        AddMessage(boundaries[3], new string[] { "Crammed full of", "POKéMON books!" });
                        boundaries.Add(new Boundary(3, 3, 2, 2));//table
                        boundaries.Add(new Boundary(0, 6, 1, 2));//plant
                        boundaries.Add(new Boundary(7, 6, 1, 2));//plant
                        boundaries.Add(new Boundary(2, 7, 2, 1));//exit
                        AddMessage(boundaries[7], new string[] { "Exit" });

                        lineXVals.Clear();
                        lineYVals.Clear();
                        screenX = 5;
                        screenY = 5;

                        screenY -= tileSize * 3;
                        screenX += tileSize * 2;

                        for (int i = 0; i < 11; i++)
                        {
                            lineXVals.Add(screenX + tileSize * i);
                            if (i != 10)
                                lineYVals.Add(screenY + tileSize * i);
                        }

                        npc.Add(new Character(lineXVals[2], lineYVals[3], tileSize, 2, 3,
                            new string[] { "Hi " + Form1.playerName + "!", Form1.rivalName + " is out at", "Grandpa's lab." }, "Left"));

                        Refresh();
                        break;
                    case "Outside":
                        imageIndex = 2;
                        boundaries.Add(new Boundary(0, 0, 18, 17));//Boarder
                        boundaries.Add(new Boundary(0, 0, 9, 1));//top Boarder
                        boundaries.Add(new Boundary(9, 0, 2, 1));//grass
                        AddMessage(boundaries[2], new string[] { "Exit" });
                        boundaries.Add(new Boundary(11, 0, 7, 1));//top Boarder

                        boundaries.Add(new Boundary(3, 2, 4, 2));//Red House
                        boundaries.Add(new Boundary(3, 4, 1, 1));//Red House
                        boundaries.Add(new Boundary(5, 4, 2, 1));//Red House
                        boundaries.Add(new Boundary(4, 4, 1, 1));//Red House door
                        AddMessage(boundaries[7], new string[] { "Exit" });
                        boundaries.Add(new Boundary(2, 4, 1, 1));//Sign 
                        AddMessage(boundaries[8], new string[] { Form1.playerName + "'s house" });
                        boundaries.Add(new Boundary(11, 2, 4, 2));//Blue House
                        boundaries.Add(new Boundary(11, 4, 1, 1));//Blue House
                        boundaries.Add(new Boundary(13, 4, 2, 1));//Blue House
                        boundaries.Add(new Boundary(12, 4, 1, 1));//Blue House door
                        AddMessage(boundaries[12], new string[] { "Exit" });

                        boundaries.Add(new Boundary(10, 4, 1, 1));//Sign 
                        AddMessage(boundaries[13], new string[] { Form1.rivalName + "'s house" });

                        boundaries.Add(new Boundary(3, 8, 3, 1));//fence
                        boundaries.Add(new Boundary(6, 8, 1, 1));//sign
                        AddMessage(boundaries[15], new string[] { "PALLET TOWN", "Shades of your",  "journey await!" });


                        boundaries.Add(new Boundary(9, 7, 6, 3));//lab
                        boundaries.Add(new Boundary(9, 10, 2, 1));//lab
                        boundaries.Add(new Boundary(11, 10, 1, 1));//lab door
                        AddMessage(boundaries[18], new string[] { "Exit" });

                        boundaries.Add(new Boundary(12, 10, 3, 1));//lab

                        boundaries.Add(new Boundary(9, 12, 3, 1));//fence
                        boundaries.Add(new Boundary(12, 12, 1, 1));//sign
                        AddMessage(boundaries[21], new string[] { "OAK POKéMON", "RESEARCH LAB" });
                        boundaries.Add(new Boundary(13, 12, 2, 1));//fence

                        boundaries.Add(new Boundary(0, 16, 1, 1));//bound
                        boundaries.Add(new Boundary(7, 16, 11, 1));//bound
                        boundaries.Add(new Boundary(3, 13, 4, 4));//lake

                        lineXVals.Clear();
                        lineYVals.Clear();
                        screenX = 5;
                        screenY = 5;

                        if (areaTrackList[areaTrackList.Count-2] == "playerHouse")
                        {
                            //screenY -= tileSize;
                        }
                        else if(areaTrackList[areaTrackList.Count - 2] == "rivalHouse")
                        {
                            screenX -= 8* tileSize;
                        }
                        else if (areaTrackList[areaTrackList.Count - 2] == "Lab")
                        {
                            screenY -= tileSize * 6;
                            screenX -= 7 * tileSize;
                        }

                        for (int i = 0; i < 19; i++)
                        {
                            lineXVals.Add(screenX + tileSize * i);
                        }
                        for (int i = 0; i < 18; i++)
                        {
                            lineYVals.Add(screenY + tileSize * i);
                        }

                        npc.Add(new Character(lineXVals[10], lineYVals[14], tileSize, 10, 14, //Towns folk
                           new string[] { "Technology is", "incredible!", "You can now store", "and recall items", "and POKéMON as", "data via PC!" }, "Up"));

                        npc.Add(new Character(lineXVals[2], lineYVals[7], tileSize, 2, 7, //Towns folk
                           new string[] { "I'm raising", "POKéMON too!", "When they get", "strong, they can", "protect me!" }, "Up"));

                        Refresh();

                        for(int i = 0; i < tileSize; i++)
                        {
                            screenY--;
                            Refresh();
                        }

                        break;
                    case "Lab":
                        imageIndex = 3;
                        boundaries.Add(new Boundary(0, 0, 10, 12));//Boarder
                        boundaries.Add(new Boundary(0, 0, 10, 1));//wall
                        boundaries.Add(new Boundary(0, 1, 4, 1));//tables
                        boundaries.Add(new Boundary(6, 1, 4, 1));//books
                        AddMessage(boundaries[3], new string[] { "Crammed full of", "POKéMON books!" });

                        boundaries.Add(new Boundary(6, 3, 1, 1));//table of pokemon
                        AddMessage(boundaries[4], new string[] { "Those are POKé", "BALLs. They", "contain POKéMON!" });
                        boundaries.Add(new Boundary(7, 3, 1, 1));//table of pokemon
                        AddMessage(boundaries[5], new string[] { "Those are POKé", "BALLs. They", "contain POKéMON!" });
                        boundaries.Add(new Boundary(8, 3, 1, 1));//table of pokemon
                        AddMessage(boundaries[6], new string[] { "Those are POKé", "BALLs. They", "contain POKéMON!" });

                        boundaries.Add(new Boundary(0, 6, 4, 2));//books
                        AddMessage(boundaries[7], new string[] { "Crammed full of", "POKéMON books!" });
                        boundaries.Add(new Boundary(6, 6, 4, 2));//books
                        AddMessage(boundaries[8], new string[] { "Crammed full of", "POKéMON books!" });

                        boundaries.Add(new Boundary(4, 11, 2, 1));//exit
                        AddMessage(boundaries[9], new string[] { "Exit" });

                        lineXVals.Clear();
                        lineYVals.Clear();
                        screenX = 5;
                        screenY = 5;

                        screenX -= tileSize;
                        screenY -= tileSize *7;

                        for (int i = 0; i < 11; i++)
                        {
                            lineXVals.Add(screenX + tileSize * i);
                        }
                        for(int i = 0; i < 13; i++)
                        {
                            lineYVals.Add(screenY + tileSize * i);
                        }

                        npc.Add(new Character(lineXVals[4], lineYVals[3], tileSize, 4, 3, 
                            new string[] { Form1.rivalName + ": Yo", Form1.playerName + "! Gramps", "isn't around!" }, "Up"));//blue

                        npc.Add(new Character(lineXVals[8], lineYVals[10], tileSize, 8, 10, //male AIDE
                           new string[] { "I study POKéMON as",  "PROF.OAK's AIDE." }, "Up"));

                        npc.Add(new Character(lineXVals[2], lineYVals[10], tileSize, 2, 10, //male AIDE
                           new string[] { "I study POKéMON as", "PROF.OAK's AIDE." }, "Up"));

                        npc.Add(new Character(lineXVals[1], lineYVals[9], tileSize, 1, 9, //female AIDE
                           new string[] { "PROF.OAK is the", "authority on", "POKéMON!", "Many POKéMON", "trainers hold him", "in high regard!" }, "Up"));

                        Refresh();
                        break;
                }
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
                        for(int i = 0; i < length/moveSpeed; i++)
                        {
                            screenX+=moveSpeed;
                            UpdateCharacters();
                            UpdateBoundaries();
                            Thread.Sleep(gameTimer.Interval);
                            Refresh();
                        }
                    }
                    else if(player.x > closeX1)
                    {
                        length = player.x - lineXVals[lineXVals.IndexOf(closeX1)];
                        for (int i = 0; i < length/moveSpeed; i++)
                        {
                            screenX+=moveSpeed;
                            UpdateCharacters();
                            UpdateBoundaries();
                            Thread.Sleep(gameTimer.Interval);
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
                        for (int i = 0; i < length / moveSpeed; i++)
                        {
                            screenX-=moveSpeed;
                            UpdateCharacters();
                            UpdateBoundaries();
                            Thread.Sleep(gameTimer.Interval);
                            Refresh();
                        }
                    }
                    else if (player.x + player.size > closeX1)
                    {
                        length = lineXVals[lineXVals.IndexOf(closeX1)] - player.x;
                        for (int i = 0; i < length / moveSpeed; i++)
                        {
                            screenX-=moveSpeed;
                            UpdateCharacters();
                            UpdateBoundaries();
                            Thread.Sleep(gameTimer.Interval);
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
                        for (int i = 0; i < length / moveSpeed; i++)
                        {
                            screenY+=moveSpeed;
                            UpdateCharacters();
                            UpdateBoundaries();
                            Thread.Sleep(gameTimer.Interval);
                            Refresh();
                        }
                    }
                    else if (player.y > closeY1)
                    {
                        length = player.y - lineYVals[lineYVals.IndexOf(closeY1)];
                        for (int i = 0; i < length / moveSpeed; i++)
                        {
                            screenY+=moveSpeed;
                            UpdateCharacters();
                            UpdateBoundaries();
                            Thread.Sleep(gameTimer.Interval);
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
                        for (int i = 0; i < length / moveSpeed; i++)
                        {
                            screenY-=moveSpeed;
                            UpdateCharacters();
                            UpdateBoundaries();
                            Thread.Sleep(gameTimer.Interval);
                            Refresh();
                        }
                    }
                    else if (player.y + player.size > closeY1)
                    {
                        length = lineYVals[lineYVals.IndexOf(closeY1)] - player.y;
                        for (int i = 0; i < length / moveSpeed; i++)
                        {
                            screenY-= moveSpeed;
                            UpdateCharacters();
                            UpdateBoundaries();
                            Thread.Sleep(gameTimer.Interval);
                            Refresh();
                        }
                    }
                    break; 
            }          
        }

        public void CheckMessage()
        {
            int x = 0, y = 0;
            string opp = "Up";
            switch (faceDirection)
            {
                case "Left":
                    x = player.x + tileSize;
                    y = player.y;
                    opp = "Right";
                    break;
                case "Right":
                    x = player.x - tileSize;
                    y = player.y;
                    opp = "Left";
                    break;
                case "Down":
                    x = player.x;
                    y = player.y - tileSize;
                    opp = "Up";
                    break;
                case "Up":
                    x = player.x;
                    y = player.y + tileSize;
                    opp = "Down";
                    break;
            }

            foreach (Boundary b in boundaries)
            {
                if (b.messages.Count == 0)
                {

                }
                else
                {
                    if (x >= lineXVals[b.xTileIndex] && x < lineXVals[b.xTileIndex] + b.tileWidth * tileSize &&
                        y >= lineYVals[b.yTileIndex] && y <= lineYVals[b.yTileIndex] + b.tileHeight * tileSize &&
                        b.messages[0] != "Exit")
                    {
                        if (chooseStarter)
                        {
                            if (b.xTileIndex == 6 && b.yTileIndex == 3)
                            {
                                DexScreen.pokemon = "CHARMANDER";
                                gameTimer.Stop();
                                DexScreen ds = new DexScreen();
                                this.Controls.Add(ds);
                                ds.Focus();
                            }
                            else if (b.xTileIndex == 7 && b.yTileIndex == 3)
                            {
                                DexScreen.pokemon = "SQUIRTLE";
                                gameTimer.Stop();
                                DexScreen ds = new DexScreen();
                                this.Controls.Add(ds);
                                ds.Focus();
                            }
                            if (b.xTileIndex == 8 && b.yTileIndex == 3)
                            {
                                DexScreen.pokemon = "BULBASAUR";
                                gameTimer.Stop();
                                DexScreen ds = new DexScreen();
                                this.Controls.Add(ds);
                                ds.Focus();
                            }
                        }
                        else
                        {
                            screenMessage = b.messages.ToArray();
                            message = true;
                        }
                        break;
                    }
                }
            }
            foreach (Character c in npc)
            {
                if (x >= lineXVals[c.xTileIndex] && x < lineXVals[c.xTileIndex] + tileSize &&
                    y >= lineYVals[c.yTileIndex] && y <= lineYVals[c.yTileIndex] + tileSize)
                {
                    c.faceDirection = opp;
                    screenMessage = c.messages.ToArray();
                    message = true;
                    // MessageBox.Show(c.message);                   
                    break;
                }
            }
        }

        public void SlideText()
        {
            messageIndex++;
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

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (message)
            {
                if(e.KeyCode == Keys.Space)
                {
                    if (messageIndex == screenMessage.Length - 2)
                    {
                        message = false;
                        messageIndex = 0;
                    }
                    else
                    {
                        SlideText();
                    }
                    Refresh();
                }
            }
            else
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
                    case Keys.Space:
                        CheckMessage();
                        break;
                }
            }
        }
    }
}
