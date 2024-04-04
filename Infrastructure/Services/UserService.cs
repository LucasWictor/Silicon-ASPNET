using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;


namespace Infrastructure.Services
{
    public class UserService
    {
        private readonly UserRepository _repository;
        private readonly AddressService _addressService;
        private readonly ILogger<UserService> _logger;

        public UserService(UserRepository repository, AddressService addressService, ILogger<UserService> logger)
        {
            _repository = repository;
            _addressService = addressService;
            _logger = logger;
        }

        public async Task<ResponseResult> CreateUserAsync(SignUpModel model)
        {
            try
            {
                var existsResult = await _repository.AlreadyExistsAsync(x => x.Email == model.Email);
                if (existsResult.StatusCode == StatusCodes.Exists)
                {
                   // _logger.LogInformation("User already exists: {Email}", model.Email);
                    return ResponseFactory.Error("User already exists");
                }
                var createResult = await _repository.CreateOneAsync(UserFactory.Create(model));

                if (createResult.StatusCode != StatusCodes.Ok)
                {
                   // _logger.LogWarning("Failed to create user: {Email}", model.Email);
                    return createResult;
                }

               // _logger.LogInformation("User created successfully: {Email}", model.Email);
                return ResponseFactory.Ok("User was created successfully");
            }
            catch (Exception ex)
            {
              //  _logger.LogError(ex, "Error creating user: {Email}", model.Email);
                return ResponseFactory.Error(ex.Message);
            }
        }
        
        public async Task<ResponseResult> SignInUserAsync(SignInModel model)
        {
            try
            {
                var result = await _repository.GetOneAsync(x => x.Email == model.Email);
                if (result.StatusCode == StatusCodes.Ok && result.ContentResult != null)
                {
                    var userEntity = (UserEntity)result.ContentResult;
                    
                    if (PasswordHasher.ValidateSecurePassword(model.Password, userEntity.Password,
                            userEntity.SecurityKey))
                        return ResponseFactory.Ok();
                }
                
                return ResponseFactory.Error("Incorrect email or password");
            }
            catch (Exception ex) { return ResponseFactory.Error(ex.Message); }
        }
    }
}