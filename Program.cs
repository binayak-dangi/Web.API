using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using Web.API.Data;
using Web.API.Services.Implementation;
using Web.API.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
builder.Services.AddScoped<IHRRoleService, HRRoleService>();
builder.Services.AddScoped<IHRBranchService, HRBranchService>();
builder.Services.AddScoped<IHRCompanyService, HRCompanyService>();
builder.Services.AddScoped<IHREmployeeService, HREmployeeService>();
builder.Services.AddScoped<IHRCorporateTitleService, HRCorporateTitleService>();
builder.Services.AddScoped<IHRFunctionalTitleService, HRFunctionalTitleService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<IHRPermissionService, HRPermissionService>();
//Add Swagger
builder.Services.AddEndpointsApiExplorer();
//Add Authorize in swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Web.API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
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
            Array.Empty<string>()
        }
    });
});
// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
         Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
     ),


        // Removes the default 5-minute grace period.
        // The JWT becomes invalid immediately when its expiration time (exp) is reached.
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = async context =>
        {
            context.HandleResponse();

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            await context.Response.WriteAsJsonAsync(new
            {
                success = false,
                message = "Unauthorized access."
            });
        }
    };
});


builder.Services.AddAuthorization();
//cros origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ReactPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();