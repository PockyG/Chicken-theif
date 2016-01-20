using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MirrorShooter
{
    public class Selector : GameObject
    {
        GameObject target;
        public static Texture2D textureRef;
        public bool isVisible;

        public Selector(Vector2 a_startPos):base(a_startPos)
        {
            
            texture = textureRef;
            target = null;
        }

        public override void Update(float a_deltaTime)
        {
            base.Update(a_deltaTime);
            if(target != null)
            midPosition = target.GetMidPosition();



        }

        public override void Draw(SpriteBatch spriteBatch, Camera a_camera)
        {
            if(isVisible)
            spriteBatch.Draw(texture, midPosition, Color.Red);

        }

        public void Death()
        {
            isAlive = false;
        }

        public void Follow(GameObject a_target)
        {
            target = a_target;
        }

    }
}
