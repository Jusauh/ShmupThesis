using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace BulletCurtainChallenges
{
    
    class BulletLazer : BulletBase
    {
        
        float length, width, maxLength,checkAngle;
        Vector2 test,origo;
        public BulletLazer(float posX, float posY, float speed, float angle, float length, float width, string color, int bullet, bool invert)
        {
            this.length = 0;
            this.position = new Vector2(posX, posY);
            this.speed = speed;
            this.angle = MathHelper.ToRadians(angle);
            this.maxLength = length;
            this.width = width;
            SetSprite(color, bullet, invert);
        }

        public override void Update(PlayerCharacter player)
        {
            base.Update(player);
        }
        protected override void UpdateBulletMovement()
        {
            UpdateSpeed();
            velocity = new Vector2((float)Math.Cos(angle) * speed, (float)Math.Sin(angle) * speed);
            velocityXY = new Vector2(speedX, speedY);
            if (length < maxLength)
            {
                length += speed;
            }
            else
            {
                length = maxLength;
                position += velocity;
                position += velocityXY;
            }
        }
        /*protected override void CheckCollision(Vector2 playerPosition)
        {
            Vector2 origo = position + new Vector2((float)Math.Cos(angle) * (length / 2), (float)Math.Sin(angle) * (width / 2));
            test = Vector2.Zero;
            if (MathFunctions.getDistanceTo(origo, playerPosition) <= (length/2))
            {
                float checkAngle;
                Vector2 checkHit;
                checkAngle = MathFunctions.getAngleTo(Vector2.Zero, velocity) + MathFunctions.getAngleTo(origo, playerPosition);
                checkHit = new Vector2((float)Math.Cos(MathHelper.ToRadians(checkAngle))*(length*0.45f),(float)Math.Sin(MathHelper.ToRadians(checkAngle))*(width*0.45f));
                test = origo + checkHit;
                //if (checkHit.Length() <= MathFunctions.getDistanceTo(origo, playerPosition))
                if (checkHit.Length() >= MathFunctions.getDistanceTo(origo, playerPosition))
                {
                    //setAlive(false);
                }
            }
        }
        ^*/
        protected override void CheckCollision(PlayerCharacter player)
        {       
            origo = position + new Vector2((float)Math.Cos(angle) * (length / 2), (float)Math.Sin(angle) * (length / 2));
            checkAngle = MathHelper.ToRadians(MathFunctions.getAngleTo(origo, player.getPosition()) -MathHelper.ToDegrees(angle));
            float distance = ((width/2) * (length/2)) / (float)Math.Sqrt((Math.Pow((length / 2) * Math.Sin(checkAngle), 2)) + (Math.Pow((width / 2) * Math.Cos(checkAngle), 2)));
            test = origo + new Vector2((float)Math.Cos(checkAngle - angle) * distance, (float)Math.Sin(checkAngle - angle) * distance);
            if(MathFunctions.getDistanceTo(origo, player.getPosition()) <= distance && player.getAlive()==true)
            {
                setAlive(false);
                player.takeHit();
            }
        }
         
        public override void Draw(SpriteBatch sb)
        {
            if(length != maxLength)
            {
                sb.Draw(Assets.bulletSheet, position + CommonData.drawOffset, delaySprite, Color.White, 0, new Vector2(delaySprite.Width / 2, delaySprite.Height / 2),((maxLength-length)/maxLength)*2, SpriteEffects.None, 0);
            }
            drawAngle = MathHelper.ToRadians(drawAngle = MathFunctions.getAngleTo(this.position, this.position + (velocity + velocityXY)));
            sb.Draw(spriteSheet2, position + CommonData.drawOffset, sprite, Color.White, drawAngle, new Vector2(0, (sprite.Height / 2)), new Vector2(length / sprite.Width, width / sprite.Height), SpriteEffects.None, 0);
            //Draw origo for testing
            //sb.Draw(spriteSheet, test, sprite, Color.White, drawAngle, new Vector2((sprite.Width / 2), (sprite.Height / 2)), 0.5f, SpriteEffects.None, 0);
            
        }
    }
}
