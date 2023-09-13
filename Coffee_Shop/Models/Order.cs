using System.ComponentModel.DataAnnotations;

namespace Coffee_Shop.Models;

public class Order
{
    [Key] 
    public int Id { get; set; }

    public string Name { get; set; }
    [Required(ErrorMessage = "Enter your Phone")]
    public string Phone { get; set; }
    public Product Product { get; set; }
    
}