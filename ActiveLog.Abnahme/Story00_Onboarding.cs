using Xunit;
using ActiveLog.Src;

namespace ActiveLog.Abnahme;

public class Story00_Onboarding
{
    [Fact(DisplayName = "Story 0: Die Werkzeugkette funktioniert (dieser Test ist sofort grün)")]
    public void Werkzeugkette_funktioniert()
    {
        Assert.True(true);
    }

    [Fact(DisplayName = "Story 0: Dein Name steht in Projektinfo.Entwickler")]
    public void Entwicklername_ist_eingetragen()
    {
        var name = Projektinfo.Entwickler.Trim();
        Assert.True(name != "???" && name.Length >= 2,
            "Öffne ActiveLog.Src/Projektinfo.cs und ersetze die drei Fragezeichen durch deinen Namen. " +
            "Führe danach die Tests erneut aus — so sieht dein Arbeitszyklus im ganzen Kurs aus.");
    }
}
