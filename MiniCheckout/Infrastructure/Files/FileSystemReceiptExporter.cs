using System.Text;
using System.Text.Json;
using MiniCheckout.Application.Interfaces;
using MiniCheckout.Models;

namespace MiniCheckout.Infrastructure.Files;

public class FileSystemReceiptExporter : IReceiptExporter
{
    private readonly string _baseFolder;

    public FileSystemReceiptExporter(string? baseFolder = null)
    {
        _baseFolder = baseFolder ?? "./exports";
    }

    public (string txtPath, string jsonPath) Export(
        string orderId,
        IEnumerable<ReceiptItem> lines,
        decimal subtotal,
        decimal total)
    {
        Directory.CreateDirectory(_baseFolder);

        var timestamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");
        var baseName = $"receipt-{orderId}-{timestamp}";

        var txtPath = Path.Combine(_baseFolder, $"{baseName}.txt");
        var jsonPath = Path.Combine(_baseFolder, $"{baseName}.json");

        // ===== TXT =====
        var sb = new StringBuilder();
        sb.AppendLine("NOVO TITULO PARA O ARQUIVO!");
        sb.AppendLine($"ORDER: {orderId}");
        sb.AppendLine("ITEMS:");
        foreach (var l in lines)
            sb.AppendLine($"- {l.Name} x{l.Quantity} @ {l.UnitPrice:C} = {l.LineTotal:C}");
        sb.AppendLine($"Subtotal: {subtotal:C}");
        sb.AppendLine($"Total:    {total:C}");

        File.WriteAllText(txtPath, sb.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));

        // ===== JSON =====
        var payload = new
        {
            orderId,
            items = lines.Select(l => new { l.Name, l.UnitPrice, l.Quantity, l.LineTotal }),
            subtotal,
            total
        };

        var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonPath, json, new UTF8Encoding(false));

        return (txtPath, jsonPath);
    }
}
