namespace ActiveLog.Src;

public class Kraftuebung
{
    private int _intensitaet;
    public string Name { get; }

    public bool IstAktiv { get; private set; }


    public int Intensitaet
    {
        get => _intensitaet;
        set => _intensitaet = Math.Clamp(value, 0, 100);
    }

    public Kraftuebung(string name)
    {
        Name = name;
        Intensitaet = 100;
    }

    public Kraftuebung(string name, int intensitaet)
    {
        Name = name;
        Intensitaet = intensitaet;
    }

    public void Aktivieren()
    {
        IstAktiv = true;
    }

    public void Deaktivieren()
    {
        IstAktiv = false;
    }

    public string Statuszeile()
    {
        string statuszeile;

        if (IstAktiv)
        {
            statuszeile = $"{Name}: aktiv ({_intensitaet} %)";
        }
        else
        {
            statuszeile = $"{Name}: pausiert";
        }

        return statuszeile;
    }
}