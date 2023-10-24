using Innoloft_Application.DBContext;
using Innoloft_Application.Services;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string connectionString = builder.Configuration.GetConnectionString("sqlite");

builder.Services.AddDbContext<EventsDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddScoped<EventsService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.


app.MapControllers();

app.MapGet("/", () => "Hello!");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
