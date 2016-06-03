

namespace GameEngine
{
    class Component
    {
        /* TODO implement
        public Transform transform;
        public BaseBehavior gameObject;
        */
        public virtual void UpdateEarly() { }
        public virtual void Update() { }
        public virtual void UpdateLate() { }

        public virtual void Start() { }

        public virtual void PaintEarly() { }
        public virtual void Paint() { }
        public virtual void PaintLate() { }

        public virtual void Destroy() { }
        public virtual void OnComponentAdd(Component component) { }
        public virtual void OnComponentRemove(Component component) { }
    }
}
