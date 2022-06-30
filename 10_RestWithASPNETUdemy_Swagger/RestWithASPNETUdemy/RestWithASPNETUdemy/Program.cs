using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Business.Implementations;
using RestWithASPNETUdemy.Repository; 
using Serilog; 
using RestWithASPNETUdemy.Repository.Generic;
using Microsoft.Net.Http.Headers;
using RestWithASPNETUdemy.Hypermedia.Filters;
using RestWithASPNETUdemy.Hypermedia.Enricher;
using Microsoft.AspNetCore.Rewrite;

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

builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
}).AddXmlSerializerFormatters();

var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

builder.Services.AddSingleton(filterOptions);

// Versioning API
builder.Services.AddApiVersioning();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "REST API's From 0 to Azure with ASP.NET Core 5 and Docker",
            Version = "v1",
            Description = "API RESTful develop in course 'REST API's From 0 to Azure with ASP.NET Core 5 and Docker'",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            {
                Name = "Gustavo Abrão",
                Url = new Uri("https://github.com/gustavofabrao")
            }
        });
});

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

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", 
                      "REST API's From 0 to Azure with ASP.NET Core 5 and Docker - v1");
});

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");

app.Run();
