using Aes.Encryptor.Interfaces;
using Aes.Encryptor.Services;
using Blazored.LocalStorage;
using MudBlazor.Services;
using HelpDesk.Components;
using HelpDesk.Mfc.Authorization;
using HelpDesk.Services.Devices;
using HelpDesk.Services.Documents;
using HelpDesk.Services.Tickets;
using HelpDesk.Services.Token;
using HelpDesk.Storage;
using HelpDesk.Utils;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();
builder.Services.AddScoped<HelpDeskContext>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<StorageSession>();
builder.Services.AddScoped<TicketService>();
builder.Services.AddScoped<DeviceService>();
builder.Services.AddScoped<DeviceInUseService>();
builder.Services.AddScoped<DocumentService>();
builder.Services.AddSingleton<MfcServiceLogon>();
builder.Services.AddSingleton<IAesService, AesService>();
builder.Services.AddSingleton<IMapper, Mapper>();
builder.Services.AddSingleton<TokenEncryptionService>(s =>
    new TokenEncryptionService("12345678901234567890123456789012", s.GetRequiredService<IAesService>()));
builder.Services.AddBlazoredLocalStorage();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HelpDeskContext>();
    dbContext.Database.Migrate();
}

app.Run();