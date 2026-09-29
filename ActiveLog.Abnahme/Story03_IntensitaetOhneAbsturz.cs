using Xunit;
using ActiveLog.Src;

namespace ActiveLog.Abnahme;

public class Story03_IntensitaetOhneAbsturz
{
    [Fact(DisplayName = "Story 3: Ab Werk steht die Intensität auf 100 %")]
    public void Startwert_ist_100()
    {
        var uebung = Hilfe.Neu("Kraftuebung", "Bankdrücken");
        Assert.True(Equals(Hilfe.Lies(uebung, "Intensitaet"), 100),
            "Direkt nach dem Anlegen muss Intensitaet den Wert 100 liefern (Startwert laut Ticket).");
    }

    [Fact(DisplayName = "Story 3: Ein gültiger Wert wird übernommen")]
    public void Gueltiger_Wert_wird_uebernommen()
    {
        var uebung = Hilfe.Neu("Kraftuebung", "Bankdrücken");
        Hilfe.Setze(uebung, "Intensitaet", 55);
        Assert.True(Equals(Hilfe.Lies(uebung, "Intensitaet"), 55),
            "Nach dem Setzen auf 55 muss Intensitaet den Wert 55 liefern.");
    }

    [Fact(DisplayName = "Story 3: Werte über 100 werden auf 100 begrenzt")]
    public void Zu_grosse_Werte_werden_begrenzt()
    {
        var uebung = Hilfe.Neu("Kraftuebung", "Bankdrücken");
        Hilfe.Setze(uebung, "Intensitaet", 1000);
        Assert.True(Equals(Hilfe.Lies(uebung, "Intensitaet"), 100),
            "Der Wert 1000 (Support-Ticket #4231!) muss auf 100 begrenzt werden — geliefert wurde ein anderer Wert.");
    }

    [Fact(DisplayName = "Story 3: Negative Werte werden auf 0 begrenzt")]
    public void Negative_Werte_werden_begrenzt()
    {
        var uebung = Hilfe.Neu("Kraftuebung", "Bankdrücken");
        Hilfe.Setze(uebung, "Intensitaet", -20);
        Assert.True(Equals(Hilfe.Lies(uebung, "Intensitaet"), 0),
            "Der Wert -20 muss auf 0 begrenzt werden.");
    }

    [Fact(DisplayName = "Story 3: Die Intensität ist gekapselt (kein öffentliches Feld)")]
    public void Intensitaet_ist_gekapselt()
    {
        Hilfe.KeineOeffentlichenFelder("Kraftuebung");
    }
}
