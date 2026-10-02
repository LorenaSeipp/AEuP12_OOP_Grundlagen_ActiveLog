using Xunit;

namespace ActiveLog.Abnahme;

public class Sprint01_Mobilityuebung
{
    [Fact(DisplayName = "Sprint 01: Es gibt eine Klasse 'Mobilityuebung'")]
    public void Klasse_Mobilityuebung_existiert()
    {
        Hilfe.Typ("Mobilityuebung");
    }

    [Fact(DisplayName = "Sprint 01: Eine Mobility-Übung bekommt ihren Namen beim Anlegen")]
    public void Mobilityuebung_bekommt_ihren_Namen_beim_Anlegen()
    {
        var uebung = Hilfe.Neu("Mobilityuebung", "Dehnen");
        var name = Hilfe.Lies(uebung, "Name");
        Assert.True(Equals(name, "Dehnen"),
            $"Nach dem Anlegen mit \"Dehnen\" muss Name genau \"Dehnen\" liefern — geliefert wurde: '{name}'.");
    }
    
    [Fact(DisplayName = "Sprint 01: Eine Mobility-Übung bekommt ihre Soll-Dauer beim Anlegen")]
    public void Mobilityuebung_bekommt_ihre_Soll_Dauer_beim_Anlegen()
    {
        var uebung = Hilfe.Neu("Mobilityuebung", "Dehnen", 75, 180);
        var solldauer = Hilfe.Lies(uebung, "SollDauerInSek");
        Assert.True(Equals(solldauer, 180),
            $"Nach dem Anlegen mit \"180\" muss SollDauerInSek genau \"180\" liefern — geliefert wurde: '{solldauer}'.");
    }


    [Fact(DisplayName = "Sprint 01: Der Name ist nach dem Anlegen unveränderlich")]
    public void Name_ist_nach_dem_Anlegen_unveraenderlich()
    {
        Hilfe.NurLesbar("Mobilityuebung", "Name");
    }
    

    [Fact(DisplayName = "Sprint 01: Das Innenleben der Übung ist gekapselt (keine öffentlichen Felder)")]
    public void Innenleben_ist_gekapselt()
    {
        Hilfe.KeineOeffentlichenFelder("Mobilityuebung");
    }
    
    [Fact(DisplayName = "Sprint 01: Eine pausierte Übung meldet 'Name: pausiert'")]
    public void Pausierte_Uebung()
    {
        var uebung = Hilfe.Neu("Mobilityuebung", "Dehnen");
        var zeile = Hilfe.Rufe(uebung, "Statuszeile");
        Assert.True(Equals(zeile, "Dehnen: pausiert"),
            $"Erwartet wird exakt \"Dehnen: pausiert\" — geliefert wurde: '{zeile}'. " +
            "Format-Tabelle auf der Story-Karte beachten (Doppelpunkt, Leerzeichen).");
    }

    [Fact(DisplayName = "Sprint 01: Eine aktive Mobilit Übung meldet 'Name: aktiv (I %)'")]
    public void Aktive_Uebung()
    {
        var uebung = Hilfe.Neu("Mobilityuebung", "Dehnen");
        Hilfe.Rufe(uebung, "Aktivieren");
        Hilfe.Setze(uebung, "Intensitaet", 75);
        var zeile = Hilfe.Rufe(uebung, "Statuszeile");
        Assert.True(Equals(zeile, "Dehnen: aktiv (75 %)"),
            $"Erwartet wird exakt \"Dehnen: aktiv (75 %)\" — geliefert wurde: '{zeile}'. " +
            "Format-Tabelle auf der Story-Karte beachten (Klammern, Leerzeichen vor %).");
    }

    [Fact(DisplayName = "Sprint 01: Das Tracking wird gestartet")]
    public void Tracking_wird_gestartet()
    {
        // Arrange 
        var uebung = Hilfe.Neu("Mobilityuebung", "Dehnen");
        Hilfe.Rufe(uebung, "Aktivieren");
        Hilfe.Setze(uebung, "Intensitaet", 75);

        // Act: Tracking starten
        Hilfe.Rufe(uebung, "StarteZeitTracking"); 
        
        // Assert
        // 1. Übung bleibt aktiv
        var istAktiv = (bool)Hilfe.Lies(uebung, "IstAktiv")!;
        Assert.True(istAktiv,
            "Nach StarteZeitTracking() muss IstAktiv weiterhin true sein.");

        // 2. Dauer vor dem Stopp soll 0 sein
        var dauerVorStopp = (int)Hilfe.Lies(uebung, "LetzteDauerInSek")!;
        Assert.True(Equals(0, dauerVorStopp),
            $"Vor dem ersten StoppeZeitTracking() muss LetzteDauerInSek 0 sein — geliefert wurde: {dauerVorStopp}.");
    }

    [Fact(DisplayName = "Sprint 01: Nach dem Stopp ist LetzteDauerInSek größer 0")]
    public void Nach_dem_Stopp_ist_Dauer_groesser_null()
    {
        // Arrange
        var uebung = Hilfe.Neu("Mobilityuebung", "Dehnen");
        Hilfe.Rufe(uebung, "Aktivieren");

        // Act: Starten, kurz warten, stoppen
        Hilfe.Rufe(uebung, "StarteZeitTracking");
        Thread.Sleep(10000);
        Hilfe.Rufe(uebung, "StoppeZeitTracking");

        // Assert
        var dauerNachStopp = (int)Hilfe.Lies(uebung, "LetzteDauerInSek")!;
        Assert.True(dauerNachStopp > 0,
            $"Nach Start + Stop muss LetzteDauerInSek > 0 sein — geliefert wurde: {dauerNachStopp}.");
    }

    [Fact(DisplayName = "Sprint 01: LetzteDauerInSek ist von außen nur lesbar")]
    public void LetzteDauerInSek_ist_nur_lesbar()
    {
        Hilfe.NurLesbar("Mobilityuebung", "LetzteDauerInSek");
    }
}