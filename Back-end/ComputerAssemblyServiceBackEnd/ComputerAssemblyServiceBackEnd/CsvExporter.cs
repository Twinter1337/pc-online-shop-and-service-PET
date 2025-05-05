using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ComputerAssemblyServiceBackEnd;

public class CsvTableExporter
{
    private readonly DbContext _context;

    public CsvTableExporter(DbContext context)
    {
        _context = context;
    }

    public async Task ExportTableToCsvFileAsync<T>() where T : class
    {
        var dbSet = _context.Set<T>();
        var data = await dbSet.ToListAsync();

        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var sb = new StringBuilder();

        sb.AppendLine(string.Join(",", properties.Select(p => p.Name)));
        
        foreach (var item in data)
        {
            var values = properties.Select(p =>
            {
                var value = p.GetValue(item);
                return value?.ToString()?.Replace("\"", "\"\"") ?? "";
            });

            sb.AppendLine(string.Join(",", values.Select(v => $"\"{v}\"")));
        }

        string fileName = $"/Users/twinter/Downloads/{typeof(T).Name}.csv";
        await File.WriteAllTextAsync(fileName, sb.ToString());
    }
}