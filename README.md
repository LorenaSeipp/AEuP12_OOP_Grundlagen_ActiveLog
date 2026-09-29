# ActiveLog 2.0 — Dein Arbeits-Repository

Willkommen im Entwicklungsteam von **FitConnect**! In diesem Repository baust du
Story für Story unsere Trainings-App **ActiveLog 2.0** auf — die App, mit der
Sportbegeisterte ihre Trainings tracken und auswerten.
Die Aufgaben (User Stories) findest du als **Issues** in diesem Repository, die
betriebliche Situation dazu im Mebis-Kurs auf den Sprint-Seiten.

## Was ist hier schon drin?

| Ordner/Datei | Inhalt |
|---|---|
| `ActiveLog.Src/` | **Dein Code.** Hier legst du deine Klassen an (eine Datei pro Klasse). |
| `ActiveLog.Src/Program.cs` | Deine Spielwiese: Probiere deine Klassen live in der Konsole aus. |
| `ActiveLog.Abnahme/` | **Die Abnahmetests der QA** (Nadja Reuter). Ansehen erlaubt — ändern tabu. |
| `docs/klassendiagramm.md` | Dein wachsendes Klassendiagramm (Bauplan des Systems). |

## Voraussetzungen

- **.NET SDK 8 oder neuer** — prüfen mit `dotnet --version`
  (Download: https://dotnet.microsoft.com/download)
- Ein Editor, z. B. **Visual Studio Code** (mit C# Dev Kit), Visual Studio oder Rider.

## Erste Schritte (Story 0)

Öffne ein Terminal im Repository-Ordner und führe aus:

```bash
dotnet test --filter Story00
```

Du siehst: **ein Test ist grün, einer ist rot.** Genau so soll es sein — der rote Test
sagt dir in seiner Meldung, was zu tun ist. Das ist dein Arbeitszyklus im ganzen Kurs:

1. **Story lesen** (Issue in diesem Repository): Ticket, User Story, Akzeptanzkriterien.
2. **Story-Branch anlegen** (unten) — für jede Story ein eigener, frisch vom aktuellen `main`.
3. **Code schreiben** in `ActiveLog.Src/`.
4. **Abnahme laufen lassen:** `dotnet test --filter StoryXX` — rote Meldungen sagen dir, was fehlt.
5. **Grün? Definition of Done prüfen** (unten) und committen.
6. **Pull Request öffnen und mergen** (unten) — dann geht es mit der nächsten Story weiter.

## Die wichtigsten Befehle

| Befehl | Wirkung |
|---|---|
| `dotnet test` | Alle Abnahmetests (alle Stories) ausführen |
| `dotnet test --filter Story03` | Nur die Abnahmetests von Story 3 ausführen |
| `dotnet run --project ActiveLog.Src` | Deine Entwicklungskonsole (`Program.cs`) starten |
| `dotnet build` | Nur kompilieren, ohne Tests |

## Definition of Done (gilt für jede Story)

- [ ] `dotnet test` ist **bis einschließlich deiner aktuellen Story grün** — auch die
      Tests früherer Stories laufen noch durch (keine Regression).
- [ ] Die OOP-Grundprinzipien sind eingehalten.
- [ ] Namen und Signaturen entsprechen der **API-Vereinbarung** der Story-Karte.
- [ ] Das Klassendiagramm in `docs/klassendiagramm.md` ist auf dem Stand deines Codes —
      modelliert werden nur die fachlichen Klassen (nicht `Program.cs` und `Projektinfo.cs`).
- [ ] Dein Stand ist committet, Nachricht nach dem Muster `Story 03: Intensität abgesichert`.
- [ ] Der Pull Request ist bei grüner CI nach `main` gemergt (siehe unten). Erst dann ist
      die Story fertig.

## Branch und Pull Request pro Story

`main` ist geschützt: Deine Arbeit kommt nur über einen Pull Request dorthin — und `main`
sammelt so Story für Story alles, was fertig ist.

```bash
git switch main
git pull                     # main mit allen gemergten Stories holen
git switch -c story03        # Story-Branch anlegen
# … Code schreiben, testen, committen …
git push -u origin story03
```

Dann auf GitHub:

1. **Pull Request als Entwurf eröffnen:** base `main` ← compare `story03`. **Der PR-Titel
   beginnt mit der Story**, z. B. `Story 03: Intensität abgesichert`.
2. **CI prüfen:** Rot? Lokal beheben, committen, pushen — der PR aktualisiert sich selbst.
3. **Grün und Checkliste abgehakt?** Erst **Ready for review**, dann **Merge pull request**.

Erst danach beginnt die nächste Story — wieder mit `git switch main` und `git pull`.

## Spielregeln

- Der Ordner `ActiveLog.Abnahme/` gehört der QA: **Abnahmetests werden nicht verändert** — wie im
  echten Projekt. Reinschauen ist ausdrücklich erlaubt (dort stehen keine Lösungen,
  nur Prüfungen).
- Ganz am Anfang sind fast alle Tests rot. Das ist **normal** — sie werden Story für
  Story grün. Du musst immer nur die Tests deiner aktuellen Story grün bekommen.

## Wenn du feststeckst

1. Lies die **rote Testmeldung** genau — sie ist als Hinweis formuliert.
2. Lies das passende **Kapitel im Mebis-Buch** (steht auf jeder Story-Karte).
3. Öffne die **Hinweis-Stufen** auf der Story-Karte (erst Stufe 1, dann Stufe 2).
4. Frag im **Standup-Forum** im Mebis-Kurs oder fordere einen **Workshop** an.
