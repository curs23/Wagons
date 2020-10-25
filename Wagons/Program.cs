using System;

namespace Wagons
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Train train = new Train(50);
                //train.OnLight();

                Res res = Algoritm_1(train);

                Console.WriteLine($"Всего вагонов {res.CountWagons}");
                Console.WriteLine($"Всего пройдено вагонов {res.TotalWagonsPassed}");
                Console.WriteLine();
                
            } while (Console.ReadKey().Key == ConsoleKey.Enter);
        }

        static Res Algoritm_1(Train train)
        {
            Movement movement = new Movement(train);
            Wagon startWagon = movement.CurrentWagon;
            startWagon.OnLight();
            movement.Back();
            movement.CurrentWagon.OffLight();
            movement.Forward();

            int countWagons = Tuda(movement);

            while (true)
            {
                if (!startWagon.Light)
                {
                    return new Res() { CountWagons = countWagons - 1, TotalWagonsPassed = movement.TotalWagonsPassed };
                }

                countWagons = Suda(movement, countWagons);

                if (!startWagon.Light)
                {
                    return new Res() { CountWagons = countWagons - 1, TotalWagonsPassed = movement.TotalWagonsPassed };
                }

                countWagons = Tuda(movement, countWagons);
            }
        }

        static int Tuda(Movement movement, int countApprovedWagons = 0)
        {
            bool lightHall = false;
            int countWagons;

            for (int i = 0; i < countApprovedWagons; i++)
            {
                movement.Forward();
            }

            while (true)
            {
                movement.Forward();

                if (movement.CurrentWagon.Light)
                {
                    lightHall = true;
                }
                else
                {
                    if (lightHall)
                    {
                        countWagons = movement.Position;
                        for (int i = 0; i < countWagons; i++)
                        {
                            movement.Back();
                            if (movement.Position != 0) movement.CurrentWagon.OffLight();
                        }
                        return countWagons;
                    }
                }
            }
        }

        static int Suda(Movement movement, int countApprovedWagons = 0)
        {
            bool lightHall = false;
            int countWagons;

            for (int i = 0; i < countApprovedWagons; i++)
            {
                movement.Back();
            }

            while (true)
            {
                movement.Back();

                if (movement.CurrentWagon.Light)
                {
                    lightHall = true;
                }
                else
                {
                    if (lightHall)
                    {
                        countWagons = Math.Abs(movement.Position);
                        for (int i = 0; i < countWagons; i++)
                        {
                            movement.Forward();
                            if (movement.Position != 0) movement.CurrentWagon.OffLight();
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

        class Movement
        {
            Train Train;
            public Wagon CurrentWagon;
            public int Position = 0;
            public int TotalWagonsPassed = 0;

            public Movement(Train train)
            {
                Train = train;
                CurrentWagon = train.NextWagon();
            }

            public void Forward()
            {
                CurrentWagon = Train.NextWagon();
                Position++;
                TotalWagonsPassed++;
            }

            public void Back()
            {
                CurrentWagon = Train.PreviousWagon();
                Position--;
                TotalWagonsPassed++;
            }

        }
    }
}
