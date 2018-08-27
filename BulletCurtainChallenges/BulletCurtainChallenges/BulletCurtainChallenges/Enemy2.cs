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
    class Enemy2 : Enemy
    {
        int frame,frame2,frame3;
        int frameDelay;
        float angle, angle2, posMod = 1, speedMod = 0, angleMod = 1, posX, posY;
        float deathScale = 0.4f, deathAlpha = 0.5f;
        bool invert = false;
        public Enemy2(GameScene scene) : base(scene)
        {
            health = 500;
            maxHealth = health;
            SetPosition(432 / 2, 140);
            angle = Rand(0, 360);
        }
        public override void Update()
        {
            frame++;
            frame2++;
            frame3++;
            if (frame == 30)
            {
                posX = 20;
                posY = 20;
                for (int i = 0; i < 6; i++)
                {
                    while (Loop(40))
                    {
                        CreateBulletA(posX, posY, "White", 21, false, 15);
                        SetBulletDataA(0, 4.5f + speedMod, -0.025f, 1.5f, angle, 0, "White", 21, false);
                        //SetBulletDataA(180, true, -0.015f, 0.5f, true, 0, "White", 21, false);
                        Shoot();
                        CreateBulletA(posX + 432-40, posY, "White", 21, true, 45);
                        SetBulletDataA(0, 4.5f + speedMod, -0.025f, 1.5f, -angle + 360f/80f, 0, "White", 21, true);
                        //SetBulletDataA(180, true, -0.015f, 0.5f, true, 0, "White", 21, true);
                        Shoot();
                        angle += 360f / 40f;
                        
                    }
                    speedMod += 0.01f;
                    angle += 0.75f;
                }
                speedMod = 0;
                frame = -40;  
            }
            if(frame2 == 100)
            {
                MoveRandom(140, 50, 432 - 140, 130, 30, 50, 0.05f);
                frame2 = 0;
            }
            if(frame3 == 240)
            {
                while(Loop(19))
                {
                    CreateBulletA(432 / 2, 576 - 10, "White", 22, invert, 30);
                    SetBulletDataA(0, 2, -0.05f, 0.7f, -90 + angle2, 0, "White", 22, invert);
                    Shoot();
                    angle2 += 360f /19f;
                }
                if (invert == true)
                    invert = false;
                else
                    invert = true;
                angle2 += 360f / 19f*1.5f;
                frame3 = 100;
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
