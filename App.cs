
namespace EdiusPlayground
{
    internal class App
    {
        private string _user = "Unknown";

        private readonly MainMenu _mainMenu;
        private readonly GameHub _gameHub = new();
        private readonly ToolHub _toolHub = new();

        public App()
        {
            _mainMenu = new MainMenu(_gameHub, _toolHub, GetUser, SetUser);
        }

        public void Run()
        {
            _mainMenu.Run();
        }

        private string GetUser()
        {
            return _user;
        }

        private void SetUser(string user)
        {
            _user = user;
        }
    }
}
