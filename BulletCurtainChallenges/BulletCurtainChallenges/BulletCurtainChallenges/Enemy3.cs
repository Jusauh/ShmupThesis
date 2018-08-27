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
    class Enemy3 : Enemy
    {
        int frame,frame2,frame3;
        float angle,angle2, speed = 0, posX, posY, radius=0;
        float deathScale = 0.4f, deathAlpha = 0.5f;
        public Enemy3(GameScene scene) : base(scene)
        {
            health = 700;
            maxHealth = health;
            SetPosition(432 / 2, 140);
            angle = 0;
            radius = 0;
            speed = 2.2f;
        }
        public override void Update()
        {
            frame++;
            frame2++;
            frame3++;
            if (frame == 30)
            {

                radius += 2;
                angle -= (float)Math.Sin((MathHelper.ToRadians(radius/ 1.3f)));

                while (Loop(9))
                {
                    posX = position.X+ (float)Math.Cos(MathHelper.ToRadians(radius)) * 50 * (float)Math.Cos(MathHelper.ToRadians(angle));
                    posY = position.Y+ (float)Math.Cos(MathHelper.ToRadians(radius)) * 50 * (float)Math.Sin(MathHelper.ToRadians(angle));
                    CreateBulletA(posX, posY, "Purple", 31, false, 30);
                    SetBulletDataA(0, speed, 0, 0, angle + 45, -2f, "Purple", 31, false);
                    SetBulletDataA(45, speed, 0, 0, true, 2f, "Purple", 31, false);
                    SetBulletDataA(45 * 2, speed, 0, 0, true, -2f, "Purple", 31, false);
                    SetBulletDataA(45 * 3, speed, 0, 0, true, 2f, "Purple", 31, false);
                    SetBulletDataA(45 * 4, speed, 0, 0, true, -2f, "Purple", 31, false);
                    SetBulletDataA(45 * 5, speed, 0, 0, true, 2f, "Purple", 31, false);
                    SetBulletDataA(45 * 6, speed, 0, 0, true, -2f, "Purple", 31, false);
                    SetBulletDataA(45 * 7, speed, 0, 0, true, 2f, "Purple", 31, false);
                    SetBulletDataA(45 * 8, speed, 0, 0, true, -2f, "Purple", 31, false);
                    Shoot();
                    angle += 360f / 9f;
                }

                frame = 28;  
            }
            if(frame2 == 100)
            {
                CreateBullet02(position.X, position.Y, 3, -0.07f, 1, angle2, 0, "Magenta", 2, false, 20);
                angle2 += Rand(100, 120);
                CreateBullet02(position.X, position.Y, 3, -0.07f, 1, angle2, 0, "Magenta", 1, false, 20);
                angle2 += Rand(100, 120);
                CreateBullet02(position.X, position.Y, 3, -0.07f, 1, angle2, 0, "Magenta", 3, false, 20);
                angle2 += Rand(100, 120);
                frame2 = 93;
            }
            base.Update();
        }
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            if (getAlive() == true)
                sb.Draw(Assets.playerSheet, position + CommonData.drawOffset, new Rectangle(0, 0, 64, 64), Color.Red, 0, new Vector2(32, 32), 1, SpriteEffects.FlipVertically, 0);
            else
            {
                if(deathScale < 8)
                deathScale += 0.2f;
                if (deathAlpha > 0 && deathScale > 4)
                    deathAlpha -= 0.01f;
                Color color = new Color(1f, 1f, 1f, 0 );
                sb.Draw(Assets.enemyExplosion, position + CommonData.drawOffset, new Rectangle(0, 0, 128, 128),Color.White*deathAlpha,0,new Vector2(64,64),deathScale,SpriteEffects.None,1);
                
            }
        }
    }
}
