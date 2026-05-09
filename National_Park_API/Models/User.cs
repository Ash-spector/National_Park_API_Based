using System.ComponentModel.DataAnnotations.Schema;

namespace National_Park_API.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
