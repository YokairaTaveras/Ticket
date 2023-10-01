using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Ticket.Data;
using Microsoft.EntityFrameworkCore;
using Ticket.DAL;
using Ticket.BLL;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

var ConStr = builder.Configuration.GetConnectionString("ConStr");

builder.Services.AddDbContext<Context>(Options => Options.UseSqlite(ConStr));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddScoped<PrioridadesBLL>();
builder.Services.AddScoped<ClientesBLL>();
builder.Services.AddScoped<TicketsBLL>();
builder.Services.AddScoped<NotificationService>();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();