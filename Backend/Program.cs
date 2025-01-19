using Backend.Data;
using Backend.Middleware;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Dodanie konfiguracji JWT w pliku appsettings.json
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

// Rejestracja kontrolerów API
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64;
    });

// Rejestracja bazy danych
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Rejestracja Identity i UserManager/RoleManager
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Rejestracja CORS - umożliwiamy dostęp z front-endu React
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:5173")  // Adres frontendowej aplikacji React
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Rejestracja logowania przy użyciu JWT
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });

// Rejestracja Swaggera
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

// Rejestracja logowania do konsoli
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// Tworzymy aplikację
var app = builder.Build();

// Konfiguracja Swaggera w trybie deweloperskim
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}

// Konfiguracja middleware
app.UseMiddleware<RequestLoggingMiddleware>();  // Możesz dodać własny middleware logujący żądania

app.UseCors("AllowReactApp");  // Włączenie CORS

app.UseAuthentication();  // Użycie autentykacji
app.UseAuthorization();   // Użycie autoryzacji

app.MapControllers();  // Mapowanie kontrolerów

// Tworzenie roli i użytkownika administracyjnego, jeśli nie istnieje
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var context = services.GetRequiredService<ApplicationDbContext>();

    var roleExist = await roleManager.RoleExistsAsync("Admin");
    if (!roleExist)
    {
        var role = new IdentityRole("Admin");
        await roleManager.CreateAsync(role);
    }

    var adminUser = await userManager.FindByEmailAsync("admin3@admin.com");
    if (adminUser == null)
    {
        var admin = new IdentityUser
        {
            UserName = "admin3@admin.com",
            Email = "admin3@admin.com"
        };
        var createAdminResult = await userManager.CreateAsync(admin, "Admin@2025");

        if (createAdminResult.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }
        else
        {
            foreach (var error in createAdminResult.Errors)
            {
                Console.WriteLine($"Błąd tworzenia użytkownika admin: {error.Description}");
            }
        }
    }
}

app.Run();
