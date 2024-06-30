using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public class FileStorageService<T>
{
    private readonly string _filePath;

    public FileStorageService(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<List<T>> LoadDataAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new List<T>();
        }

        var jsonData = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<T>>(jsonData);
    }

    public async Task SaveDataAsync(List<T> data)
    {
        var jsonData = JsonSerializer.Serialize(data);
        await File.WriteAllTextAsync(_filePath, jsonData);
    }

    public async Task DeleteDataAsync(Predicate<T> match)
    {
        var data = await LoadDataAsync();
        var itemToRemove = data.Find(match);

        if (itemToRemove != null)
        {
            data.Remove(itemToRemove);
            await SaveDataAsync(data);
        }
    }
}
