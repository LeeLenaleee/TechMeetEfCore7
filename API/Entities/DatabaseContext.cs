using EfCore7.Interceptors;
using EfCore7.ModelBuildingConventions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EfCore7.Entities;

public class DatabaseContext : DbContext
{
    private static readonly SetRetrievedInterceptor _setRetrievedInterceptor = new();

    public virtual DbSet<Employee> Employees => Set<Employee>();

    public string DbPath { get; }

    public DatabaseContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "employees.db");
    }
    
    // Single Table
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Employee>()
            .OwnsOne(employee => employee.ContactDetails, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.OwnsOne(contactDetails => contactDetails.Address);
            });
    }
    
    // Multi table
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder
    //         .Entity<Employee>()
    //         .OwnsOne(employee => employee.ContactDetails, ownedNavigationBuilder =>
    //         {
    //             ownedNavigationBuilder.ToTable("Contacts");
    //             ownedNavigationBuilder.OwnsOne(
    //                 contactDetails => contactDetails.Address, ownedOwnedNavigationBuilder =>
    //                 {
    //                     ownedOwnedNavigationBuilder.ToTable("Addresses");
    //                 });
    //         });
    // }
    
    // Single JSON collumn (only works in SQL server)
    // https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/whatsnew#mapping-to-json-columns
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder
    //         .Entity<Employee>()
    //         .OwnsOne(employee => employee.ContactDetails, ownedNavigationBuilder =>
    //         {
    //             ownedNavigationBuilder.ToJson();
    //             ownedNavigationBuilder.OwnsOne(contactDetails => contactDetails.Address);
    //         });
    // }
    
    // Trigger (SqlServer only)
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder
    //         .Entity<Employee>()
    //         .ToTable(tb => tb.HasTrigger("TriggerName"));
    // }
    
    // Key insert optimizations
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Employee>().Property(e => e.Id).UseHiLo();
    // }

    // Extra mapping type (Tpc)
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Employee>().UseTpcMappingStrategy();
    // }
    
    // UTF 8 SQL server, default UTF16
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder
    //         .Entity<Employee>()
    //         .Property(e => e.Name)
    //         .HasColumnType("varchar(max)")
    //         .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");
    // }
    
    // Model building conventions
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove(typeof(ForeignKeyIndexConvention));
        configurationBuilder.Conventions.Add(_ => new CustomDiscriminatorLengthConvention());
        configurationBuilder.Conventions.Add(_ => new MaxStringLengthConvention());
    }
    
    // Stored procedures
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder
    //         .Entity<Employee>()
    //         .InsertUsingStoredProcedure("Employee_insert", storedProcedureBuilder =>
    //         {
    //             storedProcedureBuilder.HasParameter(e => e.Name);
    //             storedProcedureBuilder.HasParameter("Age");
    //             storedProcedureBuilder.HasParameter(e => e.IsOld);
    //             storedProcedureBuilder.HasResultColumn(e => e.Id);
    //         });
    // }
    
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options
            .AddInterceptors(_setRetrievedInterceptor)
            .AddInterceptors(new LoggerInjectionInterceptor())
            .UseSqlite($"Data Source={DbPath}").LogTo(Console.WriteLine, LogLevel.Information);
}