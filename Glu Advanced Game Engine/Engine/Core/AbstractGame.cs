using GameEngine.Core;

namespace GameEngine
{
    //Hides the basic setup from the X class.
    public class AbstractGame : BaseBehavior
    {
        public override void GameInitialize()
        {
            // Optional settings.
            GAME_ENGINE.window.SetTitle("Game Engine v1.0");
            GAME_ENGINE.window.SetIcon("../../Assets/icon.ico");
            GAME_ENGINE.window.SetWidth(640);
            GAME_ENGINE.window.SetHeight(480);

            // TODO fix initialization order in GameEngine.cs
            //GAME_ENGINE.renderer.SetBackgroundColor(49, 77, 121); //The Unity background color
        }
    }
}
