using heroesBackend.Data;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.EntityFramework;
using System.Text;
using heroesBackend.models;
using heroesBackend.Repositories;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.

        //DB
        builder.Services.AddDbContext<HeroesContext>(
            options => options.UseMySQL(
                builder.Configuration.GetConnectionString("database"),null));
        //IDENTITY
        builder.Services.AddIdentity<Trainer, IdentityRole>(opt =>
        opt.User.RequireUniqueEmail = true)
                     .AddEntityFrameworkStores<HeroesContext>()
                     .AddDefaultTokenProviders();
        //Authentication
        builder.Services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(option=>
        {
            option.SaveToken = true;
            option.RequireHttpsMetadata = false;
            option.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JWT:ValidAudience"],
                ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
            };
        });
        builder.Services.Configure<IdentityOptions>(opt =>
        {
            opt.Password.RequireUppercase = true;
            opt.Password.RequireDigit = true;
            opt.Password.RequireNonAlphanumeric = true;
            opt.Password.RequiredLength = 8;
        });

        builder.Services.AddControllers().AddNewtonsoftJson(opt =>
        {
            opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });
        builder.Services.AddTransient<IAccountRepository, AccountRepository>();
        builder.Services.AddTransient<IHeroesRepository, HerorsRepository>();


        builder.Services.AddControllers();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(option =>
        {
            option.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}