using System.Linq;
using System.Collections.Generic;

namespace GameEngine.Core
{
    class ComponentManager
    {
        public List<Component> m_components = new List<Component>();

        public void addComponent(Component component)
        {
            m_components.Add(component);
        }

        public t getComponent<t>(Component component) where t : Component
        {
            t result = (t)from c in m_components where c.GetType() == typeof(t) select c;

            if (result != null) return result;
            else return default(t);
        }

        public void Update()
        {
            foreach (Component C in m_components)
            {

            }
        }

        public void Draw();
    }
}
