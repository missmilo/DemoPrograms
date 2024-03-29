﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGame
{
    class ParticleEngine
    {
        private Random m_random;
        public Vector2 m_emitterLocation { get; set; }
        private List<Particle> m_particles;
        private List<Texture2D> m_textures;
        private int m_particleStages;

        public ParticleEngine(List<Texture2D> textures, Vector2 location, int particleStages)
        {
            m_emitterLocation = location;
            m_particles = new List<Particle>();
            m_textures = textures;
            m_random = new Random();
            m_particleStages = particleStages;
        }

        private Particle GenerateNewParticle()
        {
            Texture2D texture = m_textures[m_random.Next(m_textures.Count)];
            Vector2 position = m_emitterLocation;
            Vector2 velocity = new Vector2(1f * (float)(m_random.NextDouble() * 2 - 1), 1f * (float)(m_random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(m_random.NextDouble() * 2 - 1);
            Color color = Color.White;//new Color((float)m_random.NextDouble(), (float)m_random.NextDouble(), (float)m_random.NextDouble());
            float size = (float)m_random.NextDouble();
            int ttl = 1 + m_random.Next(100,500);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl, m_particleStages);
        }

        public void Update()
        {
            int total = 1;

            for (int i = 0; i < total; i++)
            {
                m_particles.Add(GenerateNewParticle());
            }

            for(int particle =0; particle < m_particles.Count; particle++)
            {
                m_particles[particle].Update();
                if (m_particles[particle].m_ttl <= 0)
                {
                    m_particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for(int i =0; i < m_particles.Count; i++)
            {
                m_particles[i].Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
