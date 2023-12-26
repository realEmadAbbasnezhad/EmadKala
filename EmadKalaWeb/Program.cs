// This file is a part of EmadKala, Licenced under https://www.gnu.org/licenses/gpl-3.0.html
// Copyright (C) 2023-2024 Emad Abbasnezhad (real_emad_abbasnezhad@proton.me)

using System.Reflection;
using EmadKalaWeb.Models;
using EmadKalaWeb.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Services

//==== Core (ASP net core MVC) ====
builder.Services.AddControllersWithViews();


// [Quick Start] Remove this block if you want less bots in your store. Also remove it from pipeline.
//==== API (Swagger) ====
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
    opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml")));


//==== Data base (MySQL, EF core) ====
// [Quick Start] Name of connection string must match with the one on config file.
var connectionString = builder.Configuration.GetConnectionString("docker-mysql");
builder.Services.AddDbContext<EmadKalaDbContext>(opt => opt.UseMySql(
    connectionString, ServerVersion.AutoDetect(connectionString)));


//==== SMS service (Custom) ====
// [Quick Start] Make sure you checked the "EmadKalaSmsService" class.
builder.Services.AddTransient<IEmadKalaSmsService, EmadKalaSmsService>();


//==== Authorization (Identity) ====
builder.Services.AddAuthorization();
builder.Services.ConfigureApplicationCookie(opt =>
{
    // [Quick Start] better not touch this block.
    //** Cookie **
    opt.Cookie.Name = opt.ClaimsIssuer = "EmadKala";
    opt.Cookie.HttpOnly = true;
    opt.Cookie.Domain = null;
    opt.Cookie.Path = null;
    opt.Cookie.SameSite = SameSiteMode.Unspecified;
    opt.Cookie.SecurePolicy = CookieSecurePolicy.None;
    opt.Cookie.IsEssential = false;
    opt.Cookie.Expiration = opt.ExpireTimeSpan = TimeSpan.FromHours(1);
    opt.SlidingExpiration = true;

    // [Quick Start] im very recommend to use this because it will save private data encrypted in DB. 
    // opt.DataProtectionProvider

    // [Quick Start] changing parameters below will need change in code too. 
    //** URLs ***
    opt.AccessDeniedPath = "/Account/AccessDenied";
    opt.LoginPath = "/Account/Signin";
    opt.LogoutPath = "/Account/Signout";
    opt.ReturnUrlParameter = "returnUrl";
});


//==== Authentication (Identity) ====
builder.Services.AddAuthentication();
builder.Services.AddIdentity<EmadKalaUser, EmadKalaRole>()
    .AddErrorDescriber<EmadKalaIdentityErrorDescriber>()
    .AddEntityFrameworkStores<EmadKalaDbContext>()
    .AddDefaultTokenProviders();
// [Quick Start] Check the settings below.
builder.Services.Configure<IdentityOptions>(opt =>
{
    //** Lockout **
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    opt.Lockout.AllowedForNewUsers = true;
    opt.Lockout.MaxFailedAccessAttempts = 3;

    //** Password **
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 0;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredUniqueChars = 0;

    //** User **
    // [Quick Start] user phone number is using as there username. better not change this.
    opt.User.AllowedUserNameCharacters = "0123456789";
    opt.User.RequireUniqueEmail = false;

    //** Date base **
    opt.Stores.SchemaVersion = new Version();
    // [Quick Start] im very recommend to use this because it will save private data encrypted in DB.
    opt.Stores.ProtectPersonalData = false;
    opt.Stores.MaxLengthForKeys = 128;

    //** Signin **
    // this means user did buy something and sent us there
    // address and there real name is now available at bank logs.
    opt.SignIn.RequireConfirmedAccount = false;
    // this means user has get our email and sent its code to us.
    opt.SignIn.RequireConfirmedEmail = false;
    // this means user has get our sms and sent its code to us.
    opt.SignIn.RequireConfirmedPhoneNumber = false;
});

#endregion

var app = builder.Build();

#region Request Pipeline

//==== API (Swagger) ====
app.UseSwagger();
if (app.Environment.IsDevelopment()) app.UseSwaggerUI();


//==== Data base (EF core) ====
if (app.Environment.IsDevelopment())
    app.Services.CreateScope().ServiceProvider.GetService<EmadKalaDbContext>()?.Database.EnsureCreated();


//==== Authorization (Identity) ====
app.UseAuthorization();


//==== Authentication (Identity) ====
app.UseAuthentication();


//==== Core (ASP net core MVC) ====
app.MapControllers();

#endregion

app.Run();