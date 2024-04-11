using Infrastructure.Entities;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Infrastructure.Factories;

namespace Infrastructure.Services
{
    public class UserService
    {
        private readonly UserRepository _repository;
        private readonly ILogger<UserService> _logger;

        public UserService(UserRepository repository, ILogger<UserService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ResponseResult> CreateUserAsync(SignUpModel model)
        {
            try
            {
                var existsResult = await _repository.AlreadyExistsAsync(x => x.Email == model.Email);
                if (existsResult.StatusCode == Infrastructure.Models.StatusCodes.Ok)
                {
                    return ResponseFactory.Error("User already exists", Infrastructure.Models.StatusCodes.Exists);
                }

                var userEntity = UserFactory.Create(model);
                var createResult = await _repository.CreateOneAsync(userEntity);

                if (createResult.StatusCode != Infrastructure.Models.StatusCodes.Ok)
                {
                    return createResult;
                }

                return ResponseFactory.Ok("User was created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create user");
                return ResponseFactory.Error("Failed to create user", Infrastructure.Models.StatusCodes.Error);
            }
        }

        public async Task<ResponseResult> SignInUserAsync(SignInModel model)
        {
            try
            {
                var result = await _repository.GetOneAsync(x => x.Email == model.Email);
                if (result.StatusCode == Infrastructure.Models.StatusCodes.Ok && result.ContentResult != null)
                {
                    var userEntity = (UserEntity)result.ContentResult;

                    if (PasswordHasher.ValidateSecurePassword(model.Password, userEntity.Password,
                            userEntity.SecurityKey))
                        return ResponseFactory.Ok();
                }

                return ResponseFactory.Error("Incorrect email or password", Infrastructure.Models.StatusCodes.Error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to sign in user");
                return ResponseFactory.Error("Failed to sign in user", Infrastructure.Models.StatusCodes.Error);
            }
        }
    }
}

