using System;
using System.Text.Json;

namespace DesafioFindesAPI.Infra.Repositories
{
	public class PersistenceRepository implements IPersistenceRepository
    {
        private readonly string _filePath;

        public PersistenceRepository(string filePath)
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
    }
}

