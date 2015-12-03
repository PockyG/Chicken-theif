using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MirrorShooter
{
    public abstract class GameObject
    {
        public Texture2D texture;
        protected Vector2 midPosition;
        public float spriteRotation;
        public float scale;
        public bool isAlive;
      

        public GameObject(Vector2 a_midPosition)
        {
            midPosition = a_midPosition;
            scale = 1;
            spriteRotation = 0;
            isAlive = true;
        }

        public virtual void Update(float a_deltaTime)
         {


         }

        public virtual void Draw(SpriteBatch spriteBatch, Camera a_camera)
        {
            spriteBatch.Draw(texture, new Vector2(midPosition.X - texture.Width / 2 - a_camera.TopLeft.X, midPosition.Y - texture.Height / 2 - a_camera.TopLeft.Y),null, Color.White, ToRadians(spriteRotation), new Vector2(0,0),new Vector2(scale,scale),SpriteEffects.None,  0f);
        }

        public float ToRadians(float a_degrees)
        {

            return a_degrees * ((float)Math.PI / 180);
        }

        public float ToDegrees(float a_radians)
        {
            return a_radians * (float)(180 / Math.PI);
        }

       
        /// <summary>
        /// return in degrees.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public float GetAngleBetween(Vector2 a, Vector2 b)
        {
            float xDiff = b.X - a.X;
            float yDiff = b.Y - a.Y;
            float angle = (float)Math.Atan2(yDiff, xDiff) * 180 / (float)Math.PI;
            while (angle > 180)
            {
                angle -= 360;
            }
            while (angle < -180)
            {
                angle += 360;
            }
            return angle; 
        }
        

        public Vector2 GetUnitVector(float a_angle)
        {
            return new Vector2((float)Math.Sin(ToRadians(a_angle)), (float)Math.Cos(ToRadians(a_angle)));
        }

        public Vector2 GetMidPosition()
        {
            return midPosition;
        }
        public void SetMidPosition(Vector2 a_position)
        {
            midPosition = a_position;
        }

    }
}
