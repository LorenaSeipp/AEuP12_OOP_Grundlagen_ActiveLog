namespace ActiveLog.Src;

public class Trainingsplan
{
    private List<Trainingseinheit> _trainingseinheitList = new List<Trainingseinheit>();

    private int _anzahlEinheiten;
    public string Bezeichnung { get; }

    public int AnzahlEinheiten
    {
        get => _anzahlEinheiten;
    }

    public Trainingsplan(string bezeichnung)
    {
        Bezeichnung = bezeichnung;
    }

    public Trainingseinheit EinheitAnlegen(string name)
    {
        Trainingseinheit trainingseinheit = new Trainingseinheit(name);
        _trainingseinheitList.Add(trainingseinheit);
        _anzahlEinheiten++;

        return trainingseinheit;
    }

    public void AllesDeaktivieren()
    {
        foreach (Trainingseinheit t in _trainingseinheitList)
        {
            t.AllesDeaktivieren();
        }
    }

    public string Gesamtbericht()
    {
        string gesamtbericht = $"Trainingsplan Musterplan Messe";

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