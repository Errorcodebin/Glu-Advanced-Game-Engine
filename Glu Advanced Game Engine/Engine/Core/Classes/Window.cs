using System.Windows.Forms;

// SharpDX
using SharpDX.Windows;
using System.Drawing;


namespace GameEngine.Core
{
    public class Window
    {
        // Properties
        private string m_Title = "Why did you remove the SetTitle line in AbstractGame?";
        private string m_IconPath = "../../Assets/icon.ico";
        private int m_Width = 800;
        private int m_Height = 600;

        RenderForm m_RenderForm;

        public void Create(int width, int height)
        {
            if (width < 0 || height < 0)
            {
                SystemDebug.LogWarning("Can't make window with size " + width + "*" + height + ". Reverting to 800*600.");
                m_Width = 800;
                m_Height = 600;
            }
            else
            {
                m_Width = width;
                m_Height = height;
            }

            Create();
        }

        public void Create()
        {
            m_RenderForm = new RenderForm(m_Title);
            m_RenderForm.Icon = Icon.ExtractAssociatedIcon(m_IconPath);
            m_RenderForm.ClientSize = new Size(m_Width, m_Height);
            m_RenderForm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        public RenderForm GetRenderForm()
        {
            return m_RenderForm;
        }

        public void SetWidth(int width)
        {
            if (width <= 0)
            {
                Debug.LogError("The screenwidth can not be smaller than 1 pixel. (SetWidth)");
                return;
            }

            m_Width = width;
        }

        public void SetHeight(int height)
        {
            if (height <= 0)
            {
                Debug.LogError("The screenheight can not be smaller than 1 pixel. (SetHeight)");
                return;
            }

            m_Height = height;
        }

        public int GetWidth()
        {
            return m_Width;
        }

        public int GetHeight()
        {
            return m_Height;
        }

        public void SetTitle(string title)
        {
            m_Title = title;
        }

        public void SetIcon(string iconPath)
        {
            m_IconPath = iconPath;
        }
    }
}
