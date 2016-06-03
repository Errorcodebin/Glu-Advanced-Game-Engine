using GameEngine.Core;

namespace GameEngine
{
    //Hides the basic setup from the X class.
    public class AbstractGame : BaseBehavior
    {
        public override void GameInitialize()
        {
            //// Set the required values
            //GAME_ENGINE.window.SetTitle("Game Engine v1.0");
            //GAME_ENGINE.window.SetIcon("../../Assets/icon.ico");
            //
            //// Set the optional values
            //GAME_ENGINE.window.SetWidth(640);
            //GAME_ENGINE.window.SetHeight(480);
            //GAME_ENGINE.renderer.SetBackgroundColor(49, 77, 121); //The Unity background color
        }
    }
}
