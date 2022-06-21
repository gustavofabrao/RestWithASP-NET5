using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Business.Implementations;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Repository.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Connection 
//var connection = builder.Configuration["MySQLConnection: MySQLConnectionString"];
//var connection = builder.Configuration.GetConnectionString("MySQLConnection");
var connection = "Server=localhost;DataBase=rest_with_asp_net_udemy;Uid=root;Pwd=admin123";
//builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 21))));

// Versioning API
builder.Services.AddApiVersioning();

// Dependency Injection
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
