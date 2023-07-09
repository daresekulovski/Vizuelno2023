using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    public class Platform
    {
        public Point Center { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }
        public bool AtEdge { get; set; }

        public Platform()
        {
            Width = 100;
            Height = 20;
            Center = new Point(512, 510);
            Color = Color.Black;
            AtEdge = false;
        }
        public void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color);
            g.FillRectangle(brush, Center.X - (Width / 2), Center.Y - (Height / 2), Width, Height);
            brush.Dispose();
        }
        public void Move(int currX)
        {
            if (currX - (Width / 2) >= 0 && currX + (Width / 2) <= 1008)
            {
                Center = new Point(Center.X + (currX - Center.X), Center.Y);
                AtEdge = false;

            }
            else if (currX - Width / 2 < 0)
            {
                Center = new Point(0 + (Width / 2), Center.Y);
                AtEdge = true;
            }
            else
            {
                Center = new Point(1008 - (Width / 2), Center.Y);
                AtEdge = true;
            }
        }
        public void Resize(Color color)
        {
            if (color == Color.Pink)
            {
                Width = 60;
            }
            else
            {
                Width = 120;
            }
        }
        public void ResetSize()
        {
            Width = 100;
        }
    }
}
