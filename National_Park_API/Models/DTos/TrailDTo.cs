using System.ComponentModel.DataAnnotations;
using static National_Park_API.Models.Trail;

namespace National_Park_API.Models.DTos
{
    public class TrailDTo
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Distance { get; set; }
        [Required]
        public string Elevation { get; set; }
        public Difficultytype Difficulty { get; set; }
        public NationalParkDto NationalPark { get; set; }
    }
}
