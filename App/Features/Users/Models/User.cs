using App.Shared.Base;

namespace App.Features.Users.Models;

public sealed class User : Entity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Phone { get; private set; }
    public DateTime Created { get; private set; }

    public User(string name, string email, string password, string phone)
    {
        Name = name;
        Email = email.ToLower();
        Password = password;
        Phone = phone.Replace("-", "").Replace("(", "").Replace(")", "");
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