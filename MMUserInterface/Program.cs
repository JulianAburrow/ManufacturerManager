var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.ConfigureSqlConnections(configuration);
builder.Services.AddDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapGet("/documents/{category}/{filename}", (HttpContext context, string category, string filename) =>
{
    var safeCategory = Path.GetFileName(category);
    var safeFilename = Path.GetFileName(Uri.UnescapeDataString(filename));
    var env = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
    var path = Path.Combine(env.ContentRootPath, "Documents", safeCategory, safeFilename);

    if (!System.IO.File.Exists(path))
        return Results.NotFound();

    context.Response.Headers.Append("Content-Disposition", $"inline; filename=\"{safeFilename}\"");
    var contentType = "application/pdf";
    return Results.File(path, contentType);
});

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
