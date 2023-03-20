using HisabKitabDAL;
using HisabKitabDAL.Models;
using HisabKitabMVC.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HisabKitabDbContext>(option => option.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HisabKitabDB;Trusted_Connection=True;"));
builder.Services.AddSession();
builder.Services.AddAutoMapper(x => x.AddProfile(new HKMapper()));
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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
