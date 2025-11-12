using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao container
builder.Services.AddControllers();

// 💡 Adiciona o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 💡 Ativa o Swagger no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
