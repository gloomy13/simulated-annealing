using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rozwiazanie
{
    public List<Tuple<Procesor, Zadanie>> paryPZ = new List<Tuple<Procesor, Zadanie>>();
    public uint czas = 0;
    public Rozwiazanie() { }
    public void DodajKrok(Tuple<Procesor, Zadanie> para) //dodaje krok w kolejnosci do rozwiazania (para procesor/zadanie)
    {
        var para2 = new Tuple<Procesor, Zadanie>(para.Item1, para.Item1.WykonajZadanie(para.Item2));
        paryPZ.Add(para2);
    }
    public uint ZliczCzas()
    {
        foreach (var para in paryPZ)
            if (para.Item2.zalezneOd.Count != 0)
                foreach (var para2 in paryPZ)
                    foreach (var zaleznosc in para.Item2.zalezneOd)
                        if (para2.Item2.indeks == zaleznosc.indeks)
                            if (para2.Item2.czasZakonczenia > para.Item2.czasRozpoczecia)
                            {
                                uint przesuniecie = para2.Item2.czasZakonczenia - para.Item2.czasRozpoczecia; // czekanie zwieksza czas pracy procesora
                                para.Item1.PrzesuniecieZadanWCzasie(przesuniecie, para.Item2.czasRozpoczecia);
                            }
        czas = paryPZ.Select(x => x.Item1).Distinct().Max(x => x.czas);
        return czas; // czas pracy porcesora który kończy ostatni
    }
    public string DrukRozwiązania()
    {
        string kolejność = "";
        foreach (var el in paryPZ)
        {
            kolejność += "[p:" + el.Item1.indeks + " - z:" + el.Item2.indeks + "] ";
        }
        return kolejność;
    }
}
