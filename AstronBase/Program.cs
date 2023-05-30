using AstronBase;
using Microsoft.EntityFrameworkCore;
using AstronBase.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using AstronBase.DAL;
using AstronBase.Domain.Entity;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AstronBaseContext") ??
                         throw new InvalidOperationException("Connection string 'AstronBaseContext' not found.")));

var mapperConfig = new MapperConfiguration((v) => { v.AddProfile(new MappingProfile()); });

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

Addiction.AddAddiction(builder);


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseAuthentication();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();