namespace SupportTickestAPI.Models;

public class UserModel
{
    public Guid Id { get; private init; } = Guid.NewGuid();
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    
    public bool Active { get; private set; } = true;

    private UserModel() {}

    public UserModel(string name, string email)
    {
        Name = name;
        Email = email;
    }
    
    public void SetInactive() => Active = false;
    
    public void ChangeName(string name)
    {
        Name = name;
    }

    public void ChangeEmail(string email)
    {
        Email = email; 
    }
}