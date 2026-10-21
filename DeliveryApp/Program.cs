using Application.Features.Auth.Command.Behaviors;
using DeliveryApp.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TokenSettings = DeliveryApp.Domain.Settings.TokenSettings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// تجميع كل ما يخص البنية التحتية وقاعدة البيانات    
builder.Services.AddInfrastructure(builder.Configuration);

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeliveryApp.Application.Features.Wallets.Queries.GetBalance.GetBalanceQuery).Assembly));

// Validators
builder.Services.AddValidatorsFromAssembly(typeof(DeliveryApp.Application.Features.Wallets.Queries.GetBalance.GetBalanceQuery).Assembly);

// Behavior
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Options Pattern Configuration & Binding
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection(TokenSettings.SectionName));

//  فحص آمن للتأكد من وجود القسم في ملف الإعداداته
var jwtSection = builder.Configuration.GetSection(TokenSettings.SectionName);
if (!jwtSection.Exists())
{
    throw new InvalidOperationException($"The configuration section '{TokenSettings.SectionName}' is missing in appsettings.json. Please ensure it exists in the API project.");
}

var tokenSettings = jwtSection.Get<TokenSettings>()!;

// 2. Authentication & JWT مع Options Pattern
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
        ValidIssuer = tokenSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = tokenSettings.Audience,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret))
    };
});

builder.Services.AddAuthorization();

// إضافة الـ Swagger و الـ OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeliveryApp API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

// تفعيل المصادقة والصلاحيات
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();