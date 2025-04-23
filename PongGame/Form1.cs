namespace PongGame
{
    public partial class Form1 : Form
    {
        int ballXspeed = 4;
        int ballYspeed = 4;
        int speed = 6;
        Random rnd = new Random();
        bool goDown, goUp;
        int computerSpeedChange = 50;
        int playerScore = 0;
        int computerScore = 0;
        int playerSpeed = 8;
        int[] i = {5, 6, 8, 9};
        int[] j = { 10, 9, 8, 11, 12};

        public Form1()
        {
            InitializeComponent();
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            Ball.Top -= ballYspeed;
            Ball.Left -= ballXspeed;

            this.Text = "Player Score: " + playerScore + "-- Computer Score: " + computerScore;

            if (Ball.Top < 0 || Ball.Bottom > this.ClientSize.Height)
            {
                ballYspeed = -ballYspeed;
            }

            if (Ball.Left < -2)
            {
                Ball.Left = 300;
                ballXspeed = -ballXspeed;
                computerScore++;
            }

            if (Ball.Right > this.ClientSize.Width + 2)
            {
                Ball.Left = 300;
                ballXspeed = -ballXspeed;
                playerScore++;
            }

            if (Computer.Top <= 1)
            {
                Computer.Top = 0;
            }

            else if (Computer.Bottom >= this.ClientSize.Height)
            {
                Computer.Top = this.ClientSize.Height - Computer.Height;
            }

            if (Ball.Top < Computer.Top + (Computer.Height / 2))
            {
                Computer.Top -= speed;
            }
            if (Ball.Top > Computer.Top + (Computer.Height / 2))
            {
                Computer.Top += speed;
            }


            if (computerSpeedChange < 0)
            {
                speed = i[rnd.Next(i.Length)];
                computerSpeedChange = 50;
            }

            if (goDown && Player.Top + Player.Height < this.ClientSize.Height)
            {
                Player.Top += playerSpeed;
            }

            if (goUp && Player.Top > 0)
            {
                Player.Top -= playerSpeed;
            }

            if (ballXspeed > 0)
            {
                int predictedY = Ball.Top + ((this.ClientSize.Width - Ball.Left) / ballXspeed) * ballYspeed;

                if (Computer.Top + Computer.Height / 2 < predictedY - 10)
                {
                    Computer.Top += speed;
                }
                else if (Computer.Top + Computer.Height / 2 > predictedY + 10)
                {
                    Computer.Top -= speed;
                }
            }

            CheckCollision(Ball, Player, Player.Right + 5);
            CheckCollision(Ball, Computer, Computer.Left - Ball.Width);


            if (computerScore > 5)
            {
                GameOver("Sorry you are lose");
            }
            else if (playerScore > 5)
            {
                GameOver("You win the game");
            }


        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            if(e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
        }

        private void CheckCollision(PictureBox PicOne, PictureBox PicTwo, int offset)
        {
            if (PicOne.Bounds.IntersectsWith(PicTwo.Bounds))
            {
                PicOne.Left = offset;
                int x = j[rnd.Next(j.Length)];
                int y = j[rnd.Next(j.Length)];

                if (ballXspeed < 0)
                {
                    ballXspeed = x;
                }
                else
                {
                    ballXspeed = -x;
                }

                if (ballYspeed < 0)
                {
                    ballYspeed = -y;
                }
                else
                {
                    ballYspeed = y;
                }
            }
        }

        private void GameOver(string message)
        {
            GameTimer.Stop();
            MessageBox.Show(message,"Game Over");
            computerScore = 0;
            playerScore = 0;
            ballXspeed =  ballYspeed = 4;
            computerSpeedChange = 50;
            GameTimer.Start();
        }
    }
}
