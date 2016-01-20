using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MirrorShooter
{
    public class ChickenPlayer : Chicken
    {
        public ChickenPlayer(Vector2 a_position, int a_spriteRadius, int a_hitBoxRadius, Vector2 a_startVel)
            : base(a_position, a_spriteRadius, a_hitBoxRadius, a_startVel)
        {
            walletTaker = true;
            fleeSpeed = 1.4f;

        }

        public override void Update(float a_deltaTime)
        {
            midPosition += velocity;

            if (InputManager.Instance.KeyDown(Keys.W))
            {
                velocity = new Vector2(GetVelocity().X, -1 * fleeSpeed);
            }
            if (InputManager.Instance.KeyDown(Keys.S))
            {
                velocity = new Vector2(GetVelocity().X, 1 * fleeSpeed);
            }
            if (InputManager.Instance.KeyDown(Keys.A))
            {
                velocity = new Vector2(-1 * fleeSpeed, GetVelocity().Y);
            }
            if (InputManager.Instance.KeyDown(Keys.D))
            {
                velocity = new Vector2(1 * fleeSpeed, GetVelocity().Y);
            }


            SpriteManage(a_deltaTime);
        }


    }
}
