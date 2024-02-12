namespace FriskInventarAPI;
public class Category
{
    public int CategoryId { get; set; } // Primary Key
    public string CategoryName { get; set; }
    public int FridgeId { get; set; } // Foreign key
    
    public List<ProductCategory> ProductCategories {get;set;}
}