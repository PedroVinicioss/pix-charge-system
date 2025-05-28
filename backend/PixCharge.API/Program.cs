var builder = WebApplication.CreateBuilder(args);

// Registra os serviços necessários
builder.Services.AddControllers(); // <- IMPORTANTE
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers(); // <- Mapeia os Controllers automaticamente

app.Run();
