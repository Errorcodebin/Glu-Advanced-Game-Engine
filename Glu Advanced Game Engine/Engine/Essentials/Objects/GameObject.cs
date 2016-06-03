using GameEngine.Core;

namespace GameEngine
{
    public class GameObject
    {
        public ComponentManager componentManager;
        bool Active = true;


        public GameObject()
        {
            componentManager = new ComponentManager(this);
        }

        public void Update()
        {
            if (Active)
            {
                componentManager.Update();
            }
        }

        public void Paint()
        {
            if (Active)
            {
                componentManager.Paint();
            }
        }

        public void AddComponent<t>() where t : Component
        {
            componentManager.Add<t>();
        }

        public void RemoveComponent<t>() where t : Component
        {
            componentManager.Remove<t>();
        }

        public bool ContainsComponent<t>() where t : Component
        {
            return componentManager.Contains<t>();
        }
    }
}
