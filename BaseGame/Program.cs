using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseGame
{
    static class Program
    {
        // General global variables
        private static Form GameWindow;

        // User Interface elements
        private static PictureBox leftSideBar;
        private static PictureBox rightSideBar;
        private static int leftMargin;
        private static int rightMargin;

        // Level variables
        private static char[,] levelLayout;

        // Blocks (targets)
        //private static PictureBox[] blockTypes;
        private static PictureBox block;
        private static Size blockSize;

        // Player
        private static PictureBox player;
        private static int playerY;

        // Props (balls, bullets, powerups)
        private static PictureBox ball;
        private static Size ballSize;
        private static bool newBall;

        private static Timer mainTimer;
           
        static void Main()
        {
            // Create the main game window and set its properties
            GameWindow = new Form();
            GameWindow.Size = new Size(1280, 720);
            GameWindow.MinimumSize = GameWindow.Size;
            GameWindow.MaximumSize = GameWindow.Size;
            GameWindow.MaximizeBox = false;
            GameWindow.StartPosition = FormStartPosition.CenterScreen;
            GameWindow.BackColor = Color.White;
            Cursor.Hide(); // Hide the cursor



            // Set the limits of the playable zone on the Game Window (exclude sidebars)
            leftMargin = GameWindow.ClientRectangle.Width/2 - (17*48)/2;// leftSideBar.Width + 20;
            rightMargin = GameWindow.ClientRectangle.Width - 100;// GameWindow.ClientRectangle.Width - rightSideBar.Width;

            // Create the level layout
            levelLayout = new char[17, 17] { // Matrix representing block positions and types
                {'1','1',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','1','1'},
                {'1',' ',' ',' ','1','1','1','1','1','1','1','1','1',' ',' ',' ','1'},
                {' ',' ',' ',' ',' ','1','1','1','1','1','1','1',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ','1','1','1','1','1',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ','1','1','1',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ','1','1','1',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ','1',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ','1',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ','1','1','1',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ','1','1','1','1','1',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'1',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','1'},
                {'1','1',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','1','1'},
            };
            /*levelLayout = new char[17, 17] { // Matrix representing block positions and types
                {'1','1',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','1','1'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'1','1',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','1','1'},
            };*/

            blockSize = new Size(48, 18);
            newBall = true;

            // Draw the blocks on the Game Window matching the type and position defined in the level layout matrix
            for (int i = 0; i < levelLayout.GetLength(0); i++)
            {
                for (int j = 0; j < levelLayout.GetLength(1); j++)
                {
                    char blockType = levelLayout[i, j];
                    if (blockType != ' ') {
                        block = new PictureBox();
                        block.Size = blockSize;
                        block.Image = new Bitmap(Image.FromFile("Resources/block_" + blockType + ".png"), blockSize);
                        block.Location = new Point(j+j*blockSize.Width + leftMargin, i+i*blockSize.Height);
                        GameWindow.Controls.Add(block);
                    }
                }
            }

            // Create the player object
            player = new PictureBox();
            player.Size = new Size(64, 18);
            player.Image = new Bitmap(Image.FromFile("Resources/player.png"), player.Size);
            playerY = GameWindow.Height - player.Size.Height - 60;
            player.Location = new Point(GameWindow.ClientRectangle.Width/2 - player.Size.Width/2, playerY);
            GameWindow.Controls.Add(player); // Add player to the GameWindow

            // Create the ball object
            ball = new PictureBox();
            ball.Size = new Size(16, 16);
            ball.Image = new Bitmap(Image.FromFile("Resources/ball_1.png"), ball.Size);
            ball.Location = new Point(
                player.Location.X + player.Size.Width / 2 - ball.Size.Width / 2,
                player.Location.Y - ball.Size.Height);
            GameWindow.Controls.Add(ball); // Add player to the GameWindow
            
            // Create the main timer
            mainTimer = new Timer();
            mainTimer.Interval = 30;
            mainTimer.Tick += update;
            mainTimer.Start();

            // Events
            GameWindow.MouseMove += new MouseEventHandler(mouseMove);
            GameWindow.KeyDown += new KeyEventHandler(keyDown);

            Application.Run(GameWindow);
        }

        private static void mouseMove(object sender, MouseEventArgs e)
        {
            //if (e.Location.X > leftMargin && e.Location.X < rightMargin - player.Size.Width)
            player.Location = new Point(e.Location.X - player.Size.Width/2, player.Location.Y);
            if (newBall)
            {
                ball.Location = new Point(
                    player.Location.X + player.Size.Width / 2 - ball.Size.Width / 2,
                    player.Location.Y - ball.Size.Height);
            }

            // Check game area bounds
            /*if (player.Location.X <= leftMargin)
                player.Location = new Point(leftMargin, playerY);
            if (player.Location.X >= rightMargin - player.Size.Width)
                player.Location = new Point(rightMargin - player.Size.Width, playerY);*/
        }

        private static void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                newBall = false;
            }
        }

        static int move = -10;
        static void update(Object myObject, EventArgs myEventArgs)
        {
            if (!newBall)
            {
                //ball.Location.Offset(new Point(5, -10));
                ball.Location = new Point(ball.Location.X + 5, ball.Location.Y + move);
            }
            for (int i = 0; i < levelLayout.GetLength(0); i++)
            {
                for (int j = 0; j < levelLayout.GetLength(1); j++)
                {
                    char blockType = levelLayout[i, j];
                    Point top = new Point(j+j*blockSize.Width + leftMargin, i+i*blockSize.Height);
                    if (ball.Top <= top.Y)
                    {
                        move = move * -1;
                    }
                }
            }
        }

        /*static void initShotComponents()
        {
            // Initialize the "laser" of shooting
            Laser = new PictureBox();
            // image and box size
            Laser.Image = new Bitmap(Image.FromFile(
                                                "Resources/shoot.png"));
            Laser.Size = Player.Image.Size;
            Laser.Visible = false; // no shooting in the beggining
            // add the image box to the main window 
            GameWindow.Controls.Add(Laser);


            // Create a timer with a 50 ms interval.
            aiTime = new Timer();
            aiTime.Interval = 50;
            // Hook up the method for the timer. 
            aiTime.Tick += aiMove;
            aiTime.Start();
        }
        
        static void aiMove(Object myObject, EventArgs myEventArgs)
        {
            if (shoot)
            {
                Laser.Location = shootPos;
                shootPos.Offset(shootVel);
                if (shootPos.X < 0 ||
                       shootPos.X > GameWindow.ClientRectangle.Width)
                {
                    shoot = false;
                    Laser.Visible = false;
                }
                else Laser.Visible = true;
            }
            // anything else you want to do that is not user controled
            // Example: Moving the targets
        }

        private static void keydown(object sender, KeyEventArgs e)
        {
            Point p = Player.Location;

            Point newP = new Point(p.X, p.Y);
            if (e.KeyCode == Keys.Up) newP.Y -= 10;
            else if (e.KeyCode == Keys.Down) newP.Y += 10;
            else if (e.KeyCode == Keys.Left) newP.X -= 10;
            else if (e.KeyCode == Keys.Right) newP.X += 10;

            // max size on inside of the window 
            // (ClientRectangle is the inside, without it is the outside)
            int maxX = GameWindow.ClientRectangle.Width - Player.Size.Width;
            int maxY = GameWindow.ClientRectangle.Height - Player.Size.Height;

            Player.Location = TestOutOfBounds(newP, maxX, maxY);

            // testing colision with target
            // determining the center of each object
            Point t = Target.Location;
            int tx = t.X + Target.Size.Width / 2;
            int ty = t.Y + Target.Size.Height / 2;
            // remember that Player location is now newP
            int px = newP.X + Player.Size.Width / 2;
            int py = newP.Y + Player.Size.Height / 2;
            // minimum distance between the two
            int minD = (Player.Size.Height + Target.Size.Height) / 2;
            // using euclidian distance equation
            if ( euclideanDistance(px, py, tx, ty) < minD)
            {
                // collision !!! change the target
                targetLocation = targetLocation + 1; // next
                if (targetLocation >= targetLocations.Length)
                    targetLocation = 0; // reached the end, back to 0
                Target.Location = targetLocations[targetLocation];
            }

            // shooting key
            if (e.KeyCode == Keys.Space && !shoot)
            {
                shoot = true;
                shootPos = Player.Location;
                if (e.Shift) shootVel = new Point(-10, 0);
                else shootVel = new Point(10, 0);
            }

        }

        static double euclideanDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        static Point TestOutOfBounds(Point loc, int maxX, int maxY)
        {
            // testing out of bounds
            if (loc.Y < 0) loc.Y = 0;
            else if (loc.Y > maxY) loc.Y = maxY;
            else if (loc.X < 0) loc.X = 0;
            else if (loc.X > maxX) loc.X = maxX;
            return loc;
        }*/

    }
}
