using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BrickBreaker
{
    public class Ball
    {
        public Point Center { get; set; }
        public int Radius { get; set; }
        public Color Color { get; set; }
        public double Angle { get; set; }
        public int Velocity { get; set; }
        public int VelocityX { get; set; }
        public int VelocityY { get; set; }
        public bool IsMoving { get; set; }
        public Ball() 
        {
            Center = new Point(512, 490);
            Radius = 10;
            Color = Color.Black;
            Angle = 1;
            Velocity = 15;
            VelocityX = (int)(Math.Cos(Angle) * Velocity);
            VelocityY = (int)(Math.Sin(Angle) * Velocity);
            IsMoving = false;
        }
        public Ball(Point point)
        {
            Random random = new Random();
            Center = point;
            Radius = 10;
            Color = Color.Black;
            Angle = random.NextDouble() * 2 * Math.PI;
            Velocity = 15;
            VelocityX = (int)(Math.Cos(Angle) * Velocity);
            VelocityY = (int)(Math.Sin(Angle) * Velocity);
            IsMoving = true;
        }
        public void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color);
            g.FillEllipse(brush, Center.X -  Radius, Center.Y - Radius, Radius * 2, Radius * 2);
            brush.Dispose();
        }
        public void AdjustBall(int currX)
        {
            Center = new Point(Center.X + (currX - Center.X), Center.Y);
        }
        public void Move(Platform platform, List<Brick> bricks)
        {
            WallCollison();
            PlatformCollison(platform);
            BrickCollison(bricks);
            Center = new Point(Center.X + VelocityX, Center.Y + VelocityY);
            IsMoving = true;
        }
        public void WallCollison()
        {
            if (Center.X - Radius <= 0 || Center.X + Radius >= 1008)
            {
                VelocityX = -VelocityX;
            }
            if (Center.Y - Radius <= 0)
            {
                VelocityY = -VelocityY;
            }
        }
        public void PlatformCollison(Platform platform)
        {
            if ((Math.Abs(platform.Center.X - Center.X) <= platform.Width / 2 + Radius) && (Math.Abs(platform.Center.Y - Center.Y) <= platform.Height / 2 + Radius)) {
                VelocityY = -VelocityY;
            }
        }
        public void BrickCollison(List<Brick> bricks)
        {
            foreach (Brick br in bricks)
            {
                if ((Math.Abs(br.Center.X - Center.X) <= br.Width / 2 + Radius) && (Math.Abs(br.Center.Y - Center.Y) <= br.Height / 2 + Radius))
                {
                    VelocityY = -VelocityY;
                    br.Lives--;
                    br.ChangeColor();
                }
            }
        }
    }
}
