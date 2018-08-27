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
    class Enemy4 : Enemy
    {
        int frame,frame2,frame3;
        float angle,angle2, speed = 0, posX, posY, rad=0;
        float deathScale = 0.4f, deathAlpha = 0.5f;
        public Enemy4(GameScene scene) : base(scene)
        {
            health = 700;
            maxHealth = health;
            SetPosition(432 / 2, 140);
            angle = 0;
            rad = 0;
            speed = 2.2f;
        }
        public override void Update()
        {
            frame++;

            if (frame == 60)
            {
                for (int i = 0; i < 10+(int)(rad/20); i++)
                {
                    angle = Rand(0, 360);
                    CreateBulletA(position.X + (float)Math.Cos(MathHelper.ToDegrees( angle)) * rad + Rand(-2, 2), position.Y+ (float)Math.Sin(MathHelper.ToDegrees(angle)) * rad + Rand(-2, 2), "Red",32,false,60);
                    SetBulletDataA(0, 0.05f, 0, 0, Rand(0, 360), 0, "Red",32,false);
                    BulletLife(60);
                    Shoot();
                }
                frame = 59;
                rad += 5;

                if (rad > 500)
                {
                    rad = 0;
                    frame = 40;
                }
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
