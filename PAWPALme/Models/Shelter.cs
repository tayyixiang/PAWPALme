using System.ComponentModel.DataAnnotations;

namespace PAWPALme.Models
{
    public class Shelter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OwnerUserId { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = "";

        [StringLength(200)]
        public string Address { get; set; } = "";

        [StringLength(30)]
        public string Phone { get; set; } = "";

        [StringLength(200)]
        public string Description { get; set; } = "";
    }
}
