using System;
using System.Collections.Generic;

namespace KarlGaming
{
    internal class Map
    {
        private int sizeX;
        private int sizeY;
        public Tile[,] map;

        public Map(int v1, int v2)
        {
            sizeX = v1;
            sizeY = v2;
            map = new Tile[sizeX, sizeY];
            GenerateMap();
        }

        private void GenerateMap()
        {
            int z;
            float res;
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    z = (((i * j)+1) / (4 * j+1)) % ((2 * i) + 1);
                    res = NoiseMaker.Constraint(NoiseMaker.Noise((float)i, (float)j, (float)z));
                    if (res < 10)
                        map[i, j] = new Rocher(i, j);
                    else if (res < 20)
                        map[i, j] = new Arbre(i, j);
                    else
                        map[i, j] = new Herbe(i, j);
                }
            }
        }

        public bool CheckLineOfSight(int posX1, int posY1, int posX2, int posY2)
        {
            List<KeyValuePair<int, int>> listCoords = GetListOfCoordinatesBetweenTwoPoint(posX1, posY1, posX2, posY2);

            foreach (KeyValuePair<int, int> kvp in listCoords)
            {
                if (map[kvp.Key, kvp.Value].DoesBlockLineOfSight)
                    return true;
            }

            return false;
        }

        #region LineOfSight
        private List<KeyValuePair<int, int>> PlotLineLow(int x0, int y0, int x1, int y1)
        {
            int dx = x1 - x0;
            int dy = y1 - y0;
            int yi = 1;
            if (dy < 0)
            {
                yi = -1;
                dy = -dy;
            }
            int D = (2 * dy) - dx;
            int y = y0;
            List<KeyValuePair<int, int>> res = new List<KeyValuePair<int, int>>();

            for (int x = x0; x != x1; x++)
            {
                res.Add(new KeyValuePair<int, int>(x, y));
                if (D > 0)
                {
                    y += yi;
                    D += (2 * (dy - dx));
                }
                else
                {
                    D += (2 * dy);
                }
            }

            return res;
        }

        private List<KeyValuePair<int, int>> PlotLineHigh(int x0, int y0, int x1, int y1)
        {
            int dx = x1 - x0;
            int dy = y1 - y0;
            int xi = 1;
            List<KeyValuePair<int, int>> res = new List<KeyValuePair<int, int>>();

            if (dx < 0)
            {
                xi = -1;
                dx = -dx;
            }

            int D = (2 * dx) - dy;
            int x = x0;

            for (int y = y0; y != y1; y++)
            {
                res.Add(new KeyValuePair<int, int>(x, y));
                if (D > 0)
                {
                    x += xi;
                    D += (2 * (dx - dy));
                }
                else
                {
                    D += (2 * dx);
                }
            }

            return res;
        }

        private List<KeyValuePair<int, int>> GetListOfCoordinatesBetweenTwoPoint(int x0, int y0, int x1, int y1)
        {
            if (Math.Abs(y1 - y0) < Math.Abs(x1 - x0))
            {
                if (x0 > x1)
                    return PlotLineLow(x1, y1, x0, y0);
                return PlotLineLow(x0, y0, x1, y1);
            }
            else
            {
                if (y0 > y1)
                    return PlotLineHigh(x1, y1, x0, y0);
                return PlotLineHigh(x0, y0, x1, y1);
            }
        }
#endregion


        public void Show(List<Personnage> listP)
        {
            bool found;
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    {
                        found = false;
                        foreach (Personnage p in listP)
                        {
                            if (p.PosX == i && p.PosY == j)
                            {
                                found = true;
                                Console.Write($"{p.Affichage} ");
                                break;
                            }
                        }
                        if (!found)
                            Console.Write($"{map[i, j].Affichage} ");
                    }
                }
                Console.Write('\n');
            }
        }
    }
}