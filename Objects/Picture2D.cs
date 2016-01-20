using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MirrorShooter
{
    public class Picture2D:GameObject
    {
        public static List<Texture2D> pictureList = new List<Texture2D>();

        public int width;
        public int height;
        public float scaleX;
        public float scaleY;
        public bool isVisible = true;
        public Picture2D(int index, int a_width, int a_height, Vector2 a_position):base (a_position)
        {
            
            texture = pictureList[index];
            width = a_width;
            height = a_height;
            scaleX = ((float)width / (float)texture.Width);
            scaleY = ((float)height / (float)texture.Height);
            
        }

        public override void Draw(SpriteBatch spriteBatch, Camera a_camera)
        {
            if(isVisible)
            spriteBatch.Draw(texture, new Vector2(midPosition.X - width / 2 - a_camera.TopLeft.X, midPosition.Y - height /2 - a_camera.TopLeft.Y), null, Color.White, ToRadians(spriteRotation), new Vector2(0, 0), new Vector2(scaleX, scaleY), SpriteEffects.None, 0f);
        }


    }
}
