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
using System.IO;

namespace BulletCurtainChallenges
{
    class ScoreManager
    {
        static int[] scores = new int[9]{0,0,0,0,0,0,0,0,0};

        public static void ReadScores()
        {
            if (System.IO.File.Exists("..\\Scores.txt"))
            {
                string data = System.IO.File.ReadAllText("..\\Scores.txt");
                for (int i = 0; i < scores.Length; i++)
                {
                    scores[i] = (int)Char.GetNumericValue(data[i]);
                }
            }
        }
        public static int getScore(int scene)
        {
            return scores[scene];
        }
        public static void SetScore(int scene, int amount)
        {
            if (scene != -1 && amount > scores[scene])
            {
                scores[scene] = amount;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter("..\\Scores.txt"))
                {
                    foreach (int score in scores)
                    {
                        file.Write(score);
                    }
                }
            }
        }

        public static void clearFile()
        {
            if (System.IO.File.Exists("..\\Scores.txt"))
            {
                // Use a try block to catch IOExceptions, to
                // handle the case of the file already being
                // opened by another process.
                try
                {
                    System.IO.File.Delete("..\\Scores.txt");
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }
        }

    }
}
