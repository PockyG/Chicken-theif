using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MirrorShooter
{
    public class Cell
    {
        public Vector2 position;
        public static Texture2D texture;
        private int width;
        private float elapsedTime = 0;
        private const int Duration = 1000;
        private bool loop = true;
      
        public Cell(Vector2 a_position, int a_width)
        {
            position = a_position;
            width = a_width;
            
        }

        public void Update(float a_deltaTime)
        {
            if (elapsedTime >= Duration)
                loop = !loop;
            if (elapsedTime < 0)
                loop = !loop;

            if (loop)
                elapsedTime += a_deltaTime;
            else
                elapsedTime -= a_deltaTime;

            

            
        }

        public void Draw(SpriteBatch spritebatch, Camera a_camera)
        {
            
            float amount = MathHelper.Clamp(elapsedTime / Duration, 0, 1);
            spritebatch.Draw(texture, new Rectangle((int)(position.X - a_camera.TopLeft.X), (int)(position.Y - a_camera.TopLeft.Y), width, width), Color.Lerp(Color.White * 0.2f, Color.White * 0.1f, amount));

        }

    }
}
