using GameEngine.Core;
using GameEngine.Drawing;

namespace GameEngine
{
    /// <summary>
    /// Contains Draw methods and overloads
    /// </summary>
    static class Draw
    {
        static private Renderer m_Renderer = GameEngine.GetInstance().renderer;

        #region Line
        static public void Line(float startPointX, float startPointY, float endPointX, float endPointY, float width = 1)
        {
            m_Renderer.DrawLine(startPointX, startPointY, endPointX, endPointY, width);
        }

        static public void Line(Vector2 startPoint, Vector2 endPoint)
        {
            Line(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
        }
        #endregion

        #region Rectangle
        static public void Rectangle(int x, int y, int width, int height)
        {
            //Not using default parameters on purpose.
            Rectangle(x, y, width, height, 1);
        }

        static public void Rectangle(Rectangle rect)
        {
            Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }

        static public void Rectangle(int x, int y, int width, int height, int strokeWidth)
        {
            m_Renderer.DrawRectangle(x, y, width, height, strokeWidth);
        }

        static public void Rectangle(Rectangle rect, int strokeWidth)
        {
            Rectangle(rect.X, rect.Y, rect.Width, rect.Height, strokeWidth);
        }
        #endregion

        #region Fill Rectangle
        static public void FillRectangle(int x, int y, int width, int height)
        {
            m_Renderer.FillRectangle(x, y, width, height);
        }

        static public void FillRectangle(Rectangle rect)
        {
            FillRectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }
        #endregion

        #region Rounded rectangle
        static public void RoundedRectangle(float x, float y, float width, float height, float radiusX, float radiusY)
        {
            RoundedRectangle(x, y, width, height, radiusX, radiusY, 1);
        }

        static public void RoundedRectangle(Rectangle rect, Vector2 radius)
        {
            RoundedRectangle(rect.X, rect.Y, rect.Width, rect.Height, radius.X, radius.Y);
        }

        static public void RoundedRectangle(float x, float y, float width, float height, float radiusX, float radiusY, int strokeWidth)
        {
            m_Renderer.DrawRoundedRectangle(x, y, width, height, radiusX, radiusY, strokeWidth);
        }

        static public void RoundedRectangle(Rectangle rect, Vector2 radius, int strokeWidth)
        {
            RoundedRectangle(rect.X, rect.Y, rect.Width, rect.Height, radius.X, radius.Y, strokeWidth);
        }

        static public void FillRoundedRectangle(float x, float y, float width, float height, float radiusX, float radiusY)
        {
            m_Renderer.FillRoundedRectangle(x, y, width, height, radiusX, radiusY);
        }

        static public void FillRoundedRectangle(Rectangle rect, Vector2 radius)
        {
            FillRoundedRectangle(rect.X, rect.Y, rect.Width, rect.Height, radius.X, radius.Y);
        }
        #endregion

        #region Ellipse
        static public void Ellipse(int x, int y, int width, int height)
        {
            Ellipse(x, y, width, height, 1);
        }

        static public void Ellipse(Rectangle rect)
        {
            Ellipse(rect.X, rect.Y, rect.Width, rect.Height);
        }

        static public void Ellipse(int x, int y, int width, int height, int strokeWidth)
        {
            m_Renderer.DrawEllipse(x, y, width, height, strokeWidth);
        }

        static public void Ellipse(Rectangle rect, int strokeWidth)
        {
            Ellipse(rect.X, rect.Y, rect.Width, rect.Height, strokeWidth);
        }

        static public void FillEllipse(float x, float y, float width, float height)
        {
            m_Renderer.FillEllipse(x, y, width, height);
        }

        static public void FillEllipse(Rectangle rect)
        {
            FillEllipse(rect.X, rect.Y, rect.Width, rect.Height);
        }
        #endregion

        #region Bitmaps
        // TODO Re-implement Bitmaps

        /*
        static public void DrawBitmap(Bitmap bitmap, float x, float y)
        {
            m_Renderer.DrawBitmap(bitmap, x, y);
        }

        static public void DrawBitmap(Bitmap bitmap, Vector2 position)
        {
            m_Renderer.DrawBitmap(bitmap, position);
        }
        */

        static public void DrawBitmap(Bitmap bitmap, float x, float y, int sourceX, int sourceY, int sourceWidth, int sourceHeight)
        {
            m_Renderer.DrawBitmap(bitmap, x, y, sourceX, sourceY, sourceWidth, sourceHeight);
        }
        /*
        static public void DrawBitmap(Bitmap bitmap, int x, int y, Rectangle sourceRect)
        {
            m_Renderer.DrawBitmap(bitmap, x, y, sourceRect);
        }

        static public void DrawBitmap(Bitmap bitmap, Vector2 position, Rectangle sourceRect)
        {
            m_Renderer.DrawBitmap(bitmap, position, sourceRect);
        }
        */
        #endregion

        #region Text
        static public void Text(string text, int x, int y, int width, int height)
        {
            Text(null,text, x, y, width, height);
        }

        static public void Text(string text, Rectangle rect)
        {
            Text(null, text, rect.X, rect.Y, rect.Width, rect.Height);
        }

        static public void Text(Font font, string text, int x, int y, int width, int height)
        {
            m_Renderer.DrawString(font, text, x, y, width, height);
        }

        static public void Text(Font font, string text, Rectangle rect)
        {
            Text(font, text, rect.X,rect.Y,rect.Width,rect.Height);
        }
        #endregion

        #region Brush Methods
        static public void SetColor(int r, int g, int b)
        {
            m_Renderer.SetBrushColor(r, g, b);
        }
        static public void SetColor(Color color)
        {
            SetColor(color.R,color.G,color.B);
        }

        #region Gradient
        static public void SetBrushGradientColor(int r1, int g1, int b1, int r2, int g2, int b2)
        {
            Color[] colors = new Color[2];
            colors[0] = new Color(r1,g1,b1);
            colors[1] = new Color(r2,g1,b2);
            SetBrushGradientColor(colors);
        }

        static public void SetBrushGradientColor(Color color1, Color color2)
        {
            Color[] colors = new Color[2];
            colors[0] = color1;
            colors[1] = color2;
            SetBrushGradientColor(colors);
        }

        static public void SetBrushGradientColor(Color[] colors)
        {
            m_Renderer.SetBrushGradientColor(colors);
        }
        #endregion

        static public void SetBrushType(BrushType type)
        {
            m_Renderer.SetBrushType(type);
        }
        #endregion
    }
}
