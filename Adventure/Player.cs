namespace EdiusPlayground.Adventure
{
    internal class Player
    {
        public string Name { get; }
        public Room? CurrentRoom{  get; set; } 

        public Player(string name)
        {
            Name = name;
        }
    }
}