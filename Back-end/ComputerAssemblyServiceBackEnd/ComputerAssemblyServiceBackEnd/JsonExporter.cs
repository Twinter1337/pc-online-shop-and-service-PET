using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd;

public class JsonTableExporter
{
    private readonly DbContext _context;

    public JsonTableExporter(DbContext context)
    {
        _context = context;
    }

    public async Task ExportTableToJsonFileAsync<T>() where T : class
    {
        var dbSet = _context.Set<T>();
        var data = await dbSet.ToListAsync();

        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        string json = JsonSerializer.Serialize(data, jsonOptions);

        string fileName = $"/Users/twinter/Downloads/{typeof(T).Name}.json";
        await File.WriteAllTextAsync(fileName, json);
    }
}