using System.Reflection.Metadata;
using Meistaschaft;


class Program
{
    static void Main(string[] args)
    {
         // -----------------------------
        // TurnierManager erstellen
        // -----------------------------
        TurnierManager manager = new TurnierManager();
            
        if (args.Length == 0)
        {
            Console.WriteLine("Bitte Befehl angeben: new oder print");
            return;
        }

        string command = args[0].ToLower();

        if (command == "new")
        {
        // -----------------------------
        // Mannschaften erstellen
        // -----------------------------
        Mannschaft team1 = new Mannschaft("FC Bayern");
        Mannschaft team2 = new Mannschaft("Borussia Dortmund");
        Mannschaft team3 = new Mannschaft("RB Leipzig");
        Mannschaft team4 = new Mannschaft("VfB Stuttgart");

        // -----------------------------
        // Gruppe erstellen und Teams hinzufügen
        // -----------------------------
        Gruppe gruppeA = new Gruppe("Gruppe A");
        gruppeA.AddTeam(team1);
        gruppeA.AddTeam(team2);

        Gruppe gruppeB = new Gruppe("Gruppe B");
        gruppeB.AddTeam(team3);
        gruppeB.AddTeam(team4);

        manager.Gruppen.Add(gruppeA);
        manager.Gruppen.Add(gruppeB);

        // -----------------------------
        // Spiele erstellen
        // -----------------------------
        Spiel spiel1 = new Spiel(
            "S1",
            DateTime.Now.AddDays(1),
            team1,
            team2
        );
        spiel1.SetErgebnis("2:1");
        spiel1.SetQuote("Heimsieg", 1.8);
        spiel1.SetQuote("Unentschieden", 3.2);
        spiel1.SetQuote("Auswärtssieg", 4.5);

        Spiel spiel2 = new Spiel(
            "S2",
            DateTime.Now.AddDays(2),
            team3,
            team4
        );
        spiel2.SetErgebnis("1:1");
        spiel2.SetQuote("Heimsieg", 2.0);
        spiel2.SetQuote("Unentschieden", 3.0);
        spiel2.SetQuote("Auswärtssieg", 3.8);

        manager.Spiele.Add(spiel1);
        manager.Spiele.Add(spiel2);

        // -----------------------------
        // Benutzer erstellen
        // -----------------------------
        Benutzer benutzer1 = new Benutzer("Max", 100);
        Benutzer benutzer2 = new Benutzer("Anna", 50);

        manager.Benutzer.Add(benutzer1);
        manager.Benutzer.Add(benutzer2);

        // -----------------------------
        // Wetten erstellen
        // -----------------------------
        Wette wette1 = new Wette("Heimsieg", 1.8, 20);
        Wette wette2 = new Wette("Unentschieden", 3.0, 10);

        manager.Wetten.Add(wette1);
        manager.Wetten.Add(wette2);

        }

        //Ausgabe

        if (command == "print ")
        {
            Console.WriteLine("=== Gruppen ===");
        foreach (var g in manager.Gruppen)
        {
            Console.WriteLine($"Gruppe: {g}");
        }

        Console.WriteLine("\n=== Spiele ===");
        foreach (var s in manager.Spiele)
        {
            Console.WriteLine($"Spiel-ID: {s.Id}");
        }

        Console.WriteLine("\n=== Benutzer ===");
        foreach (var b in manager.Benutzer)
        {
            Console.WriteLine($"Benutzer: {b.Name}");
        }

        Console.WriteLine("\n=== Wetten ===");
        foreach (var w in manager.Wetten)
        {
            Console.WriteLine("Wette vorhanden");
        }
        }
        
    }
}
