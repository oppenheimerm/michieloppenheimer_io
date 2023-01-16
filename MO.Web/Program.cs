using Microsoft.EntityFrameworkCore;
using MO.DataStore.EFCore;
using MO.DataStore.EFCore.Repositories;
using MO.UseCases.DataStoreInterfaces;
using MO.UseCases.Interfaces;
using MO.UseCases.PostUseCase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages().
    AddRazorPagesOptions(options => {
        /* Friendly Routes - browse blog/some-slug-here */
        options.Conventions.AddPageRoute("/blog/slug", "blog/{slug}");

    });

//  Must go before  services.AddMvc() or services.AddControllersWithViews()!!
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<MODbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

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



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
