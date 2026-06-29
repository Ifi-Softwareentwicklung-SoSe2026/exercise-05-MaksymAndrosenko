using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Meistaschaft;

public class Gruppe
{
    private string name;
    private List<Mannschaft> teams;

    public Gruppe(string name)
    {
        this.name = name;
        teams = new List<Mannschaft>();
    }

    public void AddTeam(Mannschaft team)
    {
        teams.Add(team);
    }
}

public class Mannschaft
{
    private string name;
    public Mannschaft(string name)
    {
        this.name = name;
    }

    public string GetName()
    {
        return name;
    }
}

public class Spiel
{
    private string spielId;
    public string Id
    {
        get => spielId;
        set => spielId = value;
    }
    private DateTime datum;
    private Mannschaft heimMannschaft;
    private Mannschaft auswaertsMannschaft;
    private string ergebnis;
    private Dictionary<string, double> quoten;

    public Spiel(string spielId, DateTime datum, Mannschaft heimMannschaft, Mannschaft auswaertsMannschaft)
    {
        this.spielId = spielId;
        this.datum = datum;
        this.heimMannschaft = heimMannschaft;
        this.auswaertsMannschaft = auswaertsMannschaft;

        ergebnis = "";
        quoten = new Dictionary<string, double>();
    }

    public void SetErgebnis(string score)
    {
        ergebnis = score;
    }

    public void SetQuote(string typ, double quote)
    {
        quoten[typ] = quote;
    }

    public double GetQuote(string typ)
    {
        return quoten[typ];
    }
}

public class Benutzer
{
    private string name;
    public string Name
    {
        get => name;
        set => name = value;
    }

    private double guthaben;

    public Benutzer(string name, double guthaben)
    {
        this.name = name;
        this.guthaben = guthaben;
    }

    public void UpdateGuthaben(double amount)
    {
        guthaben += amount;
    }
}

public class Wette
{
    private string wetttyp;
    private double quote;
    private double einsatz;
    private bool istAusgewertet;

    public Wette(string wetttyp, double quote, double einsatz)
    {
        this.wetttyp = wetttyp;
        this.quote = quote;
        this.einsatz = einsatz;
        istAusgewertet = false;
    }

    public double BerechneGewinn(string ergebnis)
    {
        // Заглушка
        return einsatz * quote;
    }
}

public class TurnierManager
{
    public List<Gruppe> Gruppen { get; set; }
    public List<Spiel> Spiele { get; set; }
    public List<Benutzer> Benutzer { get; set; }
    public List<Wette> Wetten { get; set; }

    public TurnierManager()
    {
        Gruppen = new List<Gruppe>();
        Spiele = new List<Spiel>();
        Benutzer = new List<Benutzer>();
        Wetten = new List<Wette>();
    }

    public void SaveToJson(string filePath)
    {
        try
        {
            string json = JsonSerializer.Serialize(
                this,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Speichern der JSON-Datei: {ex.Message}");
        }
    }

    public bool LoadFromJson(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Datei nicht gefunden: {filePath}");
                return false;
            }

            string json = File.ReadAllText(filePath);

            TurnierManager? data =
                JsonSerializer.Deserialize<TurnierManager>(json);

            if (data == null)
            {
                Console.WriteLine("Fehler: JSON konnte nicht deserialisiert werden.");
                return false;
            }

            Gruppen = data.Gruppen;
            Spiele = data.Spiele;
            Benutzer = data.Benutzer;
            Wetten = data.Wetten;

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Laden der JSON-Datei: {ex.Message}");
            return false;
        }
    }

    public Spiel? FindSpielById(string id)
    {
       foreach (Spiel spiel in Spiele)
        {
            if (spiel != null && spiel.Id == id) 
            {
                return spiel;
            }
        }

        return null;
    }

    public Benutzer? FindBenutzerByName(string name)
    {
        foreach (Benutzer b in Benutzer)
        {
            if (b != null && b.Name == name) 
            {
                return b;
            }
        }

        return null;
    }
}
