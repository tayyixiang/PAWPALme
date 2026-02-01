namespace PAWPALme.Enums
{
    // ENUMS (Enumerations):
    // These replace "Magic Strings". 
    // Instead of typing "Available" (which can be typoed as "Availabel"), 
    // we force the code to use PetStatus.Available.
    // In the database, these are stored as tiny integers (0, 1, 2) to save space.

    public enum PetStatus
    {
        Available,  // 0: Shown in search results
        Pending,    // 1: Someone is applying, hidden/greyed out
        Adopted     // 2: Hidden from search, kept for history
    }

    public enum PetGender
    {
        Male,
        Female
    }

    // STATE MACHINE:
    // This controls the workflow of a visit.
    // 1. User requests -> Pending
    // 2. Shelter reviews -> Confirmed OR Denied
    // 3. Visit happens -> Completed
    public enum AppointmentStatus
    {
        Pending,    // Default state when user books
        Confirmed,  // Shelter accepted the time
        Completed,  // Visit happened
        Cancelled,  // User changed mind
        Denied      // Shelter rejected (e.g. time unavailable)
    }

    public enum ApplicationStatus
    {
        Pending,
        UnderReview, // Shelter is reading it
        Approved,    // You get the pet!
        Rejected     // Sorry, not a match
    }
}