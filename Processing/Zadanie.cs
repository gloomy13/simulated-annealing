using System;
using System.Collections.Generic;

namespace SimulatedScheduling.Processing
{
    public class Zadanie : ICloneable
    {
        public uint Indeks { get; set; }
        public uint CzasWykonania { get; set; } = 0;
        public uint CzasRozpoczecia { get; set; } = 0;
        public uint CzasZakonczenia { get; set; } = 0;
        public List<Zadanie> ZalezneOd { get; set; } = new List<Zadanie>();

        public Zadanie(uint indeks)
        {
            Indeks = indeks;
        }

        public void DodajZależności(params Zadanie[] zalezneOd)
        {
            foreach (Zadanie zaleznosc in zalezneOd)
            {
                if (this != zaleznosc)
                    ZalezneOd.Add((Zadanie)zaleznosc.Clone());
                else
                    throw new Exception("Zadanie nie może byc zalezne od samego siebie\n");
            }
        }

        public object Clone()
        {
            Zadanie klon = new Zadanie(Indeks)
            {
                ZalezneOd = ZalezneOd,
                CzasRozpoczecia = CzasRozpoczecia,
                CzasWykonania = CzasWykonania,
                CzasZakonczenia = CzasZakonczenia
            };

            return klon;
        }
    }
}