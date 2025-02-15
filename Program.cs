using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration.UserSecrets;
using MultilingualAPIDemo.Locale;
using MultilingualAPIDemo.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddScoped<IResourceLocalizer, ResourceLocalizer>();

// Define supported cultures
var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("de")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en");

    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    options.RequestCultureProviders =
    [
        new AcceptLanguageHeaderRequestCultureProvider()
    ];
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/test", () =>
{
    return "Test";
})
.WithName("Test");

app.MapGet("/articles/{id}", (int id, IResourceLocalizer resourceLocalizer) => {
    var article = DataHelper.GetArticles().FirstOrDefault(a => a.Id == id);
    if (article == null)
    {
        return Results.NotFound();
    }

    var articleDto = new ArticleDto
    {
        Name = resourceLocalizer.Localize(article.NameKey!),
        Description = resourceLocalizer.Localize(article.DescriptionKey!)
    };

    return Results.Ok(articleDto);
});

app.Run();