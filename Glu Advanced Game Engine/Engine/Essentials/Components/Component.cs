

namespace GameEngine
{
    class Component
    {
        public Transform transform;
        public BaseBehavior gameObject;

        public virtual void Update() { }
        public virtual void Start() { }
        public virtual void Paint() { }
        public virtual void Destroy() { }
        public virtual void OnComponentAdd(Component component) { }
        public virtual void OnComponentRemove(Component component) { }
    }
}
