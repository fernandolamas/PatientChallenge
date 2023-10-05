using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.OpenApi.Models;
using PatientChallenge.Extensions;
using PatientChallenge.Service.AccountService;
using PatientChallenge.Service.EncryptService;
using PatientChallenge.Service.PatientService;
using PatientChallenge.Service.Initializer;

namespace PatientChallenge
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            await DatabaseInitializer.EnsureDatabaseInitializationAsync(builder.Configuration["ConnectionStrings:PatientDB"]);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IEncryptService, EncryptService>();  
            builder.Services.AddJwtTokenServices(builder.Configuration);


            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User 1"));
            });

            builder.Services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization Header using Bearer Scheme"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                            },
                        new string[]{}
                    }
                });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.UseCors("CorsPolicy");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patient API with JWT Auth"));
            }

            
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}