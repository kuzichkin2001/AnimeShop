using System.ComponentModel.DataAnnotations;

namespace TestPet.Views;

public class UserCredentialsView
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}