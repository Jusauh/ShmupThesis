using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BulletCurtainChallenges
{
    class PlayerBullet
    {
        Vector2 position;
        bool alive = true;
        public PlayerBullet(Vector2 position)
        {
            this.position = position;
        }

        public void Update()
        {
            position.Y -= 35;
            if (position.Y < -100)
                alive = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Assets.playerSheet, position+ CommonData.drawOffset, new Rectangle(64,0,64,16), Color.White*0.5f, -MathHelper.PiOver2, new Vector2(50, 8), 1, SpriteEffects.None, 0);
        }
        public bool getAlive()
        {
            return alive;
        }
        public void setAlive(bool value)
        {
            alive = value;
            FXManager.CreateFX(Assets.playerSheet, new Rectangle(81, 17, 47, 47), new Vector2(47f / 2, 47f / 2), Color.White*0.5f, this.position + CommonData.drawOffset, false, -90, 0, 3, -0.5f, 0.7f, 0.05f, 7);
        }
        public Vector2 getPosition()
        {
            return position;
        }
    }
}
