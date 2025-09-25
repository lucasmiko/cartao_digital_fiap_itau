using MiniCheckout.Models;

namespace MiniCheckout.Application.Interfaces;

public interface IReceiptExporter
{
    (string txtPath, string jsonPath) Export(
        string orderId,
        IEnumerable<ReceiptItem> lines,
        decimal subTotal,
        decimal total
    );
}