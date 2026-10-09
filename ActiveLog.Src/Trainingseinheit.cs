namespace ActiveLog.Src;

public class Trainingseinheit 
{

    public string Name { get; }

    private List<Uebung> uebungen = new List<Uebung>();
    
    public int AnzahlUebungen { get; private set; }

    public Trainingseinheit(string name)
    {
        Name = name; 
        AnzahlUebungen = 0; 

    }

    public void UebungHinzufuegen(Uebung uebung)
    {
        uebungen.Add(uebung);
        AnzahlUebungen++;
    }

    public void AllesDeaktivieren()
    {
        foreach(Uebung uebung in uebungen)
        {
            uebung.Deaktivieren();
        }
    }

    public string Statusbericht()
    {
        if (AnzahlUebungen != 0)
        {
                string uebungBericht = string.Join(Environment.NewLine,
                    uebungen.Select(u =>
                        $"  - {u.Statuszeile()}"));

            return $"Einheit {Name}:\n {uebungBericht}";
        }

        return $"Einheit {Name} hat keine Übungen.";
    }
}