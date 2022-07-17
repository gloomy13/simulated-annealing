using System;
using System.Collections.Generic;
using System.Linq;

namespace SimulatedScheduling.Processing
{
    public class Rozwiazanie
    {
        public List<Tuple<Procesor, Zadanie>> paryPZ = new List<Tuple<Procesor, Zadanie>>();
        public uint czas = 0;

        public Rozwiazanie()
        { }

        public void DodajKrok(Tuple<Procesor, Zadanie> para) //dodaje krok w kolejnosci do rozwiazania (para procesor/zadanie)
        {
            var para2 = new Tuple<Procesor, Zadanie>(para.Item1, para.Item1.WykonajZadanie(para.Item2));
            paryPZ.Add(para2);
        }

        public uint ZliczCzas()
        {
            foreach (var para in paryPZ)
            {
                if (para.Item2.ZalezneOd.Count != 0)
                {
                    foreach (var przesuniecie in from para2 in paryPZ
                                                 from zaleznosc in para.Item2.ZalezneOd
                                                 where para2.Item2.Indeks == zaleznosc.Indeks
                                                     && para2.Item2.CzasZakonczenia > para.Item2.CzasRozpoczecia
                                                 let przesuniecie = para2.Item2.CzasZakonczenia - para.Item2.CzasRozpoczecia // czekanie zwieksza czas pracy procesora
                                                 select przesuniecie)
                    {
                        para.Item1.PrzesuniecieZadanWCzasie(przesuniecie, para.Item2.CzasRozpoczecia);
                    }
                }
            }

            czas = paryPZ.Select(x => x.Item1).Distinct().Max(x => x.Czas);
            return czas; // czas pracy porcesora który kończy ostatni
        }

        public string DrukRozwiązania()
        {
            string kolejność = "";
            foreach (var el in paryPZ)
            {
                kolejność += $"[p:{el.Item1.Indeks} - z:{el.Item2.Indeks}] ";
            }
            return kolejność;
        }
    }
}