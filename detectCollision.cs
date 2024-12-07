using System.Numerics;

namespace Machine_Learning_Attempt
{
    public class Collisions
    {
        public class BoundingBox
        {
            public Vector2 MinPoint { get; set; }
            public Vector2 MaxPoint { get; set; }
        }

        // Create bounding boxes for all obstacles
        public static List<BoundingBox> CreateBoundingBoxes(List<Obstacle> obstacles)
        {
            var boundingBoxes = new List<BoundingBox>();
            foreach (var obstacle in obstacles)
            {
                boundingBoxes.Add(obstacle.GetBoundingBox());
            }
            return boundingBoxes;
        }

        // Detect collision between two objects
        public static bool DetectCollision(BoundingBox box1, BoundingBox box2)
        {
            // Check for separation along the X or Y axes
            if (box1.MaxPoint.X < box2.MinPoint.X || box1.MinPoint.X > box2.MaxPoint.X)
            {
                return false;
            }
            if (box1.MaxPoint.Y < box2.MinPoint.Y || box1.MinPoint.Y > box2.MaxPoint.Y)
            {
                return false;
            }

            // If no separation, there is a collision
            return true;
        }
    }
}
