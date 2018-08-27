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
    class Bullet : BulletBase
    {
        public Bullet(float positionX, float positionY, float speed, float angle, int delay, string color, int shape, bool invert)
        {
            this.position = new Vector2(positionX, positionY);
            this.speed = speed;
            this.angle = MathHelper.ToRadians(angle);
            this.delay = -delay;
            SetSprite(color, shape, invert);
        }
        public Bullet(float positionX, float positionY, float speed, float acceleration, float maxSpeed, float angle, float rotation, int delay, string color,int shape, bool invert)
        {
            this.position = new Vector2(positionX, positionY);
            this.speed = speed;
            this.acceleration = acceleration;
            this.maxSpeed = maxSpeed;
            this.angle = MathHelper.ToRadians(angle);
            this.rotation = MathHelper.ToRadians(rotation);
            this.delay = -delay;
            SetSprite(color, shape, invert);
        }
        public Bullet(float positionX, float positionY, float velocityX, float accelerationX, float maxSpeedX, float velocityY, float accelerationY, float maxSpeedY, int delay, string color, int shape, bool invert)
        {
            this.position = new Vector2(positionX, positionY);
            this.speedX = velocityX;
            this.speedY = velocityY;
            this.accelerationX = accelerationX;
            this.accelerationY = accelerationY;
            this.maxSpeedX = maxSpeedX;
            this.maxSpeedY = maxSpeedY;
            this.delay = -delay;
            SetSprite(color, shape, invert);
        }

        public override void Update(PlayerCharacter player)
        {
            base.Update(player);

            
        }
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}
