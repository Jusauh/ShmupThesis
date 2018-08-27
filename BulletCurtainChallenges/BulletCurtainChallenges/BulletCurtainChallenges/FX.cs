using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BulletCurtainChallenges
{
    class FX
    {
        Vector2 position, origin, velocity;
        Color color;
        Texture2D texture;
        Rectangle crop;
        int lifetime;
        float angle, rotation, speed, acceleration, scale, scaleMod;
        bool alive = true, additive;

        public FX(Texture2D texture, Rectangle crop, Vector2 origin, Vector2 position, float angle, float speed, float scale, int lifetime)
        {
            this.position = position;
            this.texture = texture;
            this.crop = crop;
            this.origin = origin;
            this.angle = MathHelper.ToRadians(angle);
            this.speed = speed;
            this.scale = scale;
            this.lifetime = lifetime;
        }
        public FX(Texture2D texture, Rectangle crop, Vector2 origin, Color color, Vector2 position, bool additive, float angle, float rotation, float speed, float acceleration, float scale, float scaleMod, int lifetime)
        {
            this.position = position;
            this.texture = texture;
            this.crop = crop;
            this.origin = origin;
            this.angle = MathHelper.ToRadians(angle);
            this.rotation = MathHelper.ToRadians(rotation);
            this.speed = speed;
            this.acceleration = acceleration;
            this.scale = scale;
            this.scaleMod = scaleMod;
            this.lifetime = lifetime;
            this.color = color;
            this.additive = additive;
        }


        public void Update()
        {
            lifetime--;
            angle += rotation;
            scale += scaleMod;
            velocity = new Vector2((float)Math.Cos(angle) * speed, (float)Math.Sin(angle) * speed);
            position += velocity;

            if (lifetime == 0)
                alive = false;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, crop, color, angle, origin, scale, SpriteEffects.None, 0);
        }

        public bool IsAlive()
        {
            return alive;
        }
        public bool GetAdditiveStatus()
        {
            return additive;
        }
    }
}