namespace FriskInventarAPI;
public class Fridge
{
    public int FridgeId { get; set; } // Primary Key
    public string FridgeName { get; set; }
    public List<Product>? Products {get; set; }

}