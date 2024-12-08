using BanSachMVC.Models;

namespace BanSachMVC.DTO
{
    public class OrdersResponseDTO
    {
        public int TotalOrders { get; set; }
        public List<Order> Orders { get; set; }
    }
}
