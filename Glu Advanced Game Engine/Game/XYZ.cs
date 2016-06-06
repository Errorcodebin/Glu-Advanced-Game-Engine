namespace GameEngine
{
    public class XYZ : AbstractGame
    {
        GameObject obj = new GameObject();

        public override void GameStart()
        {
            obj.AddComponent(new Transform());
        }

        public override void GameEnd()
        {

        }

        public override void Update()
        {

        }

        public override void Paint()
        {
            obj.Paint();
        }
    }
}
