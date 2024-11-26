using System.Text.Json.Serialization;

namespace BanSach.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int Role { get; set; }

        // Navigation properties
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
