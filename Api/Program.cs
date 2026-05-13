using Api.Extensions;
using Application;
using Infrastructure;
using Microsoft.OpenApi;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Air Ticket Management API",
        Version = "v1",
        Description = "API REST para el sistema de gestión de tiquetes aéreos. " +
                      "Cubre geografía, personas, aerolíneas, aeropuertos, aeronaves, " +
                      "rutas, tarifas, vuelos, personal, reservas, tiquetes, check-in, " +
                      "facturación, pagos y autenticación."
    });

    options.TagActionsBy(api =>
    {
        var tags = api.ActionDescriptor.EndpointMetadata
            .OfType<Microsoft.AspNetCore.Http.TagsAttribute>()
            .SelectMany(t => t.Tags)
            .ToList();
        return tags.Count > 0 ? tags : new List<string> { api.ActionDescriptor.RouteValues["controller"]! };
    });

    options.OrderActionsBy(api => $"{api.GroupName}_{api.RelativePath}");

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath);
});

builder.Services.ConfigureCors();
builder.Services.AddApplicationServices();
builder.Services.AddMapsterConfiguration();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Swagger: en Development siempre; en Production solo si Swagger:Enabled=true (appsettings).
var swaggerEnabled = app.Environment.IsDevelopment()
    || app.Configuration.GetValue("Swagger:Enabled", false);
if (swaggerEnabled)
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // Ruta relativa al documento en /swagger/ — evita URLs inválidas detrás de proxy o si el esquema cambia.
        options.SwaggerEndpoint("./v1/swagger.json", "Air Ticket API v1");
        options.RoutePrefix = "swagger";
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        options.DefaultModelsExpandDepth(-1);
    });
}

app.UseCors("CorsPolicy");

// En Development suele usarse solo HTTP (p. ej. localhost:5155); redirigir a HTTPS rompe "Try it out" / fetch si no hay HTTPS.
if (!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();
