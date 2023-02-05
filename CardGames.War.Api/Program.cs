using Microsoft.EntityFrameworkCore;
using CardGames.War.Api.Business;
using CardGames.War.Api.Business.Interfaces;
using CardGames.War.Api.DataAccess;
using CardGames.War.Api.DataAccess.Interfaces;
var MyAllowSpecificOrigins = "_myAllowSpecificOriginsForWarCardGameApp";

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApiDb");
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy
                            .WithOrigins("https://localhost:7140")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add db services to SQL server
builder.Services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(connectionString));

// Inject (create the instances) for the business and the data access layer.
builder.Services.AddScoped<IGameBusiness, GameBusiness>();
builder.Services.AddScoped<IGameData, GameData>();

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

app.Run();
