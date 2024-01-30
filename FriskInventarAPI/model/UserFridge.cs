using System.ComponentModel.DataAnnotations.Schema;

namespace FriskInventarAPI;
public class UserFridge 
{
    public int UserFridgeId { get; set; } // Primary Key
    public int UserId { get; set; } // Foreign Key
    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; } // instance that the foreign key points to
    public int FridgeId { get; set; } // Foreign Key
    [ForeignKey(nameof(FridgeId))]
    public virtual Fridge? Fridge { get; set; } // instance that the foreign key points to
}