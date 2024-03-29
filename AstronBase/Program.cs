﻿using AstronBase.Domain.Entity;
using AstronBase;
using AutoMapper;
using AstronBase;
using AstronBase.DAL;
using AstronBase.Domain.Entity;
using Microsoft.EntityFrameworkCore;

using AstronBase.DAL;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var mapperConfig = new MapperConfiguration((v) =>
{
    v.AddProfile(new MappingProfile());
});

var mapper = mapperConfig.CreateMapper();

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection))
    .AddIdentity<User, Role>(opts =>
    {
        opts.Password.RequiredLength = 5;
        opts.Password.RequireNonAlphanumeric = false;
        opts.Password.RequireLowercase = false;
        opts.Password.RequireUppercase = false;
        opts.Password.RequireDigit = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Logging
    .ClearProviders()
    .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)
    .AddConsole();


Addiction.AddAddiction(builder, mapper);

builder.Logging
    .ClearProviders()
    .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)
    .AddConsole();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();