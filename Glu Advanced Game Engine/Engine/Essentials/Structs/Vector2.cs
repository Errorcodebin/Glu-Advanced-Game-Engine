using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public struct Vector2
    {
        public float length
        {
            get { return (float)Math.Sqrt((X - X) + (Y - Y)); }
        }

        public static Vector2 zero
        {
            get { return new Vector2(0.0f, 0.0f); }
        }

        private float m_X;
        public float X
        {
            get { return m_X; }
            set { m_X = value; }
        }

        private float m_Y;
        public float Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }

        public Vector2(float x, float y)
        {
            m_X = x;
            m_Y = y;
        }

        public static float Angle(Vector2 from, Vector2 to)
        {
            Vector2 dir = to - from;
            return (float)Math.Atan2(dir.X, dir.Y);
        }

        public static float Distance(Vector2 from, Vector2 to)
        {
            return (float)Math.Sqrt(Math.Pow(from.X - to.X, 2) + Math.Pow(from.Y - to.Y, 2));
        }

        public void Normalize()
        {
            float l = length;

            if (l == 0)
            {
                Debug.LogWarning("Vector can't be normalized, Vector has length 0");
                return;
            }

            X /= l;
            Y /= l;
        }

        public static Vector2 Normalize(Vector2 vector)
        {
            float x = vector.X / vector.length;
            float y = vector.Y / vector.length;
            return new Vector2(x, y);
        }

        public static Vector2 operator +(Vector2 vec1, Vector2 vec2)
        {
            return new Vector2(vec1.X + vec2.X, vec1.Y + vec2.Y);
        }

        public static Vector2 operator -(Vector2 vec1, Vector2 vec2)
        {
            return new Vector2(vec1.X - vec2.X, vec1.Y - vec2.Y);
        }
    }
}
