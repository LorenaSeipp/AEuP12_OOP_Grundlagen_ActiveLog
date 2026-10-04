namespace ActiveLog.Src;

public class Kraftuebung : Uebung
{

    public Kraftuebung(string name) : base(name)
    {
    }

    public Kraftuebung(string name, int intensitaet) : base(name, intensitaet)
    {
    }
    public override string Statuszeile()
    {
        string statuszeile;

        if (IstAktiv)
        {
            statuszeile = $"{Name}: aktiv ({Intensitaet} %)";
        }
        else
        {
            statuszeile = $"{Name}: pausiert";
        }

        return statuszeile;
    }
}