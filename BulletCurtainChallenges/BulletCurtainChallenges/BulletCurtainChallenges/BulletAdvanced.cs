using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletCurtainChallenges
{
    class BulletAdvanced : BulletBase
    {
        List<BulletAdvanceData> data = new List<BulletAdvanceData>();
        int dataSlot = 0;
        public BulletAdvanced(float positionX, float positionY, string color, int bullet, bool invert, int delay)
        {
            this.position = new Vector2(positionX, positionY);
            this.delay = -delay;
            SetSprite(color, bullet, invert);
        }
        public override void Update(PlayerCharacter player)
        {
            CheckStorage();
            base.Update(player);
        }
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
        protected void CheckStorage()
        {
            if(data[dataSlot].delay == this.delay)
            {
                if(data[dataSlot].keepSpeed == false)
                    this.speed = data[dataSlot].speed;
                this.acceleration = data[dataSlot].acceleration;
                this.maxSpeed = data[dataSlot].maxSpeed;
                if(data[dataSlot].keepAngle == false)
                    this.angle = MathHelper.ToRadians(data[dataSlot].angle);
                this.rotation = MathHelper.ToRadians(data[dataSlot].rotation);
                SetSprite(data[dataSlot].color, data[dataSlot].bullet, data[dataSlot].invert);
                if (dataSlot != data.Count - 1)
                dataSlot += 1;
            }
        }
        public void AddBulletData(int delay, float speed, float acceleration, float maxSpeed, float angle, float rotation, string color, int bullet, bool invert)
        {
            data.Add(new BulletAdvanceData(delay, speed, acceleration, maxSpeed, angle, rotation, color, bullet, invert));
        }
        public void AddBulletData(int delay, bool speed, float acceleration, float maxSpeed, float angle, float rotation, string color, int bullet, bool invert)
        {
            data.Add(new BulletAdvanceData(delay, speed, acceleration, maxSpeed, angle, rotation, color, bullet, invert));
        }
        public void AddBulletData(int delay, float speed, float acceleration, float maxSpeed, bool angle, float rotation, string color, int bullet, bool invert)
        {
            data.Add(new BulletAdvanceData(delay, speed, acceleration, maxSpeed, angle, rotation, color, bullet, invert));
        }
        public void AddBulletData(int delay, bool speed, float acceleration, float maxSpeed, bool angle, float rotation, string color, int bullet, bool invert)
        {
            data.Add(new BulletAdvanceData(delay, speed, acceleration, maxSpeed, angle, rotation, color, bullet, invert));
        }
    }
}
