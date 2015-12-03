using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MirrorShooter
{
    public abstract class Ship : GameObject
    {
        
        protected float rotation;
        protected float maxSpeed;
        protected float speed;
        protected int health;


        public Ship(Vector2 a_position)
            : base(a_position)
        {
            rotation = 0;
            maxSpeed = 1;
            speed = 1;
        }

        public void TakeDamage(int bullet_damage)
        {
            health -= bullet_damage;
            if (health <= 0)
                isAlive = false;

        }





    }
}
