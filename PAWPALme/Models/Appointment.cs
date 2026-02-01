using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PAWPALme.Enums;
using PAWPALme.Data;

namespace PAWPALme.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        // DATE & TIME HANDLING:
        // We store Date and Time separately. This is often easier for HTML forms 
        // (one input for the calendar, one for the clock).

        [Required]
        [DataType(DataType.Date)] // UI Hint: "Browser, please render a Calendar picker here"
        public DateTime AppointmentDate { get; set; }

        [Required]
        [DataType(DataType.Time)] // UI Hint: "Browser, please render a Clock picker here"
        public TimeSpan AppointmentTime { get; set; }

        // COMPUTED PROPERTY:
        // [NotMapped] tells Entity Framework: "Do NOT create a column for this in SQL."
        // This is a helper for our C# code. If we need to compare times, we can just use 
        // 'appt.DateTime' instead of manually adding the two fields together every time.
        [NotMapped]
        public DateTime DateTime => AppointmentDate + AppointmentTime;

        // STATE MACHINE:
        // Defaults to 'Pending' so the shelter has to approve it manually.
        [Required]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        // COMMUNICATION CHANNEL:
        // 'Notes' = What the ADOPTER says to the Shelter ("I have a big yard").
        [StringLength(500)]
        public string? Notes { get; set; }

        // 'ShelterRemarks' = What the SHELTER says back ("Please bring your ID").
        // Separating these prevents the user from accidentally editing the shelter's instructions.
        [StringLength(500)]
        public string? ShelterRemarks { get; set; }

        // --- THE RELATIONAL WEB ---
        // An appointment ties together 4 different entities:

        // 1. The Application (Optional: An appointment might happen before an official application)
        public int? AdoptionApplicationId { get; set; }
        [ForeignKey("AdoptionApplicationId")]
        public virtual AdoptionApplication? AdoptionApplication { get; set; }

        // 2. The Subject (The Pet being visited)
        public int PetId { get; set; }
        [ForeignKey("PetId")]
        public virtual Pet? Pet { get; set; }

        // 3. The Host (The Shelter)
        public int ShelterId { get; set; }
        [ForeignKey("ShelterId")]
        public virtual Shelter? Shelter { get; set; }

        // 4. The Guest (The Adopter)
        public string? AdopterUserId { get; set; }
        [ForeignKey("AdopterUserId")]
        public virtual ApplicationUser? AdopterUser { get; set; }
    }
}