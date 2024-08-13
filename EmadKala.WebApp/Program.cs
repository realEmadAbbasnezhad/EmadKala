using EmadKala.Database;
using EmadKala.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EmadKalaAccountDbContext>(opt =>
    opt.UseMySQL(builder.Configuration.GetConnectionString("Account")!));

builder.Services.AddIdentity<EmadKalaUser, EmadKalaRole>()
    .AddEntityFrameworkStores<EmadKalaAccountDbContext>()
    .AddOtpTokenProvider();

builder.Services.AddTransient<ISmsService, SmsService>();

builder.Services.AddAuthentication().AddCookie(opt =>
{
    opt.LoginPath = "/Account/Portal";
});
builder.Services.AddAuthorization();

#endregion

var app = builder.Build();

#region Pipeline

app.Services.CreateScope().ServiceProvider.GetService<EmadKalaAccountDbContext>()?.Database.EnsureCreated();

app.UseHttpsRedirection();
app.MapDefaultControllerRoute();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

#endregion

app.Run();