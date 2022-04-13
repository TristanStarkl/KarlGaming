namespace KarlGaming
{
    public class Weapon
    {
        public int Range;
        public int Degat;
        public string Name;
        public Weapon(int range, int degat)
        {
            Range = range;
            Degat = degat;
        }


    }

    internal class Arc : Weapon
    {
        public Arc() : base (3, 10)
        {
            Name = "L'arc légendaire de ta mère";
        }
    }

    internal class Epee : Weapon
    {
        public Epee() : base(1, 20)
        {
            Name = "LA LEGENDAIRE EXCALIBURNE";
        }
    }
}