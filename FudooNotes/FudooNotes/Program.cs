using BussinessLayer;
using FundooManager.Interface;
using FundooManager.Manager;
using FundooManager.Model;
using FundooRepository.Interface;
using FundooRepository.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using NLog;
using System.Text;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddSession( options => 
    {
        options.IOTimeout = TimeSpan.FromSeconds(50);
    }
        );
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddTransient<IUserManager, UserManager>();
    builder.Services.AddTransient<IUserRepository, UserRepository>();
    builder.Services.AddTransient<INoteManager, NoteManager>();
    builder.Services.AddTransient<INoteRepository, NoteRepository>();
    builder.Services.AddTransient<ILableManager, LableManager>();
    builder.Services.AddTransient<ILableRepository, LableRepository>();
    builder.Services.AddTransient<ICollabraterManager, CollabraterManager>();
    builder.Services.AddTransient<ICollabraterRepository, CollabraterRepository>();




    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Welcome to Fundoo Notes" });
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            Scheme = "bearer",
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Description = "enter JWT bearer token on textbox below !",
            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };
        c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                             {
                          { jwtSecurityScheme,Array.Empty<string>() }
                             });
    });
    var tokenKey = builder.Configuration.GetValue<string>("Jwt:Key");
    var Key = Encoding.ASCII.GetBytes(tokenKey);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Key),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }
    app.UseAuthentication();
    app.UseHttpsRedirection();
    app.UseSession();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();


    app.MapControllers();
    app.MapControllerRoute(
        name: "Admin",
        pattern: "{area : exists}/{controller=Home}/{action=index}/{id?}");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=index}/{id?}");

    app.Run();
}
catch(Exception ex)
{
    logger.Error(ex,"Stopeed Program Becuse od Exception");
}
finally
{
    NLog.LogManager.Shutdown();
}
