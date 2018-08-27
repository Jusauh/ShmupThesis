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
    class BulletBase
    {
        protected Vector2 position, velocity, velocityXY;
        protected Texture2D spriteSheet = Assets.bulletSheet;
        protected Texture2D spriteSheet2 = Assets.bulletSheet2;
        protected float speed, speedX, speedY, maxSpeed, maxSpeedX, maxSpeedY;
        protected float acceleration, accelerationX, accelerationY;
        protected float angle,drawAngle, rotation;
        protected float hitBox;
        protected float scale;
        protected int delay, lifetime = -1;
        bool additive,invert, alive = true;
        bool spin, pointVelocity;
        protected Rectangle sprite, delaySprite, destroySprite;
        Rectangle spriteRectangle;

        public virtual void Update(PlayerCharacter player)
        {
            delay++;
            if (delay >= 0)
            {
                lifetime--;
                if(lifetime == 0)
                {
                    this.alive = false;
                }
                UpdateBulletMovement();
                CheckBoundries();
                CheckCollision(player);
            }
            else if(delay >= -60)
            {
                scale = (float)-delay / 40f;
            }
            else
            {
                scale = 60f / 40f;
            }
        }

        public virtual void Draw(SpriteBatch sb)
        {
            if (delay >= 0)
            {
                if (spin == true)
                    drawAngle += 0.04f;
                if (pointVelocity == true)
                    drawAngle = MathHelper.ToRadians(drawAngle = MathFunctions.getAngleTo(this.position, this.position + (velocity + velocityXY)));
                sb.Draw(spriteSheet2, position + CommonData.drawOffset, sprite, Color.White, drawAngle, new Vector2(sprite.Width / 2, sprite.Height / 2), 1f, SpriteEffects.None, 0);
            }
            else
            {
                sb.Draw(Assets.bulletSheet, position + CommonData.drawOffset, delaySprite, Color.White, 0, new Vector2(delaySprite.Width / 2, delaySprite.Height / 2), 0.25f + scale, SpriteEffects.None, 0);
            }
        }

        protected virtual void UpdateBulletMovement()
        {
            UpdateSpeed();
            velocity = new Vector2((float)Math.Cos(angle) * speed, (float)Math.Sin(angle) * speed);
            velocityXY = new Vector2(speedX, speedY);
            position += velocity;
            position += velocityXY;
        }

        protected void UpdateSpeed()
        {
            angle += rotation;
            speed += acceleration;
            speedX += accelerationX;
            speedY += accelerationY;
            ClampSpeed();
        }

        private void ClampSpeed()
        {
            if (maxSpeed - Math.Abs(acceleration - 0.001) <= speed && maxSpeed + Math.Abs(acceleration + 0.001) >= speed)
                speed = maxSpeed;
            if (maxSpeedX - Math.Abs(accelerationX - 0.001) <= speedX && maxSpeedX + Math.Abs(accelerationX + 0.001) >= speedX)
            {
                speedX = maxSpeedX;
            }
            if (maxSpeedY - Math.Abs(accelerationY - 0.001) <= speedY && maxSpeedY + Math.Abs(accelerationY + 0.001) >= speedY)
            {
                speedY = maxSpeedY;
            }
        }
        private void CheckBoundries()
        {
            if (this.position.X <  - 150)
                setAlive(false);
            else if (this.position.X > 432+ 150)
                setAlive(false);
            else if (this.position.Y < - 150)
                setAlive(false);
            else if (this.position.Y > 576 + 150)
                setAlive(false);
        }
        protected virtual void CheckCollision(PlayerCharacter player)
        {
            if (MathFunctions.getDistanceTo(this.position, player.getPosition()) <= hitBox && this.pointVelocity == false && player.getAlive() == true)
            {
                setAlive(false);
                player.takeHit();
            }
            else if (MathFunctions.getDistanceTo(this.position, player.getPosition()) <= hitBox && this.pointVelocity == true  && player.getAlive()==true)
            {
                float width = sprite.Height;
                float length = sprite.Width;

                Vector2 origo = position;
                float checkAngle = MathHelper.ToRadians(MathFunctions.getAngleTo(origo, player.getPosition()) - MathHelper.ToDegrees(angle));
                float distance = ((width / 2) * (length / 2)) / (float)Math.Sqrt((Math.Pow((length / 2) * Math.Sin(checkAngle), 2)) + (Math.Pow((width / 2) * Math.Cos(checkAngle), 2)));
                if (MathFunctions.getDistanceTo(origo, player.getPosition()) <= distance)
                {
                    setAlive(false);
                    player.takeHit();
                }
            }
        }
        public bool getAlive()
        {
            return alive;
        }
        public void setAlive(bool value)
        {
            alive = value;
            float scale = spriteRectangle.Width/ 20;
            Rectangle crop = new Rectangle(spriteRectangle.X, 394, 25, 25);
            if (invert == true)
            {
                crop.Y += 25;
            }
            FXManager.CreateFX(Assets.bulletSheet2, crop, new Vector2(25f / 2, 25f / 2), Color.White, this.position + CommonData.drawOffset, false, 0, 0, 0, 0, scale, 0.1f, 5);
        }
        protected void SetSprite(string color,int shape, bool invert)
        {
            spriteRectangle = new Rectangle(0, 0, 0, 0);
            delaySprite = new Rectangle(0, 88, 32, 32);
            this.pointVelocity = false;
            this.spin = false;
            this.additive = false;
            //Setting the offset for shape
            if(shape == 1)
            {
                spriteRectangle.Y = 1;
                spriteRectangle.Width = 10;
                spriteRectangle.Height = 10;
                hitBox = 4;
            }
            else if (shape == 2)
            {
                spriteRectangle.Y = 13;
                spriteRectangle.Width = 20;
                spriteRectangle.Height = 20;
                hitBox = 8;
            }
            else if (shape == 3)
            {
                spriteRectangle.Y = 35;
                spriteRectangle.Width = 34;
                spriteRectangle.Height = 34;
                hitBox = 16;
            }
            else if (shape == 11)
            {
                spriteRectangle.Y = 71;
                spriteRectangle.Width = 20;
                spriteRectangle.Height = 10;
                hitBox = 10f;
                this.pointVelocity = true;
            }
            else if (shape == 12)
            {
                spriteRectangle.Y = 83;
                spriteRectangle.Width = 32;
                spriteRectangle.Height = 15;
                hitBox = 16;
                this.pointVelocity = true;
            }
            else if (shape == 21)
            {
                spriteRectangle.Y = 100;
                spriteRectangle.Width = 15;
                spriteRectangle.Height = 15;
                hitBox = 6;
                this.spin = true;
            }
            else if (shape == 22)
            {
                spriteRectangle.Y = 117;
                spriteRectangle.Width = 27;
                spriteRectangle.Height = 27;
                hitBox = 10;
                this.spin = true;
            }
            else if (shape == 31)
            {
                spriteRectangle.Y = 146;
                spriteRectangle.Width = 18;
                spriteRectangle.Height = 18;
                hitBox = 7;
                this.additive = true;
            }
            else if (shape == 32)
            {
                spriteRectangle.Y = 166;
                spriteRectangle.Width = 30;
                spriteRectangle.Height = 30;
                hitBox = 13;
                this.additive = true;
            }
            //Setting the offset for color
            if (color == "Red")
            {
                spriteRectangle.X = 1;
                delaySprite.X = 0;
            }
            else if (color == "Orange")
            {
                spriteRectangle.X = 1 + 36 * 1;
                delaySprite.X = 32 * 1;
            }
            else if (color == "LOrange")
            {
                spriteRectangle.X = 1 + 36 * 2;
                delaySprite.X = 32 *2;
            }
            else if (color == "Yellow")
            {
                spriteRectangle.X = 1 + 36 * 3;
                delaySprite.X = 32 * 3;
            }
            else if (color == "Lime")
            {
                spriteRectangle.X = 1 + 36 * 4;
                delaySprite.X = 32 * 4;
            }
            else if (color == "Green")
            {
                spriteRectangle.X = 1 + 36 * 5;
                delaySprite.X = 32 * 5;
            }
            else if (color == "Cyan")
            {
                spriteRectangle.X = 1 + 36 * 6;
                delaySprite.X = 32 * 6;
            }
            else if (color == "LBlue")
            {
                spriteRectangle.X = 1 + 36 * 7;
                delaySprite.X = 32 * 7;
            }
            else if (color == "Blue")
            {
                spriteRectangle.X = 1 + 36 * 8;
                delaySprite.X = 32 * 8;
            }
            else if (color == "Purple")
            {
                spriteRectangle.X = 1 + 36 * 9;
                delaySprite.X = 32 * 9;
            }
            else if (color == "LPurple")
            {
                spriteRectangle.X = 1 + 36 * 10;
                delaySprite.X = 32 * 10;
            }
            else if (color == "Magenta")
            {
                spriteRectangle.X = 1 + 36 * 11;
                delaySprite.X = 32 * 11;
            }
            else if (color == "White")
            {
                spriteRectangle.X = 1 + 36 * 12;
                delaySprite.X = 32 * 12;
            }
            // Adding more offset if the sprite is inverted
            if (invert == true)
            {
                spriteRectangle.Y += 197;
                delaySprite.Y += 146;
                this.invert = true;
            }
            this.sprite = spriteRectangle;
        }

        public bool getAdditive()
        {
            return additive;
        }

        protected float playerX()
        {
            return CommonData.playerPositionX;
        }
        protected float playerY()
        {
            return CommonData.playerPositionY;
        }
        protected float AngleToPlayer()
        {
            return MathFunctions.getAngleTo(this.position, new Vector2(playerX(), playerY()));
        }
        public void SetLifetime(int amount)
        {
            this.lifetime = amount;
        }
    }
}
