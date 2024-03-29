using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SignalR_App.Application;
using SignalR_App.Application.Hubs;
using SignalR_App.Persistence;
using SignalR_App.Persistence.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

var issuer = builder.Configuration["Jwt:Issuer"];
var audience = builder.Configuration["Jwt:Audience"];
var securityKey = builder.Configuration["Jwt:SecurityKey"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidAudience = audience,
            ValidIssuer = issuer,
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
            {
                bool expireCheck = expires.HasValue ? expires.Value > DateTime.UtcNow : false;
                //if (expireCheck)
                //{
                //    var userService = (IUserService)builder.Services.FirstOrDefault(c => c.ServiceType == typeof(IUserService));
                //    if (userService != null) userService.Logout().Wait();
                //}
                return expireCheck;
            },
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod()
        .SetIsOriginAllowed(host => true)
        .AllowCredentials();
    });
});

builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSignalR();

builder.Services.AddPersistenceServiceRegistration(builder.Configuration);
builder.Services.AddApplicationServiceRegistration();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "SignalRApp", Version = "v1" });

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(CreatePermissions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<MainHub>("/main-hub");
app.MapHub<BookingHub>("/booking-hub");
app.MapHub<MessageHub>("/message-hub");

app.Run();

void CreatePermissions()
{
    try
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SignalRDbContext>();
        var permissions = typeof(Permissions).GetFields(BindingFlags.Public | BindingFlags.Static).ToList();
        var appPermissions = context.Permissions.ToList();

        int counter = 0;
        foreach (var permission in permissions)
        {
            if (appPermissions.Exists(d => d.Name == permission.Name.Trim())) continue;

            context.Permissions.Add(new()
            {
                Name = permission.GetValue(permission)?.ToString(),
            });
            counter++;
        }
        if (counter > 0) context.SaveChanges();
        
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}