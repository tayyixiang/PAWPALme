using System.ComponentModel.DataAnnotations;

namespace PAWPALme.Models
{
    public class Shelter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Description { get; set; }

        public string? OwnerUserId { get; set; }
    }
}

