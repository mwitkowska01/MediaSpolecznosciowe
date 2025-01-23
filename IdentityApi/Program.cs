using IdentityApi.Data;
using IdentityApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace IdentityApi
{
    class Program
    {
        static void Main(string[] args)
        {
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

            builder.Services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer("Server=mssql,1433;Database=IdentityDb;User=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;"));
            
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Identity API",
                    Version = "v1",
                    Description = "API do zarz¹dzania u¿ytkownikami w IdentityApi",
                    Contact = new OpenApiContact
                    {
                        Name = "Twoje Imiê",
                        Email = "kontakt@example.com"
                    }
                });
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
                dbContext.Database.EnsureCreated();

                if (!dbContext.Users.Any())
                {
                    dbContext.Users.AddRange(new List<User>
                    {

                        new User { UserName = "nowaka", Password = "123" },
                        new User { UserName = "kowalski", Password = "kowalski" }
                    });
                    dbContext.SaveChanges();
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API v1");
                    c.RoutePrefix = ""; // Swagger otworzy siê na http://localhost:5002
                });
            }

            app.UseCors();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.MapGet("/", () => "IdentityApi is running...");


            app.Run();
        }
    }
}
