
//Program.cs

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

// IMPORTANT: Your XML is in Infrastructure/Data/artifacts.xml (not Api/Data)
// ContentRootPath is the API project folder, so we go up one level to backend root, then to Infrastructure/Data.
var xmlPath = Path.GetFullPath(
    Path.Combine(builder.Environment.ContentRootPath, "..", "Museum.Collection.Catalog.Infrastructure", "Data", "artifacts.xml")
);

Console.WriteLine("LOADING XML FROM: " + xmlPath);

builder.Services.AddSingleton<IArtifactRepository>(_ => new XmlArtifactRepository(xmlPath));

var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();