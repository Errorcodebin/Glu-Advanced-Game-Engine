namespace GameEngine
{
    public class XYZ : AbstractGame
    {
        Font font = new Font("Cambria",30);
        public float time = 0;

        public override void GameStart()
        {

        }

        public override void GameEnd()
        {

        }

        public override void Update()
        {
            time += GAME_ENGINE.GetDeltaTime()*100;
        }

        public override void Paint()
        {
            Draw.SetColor(Color.HsvToColor(time,0.2f,0.2f));
            Draw.FillRectangle(0, 0, Screen.width, Screen.height);
            Draw.SetColor(Color.HsvToColor(time, 1f, 1f));
            Draw.Text(font,"HELLO WORLD!",Screen.width/2,Screen.height/2,200,100);
        }
    }
}
