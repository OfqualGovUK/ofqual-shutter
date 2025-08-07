using System.Reflection;
using GovUk.Frontend.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Ofqual.Shutter.Web.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Http;

var builder = WebApplication.CreateBuilder(args);

#region Services

// Add GovUK frontend
builder.Services.AddGovUkFrontend();

// Add Controllers with Views
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

// Configure Serilog logging
builder.Host.UseSerilog((ctx, svc, cfg) => cfg
    .ReadFrom.Configuration(ctx.Configuration)
    .ReadFrom.Services(svc)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Environment", ctx.Configuration.GetValue<string>("LogzIo:Environment") ?? "Unknown")
    .Enrich.WithProperty("Assembly", Assembly.GetEntryAssembly()?.GetName()?.Name ?? "Ofqual.Recognition.Frontend")
    .WriteTo.Console(
        restrictedToMinimumLevel: ctx.Configuration.GetValue<string>("LogzIo:Environment") == "LOCAL"
            ? LogEventLevel.Verbose
            : LogEventLevel.Error)
    .WriteTo.LogzIoDurableHttp(
        requestUri: ctx.Configuration.GetValue<string>("LogzIo:Uri") ?? string.Empty,
        bufferBaseFileName: "Buffer",
        bufferRollingInterval: BufferRollingInterval.Hour,
        bufferFileSizeLimitBytes: 524288000L,
        retainedBufferFileCountLimit: 12
    )
);

// Register essential services
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<ShutterPageConfigurationModel>(builder.Configuration.GetSection("ShutterPage"));

#endregion

var app = builder.Build();

#region Middleware

// Configure middleware and request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStatusCodePagesWithReExecute("/Error/{0}");
app.UseStaticFiles();
app.UseRouting();
app.UseGovUkFrontend();

// Configure route mapping
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"
);

#endregion

app.Run();