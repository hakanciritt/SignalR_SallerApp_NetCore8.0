using Microsoft.OpenApi.Models;
using SignalR_App.Application;
using SignalR_App.Application.Hubs;
using SignalR_App.Persistence;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<MainHub>("/main-hub");
app.MapHub<BookingHub>("/booking-hub");
app.MapHub<MessageHub>("/message-hub");

app.Run();
