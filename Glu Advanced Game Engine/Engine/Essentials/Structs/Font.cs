using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Font : IDisposable
    {
        public enum Alignment
        {
            Left,
            Right,
            Center
        }

        private SharpDX.DirectWrite.TextFormat m_TextFormat;
        public SharpDX.DirectWrite.TextFormat TextFormat
        {
            get { return m_TextFormat; }
        }

        public Font(string fontName, float size)
        {
            CreateFont(fontName, size);
        }

        public void Dispose()
        {
            //This is the destructor
            m_TextFormat.Dispose();
        }

        private void CreateFont(string fontName, float size)
        {
            SharpDX.DirectWrite.Factory fontFactory = new SharpDX.DirectWrite.Factory();
            m_TextFormat = new SharpDX.DirectWrite.TextFormat(fontFactory, fontName, size);
            fontFactory.Dispose();
        }

        public void SetHorizontalAlignment(Alignment alignment)
        {
            m_TextFormat.TextAlignment = (SharpDX.DirectWrite.TextAlignment)alignment;
        }

        public void SetVerticalAlignment(Alignment alignment)
        {
            m_TextFormat.ParagraphAlignment = (SharpDX.DirectWrite.ParagraphAlignment)alignment;
        }
    }

}
