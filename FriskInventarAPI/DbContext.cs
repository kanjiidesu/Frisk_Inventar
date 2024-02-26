using Microsoft.EntityFrameworkCore;

namespace FriskInventarAPI;

public class FriskInventarContext : DbContext
{
    
    private readonly IConfiguration _configuration;
    public DbSet<User> Users {get;set;}
    public DbSet<Fridge> Fridges {get;set;}
    public DbSet<UserFridge> UserFridges {get;set;}
    public DbSet<Category> Categories {get;set;}
    public DbSet<Product> Products {get;set;}
    public DbSet<ProductCategory> ProductCategories {get;set;}
    protected override void OnConfiguring(DbContextOptionsBuilder options) 
    {
        // connection string to db
        options.UseOracle("Data Source=localhost:1541/xe;User Id=Fresh_Produce;Password=Fresh_Produce;");
        //options.UseOracle("Data Source=FriskInventarDatabase:1521/xe;User Id=system;Password=oracle;");
        
    }
    public FriskInventarContext() 
    {

    }
    public FriskInventarContext(DbContextOptions<FriskInventarContext> options,
                           IConfiguration configuration) : base(options)
    {
      this._configuration = configuration;
    }
}
