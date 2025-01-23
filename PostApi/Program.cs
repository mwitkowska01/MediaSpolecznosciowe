using Microsoft.EntityFrameworkCore;
using PostApi.Data;
using PostApi.Models;

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

builder.Services.AddDbContext<PostDbContext>(options =>
    options.UseSqlServer("Server=mssql,1433;Database=PostDb;User=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;"));

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PostDbContext>();
    dbContext.Database.EnsureCreated();

    if (!dbContext.Posts.Any())
    {
        dbContext.Posts.AddRange(new List<Post>
        {
            new Post { Title = "Post 1", Content = "Lubiê placki.", CreatedAt = DateTime.Now },
            new Post { Title = "Post 2", Content = "Fajnie.", CreatedAt = DateTime.Now.AddDays(-1) },
            new Post { Title = "Post 3", Content = "Koncertowo fajnie.", CreatedAt = DateTime.Now.AddDays(-2) }
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

app.MapGet("/", () => "PostApi is running...");


app.Run();