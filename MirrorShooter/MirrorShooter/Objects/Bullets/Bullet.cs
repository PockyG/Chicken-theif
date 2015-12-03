using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace MirrorShooter
{
    public class Bullet : GameObject
    {
        public static new Texture2D texture;
        Vector2 unitVelocity;
        int radius;
        float degrees = 0;
        float speed;
        Vector2 velocity;
        float gravity = 0.002f;

        public string tag;

        public Bullet(Vector2 a_position, Vector2 a_shootAt, float a_speed, int a_radius, string a_tag)
            : base(a_position)
        {
            degrees = GetAngleBetween(a_position, a_shootAt);
            speed = a_speed;
            unitVelocity = a_shootAt - a_position;
            unitVelocity = unitVelocity / unitVelocity.Length();
            radius = a_radius;
            velocity = unitVelocity * speed;
            tag = a_tag;

        }

        public void Update(float a_deltaTime)
        {
            midPosition += velocity * a_deltaTime;
            //velocity.Y += gravity;

            //if (midPosition.X > Game1.rightUpperBound.X || midPosition.X < Game1.leftLowerBound.X || midPosition.Y > Game1.rightUpperBound.Y || midPosition.Y < Game1.leftLowerBound.Y)
            //{
            //    isAlive = false;
            //}

            if (midPosition.X + radius > InGame.PLAYFIELD_WIDTH || midPosition.X - radius < 0 || midPosition.Y + radius > InGame.PLAYFIELD_HEIGHT || midPosition.Y - radius < 0)
            {
                isAlive = false;
            }
                

        }

        public override void Draw(SpriteBatch spriteBatch, Camera a_camera)
        {
            
            spriteBatch.Draw(texture, new Rectangle((int)(midPosition.X - radius - a_camera.TopLeft.X), (int)(midPosition.Y - radius - a_camera.TopLeft.Y), (int)radius*2, (int)radius*2), Color.Red);
        }


        public Vector2 GetVelocity()
        {
            return velocity;
        }

        public void SetVelocity(Vector2 a_velocity)
        {
            velocity = a_velocity;
        }

        public float GetRadius()
        {
            return radius;
        }









    }
}
