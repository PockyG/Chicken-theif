using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MirrorShooter
{
    public class ParticleEngine
    {
        private Random rand;
        public Vector2 position { get; set; }
        private List<Particle> particles;
        public static List<Texture2D> textures;

        public ParticleEngine(Vector2 a_position)
        {
            position = a_position;
            particles = new List<Particle>();
            rand = new Random();


        }



        public void Update(float a_deltaTime)
        {
            int total = 10;

            for (int i = 0; i < total; i++)
            {
                particles.Add(GenerateParticle());
            }

            for (int particle = 0; particle < particles.Count(); particle++)
            {
                particles[particle].Update(a_deltaTime);
                if (particles[particle].lifeSpan <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }

            }

                


        }


        public void Draw(SpriteBatch spriteBatch, Camera a_camera)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch, a_camera);
            }

        }

        private Particle GenerateParticle()
        {
            Texture2D textured = textures[rand.Next(textures.Count)];
            Vector2 pposition = this.position;
            
            Vector2 velocity = new Vector2(
                    1f * (float)(rand.NextDouble() * 2 - 1),
                    1f * (float)(rand.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(rand.NextDouble() * 2 - 1);
            Color color = new Color(
                    (float)rand.NextDouble(),
                    (float)rand.NextDouble(),
                    (float)rand.NextDouble());
            float size = (float)rand.NextDouble();
            int ttl = 20 + rand.Next(40);

            return new Particle(textured, pposition, velocity, angle, angularVelocity, color, ttl);

        }


    }
}
