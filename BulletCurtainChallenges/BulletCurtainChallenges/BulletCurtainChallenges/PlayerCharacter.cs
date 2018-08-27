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
    class PlayerCharacter
    {
        protected Vector2 position, velocity;
        protected float speed, hitTimer=20;
        protected int bombs;
        float deathScale, deathAlpha = 1;
        bool alive = true;
        
        public PlayerCharacter()
        {
            position = new Vector2(432/2, 400);
        }

        public void Update()
        {
            UpdateMovement();
            if (hitTimer <= 10)
                hitTimer--;
            if (hitTimer == 0)
                setAlive(false);
        }
        public void Draw(SpriteBatch sb)

        {
            if (getAlive() == true)
            {
                sb.Draw(Assets.playerSheet, position + CommonData.drawOffset, new Rectangle(0, 0, 64, 64), Color.White, 0, new Vector2(32, 32), 1, SpriteEffects.None, 0);
                if(hitTimer <= 10)
                {
                    sb.Draw(Assets.playerSheet,position + CommonData.drawOffset, new Rectangle(81, 17, 47, 47),Color.Red,0, new Vector2(47f / 2, 47f / 2),(hitTimer/10)*3,SpriteEffects.None,0);
                }
            }
            else
            {
                if (deathScale < 2)
                    deathScale += 0.1f;
                if (deathAlpha > 0 && deathScale > 1)
                    deathAlpha -= 0.05f;
                sb.Draw(Assets.enemyExplosion, position + CommonData.drawOffset, new Rectangle(0, 0, 128, 128), Color.White * deathAlpha, 0, new Vector2(64, 64), deathScale, SpriteEffects.None, 1);
            }
        }
        void UpdateMovement()
        {
            SetVelocity();
            position += velocity;
            ClampPosition();
        }
        void SetVelocity()
        {
            velocity = new Vector2(0, 0);
            if(Input.IsKeyDown(Keys.Left))
            {
                velocity.X = -1;
            }
            else if (Input.IsKeyDown(Keys.Right))
            {
                velocity.X = 1;
            }
            if (Input.IsKeyDown(Keys.Up))
            {
                velocity.Y = -1;
            }
            else if (Input.IsKeyDown(Keys.Down))
            {
                velocity.Y = 1;
            }
            if (Input.IsKeyDown(Keys.LeftShift))
            {
                speed = 2f;
            }
            else
            {
                speed = 4.5f;
            }
            if(velocity != new Vector2(0,0))
                velocity = Vector2.Normalize(velocity) * speed;
        }
        void ClampPosition()
        {
            if (this.position.X < CommonData.playerMinX)
                this.position.X = CommonData.playerMinX;
            if (this.position.Y < CommonData.playerMinY)
                this.position.Y = CommonData.playerMinY;
            if (this.position.X > CommonData.playerMaxX)
                this.position.X = CommonData.playerMaxX;
            if (this.position.Y > CommonData.playerMaxY)
                this.position.Y = CommonData.playerMaxY;
        }
        public Vector2 getPosition()
        {
            return position;
        }
        public void setPosition(Vector2 position)
        {
            this.position = position;
        }
        public bool getAlive()
        { return alive; }
        public void setAlive(bool value)
        { alive = value; }
        public void takeHit()
        {
            if(hitTimer > 10)
            hitTimer = 10;
        }
        public void resetHit()
        {
            hitTimer = 20;
        }
    }
}
