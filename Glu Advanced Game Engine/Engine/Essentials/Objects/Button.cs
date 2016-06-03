using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Button : BaseBehavior
    {
        public delegate void ButtonCallback();

        //State
        private Font m_DefaultFont;
        private Font m_Font;
        private Bitmap m_Bitmap; //Instead of colors we can also use a bitmap as the background

        private bool m_IsHovering;
        private bool m_IsClicked;

        //Required
        private string m_Text = "Button";
        private Rectangle m_Rectangle = new Rectangle(0, 0, 100, 25);
        private ButtonCallback m_Callback;

        //Extra settings
        private Color m_ForegroundColor = Color.Black;
        private Color m_BackgroundColor = Color.White;
        private Color m_BorderColor = Color.Black;

        private Color m_HoverForegroundColor = Color.Black;
        private Color m_HoverBackgroundColor = new Color(245, 245, 245);
        private Color m_HoverBorderColor = Color.Black;

        private Color m_ClickForegroundColor = Color.Black;
        private Color m_ClickBackgroundColor = new Color(200, 200, 200);
        private Color m_ClickBorderColor = Color.Black;

        private Vector2 m_CornerRadius = new Vector2(0, 0);


        //Functions
        public Button(ButtonCallback callback) : base()
        {
            Initialize(callback, "Button", new Rectangle(0, 0, 100, 25));
        }

        public Button(ButtonCallback callback, string text, int x, int y, int width, int height) : base()
        {
            Initialize(callback, text, new Rectangle(x, y, width, height));
        }

        public Button(ButtonCallback callback, string text, Rectangle rectangle) : base()
        {
            Initialize(callback, text, rectangle);
        }

        private void Initialize(ButtonCallback callback, string text, Rectangle rectangle)
        {
            m_Text = text;
            m_Rectangle = rectangle;
            m_Callback = callback;

            m_DefaultFont = new Font("Arial", 12.0f);
            m_DefaultFont.SetHorizontalAlignment(Font.Alignment.Center);
            m_DefaultFont.SetVerticalAlignment(Font.Alignment.Center);

            m_Font = m_DefaultFont;
        }

        public override void GameEnd()
        {
            m_DefaultFont.Dispose();

            if (m_Bitmap != null)
                m_Bitmap.Dispose();

            GAME_ENGINE.UnsubscribeGameObject(this);
        }

        public override void Update()
        {
            //Check if the mouse position is colliding with our button
            Vector2 mousePosition = GAME_ENGINE.GetMousePosition();

            m_IsClicked = false;
            m_IsHovering = (!(mousePosition.X < m_Rectangle.X ||
                              mousePosition.X > (m_Rectangle.X + m_Rectangle.Width) ||
                              mousePosition.Y < m_Rectangle.Y ||
                              mousePosition.Y > (m_Rectangle.Y + m_Rectangle.Height)));

            if (m_IsHovering)
            {
                m_IsClicked = GAME_ENGINE.GetMouseButton(0);

                if (GAME_ENGINE.GetMouseButtonUp(0))
                {
                    if (m_Callback != null)
                        m_Callback();
                }
            }
        }

        public override void Paint()
        {
            Color fgColor = m_ForegroundColor;
            Color bgColor = m_BackgroundColor;
            Color borderColor = m_BorderColor;

            if (m_IsHovering)
            {
                fgColor = m_HoverForegroundColor;
                bgColor = m_HoverBackgroundColor;
                borderColor = m_HoverBorderColor;
            }

            if (m_IsClicked)
            {
                fgColor = m_ClickForegroundColor;
                bgColor = m_ClickBackgroundColor;
                borderColor = m_ClickBorderColor;
            }

            if (m_Bitmap != null)
            {
                DrawBitmapButton();
            }
            else
            {
                if (m_CornerRadius.X == 0 && m_CornerRadius.Y == 0)
                    DrawRectangleButton(bgColor, borderColor);
                else
                    DrawRoundedRectangleButton(bgColor, borderColor);
            }

            //Text
            Draw.SetColor(fgColor);
            Draw.Text(m_Font, m_Text, m_Rectangle);
        }

        private void DrawRectangleButton(Color backgroundColor, Color borderColor)
        {
            //Background
            Draw.SetColor(backgroundColor);
            Draw.FillRectangle(m_Rectangle);

            //Border
            Draw.SetColor(borderColor);
            Draw.Rectangle(m_Rectangle);
        }

        private void DrawRoundedRectangleButton(Color backgroundColor, Color borderColor)
        {
            //Background
            Draw.SetColor(backgroundColor);
            Draw.FillRoundedRectangle(m_Rectangle, m_CornerRadius);

            //Border
            Draw.SetColor(borderColor);
            Draw.RoundedRectangle(m_Rectangle, m_CornerRadius);
        }

        private void DrawBitmapButton()
        {
            int yOffset = 0;
            if (m_IsHovering) yOffset += m_Rectangle.Height;
            if (m_IsClicked) yOffset += m_Rectangle.Height;

            Draw.DrawBitmap(m_Bitmap, m_Rectangle.X, m_Rectangle.Y, 0, yOffset, m_Rectangle.Width, m_Rectangle.Height);
        }


        //Mutators & Accessors
        public void SetCallback(ButtonCallback callback)
        {
            m_Callback = callback;
        }

        public void SetText(string text)
        {
            m_Text = text;
        }

        public void SetPosition(int x, int y)
        {
            m_Rectangle = new Rectangle(x, y, m_Rectangle.Width, m_Rectangle.Height);
        }

        public void SetSize(int width, int height)
        {
            m_Rectangle = new Rectangle(m_Rectangle.X, m_Rectangle.Y, width, height);
        }

        public void SetRectangle(int x, int y, int width, int height)
        {
            m_Rectangle = new Rectangle(x, y, width, height);
        }

        public void SetRectangle(Rectangle rectangle)
        {
            m_Rectangle = rectangle;
        }


        public void SetForegroundColor(Color color)
        {
            m_ForegroundColor = color;
        }

        public void SetBackgroundColor(Color color)
        {
            m_BackgroundColor = color;
        }

        public void SetBorderColor(Color color)
        {
            m_BorderColor = color;
        }

        public void SetHoverForegroundColor(Color color)
        {
            m_HoverForegroundColor = color;
        }

        public void SetHoverBackgroundColor(Color color)
        {
            m_HoverBackgroundColor = color;
        }

        public void SetHoverBorderColor(Color color)
        {
            m_HoverBorderColor = color;
        }

        public void SetClickForegroundColor(Color color)
        {
            m_ClickForegroundColor = color;
        }

        public void SetClickBackgroundColor(Color color)
        {
            m_ClickBackgroundColor = color;
        }

        public void SetClickBorderColor(Color color)
        {
            m_ClickBorderColor = color;
        }


        public void SetBitmap(string filepath)
        {
            m_Bitmap = new Bitmap(filepath);
        }

        public void SetBitmap(Bitmap bitmap)
        {
            m_Bitmap = bitmap;
        }

        public void SetFont(Font font)
        {
            m_Font = font;
        }

        public void SetCornerRadius(Vector2 radius)
        {
            m_CornerRadius = radius;
        }
    }

}
