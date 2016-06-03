using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class Transform : Component
    {
        private Vector2 m_Position;
        private Vector2 m_DeltaPosition;
        private Vector2 m_PreviousPosition;
        private Vector2 m_Scale;

        public Vector2 position
        {
            get { return m_DeltaPosition; }
            set {  m_DeltaPosition = value;}
        }

        public Vector2 previousPosition
        {
            get;
            set;
        }

        private float m_Rotation;

        public void Translate(Vector2 translation)
        {
            m_Position += translation;
        }

        protected override void Update()
        {

        }
    }
}
