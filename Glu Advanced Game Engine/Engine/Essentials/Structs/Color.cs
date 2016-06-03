using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public struct Color
    {
        public static Color White
        {
            get { return new Color(255, 255, 255); }
        }
        public static Color Black
        {
            get { return new Color(0, 0, 0); }
        }
        public static Color Red
        {
            get { return new Color(255, 0, 0); }
        }
        public static Color Green
        {
            get { return new Color(0, 255, 0); }
        }
        public static Color Blue
        {
            get { return new Color(0, 0, 255); }
        }
        public static Color Gray
        {
            get { return new Color(128, 128, 128); }
        }

        private int m_R;
        public int R
        {
            get { return m_R; }
            set { m_R = value; }
        }

        private int m_G;
        public int G
        {
            get { return m_G; }
            set { m_G = value; }
        }

        private int m_B;
        public int B
        {
            get { return m_B; }
            set { m_B = value; }
        }

        public Color(int r, int g, int b)
        {
            m_R = r;
            m_G = g;
            m_B = b;
        }

        // Shamelessly borrowed from StackOverflow
        // and edited to work seamlessly with the rest.
        public static Color HsvToColor(float hue, float saturation, float value)
        {
            float H = hue;

            if (H < 0) { H = -(Math.Abs(H) % 360); };
            if (H >= 360) { H = H % 360; };

            float R, G, B;

            // If we have no value or negative value,
            // Preset it to black.
            if (value <= 0)
            {
                return Black;
            }
            else if (saturation <= 0)
            {
                return new Color((int)value, (int)value, (int)value);
            }
            else
            {
                float hf = H / 60.0f;
                int i = (int)Math.Floor(hf);
                float f = hf - i;
                float pv = value * (1 - saturation);
                float qv = value * (1 - saturation * f);
                float tv = value * (1 - saturation * (1 - f));
                switch (i)
                {

                    // Red is the dominant color
                    case 0:
                        R = value;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color
                    case 1:
                        R = qv;
                        G = value;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = value;
                        B = tv;
                        break;

                    // Blue is the dominant color
                    case 3:
                        R = pv;
                        G = qv;
                        B = value;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = value;
                        break;

                    // Red is the dominant color
                    case 5:
                        R = value;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here.
                    // Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = value;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = value;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        R = G = B = value;
                        break;
                }
            }
            int r = Clamp((int)(R * 255.0));
            int g = Clamp((int)(G * 255.0));
            int b = Clamp((int)(B * 255.0));
            return new Color(r, g, b);
        }

        private static int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }
    }
}
