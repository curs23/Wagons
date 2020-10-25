using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace Wagons
{
    class Train
    {
        int CountWagons { get { return Wagons.Length; }}
        
        Wagon[] Wagons { get; set; }
        private int currentWagon;

        public Train(int countWagons)
        {
            Wagons = new Wagon[countWagons];
            for (int i = 0; i < countWagons; i++)
            {
                Wagons[i] = new Wagon();
            }

            RandomLight();
            Random rnd = new Random();
            currentWagon = rnd.Next(0, CountWagons);
        }

        public Wagon NextWagon()
        {
            if (currentWagon == CountWagons-1)
            {
                currentWagon = 0;
            }
            else
            {
                currentWagon++;
            }

            return Wagons[currentWagon];
        }

        public Wagon PreviousWagon()
        {
            if (currentWagon == 0)
            {
                currentWagon = CountWagons-1;
            }
            else
            {
                currentWagon--;
            }

            return Wagons[currentWagon];
        }

        void RandomLight()
        {
            Random rnd = new Random();
            for (int i = 0; i < CountWagons; i++)
            {
                Wagons[i].Light = rnd.Next(0, 2) == 1;
            }
        }

        void OffLight()
        {
            for (int i = 0; i < CountWagons; i++)
            {
                Wagons[i].Light = false;
            }
        }

        void OnLight()
        {
            for (int i = 0; i < CountWagons; i++)
            {
                Wagons[i].Light = true;
            }
        }

        int CountOnLight()
        {
            return Wagons.Count(x => x.Light);
        }

        int CountOffLight()
        {
            return Wagons.Count(x => !x.Light);
        }
    }
}
