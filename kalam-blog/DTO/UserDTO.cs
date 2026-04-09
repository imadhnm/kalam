public record UserDTO(string Username, string Email, string Password);

public record LoginDTO(string Username, string Password, bool IsPersist);