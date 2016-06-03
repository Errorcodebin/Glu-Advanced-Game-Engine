using GameEngine.Core;

namespace GameEngine
{
    static class Screen
    {
        static private Window m_window = GameEngine.GetInstance().window;

        public static int width
        {
            get { return m_window.GetWidth(); }
        }
        public static int height
        {
            get { return m_window.GetHeight(); }
        }

        public static Vector2 center
        {
            get { return new Vector2(width / 2, height / 2); }
        }
    }
}
