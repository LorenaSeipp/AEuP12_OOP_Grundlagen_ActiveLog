using Xunit;
using ActiveLog.Src;

namespace ActiveLog.Abnahme;

public class Story01_DieErsteUebung
{
    [Fact(DisplayName = "Story 1: Es gibt eine Klasse 'Kraftuebung'")]
    public void Klasse_Kraftuebung_existiert()
    {
        Hilfe.Typ("Kraftuebung");
    }

    [Fact(DisplayName = "Story 1: Eine Kraftübung bekommt ihren Namen beim Anlegen")]
    public void Kraftuebung_bekommt_ihren_Namen_beim_Anlegen()
    {
        var uebung = Hilfe.Neu("Kraftuebung", "Bankdrücken");
        var name = Hilfe.Lies(uebung, "Name");
        Assert.True(Equals(name, "Bankdrücken"),
            $"Nach dem Anlegen mit \"Bankdrücken\" muss Name genau \"Bankdrücken\" liefern — geliefert wurde: '{name}'.");
    }

    [Fact(DisplayName = "Story 1: Der Name ist nach dem Anlegen unveränderlich")]
    public void Name_ist_nach_dem_Anlegen_unveraenderlich()
    {
        Hilfe.NurLesbar("Kraftuebung", "Name");
    }

    [Fact(DisplayName = "Story 1: Das Innenleben der Übung ist gekapselt (keine öffentlichen Felder)")]
    public void Innenleben_ist_gekapselt()
    {
        Hilfe.KeineOeffentlichenFelder("Kraftuebung");
    }
}
