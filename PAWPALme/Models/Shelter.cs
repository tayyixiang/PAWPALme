using System.ComponentModel.DataAnnotations;

namespace PAWPALme.Models
{
    public class Shelter
    {
        // PRIMARY KEY:
        // Unique ID for the database to track this specific business.
        public int Id { get; set; }

        // BASIC INFO:
        // These fields are [Required] because a shelter cannot exist without contact details.
        // This ensures data quality before it ever reaches the database.
        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Address { get; set; } = "";

        [Required]
        public string Phone { get; set; } = "";

        // PROFILE BIO:
        // Optional (?) field allowing the shelter to write a mission statement.
        // Limited to 1000 characters to prevent layout breaking.
        [StringLength(1000)]
        public string? Description { get; set; }

        // BRANDING:
        // Stores the URL path to their logo upload (e.g., "/uploads/shelters/logo1.png").
        public string? ImageUrl { get; set; }

        // --- SECURITY LINK ---
        // This is the bridge between the "Business Data" and the "Login System".
        // It stores the ID of the 'ApplicationUser' who manages this profile.
        // This allows us to say: "Only User X can edit Shelter Y".
        public string? OwnerUserId { get; set; }

        // --- RELATIONSHIP: INVENTORY ---
        // This is a "Collection Navigation Property".
        // It establishes a One-to-Many relationship: One Shelter -> Many Pets.
        // It allows Entity Framework to efficiently fetch "All pets belonging to Shelter A".
        public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
    }
}