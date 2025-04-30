using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ScottPlot;
using ComputerAssemblyServiceBackEnd.Models;
using Colors = QuestPDF.Helpers.Colors;

public class OrdersReportDocument : IDocument
{
    private readonly List<Order> _orders;
    private readonly DateOnly _fromDate;
    private readonly DateOnly _toDate;

    private byte[] GenerateChart()
    {
        var ordersByDate = _orders
            .GroupBy(o => o.CreatedAt.ToDateTime(TimeOnly.MinValue).Date)
            .OrderBy(g => g.Key)
            .ToDictionary(g => g.Key, g => g.Sum(x => x.TotalAmount));

        var dates = ordersByDate.Keys.Select(d => d.ToOADate()).ToArray();
        var values = ordersByDate.Values.ToArray();

        var plt = new ScottPlot.Plot();
        plt.Add.Scatter(dates, values);

        plt.Axes.Bottom.Label.Text = "Date";
        plt.Axes.Left.Label.Text = "Total (UAH)";
        plt.Axes.DateTimeTicksBottom();
        plt.Title("Order Totals per Day");
        
        var tempFile = Path.GetTempFileName() + ".png";
        plt.SavePng(tempFile, 600, 300);

        var bytes = File.ReadAllBytes(tempFile);
        File.Delete(tempFile);

        return bytes;
    }

    public OrdersReportDocument(List<Order> orders, DateOnly from, DateOnly to)
    {
        _orders = orders;
        _fromDate = from;
        _toDate = to;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
{
    container.Page(page =>
    {
        page.Margin(30);

        page.Header().Background("#181818").Padding(10).Row(row =>
        {
            row.ConstantItem(60).Image("/Users/twinter/Documents/Lapitech/DB_CourseWork/Source/Front-end/ComputerAssemblyServiceFrontEnd/src/assets/LogoPng/logo.png", ImageScaling.FitArea);
            row.RelativeItem().Column(col =>
            {
                col.Item().Text("Computer Assembly Service").FontSize(20).Bold().FontColor(Colors.White);
                col.Item().Text($"Orders Report: {_fromDate:dd.MM.yyyy} - {_toDate:dd.MM.yyyy}")
                    .FontSize(12).FontColor(Colors.Grey.Lighten2);
            });
        });

        page.Content().Column(col =>
        {
            col.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(80);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.ConstantColumn(100);
                });

                table.Header(header =>
                {
                    header.Cell().Text("Order #").Bold();
                    header.Cell().Text("Client ID").Bold();
                    header.Cell().Text("Status").Bold();
                    header.Cell().Text("Amount").Bold();
                });

                foreach (var order in _orders)
                {
                    table.Cell().Text(order.OrderId.ToString());
                    table.Cell().Text(order.ClientId?.ToString() ?? "N/A");
                    table.Cell().Text(order.Status.ToString());
                    table.Cell().Text($"{order.TotalAmount:F2} UAH");
                }

                table.Cell().ColumnSpan(4).PaddingTop(5).BorderTop(1).Text("");

                table.Cell().Text("");
                table.Cell().Text("");
                table.Cell().AlignRight().Text("Total:").Bold();
                table.Cell().Text($"{_orders.Sum(o => o.TotalAmount):F2} UAH").Bold();
            });

            // Chart
            var chartImage = GenerateChart();
            col.Item().PaddingTop(25).Image(chartImage, ImageScaling.FitWidth);
        });

        page.Footer().Background("#181818").Padding(10).AlignCenter().Text(txt =>
        {
            txt.Span("Generated ").FontColor(Colors.White).SemiBold();
            txt.Span(DateTime.Now.ToString("f")).FontColor(Colors.Grey.Lighten2);
        });
    });
}
}