using SimulatedScheduling.Processing;
using System;
using System.Collections.Generic;

namespace SimulatedScheduling.Program
{
    public class Program
    {
        private static Rozwiazanie Scheduling(List<Procesor> procesory, List<Zadanie> zadania)
        {
            const uint initialTemp = 100;
            uint temp = initialTemp;
            var liczbaProcesorow = procesory.Count;
            Random rnd = new Random();
            Rozwiazanie najlepszeRozwiazanie = new Rozwiazanie
            {
                czas = uint.MaxValue
            };

            while (true)
            {
                var tmp = new Rozwiazanie();
                List<Procesor> listaProcesorow = new List<Procesor>();
                foreach (var procesor in procesory)
                {
                    listaProcesorow.Add((Procesor)procesor.Clone());
                }

                var kopiaListyZadan = new List<Zadanie>(zadania);
                while (kopiaListyZadan.Count > 0) // po kolei losowo sa przydzielane zadania do procesorow
                {
                    var wylosowaneZadanie = kopiaListyZadan[rnd.Next(kopiaListyZadan.Count)];
                    tmp.DodajKrok(new Tuple<Procesor, Zadanie>(listaProcesorow[rnd.Next(liczbaProcesorow)], wylosowaneZadanie));
                    kopiaListyZadan.Remove(wylosowaneZadanie);
                }

                tmp.ZliczCzas();

                if (tmp.czas < najlepszeRozwiazanie.czas)
                {
                    najlepszeRozwiazanie = tmp;
                }
                else
                {
                    var rn = rnd.Next(101);
                    if (rn > initialTemp - temp)
                    {
                        najlepszeRozwiazanie = tmp;
                    }
                }

                Console.WriteLine(najlepszeRozwiazanie.DrukRozwiązania() + " CZAS: " + najlepszeRozwiazanie.czas);

                if (temp == 0)
                {
                    break;
                }

                temp--;
            }
            return najlepszeRozwiazanie;
        }

        private static void Main(string[] args)
        {
            // Scenariusz z zadania

            Zadanie j0 = new Zadanie(0);
            Zadanie j1 = new Zadanie(1);
            j1.DodajZależności(j0);
            Zadanie j2 = new Zadanie(2);

            Procesor p0 = new Procesor(0);
            Procesor p1 = new Procesor(1);

            p0.DodajZadanie((Zadanie)j0.Clone(), 2);
            p0.DodajZadanie((Zadanie)j1.Clone(), 5);
            p0.DodajZadanie((Zadanie)j2.Clone(), 4);

            p1.DodajZadanie((Zadanie)j0.Clone(), 2);
            p1.DodajZadanie((Zadanie)j1.Clone(), 6);
            p1.DodajZadanie((Zadanie)j2.Clone(), 1);

            var procesory = new List<Procesor>
        {
            p0,
            p1
        };

            var zadania = new List<Zadanie>
        {
            j0,
            j1,
            j2
        };

            Scheduling(procesory, zadania);

            Console.ReadLine();
        }
    }
}