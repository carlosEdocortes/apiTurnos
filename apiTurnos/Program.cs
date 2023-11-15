using apiTurnos.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//agregamos la configuración del dbContext

builder.Services.AddDbContext<PruebaNetContext>(objConexion => objConexion.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSql")));
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

});

var reglasCors = "reglasCors";
builder.Services.AddCors(obj =>
{
    obj.AddPolicy(name: reglasCors, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(reglasCors);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
