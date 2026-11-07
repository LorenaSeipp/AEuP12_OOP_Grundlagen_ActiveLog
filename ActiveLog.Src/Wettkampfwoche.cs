namespace ActiveLog.Src;

public class Wettkampfwoche : Trainingsplan, ISzene
{

    public Wettkampfwoche(string name) : base(name)
    {
    }


    public void Anwenden(Einstellung einstellung)
    {
        foreach (Trainingseinheit t in _trainingseinheitList)
        {
            t.AllesAktivieren();
            t.Einstellen(einstellung);
        }
    }
}