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

app.MapGet("/docs/{category}/{filename}", async (HttpContext context, string category, string filename) =>
{
    var safeCategory = Path.GetFileName(category);
    var safeFilename = Path.GetFileName(filename);
    var env = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
    var path = Path.Combine(env.ContentRootPath, "Documents", safeCategory, safeFilename);

    if (!System.IO.File.Exists(path))
        return Results.NotFound();

    var contentType = "application/pdf"; // Or use FileExtensionContentTypeProvider
    return Results.File(path, contentType, safeFilename);
});

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
