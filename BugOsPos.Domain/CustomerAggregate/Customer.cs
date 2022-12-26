namespace BugOsPos.Domain.CustomerAggregate;

public class Customer
{
    public int Id { get; set; }
    public int FranchiseId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Blocked { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? DeliveryAddress { get; set; }

    private Customer(
        int franchiseId,
        string username,
        string email,
        string password,
        bool blocked,
        string name,
        string surname,
        string? deliveryAddress = null)
    {
        FranchiseId = franchiseId;
        Username = username;
        Email = email;
        Password = password;
        Blocked = blocked;
        Name = name;
        Surname = surname;
        DeliveryAddress = deliveryAddress;
    }

    public static Customer Create(
        int franchiseId,
        string username,
        string email,
        string password,
        string name,
        string surname)
    {
        return new(franchiseId, username, email, password, blocked: false, name, surname);
    }
}
