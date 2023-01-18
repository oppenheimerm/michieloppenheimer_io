using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MO.DataStore.EFCore;
using MO.DataStore.EFCore.Repositories;
using MO.UseCases.DataStoreInterfaces;
using MO.UseCases.Interfaces;
using MO.UseCases.PostUseCase;
using MO.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages().
    AddRazorPagesOptions(options => {
        /* Friendly Routes - browse blog/some-slug-here */
        options.Conventions.AddPageRoute("/blog/slug", "blog/{slug}");

    });

//  Add the Authentication Middleware services with the AddAuthentication and AddCookie methods.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
		options.SlidingExpiration = true;
		options.AccessDeniedPath = "/Forbidden/";
		options.LoginPath = "/Account/Login";
        options.LogoutPath= "/Account/Logout";
	});

//  Admin folder to prevent unauthorised users being able to access anything in it.
builder.Services.AddMvc().AddRazorPagesOptions(options => {
	options.Conventions.AuthorizeFolder("/admin");
});

//  Must go before  services.AddMvc() or services.AddControllersWithViews()!!
builder.Services.AddRouting(options => options.LowercaseUrls = true);


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<MODbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddSingleton<IBlogUserServices, BlogUserServices>();

//  Repositories
builder.Services.AddScoped<IPostsRepository, PostsRepository>();

//  Usercases
builder.Services.AddTransient<IViewBlogEntiresByFilterUseCase, ViewBlogEntiresByFilterUseCase>();
builder.Services.AddTransient<IViewBlogEntryBySlug, ViewBlogEntryBySlug>();
builder.Services.AddTransient<IViewBlogEntriesByTag, ViewBlogEntriesByTag>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<MO.DataStore.EFCore.MODbContext>();
    //   takes no action if a database for the context exists. If no
    //   database exists, it creates the database and schema. 
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

//  Call UseAuthentication
//  UseAuthorization to set the HttpContext.User property and run the Authorization
//  Middleware for requests.  UseAuthentication and UseAuthorization must be called
//  before Map methods such as MapRazorPages and MapDefaultControllerRoute
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapDefaultControllerRoute();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();



app.Run();
