using System.Text.Json.Serialization;

namespace BanSach.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        // Navigation property
        public virtual ICollection<Book> Books { get; set; }
    }
}
