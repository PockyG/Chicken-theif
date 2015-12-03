using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MirrorShooter
{
    public class Camera
    {

        public Vector2 TopLeft;
        public Vector2 Velocity;
        public Player player;
        public float cameraSpeed = 0.001f;
        private float maxSpeed = 4;
        private float friction = 0.04f;

        public bool isStatic = false;
        public Camera() : this(Vector2.Zero) { }

        public Camera(Vector2 a_initialTopLeft)
        {
            TopLeft = a_initialTopLeft;
            cameraSpeed = 1;
        }
        public void SetPlayer(Player a_player)
        {
            player = a_player;
        }

        public void Update(float a_deltaTime)
        {

            if(isStatic == true)
            { 


            if (InputManager.Instance.GetMousePosition().X > (Game1.WINDOW_WIDTH / 100) * 90)
            {
                Velocity.X += cameraSpeed * a_deltaTime;
            }
            if (InputManager.Instance.GetMousePosition().X < (Game1.WINDOW_WIDTH / 100) * 10)
            {
                Velocity.X -= cameraSpeed * a_deltaTime;
            }
            if (InputManager.Instance.GetMousePosition().Y > (Game1.WINDOW_HEIGHT / 100) * 90)
            {
                Velocity.Y += cameraSpeed * a_deltaTime;
            }
            if (InputManager.Instance.GetMousePosition().Y < (Game1.WINDOW_HEIGHT / 100) * 10)
            {
                Velocity.Y -= cameraSpeed * a_deltaTime;
            }


            if (player.GetMidPosition().Y < TopLeft.Y + (Game1.WINDOW_HEIGHT/100)*25)
                Velocity.Y -= cameraSpeed * a_deltaTime;
            if (player.GetMidPosition().X < TopLeft.X + (Game1.WINDOW_WIDTH / 100)*25)
                Velocity.X -= cameraSpeed * a_deltaTime;
            if (player.GetMidPosition().X > TopLeft.X + (Game1.WINDOW_WIDTH / 100)*75)
                Velocity.X += cameraSpeed * a_deltaTime;

            if (player.GetMidPosition().Y > TopLeft.Y + (Game1.WINDOW_HEIGHT / 100) * 75)
                Velocity.Y += cameraSpeed * a_deltaTime;



            if (Math.Abs(Velocity.Length()) < 0.1)
            {
                Velocity = Vector2.Zero;
            }

            if (Velocity.X > 0)
                Velocity.X -= friction;
            if (Velocity.Y > 0)
                Velocity.Y -= friction;
            if (Velocity.X < 0)
                Velocity.X += friction;
            if (Velocity.Y < 0)
                Velocity.Y += friction;

            Velocity.X = MathHelper.Clamp(Velocity.X, -maxSpeed, maxSpeed);
            Velocity.Y = MathHelper.Clamp(Velocity.Y, -maxSpeed, maxSpeed);

            TopLeft += Velocity;
         }
            if (InputManager.Instance.KeyDown(Keys.Left))
            {
                TopLeft.X -= cameraSpeed * a_deltaTime;
            }
            if (InputManager.Instance.KeyDown(Keys.Right))
            {
                TopLeft.X += cameraSpeed * a_deltaTime;
            }
            if (InputManager.Instance.KeyDown(Keys.Down))
            {
                TopLeft.Y += cameraSpeed * a_deltaTime;
            }
            if (InputManager.Instance.KeyDown(Keys.Up))
            {
                TopLeft.Y -= cameraSpeed * a_deltaTime;
            }

        }
    }
}
