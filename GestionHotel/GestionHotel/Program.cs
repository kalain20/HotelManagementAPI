using GestionHotel.Modeles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "GestionHotel API",
        Version = "v1",
        Description = "API pour la gestion d'hôtel",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Support",
            Email = "support@hotel.com"
        }
    });
    // Inclure la documentation XML
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Add DbContext
builder.Services.AddDbContext<GestionHotelDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GestionHotel API v1");
    c.RoutePrefix = ""; // Swagger à la racine /
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Test endpoint
app.MapGet("/api/test", () => "API fonctionnelle")
    .WithName("Test");
    
    

app.Run();