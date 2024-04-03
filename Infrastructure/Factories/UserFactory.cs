using Infrastructure.Entities;
using Infrastructure.Helpers;
using Infrastructure.Models;

namespace Infrastructure.Factories;

public class UserFactory
{
    public static UserEntity Create()
    {
        try
        {
            // Must use UTC or shit breaks
            var date = DateTime.UtcNow;
            
            return new UserEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Created = date,
                Modified = date,
            };
        }
        catch
        {
        }
        return null!;
    }
    
    public static UserEntity Create(SignUpModel model)
    {
        try
        {
            // Must use UTC or shit breaks
            var date = DateTime.UtcNow;
            var (password, securityKey) = PasswordHasher.GenerateSecurePassword(model.Password);
            
            return new UserEntity
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = password, 
                SecurityKey = securityKey,
                Created = date,
                Modified = date
            };
        }
        catch
        {
        }
        return null!;
    }
}