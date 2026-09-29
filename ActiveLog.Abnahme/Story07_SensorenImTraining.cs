using Xunit;
using ActiveLog.Src;

namespace ActiveLog.Abnahme;

public class Story07_SensorenImTraining
{
    [Fact(DisplayName = "Story 7: Eine 'nackte' Uebung kann nicht mehr entstehen (abstrakt)")]
    public void Uebung_ist_abstrakt()
    {
        Assert.True(Hilfe.Typ("Uebung").IsAbstract,
            "Nach dem Vorfall im Demo-Code gilt: 'new Uebung(...)' darf nicht mehr möglich sein. " +
            "Eine Übung ohne konkrete Trainingsart ergibt keinen Sinn (Buch-Kapitel 3.9).");
    }

    [Fact(DisplayName = "Story 7: Es gibt einen Vertrag 'ISensor' mit LiesMesswert()")]
    public void ISensor_ist_ein_Interface()
    {
        var sensor = Hilfe.Typ("ISensor");
        Assert.True(sensor.IsInterface,
            "'ISensor' muss ein Interface sein — ein reiner Vertrag ohne eigene Daten (Buch-Kapitel 3.10).");
        Assert.True(sensor.GetMethod("LiesMesswert", Type.EmptyTypes) is not null,
            "Das Interface ISensor muss die Methode 'LiesMesswert()' (Rückgabe: string) vorschreiben.");
    }

    [Fact(DisplayName = "Story 7: Die Laufeinheit erfüllt den Sensor-Vertrag und misst den Puls")]
    public void Laufeinheit_ist_ein_Sensor()
    {
        Assert.True(Hilfe.Typ("ISensor").IsAssignableFrom(Hilfe.Typ("Laufeinheit")),
            "Laufeinheit muss das Interface ISensor implementieren — die Uhr misst schließlich den Puls.");
        var laufeinheit = Hilfe.Neu("Laufeinheit", "Intervallläufe");
        Assert.True(Equals(Hilfe.Rufe(laufeinheit, "LiesMesswert"), "130 bpm"),
            "Ohne Messung muss LiesMesswert() den Startwert \"130 bpm\" liefern.");
        Hilfe.Rufe(laufeinheit, "Aktivieren");
        Hilfe.Rufe(laufeinheit, "MessePuls", 142);
        Assert.True(Equals(Hilfe.Rufe(laufeinheit, "LiesMesswert"), "142 bpm"),
            "Nach MessePuls(142) muss LiesMesswert() exakt \"142 bpm\" liefern.");
    }

    [Fact(DisplayName = "Story 7: Die Schwimmeinheit ist eine Übung, ein Sensor — und zählt Bahnen")]
    public void Schwimmeinheit_zaehlt()
    {
        Assert.True(Hilfe.Typ("Schwimmeinheit").BaseType?.Name == "Uebung",
            "Die Schwimmeinheit muss von Uebung erben — sie steht wie jede Übung im Trainingsplan.");
        Assert.True(Hilfe.Typ("ISensor").IsAssignableFrom(Hilfe.Typ("Schwimmeinheit")),
            "Die Schwimmeinheit muss das Interface ISensor implementieren.");

        var schwimmen = Hilfe.Neu("Schwimmeinheit", "Kraultraining");
        Hilfe.Rufe(schwimmen, "Aktivieren");
        Hilfe.Rufe(schwimmen, "MeldeBahn");
        Hilfe.Rufe(schwimmen, "MeldeBahn");
        Hilfe.Rufe(schwimmen, "MeldeBahn");
        Assert.True(Equals(Hilfe.Rufe(schwimmen, "LiesMesswert"), "3 Bahnen"),
            "Nach drei gemeldeten Bahnen muss LiesMesswert() exakt \"3 Bahnen\" liefern.");
        Assert.True(Equals(Hilfe.Rufe(schwimmen, "Statuszeile"), "Kraultraining: aktiv (3 Bahnen)"),
            "Die Statuszeile einer aktiven Schwimmeinheit muss exakt \"Kraultraining: aktiv (3 Bahnen)\" lauten.");
    }

    [Fact(DisplayName = "Story 7: Eine pausierte Schwimmeinheit zählt nicht")]
    public void Pausierte_Schwimmeinheit_zaehlt_nicht()
    {
        var schwimmen = Hilfe.Neu("Schwimmeinheit", "Kraultraining");
        Hilfe.Rufe(schwimmen, "MeldeBahn");
        Hilfe.Rufe(schwimmen, "Aktivieren");
        Assert.True(Equals(Hilfe.Rufe(schwimmen, "LiesMesswert"), "0 Bahnen"),
            "Solange die Einheit pausiert ist, dürfen gemeldete Bahnen nicht gezählt werden (Datenschutz-Vorgabe aus dem Ticket).");
    }

    [Fact(DisplayName = "Story 7: Eine pausierte Laufeinheit misst nicht")]
    public void Pausierte_Laufeinheit_misst_nicht()
    {
        var laufeinheit = Hilfe.Neu("Laufeinheit", "Intervallläufe");
        Hilfe.Rufe(laufeinheit, "MessePuls", 142);
        Hilfe.Rufe(laufeinheit, "Aktivieren");
        Assert.True(Equals(Hilfe.Rufe(laufeinheit, "LiesMesswert"), "130 bpm"),
            "Solange die Einheit pausiert ist, darf ein gemessener Puls nicht aufgezeichnet werden (Datenschutz-Vorgabe aus dem Ticket) — " +
            "der Messwert bleibt beim Startwert \"130 bpm\".");
    }

    [Fact(DisplayName = "Story 7: Eine Kraftübung ist KEIN Sensor")]
    public void Kraftuebung_ist_kein_Sensor()
    {
        Assert.False(Hilfe.Typ("ISensor").IsAssignableFrom(Hilfe.Typ("Kraftuebung")),
            "An der Hantel misst keine Uhr — die Kraftübung darf den Sensor-Vertrag nicht unterschreiben. " +
            "Nur wer wirklich Messwerte liefert, implementiert ISensor.");
    }
}
