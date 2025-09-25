using MiniCheckout.Api.DTOs;
using MiniCheckout.Application.Interfaces;
using MiniCheckout.Application.Discounts;
using MiniCheckout.Application.Services;
using MiniCheckout.Domain;
using MiniCheckout.Infrastructure.Repositories;
using MiniCheckout.Infrastructure.Files;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// DI
builder.Services.AddScoped<IProductRepository, InMemoryProductRepository>();
builder.Services.AddSingleton<NoDiscount>();
builder.Services.AddSingleton<TenPercentDiscount>();
builder.Services.AddScoped<IReceiptExporter>(_ => new FileSystemReceiptExporter("./exports"));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/products", (IProductRepository repo)
    => Results.Ok(repo.GetAll())).WithName("GetProducts").WithOpenApi();

app.MapPost("checkout/calculate", (
    CheckoutRequest request,
    IProductRepository repo,
    NoDiscount noDiscount,
    TenPercentDiscount tenPercentageDiscount,
    IReceiptExporter exporter,
    string? discount,
    bool export = false) =>
{
    var strategy = (discount?.ToLowerInvariant()) switch
    {
        "ten" or "tenpercentage" or "10" => (MiniCheckout.Application.Discounts.IDiscountStrategy)tenPercentageDiscount,
        _ => noDiscount
    };

    var checkoutService = new CheckoutService(repo, strategy);

    try
    {
        foreach (var item in request.Items)
        {
            checkoutService.AddItem(item.ProductId, item.Quantity);
        }

        var items = checkoutService.GetItems();
        var lines = items.Select(i => new CheckoutLineDto(
            i.product.Id,
            i.product.Name,
            i.product.Price,
            i.quantity,
            i.product.Price * i.quantity)).ToList();

        var subTotal = lines.Sum(l => l.TotalPrice);
        var total = checkoutService.GetTotal();

        string? txtPath = null;
        string? jsonPath = null;

        if (export)
        {
            var receiptItems = lines.Select(l => new MiniCheckout.Models.ReceiptItem(
                l.ProductName,
                l.UnitPrice,
                l.Quantity,
                l.TotalPrice)).ToList();

            var orderId = Guid.NewGuid().ToString()[..8];
            (txtPath, jsonPath) = exporter.Export(orderId, receiptItems, subTotal, total);
            Console.WriteLine($"Receipt exported to: {txtPath} and {jsonPath}");
        }

        var response = new CheckoutResponse(
            lines,
            subTotal,
            strategy.GetType().Name,
            total,
            txtPath,
            jsonPath);

            return Results.Ok(response);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }

   
}).WithName("CalculateCheckout").WithOpenApi();


app.Run();