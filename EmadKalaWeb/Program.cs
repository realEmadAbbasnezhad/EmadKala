// Copyright (C) 2023-2024 Emad Abbasnezhad (real_emad_abbasnezhad@proton.me)
// This file is a part of EmadKala, Licenced under http://www.gnu.org/licenses/

using System.Reflection;
using EmadKalaWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Services

//core (ASP net core MVC)
builder.Services.AddControllersWithViews();

//api (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
    opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml")));

//data base (MySQL, EF core)
// TODO: Name of connection string must match with the one on config file.
var connectionString = builder.Configuration.GetConnectionString("docker-mysql");

builder.Services.AddDbContext<EmadKalaDbContext>(opt => opt.UseMySql(
    connectionString, ServerVersion.AutoDetect(connectionString)));

//identity (IdentityCore)
builder.Services.AddIdentityCore<EmadKalaUser>().AddEntityFrameworkStores<EmadKalaDbContext>();
// TODO: Check the settings below.
builder.Services.Configure<IdentityOptions>(opt =>
{
    //** lockout **
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
    opt.Lockout.AllowedForNewUsers = true;
    opt.Lockout.MaxFailedAccessAttempts = 3;

    //** password **
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 0;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredUniqueChars = 0;

    //** user **
    // user phone number is using as there username. better not change this.
    opt.User.AllowedUserNameCharacters = "0123456789";
    opt.User.RequireUniqueEmail = false;

    //** DB **
    opt.Stores.SchemaVersion = new Version();
    opt.Stores.ProtectPersonalData = true;
    opt.Stores.MaxLengthForKeys = 128;

    //** signin **
    opt.SignIn.RequireConfirmedAccount = false;
    opt.SignIn.RequireConfirmedEmail = false;
    opt.SignIn.RequireConfirmedPhoneNumber = false;
});
builder.Services.ConfigureApplicationCookie(options =>
{
    //** cookie **
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.SlidingExpiration = true;
    
    //** urls ***
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.LogoutPath = "/Account/Logout";
    options.ReturnUrlParameter = "ReturnUrl";
});

#endregion

var app = builder.Build();

#region HTTP request pipeline

// core
app.UseRouting();

// add swagger
app.UseSwagger();

// dev
if (app.Environment.IsDevelopment())
{
    // add swagger UI
    app.UseSwaggerUI();

    // make sure data base is created.
    app.Services.CreateScope().ServiceProvider.GetService<EmadKalaDbContext>()?.Database.EnsureCreated();
}

// identity
app.UseAuthentication();
app.UseAuthorization();

#endregion

app.Run();