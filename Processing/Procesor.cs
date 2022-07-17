using System;
using System.Collections.Generic;
using System.Linq;

namespace SimulatedScheduling.Processing
{
    public class Procesor : ICloneable
    {
        public uint Indeks { get; set; }
        public uint Czas { get; set; } = 0;
        public List<Zadanie> Zadania { get; set; } = new List<Zadanie>();

        public Procesor(uint indeks)
        {
            Indeks = indeks;
        }

        public void DodajZadanie(Zadanie zadanie, uint czasWykonania)
        {
            zadanie.CzasWykonania = czasWykonania;
            Zadania.Add(zadanie);
        }

        public Zadanie WykonajZadanie(Zadanie zadanie)
        {
            Zadania[(int)zadanie.Indeks].CzasRozpoczecia = Czas;
            Czas += Zadania[(int)zadanie.Indeks].CzasWykonania;
            Zadania[(int)zadanie.Indeks].CzasZakonczenia = Czas;
            return Zadania[(int)zadanie.Indeks];
            //zadanie.wykonane = true; to trzeba bedzie zrobic w kolejkowaniu
        }

        public void PrzesuniecieZadanWCzasie(uint przesuniecie, uint odKiedy) // w przypadku czakania na zakonczenie procesu
        {
            Czas += przesuniecie;
            foreach (var zadanie in Zadania)
            {
                if (zadanie.CzasRozpoczecia >= odKiedy)
                {
                    zadanie.CzasRozpoczecia += przesuniecie;
                    zadanie.CzasZakonczenia += przesuniecie;
                }
            }
        }

        public object Clone()
        {
            Procesor klon = new Procesor(Indeks)
            {
                Zadania = Zadania.Select(x => (Zadanie)x.Clone()).ToList()
            };

            return klon;
        }
    }
}