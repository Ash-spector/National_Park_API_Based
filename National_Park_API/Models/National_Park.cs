using System.ComponentModel.DataAnnotations;

namespace National_Park_API.Models
{
    public class National_Park
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string State { get; set; }
        public byte[] ? Picture { get; set; }
        public DateTime Created { get; set; }
        public DateTime Established { get; set; }
    }
}
