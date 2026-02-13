using App.Shared.Base;

namespace App.Features.Users;

public sealed class User : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public DateTime Created { get; set; }

    public User(string name, string email, string password, string phone)
    {
        Name = name;
        Email = email;
        Password = password;
        Phone = phone;
        Created = DateTime.UtcNow;
    }

    public User(Guid id, string name, string email, string password, string phone, DateTime created) : base(id)
    {
        Name = name;
        Email = email;
        Password = password;
        Phone = phone;
        Created = created;
    }
}