using System.Windows.Forms;

// SharpDX
using SharpDX.Windows;
using SharpDX.DXGI;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.Mathematics.Interop;
using Device = SharpDX.Direct3D11.Device;
using D2DFactory = SharpDX.Direct2D1.Factory;
using DXGIFactory = SharpDX.DXGI.Factory;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using GameEngine.Drawing;

namespace GameEngine.Core
{
    public class Renderer
    {
        private SwapChain m_SwapChain;
        private RenderForm m_RenderForm;
        private RenderTarget m_RenderTarget;
        private int m_Width;
        private int m_Height;

        //Default Brush properties
        private SharpDX.Direct2D1.Brush m_CurrentBrush;
        private LinearGradientBrushProperties m_CurrentLinearProperties;
        private RadialGradientBrushProperties m_CurrentRadialProperties;
        private GradientStopCollection m_GradientStopCollection;
        private SharpDX.Color m_CurrentBrushColor;
        private D2DFactory m_Factory;

        private bool m_CanPaint;

        private Font m_DefaultFont; //So students can quickly get text on the screen without messing around with fonts.

        private SharpDX.Color m_ClearColor = new SharpDX.Color(255, 255, 255);

        public Renderer(Window window)
        {
            m_Width = window.GetWidth();
            m_Height = window.GetHeight();
            m_RenderForm = window.GetRenderForm();

            InitializeDeviceResources();

            //Default the brush & font
            m_CurrentBrush = new SolidColorBrush(m_RenderTarget, SharpDX.Color.Black);

            // To initialize the gradient stop collection..
            GradientStop[] stops = new GradientStop[1];
            stops[0].Position = 0f;
            stops[0].Color = new RawColor4(0, 0, 0, 1);

            m_GradientStopCollection = new GradientStopCollection(m_RenderTarget, stops);
            m_DefaultFont = new Font("Arial", 12.0f);
        }

        private void InitializeDeviceResources()
        {
            //DXGI_MODE_DESC (width, height, refresh rate, color format)
            ModeDescription modeDesc = new ModeDescription(m_Width, m_Height, new Rational(60, 1), Format.R8G8B8A8_UNorm);

            //DXGI_SWAP_CHAIN_DESC
            SwapChainDescription swapChainDesc = new SwapChainDescription()
            {
                ModeDescription = modeDesc,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput,
                BufferCount = 1,
                OutputHandle = m_RenderForm.Handle,
                IsWindowed = true
            };

            //Create our device & swap chain
            Device device;
            Device.CreateWithSwapChain(DriverType.Hardware,
                                       DeviceCreationFlags.BgraSupport,
                                       new SharpDX.Direct3D.FeatureLevel[] { SharpDX.Direct3D.FeatureLevel.Level_10_0 },
                                       swapChainDesc,
                                       out device,
                                       out m_SwapChain);

            //We don't need this ref as we only use 2D
            device.Dispose();

            //Create backbuffer & rendertarget
            Texture2D backBuffer = Texture2D.FromSwapChain<Texture2D>(m_SwapChain, 0);
            Surface surface = backBuffer.QueryInterface<Surface>();
            backBuffer.Dispose();

            m_Factory = new D2DFactory();
            m_RenderTarget = new RenderTarget(m_Factory, surface, new RenderTargetProperties(new SharpDX.Direct2D1.PixelFormat(Format.Unknown, AlphaMode.Premultiplied)));

            // Disable automatic ALT+Enter processing because it doesn't work properly with WinForms
            DXGIFactory DXGIFactory = m_SwapChain.GetParent<DXGIFactory>();
            DXGIFactory.MakeWindowAssociation(m_RenderForm.Handle, WindowAssociationFlags.IgnoreAltEnter);
            DXGIFactory.Dispose();

            //And replace it with a custom bind
            m_RenderForm.KeyDown += (o, e) =>
            {
                if (e.Alt && e.KeyCode == Keys.Enter)
                    m_SwapChain.IsFullScreen = !m_SwapChain.IsFullScreen;
            };
        }

        public void SetBackgroundColor(int r, int g, int b)
        {
            m_ClearColor = new SharpDX.Color(r, g, b);
        }

        public void SetBackgroundColor(Color color)
        {
            SetBackgroundColor(color.R, color.G, color.B);
        }

        public RenderTarget GetRenderTarget()
        {
            return m_RenderTarget;
        }

        public void Clear()
        {
            m_RenderTarget.Clear(m_ClearColor);
        }

        public void Present()
        {
            m_SwapChain.Present(1, PresentFlags.None); //1 for VSync
        }

        public bool PaintCheck()
        {
            if (!m_CanPaint)
            {
                SystemDebug.LogWarning("You are trying to draw outside of the paint loop!");
            }

            return m_CanPaint;
        }

        public void Enable(bool enable)
        {
            m_CanPaint = enable;
        }

        #region Draw Functions
        public void DrawLine(float startPointX, float startPointY, float endPointX, float endPointY, float width = 1)
        {
            if (!PaintCheck())
                return;

            RawVector2 p1 = new RawVector2(startPointX, startPointY);
            RawVector2 p2 = new RawVector2(endPointX, endPointY);

            if (m_CurrentBrush.GetType() == typeof(LinearGradientBrush))
            {
                m_CurrentLinearProperties.StartPoint = p1;
                m_CurrentLinearProperties.EndPoint = p2;
            }

            m_RenderTarget.DrawLine(p1, p2, m_CurrentBrush);
        }

        public void DrawLine(Vector2 startPoint, Vector2 endPoint)
        {
            DrawLine(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
        }

        //Rectangle
        public void DrawRectangle(int x, int y, int width, int height, int strokeWidth)
        {
            if (!PaintCheck())
                return;

            RawRectangleF rect = new RawRectangleF((float)x, (float)y, (float)(x + width), (float)(y + height));
            m_RenderTarget.DrawRectangle(rect, m_CurrentBrush, (float)strokeWidth);
        }

        public void FillRectangle(int x, int y, int width, int height)
        {
            if (!PaintCheck())
                return;

            RawRectangleF rect = new RawRectangleF((float)x, (float)y, (float)(x + width), (float)(y + height));

            if (m_CurrentBrush.GetType() == typeof(LinearGradientBrush))
            {
                m_CurrentLinearProperties.StartPoint = new RawVector2(rect.Left, rect.Top);
                m_CurrentLinearProperties.EndPoint = new RawVector2(rect.Right, rect.Bottom);
            }

            m_RenderTarget.FillRectangle(rect, m_CurrentBrush);
        }

        //Rounded rectangle
        public void DrawRoundedRectangle(float x, float y, float width, float height, float radiusX, float radiusY, int strokeWidth)
        {
            if (!PaintCheck())
                return;

            RawRectangleF rect = new RawRectangleF((float)x, (float)y, (float)(x + width), (float)(y + height));

            RoundedRectangle roundedRect = new RoundedRectangle();
            roundedRect.Rect = rect;
            roundedRect.RadiusX = (float)radiusX;
            roundedRect.RadiusY = (float)radiusY;

            m_RenderTarget.DrawRoundedRectangle(roundedRect, m_CurrentBrush, strokeWidth);
        }

        public void FillRoundedRectangle(float x, float y, float width, float height, float radiusX, float radiusY)
        {
            if (!PaintCheck())
                return;

            RawRectangleF rect = new RawRectangleF(x, y, (x + width), (y + height));

            RoundedRectangle roundedRect = new RoundedRectangle();
            roundedRect.Rect = rect;
            roundedRect.RadiusX = (float)radiusX;
            roundedRect.RadiusY = (float)radiusY;

            m_RenderTarget.FillRoundedRectangle(ref roundedRect, m_CurrentBrush);
        }

        //Ellipse
        public void DrawEllipse(float x, float y, float width, float height, float strokeWidth)
        {
            if (!PaintCheck())
                return;

            Ellipse ellipse = new Ellipse(new RawVector2((float)x, (float)y), (float)width, (float)height);
            m_RenderTarget.DrawEllipse(ellipse, m_CurrentBrush, (float)strokeWidth);
        }

        public void FillEllipse(float x, float y, float width, float height)
        {
            if (!PaintCheck())
                return;

            Ellipse ellipse = new Ellipse(new RawVector2(x, y), width,height);
            m_RenderTarget.FillEllipse(ellipse, m_CurrentBrush);
        }

        //Bitmaps
        public void DrawBitmap(Bitmap bitmap, float x, float y, int sourceX, int sourceY, int sourceWidth, int sourceHeight)
        {
            if (!PaintCheck())
                return;

            SharpDX.Direct2D1.Bitmap D2DBitmap = bitmap.D2DBitmap;

            if (sourceWidth == 0) sourceWidth = (int)D2DBitmap.Size.Width;
            if (sourceHeight == 0) sourceHeight = (int)D2DBitmap.Size.Height;

            RawRectangleF sourceRect = new RawRectangleF((float)sourceX, (float)sourceY, (float)(sourceX + sourceWidth), (float)(sourceY + sourceHeight));

            m_RenderTarget.Transform = SharpDX.Matrix3x2.Translation(x, y);
            m_RenderTarget.DrawBitmap(D2DBitmap, 1.0f, SharpDX.Direct2D1.BitmapInterpolationMode.NearestNeighbor, sourceRect);
            m_RenderTarget.Transform = SharpDX.Matrix3x2.Translation(0, 0);
        }

        //Text
        public void DrawString(Font font, string text, int x, int y, int width, int height)
        {
            if (!PaintCheck())
                return;

            if (font == null)
                font = m_DefaultFont;

            RawRectangleF rect = new RawRectangleF((float)x, (float)y, (float)(x + width), (float)(y + height));
            m_RenderTarget.DrawText(text, font.TextFormat, rect, m_CurrentBrush);
        }

        // Brush Methods
        public void SetBrushColor(int r, int g, int b)
        {
            if (!PaintCheck())
                return;

            m_CurrentBrushColor = new SharpDX.Color(r, g, b);
            UpdateBrush();
        }

        public void SetBrushGradientColor(Color[] colors)
        {
            if (colors.Length <= 1)
            {
                Debug.LogError("A gradient cannot have less than 2 colors!");
                return;
            }

            GradientStop[] gradientStops = new GradientStop[colors.Length];

            for (int i = 0; i < colors.Length; i++)
            {
                gradientStops[i].Color = new RawColor4((float)colors[i].R / 255, (float)colors[i].G / 255, (float)colors[i].B / 255, 1);
                gradientStops[i].Position = (1f / (colors.Length - 1)) * i;
            }

            m_GradientStopCollection.Dispose();
            m_GradientStopCollection = new GradientStopCollection(m_RenderTarget, gradientStops);
            UpdateBrush();
        }

        public void UpdateBrush()
        {
            if (m_CurrentBrush.GetType() == typeof(LinearGradientBrush))
            {
                m_CurrentBrush.Dispose();
                m_CurrentBrush = new LinearGradientBrush(m_RenderTarget, m_CurrentLinearProperties, m_GradientStopCollection);
            }
            else if (m_CurrentBrush.GetType() == typeof(RadialGradientBrush))
            {
                m_CurrentBrush.Dispose();
                m_CurrentBrush = new RadialGradientBrush(m_RenderTarget, m_CurrentRadialProperties, m_GradientStopCollection);
            }
            else if (m_CurrentBrush.GetType() == typeof(SolidColorBrush))
            {
                m_CurrentBrush.Dispose();
                m_CurrentBrush = new SolidColorBrush(m_RenderTarget, m_CurrentBrushColor);
            }
        }

        public void SetBrushType(BrushType type)
        {
            switch (type)
            {
                case BrushType.Normal:
                    m_CurrentBrush.Dispose();
                    m_CurrentBrush = new SolidColorBrush(m_RenderTarget, m_CurrentBrushColor);
                    break;
                case BrushType.LinearGradient:
                    m_CurrentBrush.Dispose();
                    m_CurrentBrush = new LinearGradientBrush(m_RenderTarget, m_CurrentLinearProperties, m_GradientStopCollection);
                    break;
                case BrushType.RadialGradient:
                    m_CurrentBrush.Dispose();
                    m_CurrentBrush = new RadialGradientBrush(m_RenderTarget, m_CurrentRadialProperties, m_GradientStopCollection);
                    break;
            }
        }
        #endregion
    }
}