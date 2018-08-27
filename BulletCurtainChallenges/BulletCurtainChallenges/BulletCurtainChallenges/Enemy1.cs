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
    class Enemy1 : Enemy
    {
        int frame,frame2;
        int frameDelay;
        float angle, angle2, posMod = 1, speedMod = 0, angleMod = 1, posX, posY;
        float deathScale = 0.4f, deathAlpha = 0.5f;
        public Enemy1(GameScene scene) : base(scene)
        {
            health = 500;
            maxHealth = health;
            SetPosition(432 / 2, 140);
        }
        public override void Update()
        {
            frame++;
            frame2++;
            if (frame == 30)
            {
                posX = (432/2) + (MathFunctions.Rand(0, 200) * posMod);
                posY = MathFunctions.Rand(30, 150);
                CreateLaser01(posX, posY, 6, MathFunctions.getAngleTo(new Vector2(posX, posY), new Vector2(playerX(), playerY())), 200, 10, "Red", 12, false);
                while(Loop(120))
                {
                    CreateBullet01(posX, posY, 1.4f + speedMod, MathFunctions.getAngleTo(new Vector2(posX, posY), new Vector2(playerX(), playerY())) + angle + 360f/240f, "Blue", 11, false,4);
                    angle += 360f / 120f*angleMod;
                    speedMod += 0.2f;
                    if (speedMod >= 1)
                        speedMod = 0;
                }
                posMod *= -1;
                angleMod *= -1;
                frame = -20;
                
            }
            if(frame2 == 100)
            {
                MoveRandom(100, 50, 432 - 100, 130, 30, 50, 0.05f);
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
