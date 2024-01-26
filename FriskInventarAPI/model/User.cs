namespace FriskInventarAPI;
public class User
{
    public int UserId { get; set; } // Primary Key
    public string UserName { get; set; }
    
    public string Email { get; set; }
    // password probably needs a different type? this needs to be secure in the database
    public string Password { get; set; }

}