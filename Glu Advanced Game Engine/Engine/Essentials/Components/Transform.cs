using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Transform : Component
    {
        private Vector2 m_Position;
        private Vector2 m_DeltaPosition;
        private Vector2 m_PreviousPosition;
        private Vector2 m_Scale;
        private float m_Rotation;

        public Vector2 position
        {
            get { return m_DeltaPosition; }
            set { m_DeltaPosition = value; }
        }

        public Vector2 previousPosition
        {
            get { return m_PreviousPosition; }
            private set { m_PreviousPosition = value; }
        }


        public Vector2 scale
        {
            get { return m_Scale; }
            private set { m_Scale = value; }
        }

        public void Rotate(float degrees)
        {
            m_Rotation += degrees;
        }

        public void Translate(Vector2 translation)
        {
            m_DeltaPosition += translation;
        }

        public override void Awake()
        {
            m_DeltaPosition = Vector2.zero;
            m_Position = Vector2.zero;
            m_Scale = new Vector2(1, 1);
            m_Rotation = 0f;
        }

        public override void UpdateLate()
        {
            m_PreviousPosition = m_Position;
            m_Position = m_DeltaPosition;
        }

        public override void Paint()
        {
            Draw.SetColor(Color.Red);
            Draw.Line(0, 0, Screen.width, Screen.height);
        }
    }
}
