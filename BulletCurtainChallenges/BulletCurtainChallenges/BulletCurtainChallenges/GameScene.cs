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
    class GameScene : Scene
    {
        int bombs = 2, shootInterval, endTimer = 60,scene;
        bool canShoot = false;
        float flashAlpha;
        public List<BulletBase> bulletList = new List<BulletBase>();
        public List<PlayerBullet> playerBulletList = new List<PlayerBullet>();
        PlayerCharacter player = new PlayerCharacter();
        Enemy enemy;
        PauseMenu pauseMenu = new PauseMenu();

        public GameScene(int scene)
        {
            if (scene == -1)
            {
                endTimer = 0;
                enemy = new EnemyBlank(this);
            }
            else if (scene == 0) 
                enemy = new TestEnemy(this);
            else if(scene == 1)
                enemy = new Enemy1(this);
            else if (scene == 2)
                enemy = new Enemy2(this);
            else if (scene == 3)
                enemy = new Enemy3(this);
            else if (scene == 4)
                enemy = new Enemy4(this);
            else if (scene == 5)
                enemy = new Enemy5(this);
            else if (scene == 6)
                enemy = new TestEnemy(this);
            else if (scene == 7)
                enemy = new TestEnemy(this);
            else if (scene == 8)
                enemy = new TestEnemy(this);
            this.scene = scene;

        }

        public override void Update(MainLoop mainLoop)
        {
            if (Input.IsKeyPressed(Keys.Escape))
            {
                if(enemy.getAlive() == true && player.getAlive() == true)
                pauseMenu.ToggleActive();
            }

            if (pauseMenu.GetActive() == false)
            {
                if (Input.IsKeyPressed(Keys.Z))
                {
                    canShoot = true;
                }

                shootInterval--;

                if (enemy.getAlive() == false || player.getAlive() == false)
                {
                    endTimer--;
                    if (player.getAlive() == false)
                        playerBulletList.Clear();
                    if (enemy.getAlive() == false)
                        player.resetHit();
                }
                //UPDATES
                if(enemy.getAlive() == true)
                    enemy.Update();
                if(player.getAlive() == true)
                    player.Update();
                FXManager.Update();
                CommonData.UpdateData(player.getPosition().X, player.getPosition().Y);
                for (int i = bulletList.Count() - 1; i >= 0; i--)
                {
                    BulletBase bullet = bulletList[i];
                    bullet.Update(player);
                    if (enemy.getAlive() == false)
                        bullet.setAlive(false);
                    if (bullet.getAlive() == false)
                    {
                        bulletList.Remove(bullet);
                    }
                }
                for (int i = playerBulletList.Count() - 1; i >= 0; i--)
                {
                    PlayerBullet bullet = playerBulletList[i];
                    bullet.Update();
                    if (bullet.getAlive() == false)
                    {
                        playerBulletList.Remove(bullet);
                    }
                }
                //INPUTS
                if (Input.IsKeyPressed(Keys.X) && bombs > 0 && player.getAlive() == true)
                {
                    ClearBullets();
                    player.resetHit();
                    flashAlpha = 1;
                    bombs--;
                }
                if (Input.IsKeyDown(Keys.Z) && shootInterval <= 0 && canShoot == true && player.getAlive() == true)
                {
                    playerBulletList.Add(new PlayerBullet(new Vector2(player.getPosition().X - 27, player.getPosition().Y - 10)));
                    playerBulletList.Add(new PlayerBullet(new Vector2(player.getPosition().X - 9, player.getPosition().Y - 30)));
                    playerBulletList.Add(new PlayerBullet(new Vector2(player.getPosition().X + 9, player.getPosition().Y - 30)));
                    playerBulletList.Add(new PlayerBullet(new Vector2(player.getPosition().X + 27, player.getPosition().Y - 10)));
                    shootInterval = 5;
                }
                //ENDING
                if (endTimer == 0)
                {
                    if (player.getAlive() == true)
                        ScoreManager.SetScore(scene, bombs + 1);
                    pauseMenu.ToggleActive();
                }

                Console.WriteLine(bulletList.Count);
            }
            else
            {
                //PAUSEMENU
                pauseMenu.Update();
            }
            base.Update(mainLoop);
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Assets.BG, CommonData.drawOffset, Color.White);
            if(flashAlpha > 0)
            {
                flashAlpha -= 0.05f;
                sb.Draw(Assets.enemyExplosion,new Vector2(400,300),new Rectangle(0,0,128,128),Color.White*flashAlpha,0, new Vector2(64, 64),10,SpriteEffects.None,0);
            }
            player.Draw(sb);
            enemy.Draw(sb);
            FXManager.Draw(sb);
            for (int i = playerBulletList.Count() - 1; i >= 0; i--)
            {
                PlayerBullet bullet = playerBulletList[i];
                bullet.Draw(sb);
            }
            sb.End();

            sb.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            for (int i = bulletList.Count() - 1; i >= 0; i--)
            {
                BulletBase bullet = bulletList[i];
                if (bullet.getAdditive() == true)
                    bullet.Draw(sb);
            }
            sb.End();

            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            for (int i = bulletList.Count() - 1; i >= 0; i--)
            {
                BulletBase bullet = bulletList[i];
                if(bullet.getAdditive() == false)
                bullet.Draw(sb);
            }

            if (enemy.getAlive() == true)
            {
                float posX = enemy.getPosition().X - 45f / 2;
                if (posX < 45f / 2)
                    posX = 45f / 2;
                else if (posX > 432 - 45f / 2)
                    posX = 432 - 45f / 2;
                sb.Draw(Assets.menuElements, new Vector2(posX + CommonData.drawOffset.X, CommonData.maxY - 16), new Rectangle(99, 310, 45, 16), Color.White);
                int width = (int)((432 - 20) * enemy.getHealthPercent());
                sb.Draw(Assets.menuElements, new Rectangle(10 + (int)CommonData.drawOffset.X, 5 + (int)CommonData.drawOffset.Y,width , 8), new Rectangle(0, 325, 0, 0), Color.LightGray);
            }
            if(player.getAlive())
                sb.Draw(Assets.menuElements, player.getPosition()+CommonData.drawOffset, new Rectangle(88, 317, 9, 9), Color.White, 0, new Vector2(4.5f, 4.5f), 1, SpriteEffects.None, 0);
            if (pauseMenu.GetActive() == true)
                pauseMenu.Draw(sb);

            sb.Draw(Assets.hud, new Vector2(0, 0), Color.White);
            for(int i = 0; i < bombs; i++)
            {
                sb.Draw(Assets.menuElements, new Vector2(140, 100 + 40 * i), new Rectangle(81, 98, 35, 35), Color.White);
            }


            base.Draw(sb);
        }

        protected void ClearBullets()
        {
            for (int i = bulletList.Count() - 1; i >= 0; i--)
            {
                BulletBase bullet = bulletList[i];
                bullet.setAlive(false);
                if (bullet.getAlive() == false)
                {
                    bulletList.Remove(bullet);
                }
            }
        }
    }
}
