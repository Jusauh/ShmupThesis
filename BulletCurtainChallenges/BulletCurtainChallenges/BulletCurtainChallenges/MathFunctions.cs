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
    class MathFunctions
    {
        static Random rand = new Random();

        public static float getDistanceTo(Vector2 position1, Vector2 position2)
        {
            {
                return Math.Abs(Vector2.Distance(position1,position2));
            }
        }
        public static float getAngleTo(Vector2 fromPosition, Vector2 toPosition)
        {
            double angle = Math.Atan2(toPosition.Y - fromPosition.Y, toPosition.X - fromPosition.X);
            return MathHelper.ToDegrees((float)angle);
        }
        public static float Rand(float minValue, float maxValue)
        {
            return minValue + (float)(rand.NextDouble() * (maxValue - minValue));
        }
    }
}
