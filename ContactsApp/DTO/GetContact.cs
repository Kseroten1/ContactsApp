using ContactsApp.Migrations;

namespace ContactsApp.DTO;

public record GetContact(int ContactId, string FirstName, string LastName, string PhoneNumber, string Email, string Category, string? SubCategory, DateOnly BirthDate);
