namespace ActiveLog.Src;

public class Kraftuebung
{
    public string Name { get; }

    public bool IstAktiv { get; private set; }

    public Kraftuebung(string name)
    {
        Name = name;
    }

    public void Aktivieren()
    {
        IstAktiv = true;
    }


    public void Deaktivieren()
    {
        IstAktiv = false;
    }
}