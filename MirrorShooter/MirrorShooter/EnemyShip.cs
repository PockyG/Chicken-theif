using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MirrorShooter
{
    public class EnemyShip: Ship
    {
        public int value;
        protected Player player;
        public float radius;

        public EnemyShip(Vector2 a_startPos, Player a_player):base (a_startPos)
        {
            player = a_player;

        }

        

        public virtual void Update(float a_deltaTime)
        {

        }

        public float GetRadius()
        {
            return radius;
        }

        


        

        



    }
}
