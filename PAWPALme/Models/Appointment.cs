using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PAWPALme.Enums;
using PAWPALme.Data; // IMPORT THIS explicitly

namespace PAWPALme.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan AppointmentTime { get; set; }

        // Helper to get full DateTime
        [NotMapped]
        public DateTime DateTime => AppointmentDate + AppointmentTime;

        [Required]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        [StringLength(500)]
        public string? Notes { get; set; }

        // --- RELATIONS ---

        public int? AdoptionApplicationId { get; set; }
        [ForeignKey("AdoptionApplicationId")]
        public virtual AdoptionApplication? AdoptionApplication { get; set; }

        public int PetId { get; set; }
        [ForeignKey("PetId")]
        public virtual Pet? Pet { get; set; }

        public int ShelterId { get; set; }
        [ForeignKey("ShelterId")]
        public virtual Shelter? Shelter { get; set; }

        public string? AdopterUserId { get; set; }

        [ForeignKey("AdopterUserId")]
        public virtual ApplicationUser? AdopterUser { get; set; }
    }
}