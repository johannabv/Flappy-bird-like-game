using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Flappy {
    public partial class Form1 : Form {
        private int planetSpeed = 8;
        private int gravity = 5;
        private int score = 0;
        private int livesLost = 0;
        private Random rand = new Random();
        private List<PictureBox> galaxy;
        private List<PictureBox> lives;
        
        public Form1() {
            InitializeComponent();
            galaxy = new List<PictureBox>()
                {pictureBox_planet1, pictureBox_planet2, pictureBox_planet3, pictureBox_planet4, pictureBox_planet5};
            lives = new List<PictureBox>() {pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5};
        }

        private void gameTimerEvent(object sender, EventArgs e) {
            pictureBox_flappy.Top += gravity;
            
            ControlColliding();
            ChangePlanetsLocation();

            if (livesLost==5) {
                pictureBox5.Visible = false;
                EndGame();
            }
        }

        private void EndGame() {
            timer1.Stop();
            string message = "Your score: " + score + ". Do you want to play again?";
            string caption = "Game over!";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            if (MessageBox.Show(message, caption, buttons) == DialogResult.Yes) ResetGame();
            else Application.Exit();
        }

        private void ResetGame() {
            score = 0;
            livesLost = 0;
            foreach (PictureBox life in lives) life.Visible = true;
            timer1.Start();
        }

        private void ControlColliding() {
            foreach (PictureBox planet in galaxy) {
                if (pictureBox_flappy.Bounds.IntersectsWith(planet.Bounds) && planet.Visible==true) {
                    livesLost++;
                    if (livesLost < 5) lives[livesLost - 1].Visible = false;
                    planet.Visible = false;
                }
            } 
        }

        private void ChangePlanetsLocation() {
            foreach (PictureBox planet in galaxy) {
                planet.Left -= planetSpeed;

                if (planet.Left < -100) {
                    planet.Left = ClientSize.Width + planet.Width * 2;
                    planet.Top = rand.Next(0, ClientSize.Height - planet.Height - pictureBox1.Height);
                    
                    if (planet.Visible==true) {
                        score++;
                        label1.Text = "SCORE: " + score;
                    }
                    else {
                        planet.Visible = true;
                    }
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Space) gravity = 5;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Space) gravity = -5;
        }

        private void pictureBox_start_Click(object sender, EventArgs e) {
            pictureBox_start.Enabled = false;
            pictureBox_start.Visible = false;
            timer1.Interval = 20;
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e) {
            timer1.Stop();
        }
    }
}
