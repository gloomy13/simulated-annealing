using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Procesor : ICloneable
{
    List<Zadanie> Zadania = new List<Zadanie>();
    public uint indeks;
    public uint czas = 0;

    public Procesor(uint indeks)
    {
        this.indeks = indeks;
    }
    public void DodajZadanie(Zadanie zadanie, uint czasWykonania)
    {
        zadanie.czasWykonania = czasWykonania;
        Zadania.Add(zadanie);
    }
    public Zadanie WykonajZadanie(Zadanie zadanie)
    {
        Zadania[(int)zadanie.indeks].czasRozpoczecia = czas;
        czas += Zadania[(int)zadanie.indeks].czasWykonania;
        Zadania[(int)zadanie.indeks].czasZakonczenia = czas;
        return Zadania[(int)zadanie.indeks];
        //zadanie.wykonane = true; to trzeba bedzie zrobic w kolejkowaniu
    }
    public void PrzesuniecieZadanWCzasie(uint przesuniecie, uint odKiedy) // w przypadku czakania na zakonczenie procesu
    {
        czas += przesuniecie;
        foreach (var zadanie in Zadania)
        {
            if (zadanie.czasRozpoczecia >= odKiedy)
            {
                zadanie.czasRozpoczecia += przesuniecie;
                zadanie.czasZakonczenia += przesuniecie;
            }
        }
    }
    public object Clone()
    {
        Procesor klon = new Procesor(this.indeks);
        klon.Zadania = this.Zadania;

        return klon;
    }
}
