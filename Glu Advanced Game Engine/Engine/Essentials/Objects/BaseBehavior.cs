using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class BaseBehavior
    {
        private GameEngine m_GameEngine;
        protected GameEngine GAME_ENGINE
        {
            get { return m_GameEngine; }
        }

        public BaseBehavior()
        {
            m_GameEngine = GameEngine.GetInstance();
            m_GameEngine.SubscribeGameObject(this);
        }

        public virtual void GameInitialize() { }
        public virtual void GameStart() { }
        public virtual void GameEnd()
        {
            m_GameEngine.UnsubscribeGameObject(this);
        }
        public virtual void Update() { }
        public virtual void Paint() { }
    }

}
