using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAWPALme.Data
{
    // INHERITANCE:
    // Instead of building a user system from scratch, we inherit from 'IdentityUser'.
    // This gives us built-in properties like Email, PasswordHash, PhoneNumber, and TwoFactorEnabled.
    // We only need to add the *extra* stuff we need for our specific app.
    public class ApplicationUser : IdentityUser
    {
        // CUSTOM PROPERTY:
        // The default IdentityUser doesn't have a "Name" field (only Username/Email).
        // So we add 'FullName' here to store the user's real name (e.g., "John Doe").
        [PersonalData] // Marks this as sensitive info (good for GDPR compliance tools)
        public string FullName { get; set; } = "";

        // THE CRITICAL LINK (Foreign Key):
        // This is a nullable integer (int?). 
        // WHY NULLABLE? Because Adopters are Users too, but they DON'T own a shelter.
        // If this is null, the user is an Adopter.
        // If this has a number (e.g., 5), the user is a Shelter Owner.
        public int? ShelterId { get; set; }

        // NAVIGATION PROPERTY:
        // This allows Entity Framework to automatically load the Shelter object.
        // So I can write 'user.Shelter.Name' in my code instead of writing a complex SQL JOIN query.
        [ForeignKey("ShelterId")]
        public virtual PAWPALme.Models.Shelter? Shelter { get; set; }
    }
}