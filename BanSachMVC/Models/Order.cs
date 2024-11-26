using System.Text.Json.Serialization;
namespace BanSachMVC.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}
