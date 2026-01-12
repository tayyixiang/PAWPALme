using System.ComponentModel.DataAnnotations;

namespace PAWPALme.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(80)]
        public string Name { get; set; } = "";

        [Required, StringLength(50)]
        public string Species { get; set; } = "";

        [StringLength(80)]
        public string Breed { get; set; } = "";

        [Range(0, 50)]
        public int Age { get; set; }

        [Required, StringLength(30)]
        public string Status { get; set; } = "Available";

        [StringLength(500)]
        public string Description { get; set; } = "";

        [StringLength(400)]
        public string? ImageUrl { get; set; }

        [Required]
        public int ShelterId { get; set; }

        public Shelter? Shelter { get; set; }
    }
}
