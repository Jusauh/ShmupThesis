using Microsoft.Xna.Framework.Input;

namespace BulletCurtainChallenges
{
    class Input
    {
        static KeyboardState keyState, keyStateOld;


        public static void UpdateState()
        {
            keyStateOld = keyState;
            keyState = Keyboard.GetState();
        }

        public static bool IsKeyDown(Keys key)
        {
            return keyState.IsKeyDown(key);
        }

        public static bool IsKeyUp(Keys key)
        {
            return !keyState.IsKeyDown(key);
        }

        public static bool IsKeyPressed(Keys key)
        {
            return keyState.IsKeyDown(key) && keyStateOld.IsKeyUp(key);
        }

        public static bool IsKeyReleased(Keys key)
        {
            return keyState.IsKeyUp(key) && keyStateOld.IsKeyDown(key);
        }
    }
}
