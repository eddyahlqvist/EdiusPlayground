
namespace EdiusPlayground
{
    internal class App
    {
        private readonly MainMenu _mainMenu;    // declares the field
        private readonly GameHub _gameHub = new();  // declares and creates the object

        public App()
        {
            _mainMenu = new MainMenu(_gameHub);
        }

        public void Run()
        {
            _mainMenu.Run();
        }
    }
}
