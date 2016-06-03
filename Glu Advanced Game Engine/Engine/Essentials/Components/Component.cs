

namespace GameEngine
{
    class Component
    {
        /* TODO implement
        public Transform transform;
        public BaseBehavior gameObject;
        */

        private Component()
        {
            Awake();
        }

        ~Component()
        {
            Destroy();
        }

        protected virtual void UpdateEarly() { }
        protected virtual void Update() { }
        protected virtual void UpdateLate() { }

        protected virtual void Awake() { }
        
        // Disabled temporarily until we have
        // found a nice spot to invoke this from
        protected virtual void Start() {}

        protected virtual void PaintEarly() { }
        protected virtual void Paint() { }
        protected virtual void PaintLate() { }

        protected virtual void Destroy() { }
        protected virtual void OnComponentAdd(Component component) { }
        protected virtual void OnComponentRemove(Component component) { }
    }
}
