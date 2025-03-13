using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class FileController : IFileController
{
    private readonly string filePath = "people.json";

    public void WriteToFile(string filePath, string content)
    {
        File.WriteAllText(filePath, content);
        Console.WriteLine($"Data skrevet til filen: {filePath}");
    }

    public string ReadFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }
        return string.Empty;
    }

    public void WriteJsonToFile<T>(string filePath, T data)
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(filePath, json);
        Console.WriteLine($"JSON-data lagret til fil: {filePath}");
    }

    public T ReadJsonFromFile<T>(string filePath) where T : new()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            var deserialized = JsonConvert.DeserializeObject<T>(json);

            // Hvis JSON-dataen er ugyldig eller null, returner en ny instans av T
            return deserialized ?? Activator.CreateInstance<T>();
        }

        // Hvis filen ikke finnes, returner en ny instans av T
        return Activator.CreateInstance<T>();
    }


    public List<Person> GetPeople()
    {
        return ReadJsonFromFile<List<Person>>(filePath) ?? new List<Person>();
    }

    public void SavePeople(List<Person> people)
    {
        WriteJsonToFile(filePath, people);
    }

    // Ny metode for å vise JSON-data i terminalen
    public void DisplayJsonData()
    {
        string json = ReadFromFile(filePath);
        if (!string.IsNullOrEmpty(json))
        {
            Console.WriteLine("\nInnhold i JSON-filen:");
            Console.WriteLine(json);
        }
        else
        {
            Console.WriteLine("JSON-filen er tom eller finnes ikke.");
        }
    }
}
