namespace FriskInventarAPI;
public class ProductCategory 
{
    public int ProductCategoryId { get; set; } // Primary Key
    public int CategoryId { get; set; } // Foreign Key
    public int ProductId { get; set; } // Foreign Key

}