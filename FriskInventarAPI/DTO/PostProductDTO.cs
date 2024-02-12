namespace FriskInventarAPI;
public class PostProductDTO
{
    public string ProductName { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int FridgeId { get; set; } 
    public int categoryId {get;set; }

}