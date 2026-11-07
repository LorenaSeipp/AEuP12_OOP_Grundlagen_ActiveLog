# Klassendiagramm — ActiveLog 2.0

Das ist der **Bauplan** deines Systems. Halte ihn nach jeder Story aktuell
(Definition of Done) — so wie es die Projektdokumentation bei FitConnect verlangt.

Zeichne als ASCII-Skizze direkt hier hinein oder verlinke ein Foto/Export
(z. B. `docs/klassendiagramm.png`), wenn du lieber auf Papier oder mit einem
Tool zeichnest.

## Notations-Spickzettel

```
+-------------------------+       +  public
| Kraftuebung             |       -  private
+-------------------------+       #  protected
| - intensitaet: int      |
+-------------------------+       Vererbung:      Kind ----|> Basisklasse
| + Aktivieren(): void    |       Interface:      Klasse ..|> ISchnittstelle
| + Statuszeile(): string |       Aggregation:    Ganzes o--- Teil
+-------------------------+       Komposition:    Ganzes *--- Teil
```

## Dein Diagramm (Stand: Story 08)
# Klassendiagramm

```mermaid
classDiagram
    direction LR

    class ISensor {
        <<interface>>
        +LiesMesswert() string
    }

    class Einstellung {
        +Intensitaet int?
        +Zielpuls int?
        +SollAnzahlBahnen int?
    }

    class Uebung {
        <<abstract>>
        -_intensitaet int
        +Name string
        +IstAktiv bool
        +Intensitaet int
        +Aktivieren() void
        +Deaktivieren() void
        +Einstellen(einstellung Einstellung) void
        +Statuszeile() string
    }

    class Kraftuebung {
        +Statuszeile() string
        +Einstellen(einstellung Einstellung) void
    }

    class Laufeinheit {
        -_zielpuls int
        -_messwert int
        +Messwert int
        +Zielpuls int
        +LiesMesswert() string
        +MessePuls(schlaege int) void
        +Einstellen(einstellung Einstellung) void
    }

    class Schwimmeinheit {
        -_anzahlBahnen int
        +SollAnzahlBahnen int
        +AnzahlBahnen int
        +LiesMesswert() string
        +MeldeBahn() int
        +Einstellen(einstellung Einstellung) void
    }

    class Mobilityuebung {
        -_startZeit DateTime?
        +SollDauerInSek int
        +ZeitTrackingLaeuft bool
        +LetzteDauerInSek int
        +StarteZeitTracking() void
        +StoppeZeitTracking() void
        +LiesMesswert() string
        -AktuelleTrainingInSek() int
    }

    class Trainingseinheit {
        -uebungen List~Uebung~
        +Name string
        +AnzahlUebungen int
        +UebungHinzufuegen(uebung Uebung) void
        +AllesAktivieren() void
        +AllesDeaktivieren() void
        +Einstellen(einstellung Einstellung) void
        +Statusbericht() string
    }

    class Trainingsplan {
        -_trainingseinheitList List~Trainingseinheit~
        +Bezeichnung string
        +AnzahlEinheiten int
        +EinheitAnlegen(name string) Trainingseinheit
        +AllesAktivieren() void
        +AllesDeaktivieren() void
        +Gesamtbericht() string
    }

    class Wettkampfwoche {
        +Wettkampfwoche(name string)
    }
    
    class ISzene {
        <<interface>>
        +Anwenden(Einstellung einstellung)
    }

    Uebung <|-- Kraftuebung
    Uebung <|-- Mobilityuebung
    Uebung <|-- Laufeinheit
    Uebung <|-- Schwimmeinheit

    ISensor <|.. Laufeinheit
    ISensor <|.. Mobilityuebung
    ISensor <|.. Schwimmeinheit

    Trainingsplan <|-- Wettkampfwoche
    ISzene <|.. Wettkampfwoche

    Trainingseinheit "1" o-- "0..*" Uebung : enthält
    Trainingsplan "1" o-- "0..*" Trainingseinheit : enthält

    Uebung ..> Einstellung : verwendet
    Trainingseinheit ..> Einstellung : verwendet
```