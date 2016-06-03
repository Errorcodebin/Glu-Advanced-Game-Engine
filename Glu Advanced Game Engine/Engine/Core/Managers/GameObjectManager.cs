using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core
{
    class GameObjectManager : IDisposable
    {
        List<BaseBehavior> m_GameObjects;

        public int Count { get { return m_GameObjects.Count; } }

        public GameObjectManager()
        {
            m_GameObjects = new List<BaseBehavior>();
        }

        public void Dispose()
        {
            for (int i = m_GameObjects.Count - 1; i >= 0; --i)
                m_GameObjects[i].GameEnd();

            m_GameObjects.Clear();
        }

        public void InitializeGameObjects()
        {
            foreach (BaseBehavior go in m_GameObjects)
                go.GameInitialize();
        }

        public void StartGameObjects()
        {
            for (int i = m_GameObjects.Count - 1; i >= 0; --i)
                m_GameObjects[i].GameStart();
        }

        public void UpdateGameObjects()
        {
            foreach (BaseBehavior go in m_GameObjects)
                go.Update();
        }

        public void PaintGameObjects()
        {
            foreach (BaseBehavior go in m_GameObjects)
                go.Paint();
        }

        public void SubscribeGameObject(BaseBehavior go)
        {
            m_GameObjects.Add(go);
        }

        public void UnsubscribeGameObject(BaseBehavior go)
        {
            m_GameObjects.Remove(go);
        }
    }
}
