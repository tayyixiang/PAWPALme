using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PAWPALme.Enums;

namespace PAWPALme.Models
{
    public class Pet
    {
        // PRIMARY KEY:
        // Entity Framework automatically knows "Id" means Primary Key.
        [Key]
        public int Id { get; set; }

        // VALIDATION ATTRIBUTES:
        // These attributes do double duty:
        // 1. Database: Creates a VARCHAR(80) NOT NULL column.
        // 2. UI: Automatically prevents the user from submitting the form if blank.
        [Required(ErrorMessage = "Pet Name is required")]
        [StringLength(80, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        // SPECIES:
        // Currently a string ("Dog", "Cat"), but allows flexibility for "Other".
        [Required]
        [StringLength(50)]
        public string Species { get; set; } = string.Empty;

        [Required]
        [StringLength(80)]
        public string Breed { get; set; } = string.Empty;

        // RANGE VALIDATION:
        // Prevents typos like "Age: 100" or "Age: -5".
        [Range(0, 30)]
        public int Age { get; set; }

        // METADATA:
        // Helps adopters filter ("I live in a small apartment, I need a Small dog").
        [StringLength(20)]
        public string Size { get; set; } = "Medium";

        // GENDER (ENUM):
        // Uses the PetGender enum (Male/Female) to ensure data consistency.
        [Required]
        public PetGender Gender { get; set; }

        // STATUS (ENUM):
        // Default is 'Available'. 
        // If a shelter marks this as 'Adopted', the pet disappears from public search.
        [Required]
        public PetStatus Status { get; set; } = PetStatus.Available;

        // DESCRIPTION:
        // Allows for a long bio (up to 1000 chars) to tell the pet's story.
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        // IMAGE HANDLING:
        // We do NOT store the actual image blob in SQL (that would be slow).
        // Instead, we store the *path* (URL) to where the file lives on the server disk.
        [StringLength(500)]
        public string? ImageUrl { get; set; }

        // --- RELATIONSHIP: OWNERSHIP ---
        // A pet CANNOT exist without a Shelter.
        // This is enforced by [Required] on the Foreign Key (ShelterId).
        [Required]
        public int ShelterId { get; set; }

        // Navigation Property: Allows us to say `myPet.Shelter.Address` easily.
        [ForeignKey("ShelterId")]
        public virtual Shelter? Shelter { get; set; }

        // AUDIT FIELD:
        // Automatically records when the pet was added to the system.
        // Useful for sorting by "Newest Arrivals".
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}