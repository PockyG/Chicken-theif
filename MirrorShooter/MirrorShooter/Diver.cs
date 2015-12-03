using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MirrorShooter
{
    public class Diver:EnemyShip
    {
        Vector2 velocity;
        float speed = 0.08f;
        float turnSpeed = 0.2f;
        public static Texture2D texture;
        float lastRotation;
        private Vector2 Force;
        private Vector2 lastForce;
        private float timer = 0;
        private float shotsPer = 2000;
        
        public Diver(Vector2 a_startPos, Player a_player, float a_rotation): base(a_startPos, a_player)
        {
            value = 1;
            maxSpeed = 0.45f;
            speed = 0.001f;
            rotation = a_rotation;
            velocity = new Vector2();
            Force = new Vector2();
            radius = 30;
            health = 1;
            

        }



        public override void Update(float a_deltaTime)
        {
            midPosition += velocity * a_deltaTime;

            timer += a_deltaTime;
            if (timer > shotsPer)
            {
                timer = 0;
                Shoot();

            }

            float desiredAngle;
            float deltaX = player.GetMidPosition().X - midPosition.X;
            float deltaY = player.GetMidPosition().Y - midPosition.Y;
            desiredAngle = ToDegrees((float)Math.Atan2(deltaY, deltaX));

            if (rotation > desiredAngle)
                rotation -= turnSpeed * a_deltaTime;
            if (rotation < desiredAngle)
                rotation -= turnSpeed * a_deltaTime;
            
            Vector2 N = new Vector2();
            N = player.GetMidPosition() - midPosition;
            N.Normalize();
            velocity += N * (speed * a_deltaTime);
            velocity.X = MathHelper.Clamp(velocity.X, -maxSpeed, maxSpeed);
            velocity.Y = MathHelper.Clamp(velocity.Y, -maxSpeed, maxSpeed);
            //Force = (velocity + Acceleration*a_deltaTime) ;
            //Force.X = MathHelper.Clamp(Force.X, -maxSpeed, maxSpeed);
            //Force.Y = MathHelper.Clamp(Force.Y, -maxSpeed, maxSpeed);
            //Force.X = (Force.X * (float)Math.Cos(rotation)) - (Force.Y * (float)Math.Sin(rotation));
            //Force.Y = (Force.Y * (float)Math.Cos(rotation)) - (Force.X * (float)Math.Sin(rotation));



            
            lastRotation = rotation;
            lastForce = Force;
        }

        public override void Draw(SpriteBatch spriteBatch, Camera a_camera)
        {
            spriteBatch.Draw(texture, new Rectangle((int)(midPosition.X - radius - a_camera.TopLeft.X), (int)(midPosition.Y - radius - a_camera.TopLeft.Y), (int)radius * 2, (int)radius * 2), Color.Yellow);
        }

        public Vector2 GetVelocity()
        {
            return velocity;
        }

        private void Shoot()
        {
            Bullet bullet = new Bullet(midPosition, player.GetMidPosition(), 0.3f, 10, "Enemy");
            InGame.addList.Add(bullet);
        }

        public void SetVelocity(Vector2 a_vel)
        {
            velocity = a_vel;
        }


    }
}
