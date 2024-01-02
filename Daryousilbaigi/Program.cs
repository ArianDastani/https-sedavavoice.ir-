using Application.Interface.Context;
using Application.Services.Education;
using Application.Services.Information;
using Application.Services.Message.Command.RemoveMessage;
using Application.Services.Message.Command.SendMessage;
using Application.Services.Message.Query;
using Application.Services.Portfolio;
using Application.Services.Pricing;
using Application.Services.Skills;
using Application.Services.TopServices;
using Application.Services.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
builder.Services.AddScoped<ISendMessageService, SendMessageService>();
builder.Services.AddScoped<IGetMessage, GetMessage>();
builder.Services.AddScoped<IReadMessage, ReadMessage>();
builder.Services.AddScoped<IRemoveMessage, RemoveMessage>();
builder.Services.AddScoped<ISkillServices, SkillServices>();
builder.Services.AddScoped<IEducationService, EducationService>();
builder.Services.AddScoped<IThingIdoServices, ThingIdoServices>();
builder.Services.AddScoped<IPortfolioServices, PortfolioServices>();
builder.Services.AddScoped<IPricingServices, PricingServices>();
builder.Services.AddScoped<IInformationServices, InformationServices>();
builder.Services.AddScoped<IUserServices, UserServices>();




builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(op =>
{
    op.LoginPath = "/Authentication/Login";
    op.ExpireTimeSpan=TimeSpan.FromDays(5);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == StatusCodes.Status404NotFound)
    {
        context.Request.Path = "/";
        await next();
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
