using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MirrorShooter
{
    public class Chicken : PhysicsObject
    {
        
        public static Texture2D textureRef;
        public static Texture2D hatTextureRef;
        public static Texture2D testc;
        public static int totalHats; //number of hats in the texture.
        public static int hatWidth;
        public static int hatHeight;
        public Texture2D hat;
        public bool winner = false;
        public float facingRotation;
        public bool isSelected = false;

        //sprite stuff
        float frameTime = 0;
        float frameDuration = 50;
        private int activeFrame = 0;

        float chickenWidth = 32.5f;
        float chickenHeight = 32;
        float chickenScale;
        public int chickenColour;
        //3 walking frames. 0-1-2
        int chickenWalkFrame = 0;
        float chickenWalkFrameDuration = 500;
        float chickenWalkFrameTimer = 0;
        //0- down  1- left  2- right  3- up
        int chickenDirection = 0;

        //Wander properties.------------
        //time spent in wander.
        private float wanderDuration = 1000;
        private float wanderDurationTimer = 0;
        private float wanderDurationMin = 500;
        //time until next wander.
        private float wanderPendingMin = 5000;
        private float wanderPending;
        private float wanderPendingTimer = 0;
        private bool isWandering = false;
        private Random random;

        public bool walletTaker = false;
        //flee
        private bool isFleeing = false;
        private float fleeTimer = 0;
        private float fleeDuration = 2000;
        Vector2 fleeDirection;
        protected float fleeSpeed = 1;

        public Chicken(Vector2 a_position, int a_spriteRadius, int a_hitBoxRadius, Vector2 a_startVel)
            : base(a_position,a_spriteRadius, a_hitBoxRadius)
        {

            velocity = a_startVel;
            facingRotation = GetAngleBetween(a_position, a_position + a_startVel);
            texture = textureRef;
            fleeSpeed = 5f;
            velocity.Normalize();
            random = new Random();
            chickenColour = random.Next(4);
            velocity *= (1 + (1/random.Next(10, 50))) * 0.5f;

            spriteScale.X = (float) (spriteRadius * 2) /  chickenWidth;
            spriteScale.Y = (float)(spriteRadius * 2) / chickenHeight;
            hitBoxScale.X = (float)(hitBoxRadius * 2) / texture.Width;
            hitBoxScale.Y = (float)(hitBoxRadius * 2) / texture.Height;

            wanderPending = wanderPendingMin;

            
            
        }

         public override void Update(float a_deltaTime)
         {
             base.Update(a_deltaTime);
             //chicken movement here.
             wanderPendingTimer += a_deltaTime;
             if (isWandering == true && isFleeing == false)
             {
                 wanderDurationTimer += a_deltaTime;

                 SetVelocity(new Vector2(0.05f * (float)Math.Cos(facingRotation * (Math.PI / 180)), 0.05f * (float)Math.Sin(facingRotation * (Math.PI / 180))));
                 if (wanderDurationTimer > wanderDuration)
                 {
                     isWandering = false;
                     wanderPendingTimer = 0;
                     wanderPending = wanderPendingMin + random.Next(1000);
                     wanderDurationTimer = 0;
                     wanderDuration = wanderDurationMin + random.Next(600);
                     int temp = random.Next(0, 2);
                     if (temp == 0)
                     {
                         facingRotation += random.Next(80);
                         
                     }
                     else
                     {
                         facingRotation -= random.Next(80);
                     }

                     while (facingRotation > 180)
                     {
                         facingRotation -= 360;
                     }
                     while(facingRotation < -180)
                     {
                         facingRotation += 360;
                     }
                 }

             }
             else if (wanderPendingTimer > wanderPending && isFleeing == false)
             {
                 isWandering = true;
             }
             else if(isFleeing)
             {
                 fleeTimer += a_deltaTime;
                 fleeDirection.Normalize();
                 if (walletTaker)
                 {
                     SetVelocity(fleeDirection * fleeSpeed * 0.02f);
                 }
                 else
                 {
                     SetVelocity(fleeDirection * fleeSpeed * 0.04f);
                 }
                 if (fleeTimer > fleeDuration)
                 {
                     isFleeing = false;
                     fleeTimer = 0;
                 }
             }
             SpriteManage(a_deltaTime);



         }

         protected void SpriteManage(float a_deltaTime)
         {
             float tempAngle = GetAngleBetween(midPosition, midPosition + velocity);
             if (tempAngle > -45 && tempAngle < 45)
             {
                 chickenDirection = 2;
             }
             else if (tempAngle > 44 && tempAngle < 135)
             {
                 chickenDirection = 0;
             }
             else if (tempAngle > 134 || tempAngle < -135)
             {
                 chickenDirection = 1;
             }
             else if (tempAngle > -135 && tempAngle < -44)
             {
                 chickenDirection = 3;
             }

             if (velocity.LengthSquared() == 0)
             {
                 chickenDirection = 0;
             }



             if (velocity.LengthSquared() > 0)
             {
                 chickenWalkFrameTimer += a_deltaTime;
                 if (chickenWalkFrameTimer > chickenWalkFrameDuration)
                 {
                     chickenWalkFrame++;
                     chickenWalkFrameTimer = 0;
                     if (chickenWalkFrame > 2)
                     {
                         chickenWalkFrame = 0;
                     }
                 }
             }


             velocity *= 0.95f;

             if (velocity.LengthSquared() < 0.005f)
             {
                 velocity = Vector2.Zero;
             }

             //spritestuff
             frameTime += a_deltaTime;

             if (frameTime > frameDuration)
             {
                 activeFrame++;
                 frameTime = 0;


             }

         }
         public virtual void Flee(Vector2 source)
         {
             //flee source here.
             fleeDirection = midPosition - source;
             float angle = GetAngleBetween(midPosition, fleeDirection);
             facingRotation = angle;
             fleeDirection.Normalize();
    
             isFleeing = true;
            


         }

         public void Death()
         {
             isAlive = false;
         }
         public override void Draw(SpriteBatch spriteBatch, Camera a_camera)
         {
             if (chickenColour < 2)
             {
                 spriteBatch.Draw(texture, new Vector2(midPosition.X  - a_camera.TopLeft.X, midPosition.Y   - a_camera.TopLeft.Y), new Rectangle(0 + (int)(chickenWalkFrame * 32.5f), 0 + (chickenDirection + (chickenColour * 4)) * 32, (int)32.5f, 32), Color.White, ToRadians(spriteRotation), new Vector2(0, 0), spriteScale, SpriteEffects.None, 0f);
             }
             else
             {
                 spriteBatch.Draw(texture, new Vector2(midPosition.X - a_camera.TopLeft.X, midPosition.Y - a_camera.TopLeft.Y), new Rectangle(0 + (int)((chickenWalkFrame + 3) * 32.5f), 0 + (chickenDirection + ((chickenColour - 2) * 4)) * 32, (int)32.5f, 32), Color.White, ToRadians(spriteRotation), new Vector2(0, 0), spriteScale, SpriteEffects.None, 0f);
             }
             if(walletTaker && winner)
             spriteBatch.Draw(testc, midPosition, Color.Red);

             //DRAW THE HAT IN THE RIGHT POSITION RELATIVE TO THE CHICKEN.
             switch(chickenDirection)
             {
                 case 0://down
                     spriteBatch.Draw(testc, new Rectangle((int)(midPosition.X + 32), (int)(midPosition.Y + 15), (int)(30), (int)(30)), Color.White);
                     break;
                 case 1://left
                     spriteBatch.Draw(testc, new Rectangle((int)(midPosition.X + 10), (int)(midPosition.Y + 20), (int)(30), (int)(30)), Color.White);
                     break;
                 case 2://right
                     spriteBatch.Draw(testc, new Rectangle((int)(midPosition.X + 50), (int)(midPosition.Y + 20), (int)(30), (int)(30)), Color.White);
                     break;
                 case 3://up
                     spriteBatch.Draw(testc, new Rectangle((int)(midPosition.X + 32), (int)(midPosition.Y + 10), (int)(30), (int)(30)), Color.White);
                     break;

             }
             
             

             
         }


         public Vector2 GetVelocity()
         {
             return velocity;
         }

         public void SetVelocity(Vector2 a_velocity)
         {
             velocity = a_velocity;
         }

         public float GetRadiusHitbox()
         {
             return hitBoxRadius;
         }

    }
}
