using ApiNet;
using ApiNet.Helpers;
using ApiNet.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var Connection = Environment.GetEnvironmentVariable("Connection");

if (!string.IsNullOrEmpty(Connection))
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
                       options.UseNpgsql(Connection));
}
else
{
    throw new Exception("La variable de entorno Connection no está configurada.");
}

builder.Services.AddTransient<IServiceEquipo,ServiceEquipo>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
