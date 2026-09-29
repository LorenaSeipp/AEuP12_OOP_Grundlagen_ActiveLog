using System.Reflection;
using Xunit.Sdk;
using ActiveLog.Src;

namespace ActiveLog.Abnahme;

/// <summary>
/// Findet die Klassen und Member deiner Lösung per Reflection.
/// Vorteil: Dieses Test-Projekt kompiliert immer — auch wenn deine Klassen
/// noch fehlen. Statt Compiler-Fehlern bekommst du verständliche Meldungen.
/// (Du musst diese Datei nicht verstehen, um den Kurs zu bearbeiten.)
/// </summary>
internal static class Hilfe
{
    private static readonly Assembly App = typeof(Projektinfo).Assembly;

    public static XunitException Fehler(string text) => new("❌ " + text);

    public static Type Typ(string name)
    {
        var typ = App.GetTypes().FirstOrDefault(t => t.Name == name);
        return typ ?? throw Fehler(
            $"Der Typ '{name}' existiert noch nicht im Projekt ActiveLog. " +
            $"Lege ihn an (z. B. Datei ActiveLog.Src/{name}.cs) — Name exakt wie in der API-Vereinbarung der Story.");
    }

    public static object Neu(string typName, params object?[] argumente)
    {
        var typ = Typ(typName);
        if (typ.IsAbstract)
            throw Fehler($"'{typName}' ist abstrakt und kann nicht direkt mit new erzeugt werden.");
        try
        {
            return Activator.CreateInstance(typ, argumente)!;
        }
        catch (MissingMethodException)
        {
            var signatur = string.Join(", ", argumente.Select(a => a?.GetType().Name ?? "null"));
            throw Fehler($"'{typName}' hat keinen öffentlichen Konstruktor mit ({signatur}). " +
                         "Vergleiche mit der API-Vereinbarung auf der Story-Karte.");
        }
        catch (TargetInvocationException e) when (e.InnerException is not null)
        {
            throw Fehler($"Der Konstruktor von '{typName}' hat eine Exception geworfen: {e.InnerException.Message}");
        }
    }

    public static object? Lies(object objekt, string property)
    {
        var p = objekt.GetType().GetProperty(property)
            ?? throw Fehler($"Die Property '{property}' fehlt auf '{objekt.GetType().Name}' (oder ist nicht public).");
        if (p.GetMethod is null || !p.GetMethod.IsPublic)
            throw Fehler($"Die Property '{objekt.GetType().Name}.{property}' hat keinen öffentlichen Getter.");
        return p.GetValue(objekt);
    }

    public static void Setze(object objekt, string property, object? wert)
    {
        var p = objekt.GetType().GetProperty(property)
            ?? throw Fehler($"Die Property '{property}' fehlt auf '{objekt.GetType().Name}' (oder ist nicht public).");
        if (p.SetMethod is null || !p.SetMethod.IsPublic)
            throw Fehler($"Die Property '{objekt.GetType().Name}.{property}' lässt sich von außen nicht setzen " +
                         "(öffentlicher Setter fehlt) — laut API-Vereinbarung soll das hier aber gehen.");
        try
        {
            p.SetValue(objekt, wert);
        }
        catch (TargetInvocationException e) when (e.InnerException is not null)
        {
            throw Fehler($"Beim Setzen von '{property}' auf '{objekt.GetType().Name}' flog eine Exception: {e.InnerException.Message}");
        }
    }

    public static object? Rufe(object objekt, string methode, params object?[] argumente)
    {
        var typ = objekt.GetType();
        var parameterTypen = argumente.Select(a => a?.GetType() ?? typeof(object)).ToArray();
        var m = typ.GetMethod(methode, parameterTypen)
            ?? typ.GetMethods().FirstOrDefault(x => x.Name == methode && x.GetParameters().Length == argumente.Length)
            ?? throw Fehler($"Die Methode '{methode}' fehlt auf '{typ.Name}' " +
                            $"(oder erwartet andere Parameter als vereinbart: {argumente.Length} Argument(e)).");
        try
        {
            return m.Invoke(objekt, argumente);
        }
        catch (TargetInvocationException e) when (e.InnerException is not null)
        {
            throw Fehler($"Beim Aufruf von '{typ.Name}.{methode}(...)' flog eine Exception: {e.InnerException.Message}");
        }
    }

    /// <summary>Scheitert, wenn die Property einen öffentlichen Setter hat.</summary>
    public static void NurLesbar(string typName, string property)
    {
        var p = Typ(typName).GetProperty(property)
            ?? throw Fehler($"Die Property '{property}' fehlt auf '{typName}' (oder ist nicht public).");
        if (p.SetMethod is not null && p.SetMethod.IsPublic)
            throw Fehler($"'{typName}.{property}' lässt sich von außen ändern. " +
                         "Vereinbart ist: von außen nur lesbar (kein öffentlicher Setter).");
    }

    /// <summary>Scheitert, wenn die Klasse öffentliche Instanz-Felder hat (Kapselung).</summary>
    public static void KeineOeffentlichenFelder(string typName)
    {
        var felder = Typ(typName).GetFields(BindingFlags.Public | BindingFlags.Instance);
        if (felder.Length > 0)
            throw Fehler($"'{typName}' hat öffentliche Felder ({string.Join(", ", felder.Select(f => f.Name))}). " +
                         "Mache die Felder privat — nach außen nur Properties und Methoden anbieten.");
    }
}
