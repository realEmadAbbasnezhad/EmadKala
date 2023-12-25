// Copyright (C) 2023-2024 Emad Abbasnezhad (real_emad_abbasnezhad@proton.me)
// This file is a part of EmadKala, Licenced under http://www.gnu.org/licenses/

using System.Reflection;
using EmadKalaWeb.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Services

// core (ASP net core MVC)
builder.Services.AddControllersWithViews();

// api (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
    opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml")));

// data base (MySQL, EF core)
var connectionString = builder.Configuration.GetConnectionString("docker-mysql");

builder.Services.AddDbContext<EmadKalaDbContext>(opt => opt.UseMySql(
    connectionString, ServerVersion.AutoDetect(connectionString)));

// identity (IdentityCore)
builder.Services.AddIdentityCore<EmadKalaUser>().AddEntityFrameworkStores<EmadKalaDbContext>();

#endregion

var app = builder.Build();

#region HTTP request pipeline

if (app.Environment.IsDevelopment())
{
    // add swagger
    app.UseSwagger();
    app.UseSwaggerUI();

    // make sure data base is created.
    app.Services.CreateScope().ServiceProvider.GetService<EmadKalaDbContext>()?.Database.EnsureCreated();
}

#endregion

app.Run();