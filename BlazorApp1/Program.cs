using BlazorApp1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// Add Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Localization");
builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        // Supported Cultures
        var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("de"),
            new CultureInfo("en"),
            new CultureInfo("fr"),
            new CultureInfo("tr")
        };
        // Default Request Culture
        options.DefaultRequestCulture = new RequestCulture(
            culture: "en",
            uiCulture: "en"
        );
        // Formatting numbers, dates, etc.
        options.SupportedCultures = supportedCultures;
        // UI strings that we have localized.
        options.SupportedUICultures = supportedCultures;
        // Language Header Request Culture
        options.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
    }
);

await builder.Build().RunAsync();
