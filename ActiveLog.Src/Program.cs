using ActiveLog.Src;

// =====================================================================
//  ActiveLog 2.0 — Entwicklungskonsole
//
//  Diese Datei gehört dir. Hier probierst du deine Klassen live aus:
//      dotnet run --project ActiveLog.Src
//
//  Die Abnahmetests der QA laufen unabhängig davon mit:
//      dotnet test
// =====================================================================

Console.WriteLine($"{Projektinfo.Produkt} — Entwicklungskonsole");
Console.WriteLine($"Am Werk: {Projektinfo.Entwickler}");
Console.WriteLine();

//--- Sprint 1: Sobald deine Kraftübung steht (Story 1–4), z. B.: -----
Kraftuebung bankdruecken = new Kraftuebung("Bankdrücken");
bankdruecken.Aktivieren();
Console.WriteLine(bankdruecken.Statuszeile());

// --- Sprint 2: Familienfoto — mehrere Übungen, ein Bericht (Story 5–7):
List<Uebung> uebungen = new List<Uebung> { /* deine Übungen */ };
foreach (Uebung uebung in uebungen)
{

     Console.WriteLine(uebung.Statuszeile());
 }

// --- Sprint 3: Dein Musterplan mit der Szene „Regenerationswoche" (Story 8–9):
Trainingsplan plan = new Trainingsplan("Musterplan Messe");
Trainingseinheit pushDay = plan.EinheitAnlegen("Push-Day");
pushDay.UebungHinzufuegen(bankdruecken);
// ...
Console.WriteLine(plan.Gesamtbericht());