using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Business.Implementations;
using RestWithASPNETUdemy.Repository; 
using Serilog;
using Pomelo.EntityFrameworkCore.MySql;
using RestWithASPNETUdemy.Repository.Generic;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger(); 

// Add services to the container.
builder.Services.AddControllers();


// Connection 
//var connection = builder.Configuration["MySQLConnection: MySQLConnectionString"];
//var connection = builder.Configuration.GetConnectionString("MySQLConnection");
var connection = "Server=localhost;DataBase=rest_with_asp_net_udemy;Uid=root;Pwd=admin123";
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

if (builder.Environment.IsDevelopment())
{
    // cria as migrations
    MigrateDatabase(connection);
} 

// Versioning API
builder.Services.AddApiVersioning();

// Dependency Injection
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>(); 
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// method migration
void MigrateDatabase(string connection)
{
    try
    {
        var evolveConnection = new MySqlConnector.MySqlConnection(connection);
        var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
        {
            Locations = new List<string> { "db/migrations","db/dataset" },
            IsEraseDisabled = true 
        };
        evolve.Migrate();
    }
    catch (Exception ex)
    {
        Log.Error("Database migration failed " + ex);
        throw;
    }
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
