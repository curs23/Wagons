using System;

namespace Wagons
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine($"Всего вагонов {rw.CountWagons}");
            //Console.WriteLine($"Включен свет {rw.CountOnLight()}");
            //Console.WriteLine($"Выключен свет {rw.CountOffLight()}");

            Algoritm_1(50);

            Console.ReadKey();
        }

        static Res Algoritm_1(int countWagons)
        {
            Train rw = new Train(countWagons);
            Wagon startWagon = rw.NextWagon();
            Wagon currentWagon = startWagon;

            Steps steps = new Steps();
            startWagon.OnLight();

            while(true)
            {
                currentWagon = rw.NextWagon();
                steps.Forward();

                if (currentWagon.Light)
                {

                }
            }



            return null;
        }

        class Res
        {
            int CountSteps;
            int CountWagons;
        }

        class Steps
        {
            public int Position = 0;
            public int All = 0;

            public void Forward()
            {
                Position++;
                All++;
            }

            public void Back()
            {
                Position--;
                All++;
            }

        }
    }
}
