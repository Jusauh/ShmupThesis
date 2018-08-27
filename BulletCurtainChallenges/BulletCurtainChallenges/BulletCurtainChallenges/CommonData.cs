using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BulletCurtainChallenges
{
    class CommonData
    {
        public static float minX = (800 - 432) / 2;
        public static float maxX = ((800 - 432) / 2) + 432;
        public static float minY = (600 - 576) / 2;
        public static float maxY = ((600 - 576) / 2) + 576;

        public static float playerMinX = 10;
        public static float playerMaxX = 432-10;
        public static float playerMinY = 10;
        public static float playerMaxY = 576 - 10;

        public static float centerX = minX + 432 / 2;
        public static float centerY = minY + 576 / 2;

        public static float playerPositionX;
        public static float playerPositionY;

        public static bool exitGame = false;

        public static int scene = -1;
        public static int cursorPosition = 0;

        public static Vector2 drawOffset = new Vector2(((800 - 432) / 2),((600 - 576) / 2));

        public static void UpdateData(float playerPosX,float playerPosY)
        {
            playerPositionX = playerPosX;
            playerPositionY = playerPosY;
        }
    }
}
