using Museum.Collection.Catalog.Application.Services;
using Museum.Collection.Catalog.Domain.Interfaces;
using Museum.Collection.Catalog.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// XML path (your existing logic)
var xmlPath = Path.GetFullPath(
    Path.Combine(builder.Environment.ContentRootPath, "..", "Museum.Collection.Catalog.Infrastructure", "Data", "artifacts.xml")
);

Console.WriteLine("LOADING XML FROM: " + xmlPath);

// Repo in Infrastructure
builder.Services.AddSingleton<IArtifactRepository>(_ => new XmlArtifactRepository(xmlPath));

// Service in Application
builder.Services.AddScoped<IArtifactService, ArtifactService>();

var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();