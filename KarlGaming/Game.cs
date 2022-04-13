using System;
using System.Collections.Generic;

namespace KarlGaming
{
    internal class Game
    {
        public Map map;
        public List<Personnage> listPerso;
        public bool EndGame;
        public int NbTours;
        public int SizeX;
        public int SizeY;
        public Game(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            map = new Map(SizeX, SizeY);
            EndGame = false;
            listPerso = new List<Personnage>();
            InitializeGame();
            NbTours = 0;
        }

        private void InitializeGame()
        {
            bool Continue = true;
            int NbPersoCrees = 0;
            while (Continue)
            { // Karl 100 5 1 1
                AfficherSeparateur();
                Console.WriteLine("Décrivez votre personnage: ");
                string result = Console.ReadLine();
                string[] listColumns = result.Split(' ');
                NbPersoCrees++;
                Personnage p = new Personnage(listColumns[0], int.Parse(listColumns[1]),  
                    int.Parse(listColumns[2]), int.Parse(listColumns[3]), NbPersoCrees, NbPersoCrees);
                listPerso.Add(p);
                Console.WriteLine("Voulez vous créer un autre personnage? (O/N)");
                if (Console.ReadLine() == "n")
                    Continue = false;
            }
            RecapPersos();
        }

        private void AfficherSeparateur()
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine(" ");
        }

        private void RecapPersos()
        {
            foreach (Personnage p in listPerso)
                Console.WriteLine(p);
            AfficherSeparateur();
        }

        public void Play()
        {
            while (!EndGame)
            {
                NbTours++;
                AfficherSeparateur();
                Console.WriteLine($"Tour numéro {NbTours}");
                map.Show(listPerso);
                Turn();
                if (CheckIfAllPlayerAreAlive())
                    EndGame = true;
            }

            Console.WriteLine($"{GetLastPersonAlive()} a gagné");
        }

        private string GetLastPersonAlive()
        {
            foreach (Personnage p in listPerso)
            {
                if (p.IsAlive)
                    return p.Name;
            }

            return string.Empty;
        }

        private bool CheckIfAllPlayerAreAlive()
        {
            int NbAlive = 0;
            foreach (Personnage p in listPerso)
            {
                if (p.IsAlive && NbAlive == 1)
                    return false;
                else if (p.IsAlive && NbAlive == 0)
                    NbAlive++;
            }

            return true;
        }

        private void Turn()
        {
            foreach (Personnage p in listPerso)
            {
                if (p.IsAlive)
                {
                    Console.WriteLine($"Tour de {p.Name} ({p.Affichage})");
                    ChoisirActions(p);
                }
            }
        }

        private void ChoisirActions(Personnage p)
        {
            string lectureAction = Console.ReadLine();
            string[] result = lectureAction.Split(' ');
            // TODO
            if (result[0] == "MOVE")
            {
                int posX = int.Parse(result[1]);
                int posY = int.Parse(result[2]);
                if (posX > SizeX || posX < 0 || posY > SizeY || posY < 0)
                    Console.WriteLine("Move impossible tu perds ton tour enculé");
                else
                { 
                    p.Move(posX, posY);
                    map.Show(listPerso);
                }
            }
            if (result[0] == "ATTACK")
            {
                int posX = int.Parse(result[1]);
                int posY = int.Parse(result[2]);
                int degat = p.Attack(posX, posY);
                if (posX > SizeX || posX < 0 || posY > SizeY || posY < 0)
                    Console.WriteLine("Move impossible tu perds ton tour enculé");
                if (degat != 0)
                {
                    foreach (Personnage p2 in listPerso)
                    {
                        if (p2.PosX == posX && p2.PosY == posY)
                        {
                            p2.Pv -= p.Arme.Degat;
                            if (p2.Pv <= 0)
                            {
                                Console.WriteLine($"{p2.Name} EST TOMBE AU COMBAT. PRESS F");
                                p2.IsAlive = false;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}