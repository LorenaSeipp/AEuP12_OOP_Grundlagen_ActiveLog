namespace ActiveLog.Src;

public class Mobilityuebung : Uebung, ISensor
{
    private DateTime? _startZeit;

    public int SollDauerInSek { get; } = 120;

    public bool ZeitTrackingLaeuft { get; private set; }

    public int LetzteDauerInSek { get; private set; } = 0;

    public Mobilityuebung(string name) : base(name)
    {
    }

    public Mobilityuebung(string name, int intensitaet) : base(name, intensitaet)
    {
    }

    public Mobilityuebung(string name, int intensitaet, int sollDauerInSek) : base(name, intensitaet)
    {
        SollDauerInSek = sollDauerInSek;
    }

    public void StarteZeitTracking()
    {
        if (ZeitTrackingLaeuft)
        {
            throw new InvalidOperationException("Tracking läuft bereits");
        }

        if (!IstAktiv)
        {
            throw new InvalidOperationException(
                "Tracking kann nur für aktive Übungen gestartet werden.");
        }

        _startZeit = DateTime.Now;
        ZeitTrackingLaeuft = true;
        IstAktiv = true;
    }

    private int AktuelleTrainingInSek()
    {
        if (!ZeitTrackingLaeuft || _startZeit == null) return 0;
        return (int)(DateTime.UtcNow - _startZeit.Value).TotalSeconds;
    }

    public void StoppeZeitTracking()
    {
        if (!ZeitTrackingLaeuft || _startZeit == null)
        {
            throw new InvalidOperationException("Tracking wurde nicht gestartet");
        }

        var ende = DateTime.Now;
        var dauer = ende - _startZeit.Value;
        var sekunden = (int)dauer.TotalSeconds;

        LetzteDauerInSek = Math.Clamp(sekunden, 0, int.MaxValue);
        _startZeit = null;
        ZeitTrackingLaeuft = false;
    }

    public override string Statuszeile()
    {
        string statuszeile;

        // Aktiv und Tracking läuft: Aktuelle Dauer 
        if (ZeitTrackingLaeuft)
        {
            int aktuelleDauer = AktuelleTrainingInSek();
            statuszeile = $"{Name}: läuft seit {aktuelleDauer} s ({Intensitaet} %";
        }

        // AktivStatus: Dauer letzte Übung anzeigen
        else if (IstAktiv && LetzteDauerInSek != 0)
        {
            statuszeile =
                $"{Name}: aktiv, Soll-Dauer: {SollDauerInSek}, Dauer letzte Übung: {LetzteDauerInSek} s ({Intensitaet} %";
        }

        else if (IstAktiv)
        {
            statuszeile = $"{Name}: aktiv ({Intensitaet} %)";
        }

        else
        {
            statuszeile = $"{Name}: pausiert";
        }

        return statuszeile;
    }

    public string LiesMesswert()
    {
        return $"{LetzteDauerInSek} sek";
    }
}