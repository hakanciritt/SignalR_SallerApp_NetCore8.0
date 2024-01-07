using SignalR_App.Application.Redis;
using StackExchange.Redis;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


//þimdilik application servisler de eklendi sebebi redise direkt eriþebilmek burasý güncellenecek.

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton(provider =>
{
    var connection = ConnectionMultiplexer.Connect("localhost:6379,abortConnect=false ");
    return new RedisConfiguration(connection);
});

// Add services to the container.
builder.Services.AddControllersWithViews()
      .AddJsonOptions(options =>
      {
          options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
      });

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddHttpClient("Categories", options => options.BaseAddress = new Uri(builder.Configuration["ApiUrl"]));
builder.Services.AddHttpClient("Products", options => options.BaseAddress = new Uri(builder.Configuration["ApiUrl"]));
builder.Services.AddHttpClient("TextContent", options => options.BaseAddress = new Uri(builder.Configuration["ApiUrl"]));
builder.Services.AddHttpClient("Sliders", options => options.BaseAddress = new Uri(builder.Configuration["ApiUrl"]));
builder.Services.AddHttpClient("Bookings", options => options.BaseAddress = new Uri(builder.Configuration["ApiUrl"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
   );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
