using App.Shared.Base;

namespace App.Features.Users.Models;

public sealed class User : Entity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Phone { get; private set; }
    public DateTime Created { get; private set; }
    public DateTime Updated { get; private set; }
    public bool Active { get; private set; }

    public void ToggleActive()
    {
        Active = !Active;
        Updated = DateTime.UtcNow;
    }

    public User(string name, string email, string password, string phone)
    {
        Name = name;
        Email = email.ToLower();
        Password = password;
        Phone = phone.Replace("-", "").Replace("(", "").Replace(")", "");
        Created = DateTime.UtcNow;
        Updated = DateTime.UtcNow;
        Active = true;
    }

    public User(Guid id, string name, string email, string password, string phone, DateTime created, DateTime updated, bool active) : base(id)
    {
        Name = name;
        Email = email;
        Password = password;
        Phone = phone;
        Created = created;
        Updated = updated;
        Active = active;
    }
}