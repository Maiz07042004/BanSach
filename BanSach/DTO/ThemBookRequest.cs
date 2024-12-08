namespace BanSach.DTO
{
    public class ThemBookRequest
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }  // Đảm bảo đây là chuỗi để lưu đường dẫn đến hình ảnh
        public decimal Discount { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
