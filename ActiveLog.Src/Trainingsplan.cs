namespace ActiveLog.Src;

public class Trainingsplan
{
    protected List<Trainingseinheit> _trainingseinheitList = new List<Trainingseinheit>();
    
    public string Bezeichnung { get; }

    public int AnzahlEinheiten => _trainingseinheitList.Count;

    public Trainingsplan(string bezeichnung)
    {
        Bezeichnung = bezeichnung;
    }

public Trainingseinheit EinheitAnlegen(string name)
{
    if (_trainingseinheitList.Any(t => t.Name == name))
        throw new ArgumentException($"Einheit '{name}' existiert bereits.");

    Trainingseinheit einheit = new Trainingseinheit(name);
    _trainingseinheitList.Add(einheit);
    
    return einheit;
}

    public void AllesDeaktivieren()
    {
        foreach (Trainingseinheit t in _trainingseinheitList)
        {
            t.AllesDeaktivieren();
        }
    }
    
    public void AllesAktivieren()
    {
        foreach (Trainingseinheit t in _trainingseinheitList)
        {
            t.AllesAktivieren();
        }
    }

    public string Gesamtbericht()
    {
        string gesamtbericht = $"Trainingsplan {Bezeichnung}";

        if (_trainingseinheitList.Count == 0)
        {
            gesamtbericht += $"\nKeine Trainingseinheiten vorhanden";
        }

        foreach (Trainingseinheit t in _trainingseinheitList)
        {
            gesamtbericht += $"\n{t.Statusbericht()}";
        }

        return gesamtbericht;
    }
}