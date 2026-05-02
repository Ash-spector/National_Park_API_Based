using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace National_Park_API.Models
{
    public class Trail
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Distance { get; set; }
        [Required]
        public string Elevation { get; set; }
        public DateTime DateCreated { get; set; }
        public enum Difficultytype { Easy, Moderate, Difficult }
        public Difficultytype Difficulty { get; set; }
        public int NationalParkId { get; set; }
        [ForeignKey("NationalParkId")]
        public National_Park NationalPark { get; set; }  
    }
}