using Microsoft.EntityFrameworkCore;
using PeopleApi.Data;
using PeopleApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<PeopleDbContext>(options =>
    options.UseSqlServer("Server=mssql,1433;Database=PeopleDb;User=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PeopleDbContext>();
    dbContext.Database.EnsureCreated();

    if (!dbContext.People.Any())
    {
        dbContext.People.AddRange(new List<People>
        {
            new People { Name = "Adi", Email = "Post" },
            new People { Name = "Mati", Email = "Sept jest fainy" },
            new People { Name = "Merry", Email = "Nie lubiê tego."}
        });
        dbContext.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapGet("/", () => "PeopleApi is running...");

app.Run();
