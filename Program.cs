using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using soft;
using soft.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Context>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("con")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<Context>();
builder.Services.AddScoped<IRepo,Repo>();
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/Home/Login";
    opt.AccessDeniedPath= "/Home/AccessDenied";
    
});

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
//app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
