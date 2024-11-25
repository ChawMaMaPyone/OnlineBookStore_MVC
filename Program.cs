using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using OnlineBookStore.Models.Domain;
using OnlineBookStore.Repositories.Abstract;
using OnlineBookStore.Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContent>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

// Add default identity services
builder.Services.AddDefaultIdentity<DefaultUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DatabaseContent>();

// Add scoped services for your repositories
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<lAuthorService, AuthorServicce>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Configure endpoints inside the UseEndpoints method
app.UseEndpoints(endpoints =>
{
    // MVC controller route mapping
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // Razor Pages route mapping
    endpoints.MapRazorPages();
});

app.Run();
