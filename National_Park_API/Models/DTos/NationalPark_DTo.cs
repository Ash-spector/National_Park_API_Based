using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace National_Park_API.Models.DTos
{
    public class NationalParkDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string State { get; set; }
        public byte[]? Picture { get; set; }
        public DateTime Created { get; set; }
        public DateTime Established { get; set; }
    }
}
