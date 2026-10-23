var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<backend.Services.ITicketService, backend.Services.TicketService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Служебные адреса (Minimal API — допустимо для /health и /info)
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.MapGet("/info", () => Results.Ok(new
{
    name = "WEB API — Учёт системного оборудования",
    environment = app.Environment.EnvironmentName,
    lab = "ЛР7",
    version = "0.1.0"
}));

app.Run();
