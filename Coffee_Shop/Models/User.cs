using System.ComponentModel.DataAnnotations.Schema;
using Coffee_Shop.Data.Enum;

namespace Coffee_Shop.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        [NotMapped]         
        static public List<Order> Basket { get; set; } = new List<Order>();
    }
}
