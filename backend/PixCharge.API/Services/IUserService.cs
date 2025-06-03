using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PixCharge.API.Models;
using PixCharge.API.Models.InputModels;
using PixCharge.API.Models.ViewModels;
using PixCharge.API.Repositories;

namespace PixCharge.API.Services
{
    public interface IUserService
    {
        Task<ResultViewModel<List<User>>> GetAll();
        Task<ResultViewModel<User>> GetById(Guid id);
        Task<ResultViewModel<User>> Register(CreateUserInputModel model);
        Task<ResultViewModel<string>> Login(LoginInputModel model);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IAuthService _authService;

        public UserService(IUserRepository repository, IAuthService authService)
        {
            _authService = authService;
            _repository = repository;
        }

        public async Task<ResultViewModel<List<User>>> GetAll()
        {
            var users = await _repository.GetAllAsync();
            if (users == null || !users.Any())
                return ResultViewModel<List<User>>.Error("No users found");

            return ResultViewModel<List<User>>.Success(users);
        }

        public async Task<ResultViewModel<User>> GetById(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
                return ResultViewModel<User>.Error("User not found");

            return ResultViewModel<User>.Success(user);
        }

        public async Task<ResultViewModel<User>> Register(CreateUserInputModel model)
        {
            if (model == null)
                return ResultViewModel<User>.Error("Invalid user data");

            var existingUser = await _repository.GetByEmailAsync(model.Email);
            if (existingUser != null)
                return ResultViewModel<User>.Error("User with this email already exists");

            model.Password = _authService.ComputeHash(model.Password);
            var user = model.ToEntity();

            await _repository.AddAsync(user);
            return ResultViewModel<User>.Success(user);
        }

        public async Task<ResultViewModel<string>> Login(LoginInputModel model)
        {
            if (model == null)
                return ResultViewModel<string>.Error("Invalid login data");

            var user = await _repository.GetByEmailAsync(model.Email);
            if (user == null || user.PasswordHash != _authService.ComputeHash(model.Password))
                return ResultViewModel<string>.Error("Invalid email or password");

            var token = _authService.GenerateToken(user.Id, user.Email, user.Role);
            return ResultViewModel<string>.Success(token);
        }
    }
}