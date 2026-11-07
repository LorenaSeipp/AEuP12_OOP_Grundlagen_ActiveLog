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

Trainingsplan plan = new Trainingsplan("Musterplan Messe");
Trainingseinheit pushDay = plan.EinheitAnlegen("Push-Day");

Console.WriteLine(plan.Gesamtbericht());

// Übungen anlegen 
Laufeinheit intervalllaeufe = new Laufeinheit("Intervallläufe");
intervalllaeufe.Aktivieren();
Schwimmeinheit schwimmen  = new Schwimmeinheit("Schwimmen");
schwimmen.Aktivieren();

pushDay.UebungHinzufuegen(bankdruecken);

Trainingseinheit cardio = plan.EinheitAnlegen("Cardio-Day");
cardio.UebungHinzufuegen(schwimmen);
cardio.UebungHinzufuegen(intervalllaeufe);

Console.WriteLine(plan.Gesamtbericht());

// Regenerationswoche
plan.AllesDeaktivieren();
Console.WriteLine();
Console.WriteLine(plan.Gesamtbericht());

// Szene "Wettkampfwoche": die Liste mit Übungen und ihren Einstellungen
Wettkampfwoche wettkampfwoche = new Wettkampfwoche("Wettkampfwoche 1");
Trainingseinheit trainingseinheit = wettkampfwoche.EinheitAnlegen("Trainingstag 1"); 
trainingseinheit.UebungHinzufuegen(new Kraftuebung("Kraftuebung"));
trainingseinheit.UebungHinzufuegen(new Schwimmeinheit("Schwimmen"));


Trainingseinheit trainingseinheit2 = wettkampfwoche.EinheitAnlegen("Trainingstag 2"); 
trainingseinheit2.UebungHinzufuegen(new Laufeinheit("Laufen"));
trainingseinheit2.UebungHinzufuegen(new Mobilityuebung("Mobilityuebung"));

Einstellung einstellung = new Einstellung(80, 120, 30, 23);

wettkampfwoche.Anwenden(einstellung);

Console.WriteLine();
Console.WriteLine(wettkampfwoche.Gesamtbericht());