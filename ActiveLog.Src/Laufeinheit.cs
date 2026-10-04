namespace ActiveLog.Src;

public class Laufeinheit : Uebung
{
    private int _zielpuls = 130;

    public int Zielpuls
    {
        get => _zielpuls;
        set => _zielpuls = Math.Clamp(value, 90, 180);
    }

    public Laufeinheit(string name) : base(name)
    {
    }

    public Laufeinheit(string name, int intensitaet) : base(name, intensitaet)
    {
    }

    public Laufeinheit(string name, int intensitaet, int zielpuls) : base(name, intensitaet)
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
}