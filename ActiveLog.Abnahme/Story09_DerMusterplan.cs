using System.Reflection;
using Xunit;
using ActiveLog.Src;

namespace ActiveLog.Abnahme;

public class Story09_DerMusterplan
{
    [Fact(DisplayName = "Story 9: Ein Trainingsplan kennt seine Bezeichnung")]
    public void Plan_kennt_seine_Bezeichnung()
    {
        var plan = Hilfe.Neu("Trainingsplan", "Musterplan Messe");
        Assert.True(Equals(Hilfe.Lies(plan, "Bezeichnung"), "Musterplan Messe"),
            "Nach dem Anlegen mit \"Musterplan Messe\" muss Bezeichnung genau diesen Text liefern.");
    }

    [Fact(DisplayName = "Story 9: Einheiten entstehen im Trainingsplan — und nur dort")]
    public void Einheiten_entstehen_im_Plan()
    {
        var plan = Hilfe.Neu("Trainingsplan", "Musterplan Messe");
        var pushDay = Hilfe.Rufe(plan, "EinheitAnlegen", "Push-Day")
            ?? throw Hilfe.Fehler("EinheitAnlegen(\"Push-Day\") muss die neu angelegte Einheit zurückgeben.");
        Assert.True(pushDay.GetType().Name == "Trainingseinheit",
            "EinheitAnlegen(...) muss ein Trainingseinheit-Objekt zurückgeben, das der Plan selbst erzeugt hat.");
        Assert.True(Equals(Hilfe.Lies(pushDay, "Name"), "Push-Day"),
            "Die zurückgegebene Einheit muss den übergebenen Namen tragen.");
        Hilfe.Rufe(plan, "EinheitAnlegen", "Leg-Day");
        Assert.True(Equals(Hilfe.Lies(plan, "AnzahlEinheiten"), 2),
            "Nach zweimal EinheitAnlegen(...) muss AnzahlEinheiten 2 liefern.");
    }

    [Fact(DisplayName = "Story 9: Kein Weg führt eine fremde Einheit in den Plan (Komposition)")]
    public void Keine_fremden_Einheiten()
    {
        var einheitTyp = Hilfe.Typ("Trainingseinheit");
        var verdaechtige = Hilfe.Typ("Trainingsplan")
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(m => m.GetParameters().Any(p => p.ParameterType == einheitTyp))
            .Select(m => m.Name)
            .ToList();
        Assert.True(verdaechtige.Count == 0,
            $"Der Trainingsplan nimmt keine fertigen Trainingseinheit-Objekte von außen an (gefunden: {string.Join(", ", verdaechtige)}). " +
            "Einheiten gehören untrennbar zu ihrem Plan: In einen Plan kommen sie ausschließlich über EinheitAnlegen(name) — Komposition, Buch-Kapitel 3.8.");
    }

    [Fact(DisplayName = "Story 9: Szene 'Regenerationswoche' — ein Befehl, alles pausiert")]
    public void Szene_Regenerationswoche()
    {
        var plan = Hilfe.Neu("Trainingsplan", "Musterplan Messe");
        var pushDay = Hilfe.Rufe(plan, "EinheitAnlegen", "Push-Day")!;
        var legDay = Hilfe.Rufe(plan, "EinheitAnlegen", "Leg-Day")!;
        var bankdruecken = Hilfe.Neu("Kraftuebung", "Bankdrücken");
        var kniebeuge = Hilfe.Neu("Kraftuebung", "Kniebeuge");
        Hilfe.Rufe(bankdruecken, "Aktivieren");
        Hilfe.Rufe(kniebeuge, "Aktivieren");
        Hilfe.Rufe(pushDay, "UebungHinzufuegen", bankdruecken);
        Hilfe.Rufe(legDay, "UebungHinzufuegen", kniebeuge);

        Hilfe.Rufe(plan, "AllesDeaktivieren");

        Assert.True(Equals(Hilfe.Lies(bankdruecken, "IstAktiv"), false),
            "Trainingsplan.AllesDeaktivieren() muss auch das Bankdrücken im Push-Day erreichen (über die Einheit!).");
        Assert.True(Equals(Hilfe.Lies(kniebeuge, "IstAktiv"), false),
            "Trainingsplan.AllesDeaktivieren() muss auch die Kniebeuge im Leg-Day erreichen.");
    }

    [Fact(DisplayName = "Story 9: Der Gesamtbericht zeigt den ganzen Plan")]
    public void Gesamtbericht_zeigt_alles()
    {
        var plan = Hilfe.Neu("Trainingsplan", "Musterplan Messe");
        var pushDay = Hilfe.Rufe(plan, "EinheitAnlegen", "Push-Day")!;
        var bankdruecken = Hilfe.Neu("Kraftuebung", "Bankdrücken");
        Hilfe.Rufe(bankdruecken, "Aktivieren");
        Hilfe.Rufe(pushDay, "UebungHinzufuegen", bankdruecken);

        var bericht = Hilfe.Rufe(plan, "Gesamtbericht") as string
            ?? throw Hilfe.Fehler("Gesamtbericht() muss einen string liefern.");

        Assert.True(bericht.Contains("Musterplan Messe"),
            $"Der Gesamtbericht muss die Bezeichnung des Plans enthalten. Geliefert wurde:\n{bericht}");
        Assert.True(bericht.Contains("Push-Day"),
            $"Der Gesamtbericht muss jede Einheit nennen. Geliefert wurde:\n{bericht}");
        Assert.True(bericht.Contains((string)Hilfe.Rufe(bankdruecken, "Statuszeile")!),
            $"Der Gesamtbericht muss die Statuszeile jeder Übung enthalten (nutze die Berichte der Einheiten!). Geliefert wurde:\n{bericht}");
    }
}
