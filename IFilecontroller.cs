public interface IFileController
{
    void WriteToFile(string filePath, string content);
    string ReadFromFile(string filePath);
    void WriteJsonToFile<T>(string filePath, T data);
    T ReadJsonFromFile<T>(string filePath) where T : new(); // ✅ Legg til new() begrensning
    List<Person> GetPeople();
    void SavePeople(List<Person> people);
    void DisplayJsonData();
}
