using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// SharpDX
using SharpDX.IO;
using SharpDX.WIC;
using SharpDX.Direct2D1;

namespace GameEngine
{
    public class Bitmap : IDisposable
    {
        private SharpDX.Direct2D1.Bitmap m_D2DBitmap;
        public SharpDX.Direct2D1.Bitmap D2DBitmap
        {
            get { return m_D2DBitmap; }
        }

        public Bitmap(string filePath)
        {
            //LoadBitmap("../../Assets/" + filePath);
        }

        public void Dispose()
        {
            //This is the destructor
            m_D2DBitmap.Dispose();
        }

        /*
        private void LoadBitmap(string filePath)
        {
            //Read the image
            ImagingFactory imagingFactory = new ImagingFactory();
            NativeFileStream fileStream = new NativeFileStream(filePath, NativeFileMode.Open, NativeFileAccess.Read);

            //Decode and get the frame (decodes all sorts of image formats for us)
            BitmapDecoder bitmapDecoder = new BitmapDecoder(imagingFactory, fileStream, DecodeOptions.CacheOnDemand);
            BitmapFrameDecode frame = bitmapDecoder.GetFrame(0);

            //Convert the colors to the Direct2D standard
            FormatConverter converter = new FormatConverter(imagingFactory);
            converter.Initialize(frame, SharpDX.WIC.PixelFormat.Format32bppPRGBA);

            RenderTarget renderTarget = GameEngine.GetInstance().m_wi;
            m_D2DBitmap = SharpDX.Direct2D1.Bitmap1.FromWicBitmap(renderTarget, converter);
        }
        */

        private void SetTransparancyColor(Color color)
        {
            //To be made, for now use PNG
            //new SharpDX.Direct2D1.Effects.ColorManagement()
        }

        public int GetWidth()
        {
            return (int)m_D2DBitmap.Size.Width;
        }

        public int GetHeight()
        {
            return (int)m_D2DBitmap.Size.Height;
        }
    }

}
