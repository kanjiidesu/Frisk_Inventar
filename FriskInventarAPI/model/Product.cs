namespace FriskInventarAPI;
public class Product
{
    public int ProductId { get; set; } // Primary Key
    public string ProductName { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int FridgeId { get; set; } // Foreign Key

}