namespace ActiveLog.Src;

public class Schwimmeinheit : Uebung, ISensor
{
    private int _anzahlBahnen = 0;

    public int SollAnzahlBahnen { get; private set; }

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

        else if (IstAktiv && SollAnzahlBahnen != 0)
        {
            statuszeile = $"{Name}: aktiv ({_anzahlBahnen} Bahnen geschwommen von {SollAnzahlBahnen})";
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

    public override void Einstellen(Einstellung einstellung)
    {
        base.Einstellen(einstellung);
        if (einstellung.SollAnzahlBahnen != null)
            SollAnzahlBahnen = einstellung.SollAnzahlBahnen.Value;
    }
}