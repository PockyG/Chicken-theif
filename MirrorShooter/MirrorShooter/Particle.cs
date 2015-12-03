using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace MirrorShooter
{
    public class Particle : GameObject
    {
        Vector2 velocity { get; set; }
        public float lifeSpan { get; set; }
        float angle { get; set; }
        float angularVelocity { get; set; }
        public Color color { get; set; }

        public Particle(Texture2D a_texture, Vector2 position, Vector2 a_velocity,
            float a_angle, float a_angularVelocity, Color a_color, int a_lifespan):base(position)
        {
            texture = a_texture;
            velocity = a_velocity;
            angle = a_angle;
            angularVelocity = a_angularVelocity;
            color = a_color;
            
            lifeSpan = a_lifespan;

        }


        public void Update(float a_deltaTime)
        {
            lifeSpan -= a_deltaTime;
            midPosition += velocity;
            angle += angularVelocity;
        }


        public override void Draw(SpriteBatch spriteBatch, Camera a_camera)
        {
            base.Draw(spriteBatch, a_camera);
        }



    }
}
