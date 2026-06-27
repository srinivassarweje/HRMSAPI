namespace DAL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Registration
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public bool IsActive { get; set; } = true;

        // Added per request
        public string Password { get; set; } = string.Empty;

        // ConfirmPassword is usually only used for UI validation and should not be persisted.
        // Marked NotMapped so it won't affect EF Core model/schema unless you intend to store it.
        [NotMapped]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string? RegistrationCode { get; set; }
    }
}
