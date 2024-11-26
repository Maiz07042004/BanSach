namespace BanSachMVC.Models
{
    public class CartDTO
    {
        public int CartId { get; set; }
        public List<CartItemDTO> Items { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
