using System.Text.Json;

namespace DigitalBank.Api.Infrastructure.Repositories;

public abstract class RepositoryBase<T>
{
    private readonly string _filePath;
    private static readonly object _fileLock = new();
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true
    };

    public RepositoryBase(IHostEnvironment env, string fileName)
    {
        var dataDirectory = Path.Combine(env.ContentRootPath, "Data");
        Directory.CreateDirectory(dataDirectory);

        _filePath = Path.Combine(dataDirectory, fileName);
        EnsureFileExistsAndValid();
    }

    protected TResult WithLock<TResult>(Func<List<T>, TResult> action)
    {
        lock (_fileLock)
        {
            var items = LoadUnlocked();
            var result = action(items);
            SaveUnlocked(items);
            return result;
        }
    }

    protected void WithLock(Action<List<T>> action)
    {
        lock (_fileLock)
        {
            var items = LoadUnlocked();
            action(items);
            SaveUnlocked(items);
        }
    }

    protected TResult ReadOnly<TResult>(Func<List<T>, TResult> action)
    {
        lock (_fileLock)
        {
            var items = LoadUnlocked();
            return action(items);
        }
    }


    private void EnsureFileExistsAndValid()
    {
        try
        {
            if (!File.Exists(_filePath) || new FileInfo(_filePath).Length == 0)
            {
                File.WriteAllText(_filePath, "[]");
                return;
            }

            var text = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(text))
            {
                File.WriteAllText(_filePath, "[]");
                return;
            }

            using var _ = JsonDocument.Parse(text);
        }
        catch
        {
            File.WriteAllText(_filePath, "[]");
        }
    }

    private List<T> LoadUnlocked()
    {
        try
        {
            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<T>();
            }

            return JsonSerializer.Deserialize<List<T>>(json, _jsonOptions) ?? new List<T>();
        }
        catch
        {
            File.WriteAllText(_filePath, "[]");
            return new List<T>();
        }
    }

    private void SaveUnlocked(List<T> items)
    {
        var json = JsonSerializer.Serialize(items, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }
}