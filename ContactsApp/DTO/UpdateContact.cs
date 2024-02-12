using ContactsApp.Models;

namespace ContactsApp.DTO;
public record UpdateContact(int contactId, string FirstName, string LastName, string PhoneNumber, string Email, int CategoryId, string SubCategoryName, DateOnly BirthDate);