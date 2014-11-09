using System.Drawing;
using System.Windows.Forms;

namespace BaseGame
{
    class Player : PictureBox
    {
        public int moveSpeed = 10;
        public int horizontalAxis;
        public bool isMovingLeft = false;
        public bool isMovingRight = false;
        public float rateOfFire = 2.5f; // shots per second
        public bool hasWeapon = false;
        public bool isShooting = false;
        public PictureBox weaponLeft, weaponRight;

        public Player()
        {
            this.Size = new Size(64, 18);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            // Center the player at the bottom of the Game Window
            this.horizontalAxis = Program.GameWindow.Height - this.Size.Height - 60;
            this.Location = new Point(Program.GameWindow.ClientRectangle.Width / 2 - this.Size.Width / 2, this.horizontalAxis);
            this.Image = new Bitmap(Image.FromFile("Resources/player.png"));
            Program.GameWindow.Controls.Add(this);
            this.BringToFront();
        }

        public void moveLeft()
        {
            this.Location = new Point(this.Location.X - this.moveSpeed, this.horizontalAxis);
            this.updateWeaponLocation();
        }

        public void moveRight()
        {
            this.Location = new Point(this.Location.X + this.moveSpeed, this.horizontalAxis);
            this.updateWeaponLocation();
        }

        public void upgradeWeapon()
        {
            if (!this.hasWeapon)
            {
                this.weaponLeft = new PictureBox();
                this.weaponRight = new PictureBox();
                this.weaponLeft.Size = new Size(18, 18);
                this.weaponRight.Size = new Size(18, 18);
                this.weaponLeft.Image = new Bitmap(Image.FromFile("Resources/weapon.png"), this.weaponLeft.Size);
                this.weaponRight.Image = new Bitmap(Image.FromFile("Resources/weapon.png"), this.weaponRight.Size);
                Program.GameWindow.Controls.Add(this.weaponLeft);
                Program.GameWindow.Controls.Add(this.weaponRight);
                this.weaponLeft.BringToFront();
                this.weaponRight.BringToFront();

                this.hasWeapon = true;
                this.updateWeaponLocation();
            }
        }

        public void upgradeSize()
        {
            this.Size = new Size(this.Size.Width+16, this.Size.Height);
            this.updateWeaponLocation();
        }

        private void updateWeaponLocation()
        {
            if (this.hasWeapon)
            {
                this.weaponLeft.Location = new Point(this.Location.X, this.Location.Y);
                this.weaponRight.Location = new Point(this.Location.X + this.Size.Width - this.weaponRight.Width, this.Location.Y);
            }
        }
    }
}
