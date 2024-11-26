using System.Text.Json.Serialization;
namespace BanSachMVC.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navigation properties
        public virtual Order Order { get; set; }
        public virtual Book Book { get; set; }
    }
}
