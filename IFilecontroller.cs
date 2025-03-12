using System.Collections.Generic;

public interface IFileController
{
    void WriteToFile(string filePath, string content);
    string ReadFromFile(string filePath);
    void WriteJsonToFile<T>(string filePath, T data);
    T ReadJsonFromFile<T>(string filePath);
}
