namespace ContactsApp.DTO;
public record AddContact(string FirstName, string LastName, string PhoneNumber, string Email, int CategoryId, string? SubCategoryName, DateOnly BirthDate);