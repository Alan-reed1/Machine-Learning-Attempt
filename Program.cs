using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Numerics;

namespace Machine_Learning_Attempt{

    public class GameForm : Form
    {
        private readonly System.Windows.Forms.Timer gameTimer;

        private bool isMovingLeft = false;  // Track left movement
        private bool isMovingRight = false; // Track right movement
        private bool isMovingUp = false; // Track up movement
        private bool isMovingDown = false; // Track down movement
        private bool isMoving = false;

        private float playerX = 50; // Player's X position
        private float playerY; // Player's Y position
        private float playerSpeed = 0; // Speed of the player
        private readonly float playerAcceleration = .1f;

        public static List<Obstacle> obstacles = new List<Obstacle>(); // Obstacle list

        public GameForm()
        {
            // Initialize form properties
            this.Text = "2D Cube Game";
            this.ClientSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.DoubleBuffered = true;
            playerY = this.ClientSize.Height / 2;

            // Attach key event handlers
            this.KeyDown += OnKeyDown;
            this.KeyUp += OnKeyUp;

            // Initialize the game timer
            gameTimer = new System.Windows.Forms.Timer { Interval = 20 }; // ~60 FPS
            gameTimer.Tick += GameLoop;

            // Create obstacles
            ManualObstacles();

            gameTimer.Start();
        }

        // Update method: everything that needs to be checked every tick
         private void GameLoop(object sender, EventArgs e)
        {
            // Update player position based on input
            if (isMovingLeft)
                playerX -= playerSpeed;

            if (isMovingRight)
                playerX += playerSpeed;

            if (isMovingUp)
                playerY -= playerSpeed;

            if (isMovingDown)
                playerY += playerSpeed;

            // Prevent the player from going out of bounds
            playerX = Math.Max(0, Math.Min(ClientSize.Width - 50, playerX));
            playerY = Math.Max(0, Math.Min(ClientSize.Height - 50, playerY));


            // Collision Detection

            var playerBoundingBox = new Collisions.BoundingBox
            {
                MinPoint = new Vector2(playerX, playerY),
                MaxPoint = new Vector2(playerX + 50, playerY + 50)
            };

            var obstaclesBoundingBoxes = Collisions.CreateBoundingBoxes(GameForm.obstacles);

            foreach (var obstacleBox in obstaclesBoundingBoxes)
            {
                if (Collisions.DetectCollision(playerBoundingBox, obstacleBox))
                {
                    // Handle collision

                    // Add a playerKill() function that resets position, acceleration, velocity etc.
                    // And call that here
                    playerX = 50;
                    playerY = ClientSize.Height / 2;
                }
            }



            if (isMoving && playerSpeed < 10)
            {
                playerSpeed += playerAcceleration;
            }
            if (!isMoving) 
            {
                playerSpeed = 0;
            }

            // Refresh the screen (calls OnPaint)
            Invalidate();
        }

        // Create manual obstacles, until I implement some seed generation
        public static void ManualObstacles() 
        {
            obstacles.Add(new Obstacle(0, 200, 500, 300));
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                isMovingLeft = true;
                isMoving = true;

            if (e.KeyCode == Keys.D)
                isMovingRight = true;
                isMoving = true;

            if (e.KeyCode == Keys.W)
                isMovingUp = true;
                isMoving = true;

            if (e.KeyCode == Keys.S)
                isMovingDown = true;
                isMoving = true;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                isMovingLeft = false;

            if (e.KeyCode == Keys.D)
                isMovingRight = false;

            if (e.KeyCode == Keys.W)
                isMovingUp = false;

            if (e.KeyCode == Keys.S)
                isMovingDown = false;

            if (!isMovingDown && !isMovingLeft && !isMovingRight && !isMovingUp) 
            {
                isMoving = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Draw obstacles
            foreach (var obstacle in obstacles)
            {
                obstacle.Draw(g);
            }

            // Draw a blue player cube
            g.FillRectangle(Brushes.Blue, playerX, playerY, 50, 50);
        }
    }

    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create and run the game form
            Application.Run(new GameForm());
        }
    }
}
