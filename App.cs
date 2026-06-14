
namespace EdiusPlayground
{
    internal class App
    {
        private readonly MainMenu _mainMenu;
        private readonly GameHub _gameHub = new();
        private readonly ToolHub _toolHub = new();

        public App()
        {
            _mainMenu = new MainMenu(_gameHub, _toolHub);
        }

        public void Run()
        {
            _mainMenu.Run();
        }
    }
}
