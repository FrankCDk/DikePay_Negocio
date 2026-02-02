using System.Reflection;
using System.Text;
using Asp.Versioning;
using DikePay.Modules.Auth.Infrastructure;
using DikePay.Modules.Catalog.Infrastructure;
using DikePay.Modules.Configuration.Infrastructure;
using DikePay.Modules.Promotions.Infrastructure;
using DikePay.Shared.Infrastructure.Behaviors;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    // Desactiva la comprobación automática de ModelState para que FluentValidation tome el control.
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Add services to the container.
builder.Services.AddControllers(options =>
{
    // 🚨 Desactivar la validación automática del modelo de ASP.NET Core MVC.
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddCatalogModule(builder.Configuration);
builder.Services.AddAuthModule(builder.Configuration);
builder.Services.AddConfigurationModule(builder.Configuration);
builder.Services.AddPromotionsModule(builder.Configuration);

#region Versionamiento de la API
//Configuracion de versionamiento de api
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true; //Si el cliente no especifica una versión usamos la versión por defecto (linea de abajo)
    options.DefaultApiVersion = new ApiVersion(1, 0); //Indica la versión predeterminada
    options.ReportApiVersions = true; //Devuelve en las cabeceras las versiones disponibles de la API
    options.ApiVersionReader = ApiVersionReader.Combine( //Aqui definimos como el cliente puede enviar la versión de la Api
        new UrlSegmentApiVersionReader(), //Versión en la URL
        new HeaderApiVersionReader("X-Version"), //Versión en la cabecera personalizada
        new MediaTypeApiVersionReader("ver") //Versión en el Content-Type o Accept como parámetro
    );
}).AddApiExplorer(options => //Habilita el soporte para ApiExplorer (Swagger)
{
    options.GroupNameFormat = "'v'VVV"; //Define el formato del grupo de versión en Swagger
    options.SubstituteApiVersionInUrl = true; //Hace que el placeholder {version} en tus rutas se reemplace automáticamente por la versión correspondiente (En los Controllers)
});
#endregion

#region Configuración de MediatR

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

#endregion

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
