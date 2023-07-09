using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    public class Buff
    {
        public Point Center { get; set; }
        public int Radius { get; set; }
        public Color Color { get; set; }
        public Buff(Point point, Color color) 
        {
            Center = point;
            Radius = 10;
            Color = color;
        }
        public void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color);
            g.FillEllipse(brush, Center.X - Radius, Center.Y - Radius, Radius * 2, Radius * 2);
            brush.Dispose();
        }
        public void Move()
        {
            Center = new Point(Center.X, Center.Y + 10);
        }
        public bool Consumed(Platform platform)
        {
            return ((Math.Abs(platform.Center.X - Center.X) <= platform.Width / 2 + Radius) && (Math.Abs(platform.Center.Y - Center.Y) <= platform.Height / 2 + Radius));
        }
    }
}
