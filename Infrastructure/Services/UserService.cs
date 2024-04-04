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
            var existsResult = await _repository.AlreadyExistsAsync(x => x.Email == model.Email);
            if (existsResult.StatusCode == StatusCodes.Exists)
            {
                return ResponseFactory.Error("User already exists");
            }

            var userEntity = UserFactory.Create(model); 
            var createResult = await _repository.CreateOneAsync(userEntity);

            if (createResult.StatusCode != StatusCodes.Ok)
            {
                return createResult;
            }

            return ResponseFactory.Ok("User was created successfully");
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