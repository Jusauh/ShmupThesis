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
    class Enemy
    {
        GameScene currentScene;
        BulletAdvanced tempBullet;
        protected Vector2 position, movePosition;
        int loops, moveDuration, bulletHitBox = 40;
        bool loopEnd = true, alive = true;
        float moveX, moveY, moveDistance, moveAcceleration, moveSpeed, moveAngle;
        protected int health, maxHealth;
        public Enemy(GameScene scene)
        {
            currentScene = scene;
        }
        public virtual void Update()
        {
            if (health <= 0)
                alive = false;
            UpdateMovement();
            CheckCollision();
        }
        public virtual void Draw(SpriteBatch sb)
        {

        }
        public void CheckCollision()
        {
            for (int i = currentScene.playerBulletList.Count() - 1; i >= 0; i--)
            {
                if (MathFunctions.getDistanceTo(this.position, currentScene.playerBulletList[i].getPosition()) <= bulletHitBox)
                {
                    currentScene.playerBulletList[i].setAlive(false);
                    health--;
                }
            }
        }
        public void UpdateMovement()
        {
            if (moveDuration > 0)
            {
                Vector2 velocity = new Vector2((float)Math.Cos(MathHelper.ToRadians(moveAngle)) * moveSpeed, (float)Math.Sin(MathHelper.ToRadians(moveAngle)) * moveSpeed);
                position += velocity;
                //Console.WriteLine(moveSpeed);
                moveSpeed -= moveAcceleration;
                moveDuration--;
            }
            if (moveDuration == 0)
            {
                //Console.WriteLine(position);
                //position = movePosition;
                //Console.WriteLine(position);
                moveDuration--;
            }
        }
        public void SetPosition(float posX, float posY)
        {
            position.X = posX;
            position.Y = posY;
        }
        //Common Bullet functions
        public void CreateBullet01(float posX, float posY, float speed, float angle, string color, int shape, bool invert, int delay)
        {
            currentScene.bulletList.Add(new Bullet(posX, posY, speed, angle, delay, color, shape, invert));
        }
        public void CreateBullet02(float positionX, float positionY, float speed, float acceleration, float maxSpeed, float angle, float turn, string color, int shape, bool invert, int delay)
        {
            currentScene.bulletList.Add(new Bullet(positionX, positionY, speed, acceleration, maxSpeed, angle, turn, delay, color, shape, invert));
        }
        public void CreateBulletXY(float positionX, float positionY, float Xspeed, float Xacceleration, float XMaxSpeed, float Yspeed, float Yacceleration, float YmaxSpeed, string color, int shape, bool invert, int delay)
        {
            currentScene.bulletList.Add(new Bullet(positionX, positionY, Xspeed, Xacceleration, XMaxSpeed, Yspeed, Yacceleration, YmaxSpeed, delay, color, shape, invert));
        }
        public void CreateBulletA(float positionX, float positionY, string color, int bullet, bool invert, int delay)
        {
            tempBullet = new BulletAdvanced(positionX, positionY, color, bullet, invert, delay);
        }
        public void SetBulletDataA(int delay, float speed, float acceleration, float maxSpeed, float angle, float rotation, string color, int bullet, bool invert)
        {
            tempBullet.AddBulletData(delay, speed, acceleration, maxSpeed, angle, rotation, color, bullet, invert);
        }
        public void SetBulletDataA(int delay, bool speed, float acceleration, float maxSpeed, float angle, float rotation, string color, int bullet, bool invert)
        {
            tempBullet.AddBulletData(delay, speed, acceleration, maxSpeed, angle, rotation, color, bullet, invert);
        }
        public void SetBulletDataA(int delay, float speed, float acceleration, float maxSpeed, bool angle, float rotation, string color, int bullet, bool invert)
        {
            tempBullet.AddBulletData(delay, speed, acceleration, maxSpeed, angle, rotation, color, bullet, invert);
        }
        public void SetBulletDataA(int delay, bool speed, float acceleration, float maxSpeed, bool angle, float rotation, string color, int bullet, bool invert)
        {
            tempBullet.AddBulletData(delay, speed, acceleration, maxSpeed, angle, rotation, color, bullet, invert);
        }
        public void Shoot()
        {
            currentScene.bulletList.Add(tempBullet);
        }
        public void CreateLaser01(float posX, float posY, float speed, float angle, float length, float width, string color, int bullet, bool invert)
        {
            currentScene.bulletList.Add(new BulletLazer(posX, posY, speed, angle, length, width, color, bullet, invert));
        }
        public void BulletLife(int amount)
        {
            tempBullet.SetLifetime(amount);
        }
        public void MoveTo(float posX, float posY, int duration)
        {
            moveDistance = MathFunctions.getDistanceTo(this.position, new Vector2(posX, posY));
            moveAcceleration = 0;
            moveSpeed = (float)(moveDistance / duration);
            moveDuration = duration;
            moveAngle = MathFunctions.getAngleTo(this.position, new Vector2(posX, posY));
            movePosition = new Vector2(posX, posY);

        }
        public void MoveTo2(float posX, float posY, float acceleration)
        {
            moveDuration = 0;
            moveSpeed = 0;
            float tempDistance = 0;
            moveDistance = MathFunctions.getDistanceTo(this.position, new Vector2(posX, posY));
            moveAcceleration = acceleration;
            while (tempDistance < moveDistance)
            {
                moveSpeed += acceleration;
                tempDistance += moveSpeed;
                moveDuration += 1;
            }
            tempDistance -= moveDistance;
            moveAngle = MathFunctions.getAngleTo(this.position, new Vector2(posX, posY));
            position -= new Vector2((float)Math.Cos(MathHelper.ToRadians(moveAngle)) * tempDistance, (float)Math.Sin(MathHelper.ToRadians(moveAngle)) * tempDistance);
            movePosition = new Vector2(posX, posY);
        }
        public void MoveRandom(float minX, float minY, float maxX, float maxY, float minDistance, float maxDistance, float acceleration)
        {
            float angle = Rand(0, MathHelper.Pi * 2);
            float distance = Rand(minDistance, maxDistance);
            Vector2 movePosition = new Vector2(position.X + distance * (float)Math.Cos(angle), position.Y + distance * (float)Math.Sin(angle));
            while (movePosition.X < minX || movePosition.X > maxX || movePosition.Y < minY || movePosition.Y > maxY)
            {
                angle = Rand(0, MathHelper.Pi * 2);
                distance = Rand(minDistance, maxDistance);
                movePosition = new Vector2(position.X + distance * (float)Math.Cos(angle), position.Y + distance * (float)Math.Sin(angle));
            }
            MoveTo2(movePosition.X, movePosition.Y, acceleration);
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
        protected bool Loop(int amount)
        {
            if (loopEnd == true)
            {
                loops = amount;
                loopEnd = false;
            }
            if (loops > 0)
            {
                loops--;
                return true;
            }
            else
            {
                loopEnd = true;
                return false;
            }
        }
        public bool getAlive()
        { return alive; }
        protected float Rand(float min, float max)
        {
            return MathFunctions.Rand(min, max);
        }
        public Vector2 getPosition()
        { return position; }
        public float getHealthPercent()
        { return (float)health / (float)maxHealth; }
    }
}
