using System.Text.Json.Serialization;

namespace BanSach.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navigation properties
        public virtual Cart Cart { get; set; }
        public virtual Book Book { get; set; }
    }
}
