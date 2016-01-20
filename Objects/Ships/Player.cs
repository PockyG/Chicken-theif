using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MirrorShooter
{
    public class Player: Ship
    {
        float radius;
        float hitboxWidth, hitboxHeight;
        public static Texture2D hitboxCircle;
        public static Texture2D textureRef;
        private Vector2 velocity;
        private float friction = 0.02f;
        private float acceleration = 0.02f;
        public float bulletSpeed = 1;

        public int gunAmmo = 5;

        public Player(Vector2 a_position, float a_radius)
            : base(a_position)
        {
            radius = a_radius;
            hitboxWidth = 2 * radius;
            hitboxHeight = 2 * radius;
            velocity = new Vector2();
            maxSpeed = 5;
            health = 1000000;
            texture = hitboxCircle;
            

            

        }

        public void Update(float a_deltaTime)
        {
            //rotation++;
            if (InputManager.Instance.KeyDown(Keys.LeftShift))
            {
                maxSpeed = 5;
            }
            else
            {
                maxSpeed = 5;
            }






            velocity.X = MathHelper.Clamp(velocity.X, -maxSpeed, maxSpeed);
            velocity.Y = MathHelper.Clamp(velocity.Y, -maxSpeed, maxSpeed);

            midPosition += velocity;

            if (Math.Abs(velocity.Length()) < 0.1)
            {
                velocity = Vector2.Zero;
            }

            if (velocity.X > 0)
                velocity.X -= friction;
            if (velocity.Y > 0)
                velocity.Y -= friction;
            if (velocity.X < 0)
                velocity.X += friction;
            if (velocity.Y < 0)
                velocity.Y += friction;
            
           
                

           // if (InputManager.Instance.KeyDown(Keys.S))
           // {
           //     velocity.Y += acceleration * a_deltaTime;
           // }
           // if (InputManager.Instance.KeyDown(Keys.A))
           // {
           //     velocity.X -= acceleration * a_deltaTime;
           // }
           // if (InputManager.Instance.KeyDown(Keys.D))
           // {
           //     velocity.X += acceleration * a_deltaTime;
           // }
           // if (InputManager.Instance.KeyDown(Keys.W))
           // {
           //     velocity.Y -= acceleration * a_deltaTime;
           // }
           // //float radians = ToRadians(rotation);
           // //float newX = (midPosition.X * (float)Math.Cos(radians) - midPosition.Y * (float)Math.Sin(radians))  ;
           // //float newY = (midPosition.Y * (float)Math.Cos(radians) + midPosition.X * (float)Math.Sin(radians))  ;
           // //midPosition = new Vector2(newX, newY);
           //
           // if (InputManager.Instance.KeyDown(Keys.D))
           //     {
           //     rotation += 1;
           //        
           //         
           //     }
           // if (InputManager.Instance.KeyDown(Keys.A))
           // {
           //     rotation -= 1;
           //
           //
           // }

            //if (InputManager.Instance.KeyPressed(Keys.Space))
            //{
            //    float counter = 0;
            //    float bulletAmount = 30;
            //    for (int i = 0; i < bulletAmount; i++)
            //    {
            //
            //        Vector2 shootat = midPosition + GetUnitVector(counter * (360 / bulletAmount));
            //        Bullet bullet = new Bullet(midPosition, shootat, bulletSpeed, 30, "Player");
            //        InGame.addList.Add(bullet);
            //        counter++;
            //
            //    }
            //
            //}

            //if (InputManager.Instance.MousePressed(MouseButton.Left))
            //{
            //    Shoot( InputManager.Instance.GetMouseWorldPosition());
            //    
            //}

            if (midPosition.X - radius < 0)
                midPosition.X = radius;
            if (midPosition.Y - radius < 0)
                midPosition.Y = radius;
            if (midPosition.X + radius > 3000)
                midPosition.X = 3000 - radius;
            if (midPosition.Y + radius > 3000)
                midPosition.Y = 3000 -radius;
        
            

            
         
        }

        //public override void Draw(SpriteBatch spriteBatch, Camera a_camera)
        //{
        //    spriteBatch.Draw(hitboxCircle, new Rectangle((int)(midPosition.X - hitboxWidth/2 - a_camera.TopLeft.X), (int)(midPosition.Y - hitboxHeight/2- a_camera.TopLeft.Y) , (int)hitboxWidth, (int)hitboxHeight), Color.Green);
        //}

        private void Shoot(Vector2 shootAt)
        {


        }

        public float GetRadius()
        {
            return radius;
        }


    }
}
