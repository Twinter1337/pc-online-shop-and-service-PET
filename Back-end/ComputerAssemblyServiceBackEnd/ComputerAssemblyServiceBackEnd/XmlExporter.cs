using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd;

public class XmlTableExporter
{
    private readonly DbContext _context;

    public XmlTableExporter(DbContext context)
    {
        _context = context;
    }

    public async Task ExportTableToXmlFileAsync<T>() where T : class
    {
        var dbSet = _context.Set<T>();
        var data = await dbSet.ToListAsync();

        var serializer = new XmlSerializer(typeof(List<T>));

        string fileName = $"/Users/twinter/Downloads/{typeof(T).Name}.xml";
        await using var stream = File.Create(fileName);
        serializer.Serialize(stream, data);
    }
}