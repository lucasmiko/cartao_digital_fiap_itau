
using DigitalBank.Api.Application.Services;
using DigitalBank.Api.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adicionar os controllers e habilitar o Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<IAccountRepository, AccountRepository>();
builder.Services.AddSingleton<ICardRepository, CardRepository>(); // <<<<<<<<<<<<<<<<

builder.Services.AddSingleton<AccountService>();
builder.Services.AddSingleton<CardService>(); // <<<<<<<<<<<<<<<<

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();