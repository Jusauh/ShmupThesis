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
    class TestEnemy : Enemy
    {
        int frame,frame2;
        int frameDelay;
        float angle, angle2, posMod = 1, speedMod = 0, angleMod = 1, posX, posY;
        float deathScale = 0.4f, deathAlpha = 0.5f;
        public TestEnemy(GameScene scene) : base(scene)
        {
            health = 550;
            maxHealth = health;
            SetPosition(432 / 2, 140);
        }
        public override void Update()
        {
            frame++;
            frame2++;
            if(frame == 20)
            {

                while(Loop(10))
                {
                    CreateBulletA(position.X, position.Y, "Green", 12, false, 44);
                    SetBulletDataA(0, 2, MathFunctions.Rand(0.1f, 0.15f), 3, angle2, 0, "Green", 12, false);
                    SetBulletDataA(50, 3, -MathFunctions.Rand(0.01f, 0.015f), 1.5f, angle2, 0.3f, "Green", 12, false);
                    SetBulletDataA(150, true, 0, 0, true, 0, "Green", 12, false);
                    Shoot();
                    CreateBulletA(position.X, position.Y, "Cyan", 12, false, 44);
                    SetBulletDataA(0, 2, MathFunctions.Rand(0.1f, 0.15f), 3, -angle2, 0, "Cyan", 12, false);
                    SetBulletDataA(50, 3, -MathFunctions.Rand(0.01f, 0.015f), 1.5f, -angle2, -0.3f, "Cyan", 12, false);
                    SetBulletDataA(150, true, 0, 0, true, 0, "Green", 12, false);
                    Shoot();
                    angle2 += 360f / 10f;
                }

                angle2 += 22.12f;
                frame = 11;
            }
            if(frame2 == 120)
            {
                while(Loop(20))
                {
                    CreateBulletA(position.X, position.Y, "Lime", 3, false, 20);
                    SetBulletDataA(0, 8, -0.1f, 1.5f, AngleToPlayer()+ angle, 0, "Lime", 3, false);
                    Shoot();
                    angle += 360 / 20;
                }
                frame2 = 40;
                
            }
            if (frame == 30)
            {
                posX = (432/2) + (MathFunctions.Rand(0, 200) * posMod);
                posY = MathFunctions.Rand(30, 150);
                //CreateLaser01(posX, posY, 6, MathFunctions.getAngleTo(new Vector2(posX, posY), new Vector2(playerX(), playerY())), 200, 10, "Red", 12, false);
                while(Loop(120))
                {
                    CreateBullet01(posX, posY, 1.4f + speedMod, MathFunctions.getAngleTo(new Vector2(posX, posY), new Vector2(playerX(), playerY())) + angle + 360f/240f, "Purple", 11, false,4);
                    angle += 360f / 120f*angleMod;
                    speedMod += 0.2f;
                    if (speedMod >= 1)
                        speedMod = 0;
                }
                posMod *= -1;
                angleMod *= -1;
                frame = -20;
                if(frame == 30)
                {

                }
            }

            if(frame2 == 600)
            {
                MoveTo2(0, 140, 0.2f);
            }
            if (frame2 == 1200)
            {
                MoveTo2(432, 140, 0.2f);
                frame2 = 0;
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
