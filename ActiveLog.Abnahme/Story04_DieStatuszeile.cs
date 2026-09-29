using Xunit;
using ActiveLog.Src;

namespace ActiveLog.Abnahme;

public class Story04_DieStatuszeile
{
    [Fact(DisplayName = "Story 4: Eine pausierte Übung meldet 'Name: pausiert'")]
    public void Pausierte_Uebung()
    {
        var uebung = Hilfe.Neu("Kraftuebung", "Bankdrücken");
        var zeile = Hilfe.Rufe(uebung, "Statuszeile");
        Assert.True(Equals(zeile, "Bankdrücken: pausiert"),
            $"Erwartet wird exakt \"Bankdrücken: pausiert\" — geliefert wurde: '{zeile}'. " +
            "Format-Tabelle auf der Story-Karte beachten (Doppelpunkt, Leerzeichen).");
    }

    [Fact(DisplayName = "Story 4: Eine aktive Übung meldet 'Name: aktiv (I %)'")]
    public void Aktive_Uebung()
    {
        var uebung = Hilfe.Neu("Kraftuebung", "Bankdrücken");
        Hilfe.Rufe(uebung, "Aktivieren");
        Hilfe.Setze(uebung, "Intensitaet", 75);
        var zeile = Hilfe.Rufe(uebung, "Statuszeile");
        Assert.True(Equals(zeile, "Bankdrücken: aktiv (75 %)"),
            $"Erwartet wird exakt \"Bankdrücken: aktiv (75 %)\" — geliefert wurde: '{zeile}'. " +
            "Format-Tabelle auf der Story-Karte beachten (Klammern, Leerzeichen vor %).");
    }

    [Fact(DisplayName = "Story 4: Frisch aktiviert trainiert die Übung mit 100 %")]
    public void Frisch_aktiviert()
    {
        var uebung = Hilfe.Neu("Kraftuebung", "Kniebeuge");
        Hilfe.Rufe(uebung, "Aktivieren");
        var zeile = Hilfe.Rufe(uebung, "Statuszeile");
        Assert.True(Equals(zeile, "Kniebeuge: aktiv (100 %)"),
            $"Erwartet wird exakt \"Kniebeuge: aktiv (100 %)\" — geliefert wurde: '{zeile}'.");
    }
}
