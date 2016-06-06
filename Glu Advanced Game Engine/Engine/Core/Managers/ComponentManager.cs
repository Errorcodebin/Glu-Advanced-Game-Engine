using System.Linq;
using System.Collections.Generic;

namespace GameEngine.Core
{
    public class ComponentManager
    {
        private List<Component> m_components = new List<Component>();
        private GameObject m_GameObject;

        public ComponentManager(GameObject gameobject)
        {
            m_GameObject = gameobject;
        }

        private void SortByExecutionOrder()
        {

        }

        public void Add(Component component)
        {
            m_components.Add(component);
        }

        public t Get<t>() where t : Component
        {
            t result = (t)from element in m_components
                          where element.GetType() == typeof(t)
                          select element;
            return result;
        }

        public void Remove<t>() where t : Component
        {
            foreach (Component c in m_components)
            {
                if (c.GetType() == typeof(t))
                {
                    m_components.Remove(c);
                    break;
                }
            }
        }

        public bool Contains<t>() where t : Component
        {
            foreach (Component c in m_components)
            {
                if (c.GetType() == typeof(t))
                {
                    return true;
                }
            }
            return false;
        }

        public void Update()
        {
            foreach (Component C in m_components)
            {
                C.UpdateEarly();
                C.Update();
                C.UpdateLate();
            }
        }

        public void Paint()
        {
            foreach (Component C in m_components)
            {
                C.PaintEarly();
                C.Paint();
                C.PaintLate();
            }
        }
    }
}
