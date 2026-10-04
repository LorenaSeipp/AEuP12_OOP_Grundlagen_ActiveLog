namespace ActiveLog.Src;

public class Schwimmeinheit : Uebung, ISensor
{
    private int _anzahlBahnen = 0;

    public int AnzahlBahnen
    {
        get => _anzahlBahnen;
        private set => _anzahlBahnen = value;
    }


    public Schwimmeinheit(string name) : base(name)
    {
    }

    public override string Statuszeile()
    {
        string statuszeile;

        if (IstAktiv)
        {
            statuszeile = $"{Name}: aktiv ({_anzahlBahnen} Bahnen)";
        }
        else
        {
            statuszeile = $"{Name}: pausiert";
        }

        return statuszeile;
    }

    public string LiesMesswert()
    {
        return $"{_anzahlBahnen} Bahnen";
    }

    public int MeldeBahn()
    {
        if (IstAktiv)
        {
            AnzahlBahnen++;
        }

        return _anzahlBahnen;
    }
}