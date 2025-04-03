using Microsoft.EntityFrameworkCore;
using WaterProject.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WaterDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("WaterConnection")));

// ? Add named CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowCredentials()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddCors(options => 
    options.AddPolicy("AllowReactAppBlah",
    policy => {
        policy.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    }));

var app = builder.Build();

// ? Use CORS policy by name
app.UseCors("AllowReactAppBlah");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
