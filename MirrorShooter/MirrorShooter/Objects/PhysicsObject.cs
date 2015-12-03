using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MirrorShooter
{
    public abstract class PhysicsObject : GameObject
    {
        Vector2 unitVelocity = Vector2.Zero;
        public int spriteRadius = 1;
        public int hitBoxRadius = 1;
        public Vector2 spriteScale;
        public Vector2 hitBoxScale;
        float speed = 0;
        public Vector2 velocity = new Vector2(0,0);

        public PhysicsObject(Vector2 a_position, int a_spriteRadius, int a_hitBoxRadius) : base (a_position)
        {
            spriteRadius = a_spriteRadius;
            hitBoxRadius = a_hitBoxRadius;




        }


        public new virtual void Update(float a_deltaTime)
        {
            midPosition += velocity * a_deltaTime;


        }

        public new virtual void Draw(SpriteBatch spriteBatch, Camera a_camera)
        {
            
            spriteBatch.Draw(texture, new Vector2(midPosition.X - texture.Width / 2 - a_camera.TopLeft.X, midPosition.Y - texture.Height / 2 - a_camera.TopLeft.Y), null, Color.White, ToRadians(spriteRotation), new Vector2(0, 0), spriteScale, SpriteEffects.None, 0f);
        #if DEBUG
            spriteBatch.Draw(texture, new Vector2(midPosition.X - texture.Width / 2 - a_camera.TopLeft.X, midPosition.Y - texture.Height / 2 - a_camera.TopLeft.Y), null, Color.White, ToRadians(spriteRotation), new Vector2(0, 0), hitBoxScale, SpriteEffects.None, 0f);
            

#endif
        }


    }



}
