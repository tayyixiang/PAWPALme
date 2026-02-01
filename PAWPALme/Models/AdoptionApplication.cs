using System.ComponentModel.DataAnnotations;
using PAWPALme.Data;
using PAWPALme.Enums;

namespace PAWPALme.Models
{
    public class AdoptionApplication
    {
        // PRIMARY KEY: Unique ID for this specific application form.
        [Key]
        public int Id { get; set; }

        // RELATIONSHIP 1: The Applicant (Adopter)
        // We link this application to a registered user account.
        // UserId is a String because ASP.NET Identity uses GUID strings (e.g., "ab12-cd34...") by default.
        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }

        // RELATIONSHIP 2: The Target (Pet)
        // We link this application to the specific animal they want.
        public int PetId { get; set; }
        public virtual Pet? Pet { get; set; }

        // --- STUB PROPERTIES ---
        // These are helper fields. While 'User' links to the account, 
        // storing Name/Email here directly makes it faster to show the list to the Shelter
        // without doing complex database joins every time.
        public string? ApplicantName { get; set; }
        public string? ApplicantEmail { get; set; }

        // METADATA:
        // Automatically stamps the time when the object is created.
        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        // STATE MACHINE:
        // Uses the Enum we defined earlier. Defaults to 'Pending' so shelter staff see it immediately.
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    }
}