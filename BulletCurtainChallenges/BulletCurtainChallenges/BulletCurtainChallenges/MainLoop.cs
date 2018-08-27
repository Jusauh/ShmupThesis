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
    class MainLoop
    {
        Scene testScene = new GameScene(-1);
        bool changeScene;
        Scene newScene;
        public MainLoop()
        {
            ScoreManager.ReadScores();
        }
        public void Update()
        {
            Input.UpdateState();
            if(CommonData.scene != -1)
            {
                UpdateScene(CommonData.scene);
            }
            testScene.Update(this);
            ManageScene();
        }

        public void Draw(SpriteBatch sb)
        {
            testScene.Draw(sb);
        }

        private void UpdateScene(int scene)
        {
            ChangeScene(new GameScene(scene));
            CommonData.scene = -1;
        }

        public void ChangeScene(Scene newScene)
        {
            changeScene = true;
            this.newScene = newScene;
        }

        protected void ManageScene()
        {
            if(changeScene == true)
            {
                testScene = newScene;
                changeScene = false;
                FXManager.Clear();

            }
        }
    }
}
