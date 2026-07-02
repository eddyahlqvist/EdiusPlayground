using EdiusPlayground.Adventure;
using EdiusPlayground.Games;
using EdiusPlayground.Menus;
using EdiusPlayground.Tools;

namespace EdiusPlayground.Core
{
    internal class App
    {
        public const bool IsDebugMode = true;

        private string _user = "Unknown";

        private readonly MainMenu _mainMenu;
        private readonly GameHub _gameHub = new();
        private readonly ToolHub _toolHub = new();
        private readonly AdventureGame _adventureGame = new();

        public App()
        {
            _mainMenu = new MainMenu(_gameHub, _toolHub, _adventureGame, GetUser, SetUser);
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
