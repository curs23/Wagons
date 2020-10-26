using System;
using System.Collections.Generic;
using System.Linq;

namespace Wagons
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                int countWagons = 100;
                int countTrails = 100;





                List<int> list_1 = new List<int>();
                List<int> list_2 = new List<int>();

                for (int i = 0; i < countTrails; i++)
                {
                    Train train_1 = new Train(countWagons);
                    Train train_2 = train_1.GetCopy();

                    Res res_1 = Algoritm_1(train_1);
                    Res res_2 = Algoritm_2(train_2);

                    list_1.Add(res_1.TotalWagonsPassed);
                    list_2.Add(res_2.TotalWagonsPassed);

                    Console.WriteLine($"Всего вагонов (1) {res_1.CountWagons}");
                    Console.WriteLine($"Всего пройдено вагонов (1) {res_1.TotalWagonsPassed}");
                    Console.WriteLine($"Всего вагонов (2) {res_2.CountWagons}");
                    Console.WriteLine($"Всего пройдено вагонов (2) {res_2.TotalWagonsPassed}");
                    Console.WriteLine("----------------------------------");
                }

                Console.WriteLine($"Среднее кол-во пройденных вагонов для {countWagons} вагонов (1): " + list_1.Select(x => x).Sum() / list_1.Count);
                Console.WriteLine($"Среднее кол-во пройденных вагонов для {countWagons} вагонов (2): " + list_2.Select(x => x).Sum() / list_2.Count);
            } while (Console.ReadKey().Key == ConsoleKey.Enter);
        }

        static Res Algoritm_1(Train train)
        {
            People people = new People(train);
            Wagon startWagon = people.CurrentWagon;
            startWagon.OnLight();
            people.GoBack();
            people.CurrentWagon.OffLight();
            people.GoForward();

            int countWagons = Tuda_1(people);

            while (true)
            {
                if (!startWagon.Light)
                {
                    return new Res() { CountWagons = countWagons - 1, TotalWagonsPassed = people.TotalWagonsPassed };
                }

                countWagons = Suda_1(people, countWagons);

                if (!startWagon.Light)
                {
                    return new Res() { CountWagons = countWagons - 1, TotalWagonsPassed = people.TotalWagonsPassed };
                }

                countWagons = Tuda_1(people, countWagons);
            }
        }

        static Res Algoritm_2(Train train)
        {
            People people = new People(train);
            Wagon startWagon = people.CurrentWagon;
            startWagon.OnLight();
            people.GoBack();
            people.CurrentWagon.OffLight();
            people.GoForward();


            Tuda_2(people);
            while (true)
            {
                if (TudaCheckLightOff(people, people.Position - 2))
                {
                    int potencialCountWagon = (people.Position + 1) / 2;

                    // проверка света стартвого вагона бегом назад
                    while(people.Position != 0)
                    {
                        people.GoBack();
                    }

                    if (people.CurrentWagon.Light)
                    {
                        // не прошла
                        Tuda_2(people);
                    }
                    else
                    {
                        // прошла - вывод результата
                        return new Res() { CountWagons = potencialCountWagon, TotalWagonsPassed = people.TotalWagonsPassed};
                    }
                }
                else
                {
                    Tuda2_2(people);
                }
            }
        }

        static void Tuda_2(People people)
        {
            bool lightHall = false;

            while (true)
            {
                people.GoForward();

                if (people.CurrentWagon.Light)
                {
                    lightHall = true;
                    people.CurrentWagon.OffLight();
                }
                else
                {
                    if (lightHall)
                    {
                        return;
                    }
                }
            }
        }

        static void Tuda2_2(People people)
        {
            people.CurrentWagon.OffLight();
            while (true)
            {
                people.GoForward();

                if (people.CurrentWagon.Light)
                {
                    people.CurrentWagon.OffLight();
                }
                else
                {
                    return;
                }
            }
        }

        static bool TudaCheckLightOff(People people, int countWagons)
        {
            for (int i = 0; i < countWagons; i++)
            {
                people.GoForward();
                if (people.CurrentWagon.Light) return false;
            }

            return true;
        }


        static int Tuda_1(People people, int countApprovedWagons = 0)
        {
            bool lightHall = false;
            int countWagons;

            for (int i = 0; i < countApprovedWagons; i++)
            {
                people.GoForward();
            }

            while (true)
            {
                people.GoForward();

                if (people.CurrentWagon.Light)
                {
                    lightHall = true;
                }
                else
                {
                    if (lightHall)
                    {
                        countWagons = people.Position;
                        for (int i = 0; i < countWagons; i++)
                        {
                            people.GoBack();
                            if (people.Position != 0) people.CurrentWagon.OffLight();
                        }
                        return countWagons;
                    }
                }
            }
        }

        static int Suda_1(People people, int countApprovedWagons = 0)
        {
            bool lightHall = false;
            int countWagons;

            for (int i = 0; i < countApprovedWagons; i++)
            {
                people.GoBack();
            }

            while (true)
            {
                people.GoBack();

                if (people.CurrentWagon.Light)
                {
                    lightHall = true;
                }
                else
                {
                    if (lightHall)
                    {
                        countWagons = Math.Abs(people.Position);
                        for (int i = 0; i < countWagons; i++)
                        {
                            people.GoForward();
                            if (people.Position != 0) people.CurrentWagon.OffLight();
                        }
                        return countWagons;
                    }
                }
            }
        }

        class Res
        {
            public int TotalWagonsPassed;
            public int CountWagons;
        }

        class People
        {
            Train Train;
            public Wagon CurrentWagon;
            public int Position = 0;
            public int TotalWagonsPassed = 0;

            public People(Train train)
            {
                Train = train;
                CurrentWagon = train.NextWagon();
            }

            public void GoForward()
            {
                CurrentWagon = Train.NextWagon();
                Position++;
                TotalWagonsPassed++;
            }

            public void GoBack()
            {
                CurrentWagon = Train.PreviousWagon();
                Position--;
                TotalWagonsPassed++;
            }

        }


    
    }
}
