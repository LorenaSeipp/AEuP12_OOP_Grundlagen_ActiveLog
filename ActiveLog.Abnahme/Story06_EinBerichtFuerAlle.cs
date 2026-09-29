using Xunit;
using ActiveLog.Src;

namespace ActiveLog.Abnahme;

public class Story06_EinBerichtFuerAlle
{
    private static void PruefeEigeneStatuszeile(string klasse)
    {
        var typ = Hilfe.Typ(klasse);
        var m = typ.GetMethod("Statuszeile", Type.EmptyTypes)
            ?? throw Hilfe.Fehler($"Die Methode 'Statuszeile()' fehlt auf '{klasse}'.");
        Assert.True(m.DeclaringType == typ,
            $"'{klasse}' liefert noch die allgemeine Statuszeile aus Uebung. " +
            $"Die Klasse braucht ihre eigene Fassung mit den passenden Details (Buch-Kapitel 3.9).");
        Assert.True(m.GetBaseDefinition().DeclaringType?.Name == "Uebung",
            $"'{klasse}.Statuszeile()' ersetzt die Fassung aus Uebung nicht wirklich — " +
            "Statuszeile muss in Uebung angekündigt und in der Kindklasse überschrieben werden (Buch-Kapitel 3.9).");
    }

    [Fact(DisplayName = "Story 6: Die Kraftübung hat ihre eigene Statuszeile (mit Intensität)")]
    public void Kraftuebung_meldet_details()
    {
        PruefeEigeneStatuszeile("Kraftuebung");
        var uebung = Hilfe.Neu("Kraftuebung", "Bankdrücken");
        Hilfe.Rufe(uebung, "Aktivieren");
        Hilfe.Setze(uebung, "Intensitaet", 75);
        var zeile = Hilfe.Rufe(uebung, "Statuszeile");
        Assert.True(Equals(zeile, "Bankdrücken: aktiv (75 %)"),
            $"Erwartet wird exakt \"Bankdrücken: aktiv (75 %)\" — geliefert wurde: '{zeile}'.");
    }

    [Fact(DisplayName = "Story 6: Die Laufeinheit hat ihre eigene Statuszeile (mit Zielpuls)")]
    public void Laufeinheit_meldet_details()
    {
        PruefeEigeneStatuszeile("Laufeinheit");
        var laufeinheit = Hilfe.Neu("Laufeinheit", "Intervallläufe");
        var pausiert = Hilfe.Rufe(laufeinheit, "Statuszeile");
        Assert.True(Equals(pausiert, "Intervallläufe: pausiert"),
            $"Pausiert wird exakt \"Intervallläufe: pausiert\" erwartet — geliefert wurde: '{pausiert}'.");
        Hilfe.Rufe(laufeinheit, "Aktivieren");
        Hilfe.Setze(laufeinheit, "Zielpuls", 145);
        var aktiv = Hilfe.Rufe(laufeinheit, "Statuszeile");
        Assert.True(Equals(aktiv, "Intervallläufe: aktiv (Zielpuls 145 bpm)"),
            $"Aktiv wird exakt \"Intervallläufe: aktiv (Zielpuls 145 bpm)\" erwartet — geliefert wurde: '{aktiv}'.");
    }

    [Fact(DisplayName = "Story 6: Auch über den Basistyp Uebung erscheint die spezifische Statuszeile")]
    public void Aufruf_ueber_den_Basistyp()
    {
        var statuszeileVonUebung = Hilfe.Typ("Uebung").GetMethod("Statuszeile", Type.EmptyTypes)
            ?? throw Hilfe.Fehler("Uebung muss die Methode 'Statuszeile()' ankündigen, damit der Bericht sie bei jeder Übung aufrufen kann.");

        var uebung = Hilfe.Neu("Kraftuebung", "Bankdrücken");
        Hilfe.Rufe(uebung, "Aktivieren");
        var ueberBasistyp = statuszeileVonUebung.Invoke(uebung, null);

        Assert.True(Equals(ueberBasistyp, Hilfe.Rufe(uebung, "Statuszeile")),
            "Der Bericht kennt nur 'Uebung' — trotzdem muss beim Aufruf die kraftübungs-spezifische Statuszeile herauskommen " +
            "(Polymorphie, Buch-Kapitel 3.11). Tipp: Methoden verstecken (new) ist etwas anderes als überschreiben.");
    }
}
