using Xunit;
using ActiveLog.Src;

namespace ActiveLog.Abnahme;

public class Story08_EinheitenPlanen
{
    [Fact(DisplayName = "Story 8: Eine Trainingseinheit kennt ihren Namen")]
    public void Einheit_kennt_ihren_Namen()
    {
        var einheit = Hilfe.Neu("Trainingseinheit", "Push-Day");
        Assert.True(Equals(Hilfe.Lies(einheit, "Name"), "Push-Day"),
            "Nach dem Anlegen mit \"Push-Day\" muss Name genau \"Push-Day\" liefern.");
    }

    [Fact(DisplayName = "Story 8: Eine Trainingseinheit sammelt Übungen")]
    public void Einheit_sammelt_uebungen()
    {
        var einheit = Hilfe.Neu("Trainingseinheit", "Push-Day");
        Assert.True(Equals(Hilfe.Lies(einheit, "AnzahlUebungen"), 0),
            "Eine frisch angelegte Trainingseinheit muss 0 Übungen haben.");
        Hilfe.Rufe(einheit, "UebungHinzufuegen", Hilfe.Neu("Kraftuebung", "Schulterdrücken"));
        Hilfe.Rufe(einheit, "UebungHinzufuegen", Hilfe.Neu("Schwimmeinheit", "Technik-Bahnen"));
        Assert.True(Equals(Hilfe.Lies(einheit, "AnzahlUebungen"), 2),
            "Nach dem Hinzufügen von zwei Übungen muss AnzahlUebungen 2 liefern. " +
            "UebungHinzufuegen nimmt jede Uebung an — egal ob Kraftuebung oder Schwimmeinheit.");
    }

    [Fact(DisplayName = "Story 8: 'Alles deaktivieren' pausiert jede Übung der Einheit")]
    public void Alles_deaktivieren_in_der_Einheit()
    {
        var einheit = Hilfe.Neu("Trainingseinheit", "Leg-Day");
        var kniebeuge = Hilfe.Neu("Kraftuebung", "Kniebeuge");
        var dauerlauf = Hilfe.Neu("Laufeinheit", "Dauerlauf");
        Hilfe.Rufe(kniebeuge, "Aktivieren");
        Hilfe.Rufe(dauerlauf, "Aktivieren");
        Hilfe.Rufe(einheit, "UebungHinzufuegen", kniebeuge);
        Hilfe.Rufe(einheit, "UebungHinzufuegen", dauerlauf);

        Hilfe.Rufe(einheit, "AllesDeaktivieren");

        Assert.True(Equals(Hilfe.Lies(kniebeuge, "IstAktiv"), false),
            "Nach AllesDeaktivieren() muss auch die Kniebeuge pausiert sein.");
        Assert.True(Equals(Hilfe.Lies(dauerlauf, "IstAktiv"), false),
            "Nach AllesDeaktivieren() muss auch der Dauerlauf pausiert sein.");
    }

    [Fact(DisplayName = "Story 8: Der Statusbericht nennt die Einheit und jede Übung")]
    public void Statusbericht_enthaelt_alles()
    {
        var einheit = Hilfe.Neu("Trainingseinheit", "Push-Day");
        var bankdruecken = Hilfe.Neu("Kraftuebung", "Bankdrücken");
        var intervalle = Hilfe.Neu("Laufeinheit", "Intervallläufe");
        Hilfe.Rufe(bankdruecken, "Aktivieren");
        Hilfe.Rufe(einheit, "UebungHinzufuegen", bankdruecken);
        Hilfe.Rufe(einheit, "UebungHinzufuegen", intervalle);

        var bericht = Hilfe.Rufe(einheit, "Statusbericht") as string
            ?? throw Hilfe.Fehler("Statusbericht() muss einen string liefern.");

        Assert.True(bericht.Contains("Push-Day"),
            $"Der Bericht muss den Namen der Einheit enthalten. Geliefert wurde:\n{bericht}");
        Assert.True(bericht.Contains((string)Hilfe.Rufe(bankdruecken, "Statuszeile")!),
            $"Der Bericht muss die Statuszeile des Bankdrückens enthalten (nutze Statuszeile() der Übungen!). Geliefert wurde:\n{bericht}");
        Assert.True(bericht.Contains((string)Hilfe.Rufe(intervalle, "Statuszeile")!),
            $"Der Bericht muss die Statuszeile der Intervallläufe enthalten. Geliefert wurde:\n{bericht}");
    }
}
