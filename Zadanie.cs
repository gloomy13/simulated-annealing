using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Zadanie : ICloneable
{
    public List<Zadanie> zalezneOd = new List<Zadanie>(); // zadania z tej listy muszą zostać wykonane przed tym zadaniem
    public uint indeks;
    public uint czasWykonania = 0;
    public uint czasRozpoczecia = 0;
    public uint czasZakonczenia = 0;
    public Zadanie(uint indeks)
    {
        this.indeks = indeks;
    }
    public void DodajZależności(params Zadanie[] zalezneOd)
    {
        foreach (Zadanie zaleznosc in zalezneOd)
        {
            if (this != zaleznosc)
                this.zalezneOd.Add((Zadanie)zaleznosc.Clone());
            else
                throw new Exception("Zadanie nie może byc zalezne od samego siebie\n");
        }
    }
    public object Clone()
    {
        Zadanie klon = new Zadanie(this.indeks);
        klon.zalezneOd = this.zalezneOd;
        klon.czasRozpoczecia = this.czasRozpoczecia;
        klon.czasWykonania = this.czasWykonania;
        klon.czasZakonczenia = this.czasZakonczenia;

        return klon;
    }
}
