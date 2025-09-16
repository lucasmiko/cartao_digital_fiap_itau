using System.Text.Json;

public class ClientesFileRepo
{
    private readonly string _path = "clientes.json";
    private readonly JsonSerializerOptions _opt = new(JsonSerializerDefaults.Web)
    { WriteIndented = true };

    public List<Customer> Load()
    {
        if (!File.Exists(_path)) return new();
        var json = File.ReadAllText(_path);
        return JsonSerializer.Deserialize<List<Customer>>(json, _opt) ?? new();
    }

    public void Save(List<Customer> data)
    {
        var json = JsonSerializer.Serialize(data, _opt);
        File.WriteAllText(_path, json);
    }

    public void Add(Customer c)
    {
        var todos = Load();
        todos.Add(c);
        Save(todos);
    }
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}