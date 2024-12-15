using ServerAPI.Repositories;
using ServerAPI.Services;
using ServerAPI.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using Core.Models;
var builder = WebApplication.CreateBuilder(args);

// Registrer enum-serializers for Status og Rolle
BsonSerializer.RegisterSerializer(typeof(Status), new EnumSerializer<Status>(BsonType.String));
BsonSerializer.RegisterSerializer(typeof(Rolle), new EnumSerializer<Rolle>(BsonType.String));

// Tilføj MongoDB-indstillinger fra appsettings.json
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// Registrer MongoDB-komponenter som services
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddSingleton<MongoDbService>();

// Registrer repositories
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IOpgaveRepository, OpgaveRepository>();
builder.Services.AddScoped<IBrugerRepository, BrugerRepository>();

// CORS-politik
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Konfigurer controllers med JSON-indstillinger
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Swagger-konfiguration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger UI og middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
