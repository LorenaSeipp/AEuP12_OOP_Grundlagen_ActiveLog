using Xunit;
using ActiveLog.Src;

namespace ActiveLog.Abnahme;

public class Story05_DieUebungsfamilie
{
    [Fact(DisplayName = "Story 5: Es gibt eine Basisklasse 'Uebung'")]
    public void Basisklasse_existiert()
    {
        Hilfe.Typ("Uebung");
    }

    [Fact(DisplayName = "Story 5: Eine Kraftübung ist eine Uebung")]
    public void Kraftuebung_ist_eine_Uebung()
    {
        Assert.True(Hilfe.Typ("Kraftuebung").BaseType?.Name == "Uebung",
            "Die Klasse Kraftuebung muss von Uebung erben ('ist eine Übung') — Buch-Kapitel 3.9 hilft.");
    }

    [Fact(DisplayName = "Story 5: Eine Laufeinheit ist eine Uebung")]
    public void Laufeinheit_ist_eine_Uebung()
    {
        Assert.True(Hilfe.Typ("Laufeinheit").BaseType?.Name == "Uebung",
            "Die Klasse Laufeinheit muss von Uebung erben ('ist eine Übung').");
    }

    [Fact(DisplayName = "Story 5: Eine Laufeinheit kennt Name und Aktiv-Zustand wie jede Übung")]
    public void Laufeinheit_grundverhalten()
    {
        var laufeinheit = Hilfe.Neu("Laufeinheit", "Intervallläufe");
        Assert.True(Equals(Hilfe.Lies(laufeinheit, "Name"), "Intervallläufe"),
            "Nach dem Anlegen mit \"Intervallläufe\" muss Name genau \"Intervallläufe\" liefern.");
        Assert.True(Equals(Hilfe.Lies(laufeinheit, "IstAktiv"), false),
            "Eine frisch angelegte Laufeinheit muss pausiert sein.");
        Hilfe.Rufe(laufeinheit, "Aktivieren");
        Assert.True(Equals(Hilfe.Lies(laufeinheit, "IstAktiv"), true),
            "Nach Aktivieren() muss IstAktiv true liefern — dieses Verhalten teilt sich die Laufeinheit mit der Kraftübung.");
    }

    [Fact(DisplayName = "Story 5: Der Zielpuls startet bei 130 bpm")]
    public void Zielpuls_startet_bei_130()
    {
        var laufeinheit = Hilfe.Neu("Laufeinheit", "Intervallläufe");
        Assert.True(Equals(Hilfe.Lies(laufeinheit, "Zielpuls"), 130),
            "Direkt nach dem Anlegen muss Zielpuls den Wert 130 liefern.");
    }

    [Fact(DisplayName = "Story 5: Der Zielpuls bleibt zwischen 90 und 180 bpm")]
    public void Zielpuls_ist_begrenzt()
    {
        var laufeinheit = Hilfe.Neu("Laufeinheit", "Intervallläufe");
        Hilfe.Setze(laufeinheit, "Zielpuls", 145);
        Assert.True(Equals(Hilfe.Lies(laufeinheit, "Zielpuls"), 145),
            "Der gültige Wert 145 muss übernommen werden.");
        Hilfe.Setze(laufeinheit, "Zielpuls", 210);
        Assert.True(Equals(Hilfe.Lies(laufeinheit, "Zielpuls"), 180),
            "Der Wert 210 muss auf 180 begrenzt werden (sportmedizinische Obergrenze).");
        Hilfe.Setze(laufeinheit, "Zielpuls", 40);
        Assert.True(Equals(Hilfe.Lies(laufeinheit, "Zielpuls"), 90),
            "Der Wert 40 muss auf 90 angehoben werden (darunter ist es kein Ausdauertraining).");
    }

    [Fact(DisplayName = "Story 5: Gemeinsames Verhalten existiert genau einmal — in Uebung")]
    public void Gemeinsames_steht_in_der_Basisklasse()
    {
        var kraftuebung = Hilfe.Typ("Kraftuebung");
        foreach (var property in new[] { "Name", "IstAktiv" })
        {
            var p = kraftuebung.GetProperty(property) ?? throw Hilfe.Fehler($"Die Property '{property}' fehlt auf Kraftuebung.");
            Assert.True(p.DeclaringType?.Name == "Uebung",
                $"'{property}' soll nur einmal existieren: deklariert in der Basisklasse Uebung — nicht noch einmal in Kraftuebung.");
        }
        foreach (var methode in new[] { "Aktivieren", "Deaktivieren" })
        {
            var m = kraftuebung.GetMethod(methode, Type.EmptyTypes) ?? throw Hilfe.Fehler($"Die Methode '{methode}' fehlt auf Kraftuebung.");
            Assert.True(m.DeclaringType?.Name == "Uebung",
                $"'{methode}' soll in Uebung stehen — Kraftuebung und Laufeinheit erben sie, statt sie zu kopieren.");
        }
    }
}
