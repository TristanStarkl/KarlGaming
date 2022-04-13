using System;

namespace KarlGaming
{

    public class Personnage
    {
        public string Name;
        public string Affichage;
        public int Pv;
        public int PosX;
        public int PosY;
        public int Numero;
        public bool IsAlive;
        public Weapon Arme;

        public Personnage(string name, int pv, int posX, int posY, int affi, int numero)
        {
            Numero = numero;
            Name = name;
            Pv = pv;
            PosX = posX;
            PosY = posY;
            Affichage = affi.ToString();
            IsAlive = true;
            Random rand = new Random();
            if (rand.Next(100) > 50)
                Arme = new Arc();
            else
                Arme = new Epee();
        }

        public override string ToString()
        {
            return $"({Numero}) {Name} ({Pv}pv): pos {PosX} {PosY}: Il est équippé de {Arme.Name}";
        }

        internal void Move(int posX, int posY)
        {
            PosX = posX;
            PosY = posY;
        }

        internal int Attack(int posX, int posY)
        {
            if (!IsInRange(posX, posY))
                Console.WriteLine("Impossible d'attaquer! La distance est trop loin");
            return Arme.Degat;
        }

/*        public bool Attack(int posX, int posY)
        {
            if (!IsInRange(posX, posY))
            {
                Console.WriteLine("Impossible d'attaquer! La distance est trop loin");
                return false;
            }
            else
                return true;
        }
*/
        private bool IsInRange(int posX, int posY)
        {
            int d = (int)Math.Sqrt(((PosX - posX) * (PosX - posX)) + ((PosY - posY) * (PosY - posY)));
            return d < Arme.Range;
        }
    }
}