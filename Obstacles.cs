using System.Drawing;
using System.Numerics;

namespace Machine_Learning_Attempt
{
    public class Obstacle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Brush Color { get; set; } = Brushes.Gray;

        public Obstacle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        // Draw the obstacle on the screen
        public void Draw(Graphics g)
        {
            g.FillRectangle(Color, X, Y, Width, Height);
        }

        // Get the bounding box for the obstacle
        public Collisions.BoundingBox GetBoundingBox()
        {
            return new Collisions.BoundingBox
            {
                MinPoint = new Vector2(X, Y),
                MaxPoint = new Vector2(X + Width, Y + Height)
            };
        }
    }
}
