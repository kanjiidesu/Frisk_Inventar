namespace FriskInventarAPI;
public class UserFridge 
{
    public int UserFridgeId { get; set; } // Primary Key
    public int UserId { get; set; } // Foreign Key
    public int FridgeId { get; set; } // Foreign Key
}