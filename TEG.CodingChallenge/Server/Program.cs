using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Serilog;
using Serilog.Events;
using System.Net.Http.Headers;
using System.Reflection;
using TEG.CodingChallenge.Application.Configs;
using TEG.CodingChallenge.Application.Contracts.Services;
using TEG.CodingChallenge.Application.Services;
using TEG.CodingChallenge.Domain.Repositories;
using TEG.CodingChallenge.Infrastructure;
using TEG.CodingChallenge.Infrastructure.Repositories;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();


var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog();


//Add Application Services
builder.Services.AddScoped<IVenueService, VenueService>();
builder.Services.AddScoped<IEventService, EventService>();

//Add Repository
builder.Services.AddScoped<IVenueRepository, VenueRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();

//configurations
builder.Services.Configure<EventDataSettings>(option => builder.Configuration.Bind("EventDataSettings", option));


builder.Services.AddSingleton<InMemoryDatabase>();

builder.Services.AddHttpClient("Default")
    .AddPolicyHandler(GetRetryPolicy());

builder.Services
    .AddSingleton<HttpClient>(svc => 
        svc.GetRequiredService<IHttpClientFactory>().CreateClient("Default"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TEG Events Api v1");
    });

}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();


IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(retry * 15));
}