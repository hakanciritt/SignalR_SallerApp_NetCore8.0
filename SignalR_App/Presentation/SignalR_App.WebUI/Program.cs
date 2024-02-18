using Microsoft.AspNetCore.Authentication.JwtBearer;
using SignalR_App.WebUI;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TokenDelegateHandler>();
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Admin/User/Index";
        opt.LogoutPath= "/Admin/User/Logout";
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

#region Admin Services

builder.Services.AddHttpClient("Categories", options => options.BaseAddress = new Uri(builder.Configuration["ApiUrl"])).AddHttpMessageHandler<TokenDelegateHandler>(); ;
builder.Services.AddHttpClient("Products", options => options.BaseAddress = new Uri(builder.Configuration["ApiUrl"])).AddHttpMessageHandler<TokenDelegateHandler>();
builder.Services.AddHttpClient("TextContent", options => options.BaseAddress = new Uri(builder.Configuration["ApiUrl"]));
builder.Services.AddHttpClient("Sliders", options => options.BaseAddress = new Uri(builder.Configuration["ApiUrl"]));
builder.Services.AddHttpClient("Bookings", options => options.BaseAddress = new Uri(builder.Configuration["ApiUrl"]));
builder.Services.AddHttpClient("Chats", options => options.BaseAddress = new Uri(builder.Configuration["ApiUrl"]));
builder.Services.AddHttpClient("Auth", options => options.BaseAddress = new Uri(builder.Configuration["ApiUrl"]));

#endregion

#region Client Services

builder.Services.AddHttpClient("CategoriesWeb", options => options.BaseAddress = new Uri(builder.Configuration["ApiUrl"]));
builder.Services.AddHttpClient("ProductsWeb", options => options.BaseAddress = new Uri(builder.Configuration["ApiUrl"]));

#endregion

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
   );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
