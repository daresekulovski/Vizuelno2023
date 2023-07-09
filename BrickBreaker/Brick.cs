using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    public class Brick
    {
        public Point Center { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }
        public int Lives { get; set; }
        public Brick()
        {
            Random random = new Random();
            Center = new Point(random.Next(30, 990), random.Next(40, 350));
            Width = 30;
            Height = 20;
            int rng = random.Next(0, 101);
            if (rng <= 60)
            {
                Color = Color.Green;
                Lives = 2;
            }
            else if (rng > 60 && rng <= 85)
            {
                Color = Color.Yellow;
                Lives = 1;
            }
            else
            {
                Color = Color.Red;
                Lives = 0;
            }
        }
        public void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color);
            g.FillRectangle(brush, Center.X - Width, Center.Y - Height, Width, Height);
            brush.Dispose();
        }
        public bool Intersect(Brick brick)
        {
            return (Math.Abs(Center.X - brick.Center.X) <= brick.Width + Width && Math.Abs(Center.Y - brick.Center.Y) <= brick.Height + Height);
        }
        public void ChangeColor()
        {
            if (Lives == 1)
            {
                Color = Color.Yellow;
            }
            if (Lives == 0)
            {
                Color = Color.Red;
            }
        }
    }
}
