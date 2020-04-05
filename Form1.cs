using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//CREDIT TO https://www.mooict.com/c-tutorial-create-a-pong-arcade-game-in-visual-studio/ for the tutorial
namespace Pong2
{
    public partial class Form1 : Form
    {
        bool goUp; //boolean used to detect player up position
        bool goDown; // bool used to detect player down position
        int speed = 5; // int for ball speed
        int ballxAxis = 5; // speed for ball x axis 
        int ballyAxis = 5; // speed for y ball axis
        int playerPoints = 0; //intializes player score 
        int cpuPoints = 0; // intializes CPU score

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) //If the players presses the up key, change the goUp bool to true
            {
                goUp = true;
            }

            if (e.KeyCode == Keys.Down) //If the players presses the up key, change the goUp bool to true
            {
                goDown = true;
            }

        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goUp = false; //if the players releases the up or down key it sets the goUp and goDown booleans to false. 
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }


        private void timerTick(object sender, EventArgs e)
        {
            playerScore.Text = "" + playerPoints; //sets player score label to display player points
            cpuScore.Text = "" + cpuPoints; //sets CPU score label to display the cpu points

            ball.Top -= ballyAxis; //assigns the object ball to top control and to the y integer
            ball.Left -= ballxAxis; // assigns the object ball to left control and to the x integer

            cpu.Top += speed; //assigns the CPU top speed integer

            if (playerPoints < 5) //if player points are less then 5 move in to nested if
            {
                if (cpu.Top < 0 || cpu.Top > 455) //if CPU has reached the top or gone to the bottom of the screen
                {
                    speed = -speed; //then change the speed direction so it moves up OR down
                }
            }
            else
            {
                //if the score is greater than 5 increase the CPU's difficulty by allowing the CPU to follow the ball so it won't miss
                cpu.Top = ball.Top + 30;
            }
            //If the ball goes past the player on the left
            if (ball.Left < 0)
            {
                ball.Left = 434; //sets the ball to the middle of the screen
                ballxAxis = -ballxAxis; //changes the balls direction
                ballxAxis -= 2; //increases the ball speed
                cpuPoints++; //increments CPU's score
            }

            //If the ball goes past the CPU on the right
            if (ball.Left +ball.Width > ClientSize.Width)
            {
                ball.Left = 434; //sets the ball to the center of the screen
                ballxAxis = -ballxAxis; //Change the direction of the ball
                ballxAxis += 2;//Increases the speed of the ball
                playerPoints++;//increment player points 
            }
            //Controlling the ball and staying in bounds
            if (ball.Top < 0 || ball.Top + ball.Height > ClientSize.Height)
            {
                ballyAxis = -ballyAxis; //then reverse the speed of the ball so it stays on screen
            }

            //if the ball hits the player or CPU paddle
            if (ball.Bounds.IntersectsWith(player.Bounds) || ball.Bounds.IntersectsWith(cpu.Bounds))
            {
                //then bounce the ball in the other direction
                ballxAxis = -ballxAxis; 
            }

            //Player Controls
            if (goUp == true && player.Top > 0) //if goUp bool is and the player is within the top boundry
            {
                player.Top -= 8; //Move the player towards the top
            }

            if (goDown == true && player.Top < 455)
            {
                player.Top += 8; //Then move the player to the bottom of the screen
            }
            //The game ends when the player reaches 10. Stop the timer and show a message
            if (playerPoints > 10)
            {
                gameTimer.Stop();
                MessageBox.Show("Congratulation! You Wasted A Lot Of Your Time"); 
            }
            //...if the CPU Wins
            if (cpuPoints > 10)
            {
                gameTimer.Stop();
                MessageBox.Show("Wow, couldn't even beat the computer. Time to hang it up Jack");
            }

        }

   
    }
    
}
