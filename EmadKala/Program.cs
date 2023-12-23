/* Copyright (C) 2023-2024 Emad Abbasnezhad (real_emad_abbasnezhad@proton.me)
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License. */

using EmadKala.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Services

// data base (MySQL, EF core)
var connectionString = builder.Configuration.GetConnectionString("docker-mysql");

builder.Services.AddDbContext<EmadKalaDbContext>(opt => opt.UseMySql(
    connectionString, ServerVersion.AutoDetect(connectionString)));

// identity (IdentityCore)
builder.Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<EmadKalaDbContext>();

#endregion

var app = builder.Build();

#region HTTP request pipeline

// make sure data base is created.
app.Services.CreateScope().ServiceProvider.GetService<EmadKalaDbContext>()?.Database.EnsureCreated();

#endregion

app.Run();