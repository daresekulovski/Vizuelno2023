using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrickBreaker
{
    public partial class Form1 : Form
    {
        public Scene Scene { get; set; }
        public Timer timerMove;
        public Form1()
        {
            InitializeComponent();
            Scene = new Scene();
            Scene.form1 = this;
            timerMove = new Timer();
            timerMove.Interval = 50;
            timerMove.Tick += new EventHandler(timer_move);
            UpdateStatus();
            this.DoubleBuffered = true;
        }
        public void timer_move(object sender, EventArgs e)
        {
            Scene.MoveBalls();
            Scene.DestroyBrick();
            Scene.MoveBuff();
            Scene.ConsumeBuff();
            Scene.Passed();
            Scene.AnotherTry();
            UpdateStatus();
            if (Scene.AttemptsLeft == 0)
            {
                timerMove.Stop();
                if (MessageBox.Show("You have lost. Do you want to try again ?", "Brick Breaker", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    timerMove.Interval = 50;
                    Scene = new Scene();
                    Scene.form1 = this;
                    UpdateStatus();
                }
                else
                {
                    this.Close();
                }
            }
            if (Scene.Bricks.Count == 0)
            {
                timerMove.Stop();
                if (MessageBox.Show("You won. Do you want to play again ?", "Brick Breaker", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    timerMove.Interval = 50;
                    Scene = new Scene();
                    Scene.form1 = this;
                    UpdateStatus();
                }
                else
                {
                    this.Close();
                }
            }
            Invalidate();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Scene.Bricks.Count > 0 && Scene.AttemptsLeft != 0)
            {
                Scene.PlatformMove(e.X);
                Scene.AdjustBall(e.X);
                Invalidate();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Scene.Draw(e.Graphics);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (Scene.Bricks.Count > 0 && Scene.AttemptsLeft != 0 && !Scene.Balls[0].IsMoving)
            {
                timerMove.Start();
            }
        }
        public void SpeedUp()
        {
            timerMove.Interval -= 10;
            timerMove.Stop();
            timerMove.Start();
            switch (Scene.Speed)
            {
                case 0:
                    Scene.buffDuration = 375;
                    break;
                case 1:
                    Scene.buffDuration = 500;
                    break;
                case 2:
                    Scene.buffDuration = 750;
                    break;
            }
            Scene.Speed++;
        }
        public void UpdateStatus()
        {
            toolStripStatusLabel1.Text = $"Score: {Scene.Score}";
            toolStripStatusLabel2.Text = $"Lives: {Scene.AttemptsLeft}";
        }
    }
}
