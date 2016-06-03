using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public struct Rectangle
    {
        private int m_X;
        public int X
        {
            get { return m_X; }
            set { m_X = value; }
        }

        private int m_Y;
        public int Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }

        private int m_Width;
        public int Width
        {
            get { return m_Width; }
            set { m_Width = value; }
        }

        private int m_Height;
        public int Height
        {
            get { return m_Height; }
            set { m_Height = value; }
        }

        public Rectangle(int x, int y, int width, int height)
        {
            m_X = x;
            m_Y = y;
            m_Width = width;
            m_Height = height;
        }
    }
}
