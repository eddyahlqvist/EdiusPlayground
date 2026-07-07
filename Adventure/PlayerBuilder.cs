
namespace EdiusPlayground.Adventure
{
    internal class PlayerBuilder
    {
        public Player BuildPlayer(string name)
        {
            const int StartingHP = 10;
            return new Player(name, StartingHP);
        }
    }
}
