namespace SupportTickestAPI.Models.User;

public record UserResponse(Guid Id, string Name, string Email, bool Active);