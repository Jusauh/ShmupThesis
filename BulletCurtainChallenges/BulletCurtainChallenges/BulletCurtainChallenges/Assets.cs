using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace BulletCurtainChallenges
{
    class Assets
    {
        public static Texture2D bulletSheet;
        public static Texture2D playerSheet;
        public static Texture2D bulletSheet2;
        public static Texture2D hud;
        public static Texture2D menuBG;
        public static Texture2D menuElements;
        public static Texture2D enemyExplosion;
        public static Texture2D BG;

        public static void LoadAssets(ContentManager content)
        {
            bulletSheet = content.Load<Texture2D>("BulletSheet_WIP");
            bulletSheet2 = content.Load<Texture2D>("BulletSheet2");
            playerSheet = content.Load<Texture2D>("PlayerSheet");
            hud = content.Load<Texture2D>("Hud_WIP");
            menuBG = content.Load<Texture2D>("MenuBG");
            menuElements = content.Load<Texture2D>("MenuElements");
            enemyExplosion = content.Load<Texture2D>("enemyExplosion");
            BG = content.Load<Texture2D>("bg");
        }
    }
}
