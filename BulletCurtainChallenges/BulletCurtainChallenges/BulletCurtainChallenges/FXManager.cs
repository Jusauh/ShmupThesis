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
    class FXManager
    {
        public static List<FX> FXList = new List<FX>();

        public static void CreateFX(Texture2D texture, Rectangle crop, Vector2 origin, Vector2 position, float angle, float speed, float scale, int lifetime)
        {
            FXList.Add(new FX( texture,  crop,  origin,  position,  angle,  speed,  scale,  lifetime));
        }
        public static void CreateFX(Texture2D texture, Rectangle crop, Vector2 origin, Color color, Vector2 position, bool additive, float angle, float rotation, float speed, float acceleration, float scale, float scaleMod, int lifetime)
        {
            FXList.Add(new FX(texture, crop, origin, color, position, additive, angle, rotation, speed, acceleration, scale, scaleMod, lifetime));
        }

        public static void Clear()
        {
            FXList.Clear();
        }

        public static void Update()
        {
            for (int i = FXList.Count() - 1; i >= 0; i--)
            {
                FXList[i].Update();
                if (FXList[i].IsAlive() == false)
                {
                    FXList.RemoveAt(i);
                }
            }
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int i = FXList.Count() - 1; i >= 0; i--)
            {
                FXList[i].Draw(spriteBatch);
            }
        }
    }
}
