using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MirrorShooter
{
    public class Mirror: GameObject
    {
        public static new Texture2D texture;
        public Rectangle rectangle;
        private Vector2 rectangleOrigin;
        
        private Rectangle drawRect;
        int width;
        int height;
        float rotation;

        Vector2 topLeft, topRight, bottomLeft, bottomRight;
        

        public Mirror(Vector2 a_midPoint, int a_width, int a_height, float a_rotation) : base(a_midPoint)
        {
            width = a_width;
            height = a_height;

            

            topLeft = new Vector2(a_midPoint.X, a_midPoint.Y );
            rectangle = new Rectangle((int)topLeft.X, (int)topLeft.Y, width, height);
            rectangleOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
            rotation = a_rotation;
            


            
            topRight = new Vector2(rectangle.X + width, rectangle.Y);
            bottomLeft = new Vector2(rectangle.X, rectangle.Y + height);
            bottomRight = new Vector2(rectangle.X + width, rectangle.Y + height);


        }

        public virtual void Update(float a_deltaTime)
        {



            rectangle = new Rectangle((int)midPosition.X, (int)midPosition.Y, width, height);
            float radians = ToRadians(rotation);

            topLeft = new Vector2((float)((midPosition.X - width) * Math.Sin(radians)), (float)(midPosition.Y * Math.Cos(radians)));
            topRight = new Vector2((float)((midPosition.X + width) * Math.Sin(radians)), (float)(midPosition.Y * Math.Cos(radians)));
            bottomLeft = new Vector2((float)(midPosition.X * Math.Sin(radians)), (float)((midPosition.Y + height) * Math.Cos(radians)));
            bottomRight = new Vector2((float)((midPosition.X + width) * Math.Sin(radians)), (float)((midPosition.Y + height) * Math.Cos(radians)));


            if (InputManager.Instance.KeyDown(Keys.NumPad4))
            {
                rotation -= 1;
                //Console.WriteLine("GET RECT SCRUB");
                //Console.WriteLine(topLeft.X + "  " + topLeft.Y);
                //Console.WriteLine(topRight.X + "  " + topRight.Y);
                //Console.WriteLine(bottomLeft.X + "  " + bottomLeft.Y);
                //Console.WriteLine(bottomRight.X + "  " + bottomRight.Y);
                //Console.WriteLine("ROTATION: " + rotation);
            }
            if(InputManager.Instance.KeyDown(Keys.NumPad6))
            {
                rotation += 1;
                //Console.WriteLine("GET RECT SCRUB");
                //Console.WriteLine(topLeft.X + "  " + topLeft.Y);
                //Console.WriteLine(topRight.X + "  " + topRight.Y);
                //Console.WriteLine(bottomLeft.X + "  " + bottomLeft.Y);
                //Console.WriteLine(bottomRight.X + "  " + bottomRight.Y);
                //Console.WriteLine("ROTATION: " + rotation);
            }

        }

        public override void Draw(SpriteBatch spriteBatch, Camera a_camera)
        {
            drawRect = new Rectangle((int)(rectangle.X - a_camera.TopLeft.X),(int)(rectangle.Y - a_camera.TopLeft.Y), rectangle.Width, rectangle.Height);


            spriteBatch.Draw(texture, drawRect, null, Color.White, ToRadians(rotation), rectangleOrigin, SpriteEffects.None, 1);
        }

    }
}
