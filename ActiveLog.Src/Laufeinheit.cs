namespace ActiveLog.Src;

public class Laufeinheit : Uebung, ISensor
{
    private int _zielpuls = 130;
    private int _messwert = 130; 
    public int Messwert { get => _messwert; private set =>  _messwert = value; }

    public int Zielpuls
    {
        get => _zielpuls;
        set => _zielpuls = Math.Clamp(value, 90, 180);
    }

    public Laufeinheit(string name) : base(name)
    {
    }
    
    public Laufeinheit(string name,  int zielpuls) : base(name)
    {
        Zielpuls = zielpuls;
    }

    public override string Statuszeile()
    {
        string statuszeile;

        if (IstAktiv)
        {
            statuszeile = $"{Name}: aktiv (Zielpuls {_zielpuls} bpm)";
        }
        else
        {
            statuszeile = $"{Name}: pausiert";
        }

        return statuszeile;
    }
    
    public string LiesMesswert()
    {
        return $"{_messwert} bpm"; 
    }

    public void MessePuls(int schlaege)
    {
        if (IstAktiv)
        {
            _messwert = schlaege;
        }
    }
    public override void Einstellen(Einstellung einstellung)
    {
        base.Einstellen(einstellung);
        if (einstellung.Zielpuls != null)
            Zielpuls = einstellung.Zielpuls.Value;
    }
}