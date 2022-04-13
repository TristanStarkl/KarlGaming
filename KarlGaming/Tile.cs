namespace KarlGaming
{
    public class Tile
    {
        public int coordX;
        public int coordY;
        public string name;
        public char Affichage;
        public bool DoesBlockLineOfSight;
        public Tile(int coordX, int coordY, string name, char affi, bool doesBlockLineOfSight)
        {
            this.coordX = coordX;
            this.coordY = coordY;
            this.name = name;
            DoesBlockLineOfSight = doesBlockLineOfSight;
            Affichage = affi;
        }
    }

    public class Herbe : Tile
    {

        public Herbe(int coordX, int coordY) : base(coordX, coordY, "Herbe", 'h', false)
        {
        }
    }

    public class Arbre : Tile
    {

        public Arbre(int coordX, int coordY) : base(coordX, coordY, "Arbre", 't', true)
        {
        }
    }

    public class Rocher : Tile
    {

        public Rocher(int coordX, int coordY) : base(coordX, coordY, "Rocher", 'r', true)
        {
        }
    }



}