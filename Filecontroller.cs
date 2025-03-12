using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class FileController : IFileController
{
    private readonly string filePath = "people.json";

    // Implementere WriteToFile som forespurt i IFileController
    public void WriteToFile(string filePath, string content)
    {
        File.WriteAllText(filePath, content);
        Console.WriteLine($"Data skrevet til filen: {filePath}");
    }

    public void WriteJsonToFile<T>(string filePath, T data)
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(filePath, json);
        Console.WriteLine($"JSON-data lagret til fil: {filePath}");
    }

    public string ReadFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }
        return string.Empty;
    }

    public T ReadJsonFromFile<T>(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
        return default;
    }

    public List<Person> GetPeople()
    {
        return ReadJsonFromFile<List<Person>>(filePath) ?? new List<Person>();
    }

    public void SavePeople(List<Person> people)
    {
        WriteJsonToFile(filePath, people);
    }
}
