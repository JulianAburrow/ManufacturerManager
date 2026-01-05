namespace MMDataAccess.Data;

public class ManufacturerManagerContext(DbContextOptions<ManufacturerManagerContext> options) : DbContext(options)
{
    public DbSet<CategoryModel> Categories { get; set; }
    public DbSet<ColourJustificationModel> ColourJustifications { get; set; }
    public DbSet<ColourModel> Colours { get; set; }
    public DbSet<ErrorModel> Errors { get; set; }
    public DbSet<ManufacturerModel> Manufacturers { get; set; }
    public DbSet<ManufacturerStatusModel> ManufacturerStatuses { get; set; }
    public DbSet<MyMMModel> MyMMs { get; set; }
    public DbSet<MyMMStatusModel> MyMMStatuses { get; set; }
    public DbSet<WidgetModel> Widgets { get; set; }
    public DbSet<WidgetStatusModel> WidgetStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties()
            .Where(p => p.ClrType == typeof(string))))
        {
            property.SetIsUnicode(false);
        }

        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new ColourConfiguration());
        builder.ApplyConfiguration(new ColourJustificationConfiguration());
        builder.ApplyConfiguration(new ErrorConfiguration());
        builder.ApplyConfiguration(new ManufacturerConfiguration());
        builder.ApplyConfiguration(new ManufacturerStatusConfiguration());
        builder.ApplyConfiguration(new MyMMConfiguration());
        builder.ApplyConfiguration(new MyMMStatusConfiguration());
        builder.ApplyConfiguration(new WidgetConfiguration());
        builder.ApplyConfiguration(new WidgetStatusConfiguration());
    }
}
