using electronic_library_6.Data;
using electronic_library_6.Domain.Entities;
using electronic_library_6.Domain.Services;
using electronic_library_6.Infrastructure;
using electronic_library_6.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("local"));
using (var context = new ELibraryContext(optionsBuilder.Options))
{
    
}

builder.Services.AddControllersWithViews();
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.ExpireTimeSpan = TimeSpan.FromHours(1);
        opt.Cookie.Name = "library_session";
        opt.Cookie.HttpOnly = true;
        opt.Cookie.SameSite = SameSiteMode.Strict;
        opt.LoginPath = "/User/Login";
        opt.AccessDeniedPath = "/User/AccessDenied";
    });
builder.Services.AddDbContext<ELibraryContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("local")));
builder.Services.AddScoped<IRepository<User>, EFRepository<User>>();
builder.Services.AddScoped<IRepository<Role>, EFRepository<Role>>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBooksReader, BooksReader>();
builder.Services.AddScoped<IRepository<Book>, EFRepository<Book>>();
builder.Services.AddScoped<IRepository<Categories>, EFRepository<Categories>>();
builder.Services.AddScoped<IRepository<PollingStations>, EFRepository<PollingStations>>();

//...
builder.Services.AddScoped<IBooksReader, BooksReader>();
builder.Services.AddScoped<IBooksService, BooksService>();

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseBooksProtection();
app.UseStaticFiles();

app.MapControllerRoute("default", "{Controller=Books}/{Action=Index}");
app.Run();
