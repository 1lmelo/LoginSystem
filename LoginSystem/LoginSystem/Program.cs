using LoginSystem.Models;
using LoginSystem.Models.DAL;
using LoginSystem.Models.Interfaces;
using LoginSystem.Utils;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using static Dapper.SqlMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddScoped<IUserDAL, UserDAL>();
builder.Services.AddTransient<IUtil, Util>();
builder.Services.AddTransient<IEmailDAL, EmailDAL>();

builder.Services.AddOptions();
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddMvc();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
