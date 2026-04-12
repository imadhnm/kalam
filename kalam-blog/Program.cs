using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<KalamDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config =>
{
    config.Password.RequireDigit = true;
    config.Password.RequiredLength = 8;
    config.Password.RequireUppercase = true;
    config.Password.RequireNonAlphanumeric = true;

    config.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    // config.SignIn.RequireConfirmedEmail = true;
})
.AddEntityFrameworkStores<KalamDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(option =>
{
    option.Cookie.HttpOnly = true;
    option.ExpireTimeSpan = TimeSpan.FromDays(30);
    option.SlidingExpiration = true;
    option.LoginPath = "/user/login";
});

// Configure Options
builder.Services.Configure<PwdRecipe>(builder.Configuration.GetSection("PwdRecipe"));

//configure service container with di
builder.Services.UserServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    //apply migrations on runtime
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<KalamDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

//custom route mapping to url:port/actionName
app.MapControllerRoute(
    name: "AuthActions",
    pattern: "{action=Index}/{id?}",
    new { controller = "Auth" })
    .WithStaticAssets();

app.MapControllerRoute(
    name: "HomeActions",
    pattern: "home",
    new { controller = "Home" })
    .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
