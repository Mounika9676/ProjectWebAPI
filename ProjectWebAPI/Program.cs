using Microsoft.EntityFrameworkCore;
using ProjectWebAPI.Entity;
using ProjectWebAPI.Repo;
using AutoMapper;
using ProjectWebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
namespace ProjectWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

                    builder.Services.AddControllers();
                    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                    builder.Services.AddEndpointsApiExplorer();
                    builder.Services.AddSwaggerGen();
                    IConfiguration config = builder.Configuration;
                    //builder.Services.AddDbContext<oExamDbContext>text(opt => opt.UseSqlServer(config["ConnString"].ToString()));
                    builder.Services.AddTransient<oExamDbContext>();
                    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                    builder.Services.AddTransient<UnitOfWork>();
                    builder.Services.AddTransient<IUserService, UserService>();
                    builder.Services.AddLog4net();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
                    //builder.Services.AddSwaggerGen();
                    //Configure Authentication Schema to validate Token
                    builder.Services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    }).AddJwtBearer(o =>
                    {
                        o.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = builder.Configuration["Jwt:Issuer"],
                            ValidAudience = builder.Configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = false,
                            ValidateIssuerSigningKey = true,

                        };
                    });
                    builder.Services.AddSwaggerGen(c => {
                        c.SwaggerDoc("v1", new OpenApiInfo
                        {
                            Title = "JWTToken_Auth_API",
                            Version = "v1"
                        });
                        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                        {
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer",
                            BearerFormat = "JWT",
                            In = ParameterLocation.Header,
                            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                        });
                        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
                    });

                    var app = builder.Build();

                    // Configure the HTTP request pipeline.
                    if (app.Environment.IsDevelopment())
                    {
                        app.UseSwagger();
                        app.UseSwaggerUI();
                    }

                    app.UseAuthentication(); //add Authentication Middleware to the Application
                    app.UseAuthorization();


                    app.MapControllers();

                    app.Run();
                }
            }
        }