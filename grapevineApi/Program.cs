

using grapevineData;
using grapevineData.Interfaces;
using grapevineRepository;
using grapevineRepository.Interfaces;
using grapevineService.Interfaces;
using grapevineServices.Interfaces;
using grapevineServices.Model;
using grapevineServices.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Authentication & JWT Configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("Jwt");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

// 2. Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Grapevine API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// 3. Dependency Injection (DI) Registration
builder.Services.AddScoped<IDapperExecutor, DapperSqlExecutor>();
builder.Services.AddScoped<ISqlExecutor, SqlExecutor>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Workplace Module
builder.Services.AddScoped<IWorkplaceRepository, WorkplaceRepository>();
builder.Services.AddScoped<IWorkplaceService, WorkplaceService>();

// Existing Feeds Module
builder.Services.AddScoped<IFeedRepository, FeedRepository>();
builder.Services.AddScoped<IFeedsService, FeedsService>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<UtilityService, UtilityService>();


builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddScoped<IStringEncryptor, StringEncryptor>();


builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();