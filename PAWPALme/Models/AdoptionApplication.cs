using System.ComponentModel.DataAnnotations;
using PAWPALme.Data;
using PAWPALme.Enums; // Required for the new Enum

namespace PAWPALme.Models
{
    public class AdoptionApplication
    {
        [Key]
        public int Id { get; set; }

        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }

        public int PetId { get; set; }
        public virtual Pet? Pet { get; set; }

        // --- STUB PROPERTIES FOR YIXIANG'S TESTING ---
        public string? ApplicantName { get; set; }
        public string? ApplicantEmail { get; set; }

        // --- ADD THESE MISSING PROPERTIES ---
        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    }
}