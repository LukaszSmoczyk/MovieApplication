using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MovieApplication.Data;
using MovieApplication.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Seed data from files
var filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data"));
var genreList = SeedData.SeedGenreData(filePath);
var movieList = SeedData.SeedMovieData(filePath);
// Add services to the container.
builder.Services.AddSingleton(movieList);
builder.Services.AddSingleton(genreList);
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieApplication", Version = "v1" });
    var commentsfileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
    var filePath = Path.Combine(AppContext.BaseDirectory, "MovieApplication.xml");
    c.IncludeXmlComments(filePath);
    c.CustomSchemaIds(t => t.FullName);
});

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
