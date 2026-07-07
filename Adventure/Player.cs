namespace EdiusPlayground.Adventure
{
    internal class Player
    {
        public string Name { get; }
        public Room? CurrentRoom { get; set; }
        public int HP { get; set; }

        public Player(string name, int hp)
        {
            Name = name;
            HP = hp;
        }
    }
}