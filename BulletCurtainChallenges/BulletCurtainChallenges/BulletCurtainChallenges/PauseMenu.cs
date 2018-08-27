using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BulletCurtainChallenges
{
    class PauseMenu
    {
        int cursor = 1;
        int stage = CommonData.cursorPosition;
        float alpha = 0, alpahMod= -1;
        bool active = false;

        public void Update()
        {
            alpha += 0.04f * alpahMod;

            if (alpha < 0)
            {
                alpha = 0;
                active = false;
            }
            else if (alpha > 0.8f)
                alpha = 0.8f;

            if (alpha == 0.8f)
            {
                if (Input.IsKeyPressed(Keys.Left))
                {
                    if (cursor == 0)
                        changeStage(-1);
                }
                else if (Input.IsKeyPressed(Keys.Right))
                {
                    if (cursor == 0)
                        changeStage(1);
                }
                if (Input.IsKeyPressed(Keys.Up))
                {
                    MoveCursor(-1);
                }
                if (Input.IsKeyPressed(Keys.Down))
                {
                    MoveCursor(1);
                }
                if (Input.IsKeyPressed(Keys.Z))
                {
                    Execute();
                }
                if(Input.IsKeyPressed(Keys.Delete))
                {
                    ScoreManager.clearFile();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Assets.menuBG, new Vector2(CommonData.minX, CommonData.minY), new Color(255f, 255f, 255f, alpha));
            float xMod = -400 + (500 * alpha);
            spriteBatch.Draw(Assets.menuElements, new Vector2(xMod+CommonData.minX + 36, CommonData.minY + 140), new Rectangle(27, 0, 112, 92), Color.White);
            spriteBatch.Draw(Assets.menuElements, new Vector2(xMod+CommonData.minX + 8, CommonData.minY + 142 + 33*cursor), new Rectangle(0, 0, 24, 19), Color.White);
            spriteBatch.Draw(Assets.menuElements, new Vector2(xMod+CommonData.minX + 110, CommonData.minY + 140), new Rectangle(4, 33 + (int)(32.6f*stage), 18, 25), Color.White);
            if(ScoreManager.getScore(stage) > 0)
            {
                spriteBatch.Draw(Assets.menuElements, new Vector2(xMod + CommonData.minX + 130, CommonData.minY + 140), new Rectangle(28 + 17 * ScoreManager.getScore(stage) - 17, 98, 17, 23), Color.White);
            }
        }

        void MoveCursor(int value)
        {
            cursor += value;
            if(cursor == -1)
            {
                cursor = 2;
            }
            else if(cursor == 3)
            {
                cursor = 0;
            }
        }

        void changeStage(int value)
        {
            stage += value;
            if (stage == -1)
            {
                stage = 8;
            }
            else if (stage == 9)
            {
                stage = 0;
            }
        }
        public void ToggleActive()
        {
            if (alpha == 0 || alpha == 0.8f)
            {
                alpahMod *= -1;
                if (active == false)
                    active = true;
            }
        }
        void Execute()
        {
            if(cursor == 1)
            {
                CommonData.scene = stage;
                CommonData.cursorPosition = stage;
            }
            else if(cursor == 2)
            {
                CommonData.exitGame = true;
            }
        }
        public bool GetActive()
        {
            return active;
        }
    }
    
}
