using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MirrorShooter
{
    public abstract class GameState
    {
        public GameState()
        {

        }


        public abstract void Update(float a_deltaTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        

    }
}
