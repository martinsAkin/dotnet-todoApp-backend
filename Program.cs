using Microsoft.EntityFrameworkCore;
using todoApp.Data;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

//load .env file
Env.Load();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
builder.Configuration["ConnectionStrings:DefaultConnection"] = connectionString;

// Add DB Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString)
);

// ✅ Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:5173") // Frontend origin
              .AllowAnyHeader()
              .AllowAnyMethod()
    );
});

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// ✅ Use CORS middleware before routing
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
