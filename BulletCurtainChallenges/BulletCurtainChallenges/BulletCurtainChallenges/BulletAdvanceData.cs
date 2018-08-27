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
    class BulletAdvanceData
    {
        public int delay;
        public float speed, acceleration, maxSpeed, angle, rotation;
        public bool invert,keepAngle = false, keepSpeed = false;
        public int bullet;
        public string color;

        public BulletAdvanceData(int delay, float speed, float acceleration,float maxSpeed,float angle,float rotation, string color, int bullet, bool invert)
        {
            this.delay = delay;
            this.speed = speed;
            this.acceleration = acceleration;
            this.maxSpeed = maxSpeed;
            this.angle = angle;
            this.rotation = rotation;
            this.color = color;
            this.bullet = bullet;
            this.invert = invert;
        }
        public BulletAdvanceData(int delay, bool speed, float acceleration, float maxSpeed, float angle, float rotation, string color, int bullet, bool invert)
        {
            this.delay = delay;
            this.keepSpeed = speed;
            this.acceleration = acceleration;
            this.maxSpeed = maxSpeed;
            this.angle = angle;
            this.rotation = rotation;
            this.color = color;
            this.bullet = bullet;
            this.invert = invert;
        }
        public BulletAdvanceData(int delay, float speed, float acceleration, float maxSpeed, bool angle, float rotation, string color, int bullet, bool invert)
        {
            this.delay = delay;
            this.speed = speed;
            this.acceleration = acceleration;
            this.maxSpeed = maxSpeed;
            this.keepAngle = angle;
            this.rotation = rotation;
            this.color = color;
            this.bullet = bullet;
            this.invert = invert;
        }
        public BulletAdvanceData(int delay, bool speed, float acceleration, float maxSpeed, bool angle, float rotation, string color, int bullet, bool invert)
        {
            this.delay = delay;
            this.keepSpeed = speed;
            this.acceleration = acceleration;
            this.maxSpeed = maxSpeed;
            this.keepAngle = angle;
            this.rotation = rotation;
            this.color = color;
            this.bullet = bullet;
            this.invert = invert;
        }
    }
}
