using GameEngine.Core;

namespace GameEngine
{
    public class Component
    {
        public Transform transform;
        public GameObject gameObject;
        public int ExecutionOrder;

        public Component()
        {
            Awake();
        }

        ~Component()
        {
            Destroy();
        }

        public virtual void UpdateEarly() { }
        public virtual void Update() { }
        public virtual void UpdateLate() { }

        public virtual void Awake() { }
        //public virtual void Start() {}

        public virtual void PaintEarly() { }
        public virtual void Paint() { }
        public virtual void PaintLate() { }

        public virtual void Destroy() { }
        public virtual void OnComponentAdd(Component component) { }
        public virtual void OnComponentRemove(Component component) { }
    }
}
