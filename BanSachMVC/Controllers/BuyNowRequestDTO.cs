namespace BanSachMVC.Controllers
{
    internal class BuyNowRequestDTO
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string OrtherNotes { get; set; }
    }
}