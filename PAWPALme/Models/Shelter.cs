using System.ComponentModel.DataAnnotations;

namespace PAWPALme.Models
{
    public class Shelter
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Address { get; set; } = "";

        [Required]
        public string Phone { get; set; } = "";

        [StringLength(1000)]
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public string? OwnerUserId { get; set; }
        public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
    }
}