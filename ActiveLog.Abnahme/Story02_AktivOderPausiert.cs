using Xunit;
using ActiveLog.Src;

namespace ActiveLog.Abnahme;

public class Story02_AktivOderPausiert
{
    [Fact(DisplayName = "Story 2: Eine frisch angelegte Übung ist pausiert")]
    public void Neue_Uebung_ist_pausiert()
    {
        var uebung = Hilfe.Neu("Kraftuebung", "Bankdrücken");
        Assert.True(Equals(Hilfe.Lies(uebung, "IstAktiv"), false),
            "Direkt nach dem Anlegen muss IstAktiv den Wert false liefern — eine Übung startet pausiert, bis der Athlet sie einplant.");
    }

    [Fact(DisplayName = "Story 2: Aktivieren aktiviert die Übung")]
    public void Aktivieren_aktiviert()
    {
        var uebung = Hilfe.Neu("Kraftuebung", "Bankdrücken");
        Hilfe.Rufe(uebung, "Aktivieren");
        Assert.True(Equals(Hilfe.Lies(uebung, "IstAktiv"), true),
            "Nach dem Aufruf von Aktivieren() muss IstAktiv true liefern.");
    }

    [Fact(DisplayName = "Story 2: Deaktivieren pausiert die Übung wieder")]
    public void Deaktivieren_pausiert()
    {
        var uebung = Hilfe.Neu("Kraftuebung", "Bankdrücken");
        Hilfe.Rufe(uebung, "Aktivieren");
        Hilfe.Rufe(uebung, "Deaktivieren");
        Assert.True(Equals(Hilfe.Lies(uebung, "IstAktiv"), false),
            "Nach Aktivieren() und anschließendem Deaktivieren() muss IstAktiv wieder false liefern.");
    }

    [Fact(DisplayName = "Story 2: Jede Übung schaltet für sich (Objekte sind unabhängig)")]
    public void Uebungen_schalten_unabhaengig_voneinander()
    {
        var bankdruecken = Hilfe.Neu("Kraftuebung", "Bankdrücken");
        var kniebeuge = Hilfe.Neu("Kraftuebung", "Kniebeuge");
        Hilfe.Rufe(bankdruecken, "Aktivieren");
        Assert.True(Equals(Hilfe.Lies(kniebeuge, "IstAktiv"), false),
            "Die Kniebeuge muss pausiert bleiben, wenn nur das Bankdrücken aktiviert wird. " +
            "Jedes Objekt braucht seinen eigenen Zustand (Vorsicht mit static).");
    }

    [Fact(DisplayName = "Story 2: Der Aktiv-Zustand lässt sich nicht von außen überschreiben")]
    public void Zustand_nicht_von_aussen_setzbar()
    {
        Hilfe.NurLesbar("Kraftuebung", "IstAktiv");
    }
}
