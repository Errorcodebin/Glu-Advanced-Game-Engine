using System;
using System.Diagnostics;

//SharpDX
using SharpDX.Windows;
using SharpDX.Direct2D1;

//Engine
using GameEngine.Core;

namespace GameEngine
{
    public class GameEngine : IDisposable
    {
        //Singleton
        private static GameEngine m_Instance;

        //Subdevisions
        private InputManager m_InputManager;
        private AudioManager m_AudioManager;
        private GameObjectManager m_GameObjectManager = new GameObjectManager();

        private Window m_Window;
        public Window window
        {
            get { return m_Window; }
        }

        private Renderer m_Renderer;
        public Renderer renderer
        {
            get { return m_Renderer; }
        }

        private RenderTarget m_RenderTarget;

        //For deltaTime calculation
        private Stopwatch m_Stopwatch;
        private float m_LastDeltaTime = 0.0f;

        //To force students to draw only in the paint function!
        private bool m_CanIPaint = false;

        //--------------------------
        // Functions
        //--------------------------

        //Core
        #region Core

        public void Dispose()
        {
            m_AudioManager.Dispose();
        }

        public void Run()
        {
            if (m_GameObjectManager.Count == 0)
            {
                Debug.LogError("We are trying to run an undefined game!");
                return;
            }

            //Initialize the game
            m_GameObjectManager.InitializeGameObjects();

            //Initialize the engine (window)
            m_Window = new Window();
            m_Window.Create(-1, 0);
            m_Renderer = new Renderer(m_Window);
            m_RenderTarget = m_Renderer.GetRenderTarget();

            m_InputManager = new InputManager(m_Window.GetRenderForm());
            m_AudioManager = new AudioManager(44100, 2);

            m_GameObjectManager.StartGameObjects();

            //Start the core game / renderloop
            m_Stopwatch = new Stopwatch();
            m_Stopwatch.Start();

            RenderLoop.Run(m_Window.GetRenderForm(), () =>
            {
                m_RenderTarget.BeginDraw();
                m_Renderer.Clear();

                //Update
                m_InputManager.Update();
                m_GameObjectManager.UpdateGameObjects();

                //Draw
                m_Renderer.Enable(true);

                m_GameObjectManager.PaintGameObjects();

                m_Renderer.Enable(false);

                m_RenderTarget.EndDraw();

                m_Renderer.Present();
                m_LastDeltaTime = m_Stopwatch.ElapsedMilliseconds / 1000.0f;
                m_Stopwatch.Restart();
            });
        }

        private bool PaintCheck()
        {
            if (m_CanIPaint == false)
            {
                Debug.LogError("You are painting outside of the paint function!");
            }

            return m_CanIPaint;
        }

        public void SubscribeGameObject(BaseBehavior go)
        {
            m_GameObjectManager.SubscribeGameObject(go);
        }

        public void UnsubscribeGameObject(BaseBehavior go)
        {
            m_GameObjectManager.SubscribeGameObject(go);
        }
        #endregion

        // Window mutators & accessors (we are not using C# properties to make everything more clear.)
        #region Window Mutators $ Accessors
        public static GameEngine GetInstance()
        {
            if (m_Instance == null) { m_Instance = new GameEngine(); }
            return m_Instance;
        }

        public float GetDeltaTime()
        {
            return m_LastDeltaTime;
        }
        #endregion

        //Input methods
        #region Input Methods
        public bool GetKey(Key key)
        {
            return m_InputManager.GetKey(key);
        }

        public bool GetKeyDown(Key key)
        {
            return m_InputManager.GetKeyDown(key);
        }

        public bool GetKeyUp(Key key)
        {
            return m_InputManager.GetKeyUp(key);
        }

        public bool GetMouseButton(int buttonID)
        {
            return m_InputManager.GetMouseButton(buttonID);
        }

        public bool GetMouseButtonDown(int buttonID)
        {
            return m_InputManager.GetMouseButtonDown(buttonID);
        }

        public bool GetMouseButtonUp(int buttonID)
        {
            return m_InputManager.GetMouseButtonUp(buttonID);
        }

        public Vector2 GetMousePosition()
        {
            return m_InputManager.GetMousePosition();
        }
        #endregion

        //Audio methods
        #region Audio
        public void PlayAudio(Audio audio)
        {
            m_AudioManager.PlayAudio(audio);
        }

        public void StopAudio(Audio audio)
        {
            m_AudioManager.StopAudio(audio);
        }

        public void SetVolume(float volume)
        {
            if (volume < 0.0f)
            {
                Debug.LogError("The volume cannot be lower than 0.0");
                return;
            }

            if (volume > 1.0f)
            {
                Debug.LogError("The volume cannot be higher than 1.0");
                return;
            }

            m_AudioManager.SetVolume(volume);
        }
        #endregion
    }
}
