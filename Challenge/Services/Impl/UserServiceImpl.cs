using Challenge2.Data;
using Challenge2.DTOs.Requests;
using Challenge2.DTOs.Responses;
using Challenge2.Exceptions;
using Challenge2.Repositories;

namespace Challenge2.Services.Impl
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserServiceImpl(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int createUser(UserRequest request)
        {
            var user = new User
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email,
                Phone = request.Phone,
            };
            return _userRepository.createUser(user);
        }

        public void deleteUser(int id)
        {
            _userRepository.deleteUser(id);
        }

        public UserResponse getUser(int id)
        {
            var user = _userRepository.getUser(id);
            if (user == null) throw new NotFoundException($"User with id = {id} not found");
            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
            };
        }

        public IEnumerable<UserResponse> getUsers()
        {
            return _userRepository.getUsers()
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    Phone = u.Phone
                })
                .ToList();
        }

        public void updateUser(int id, UserRequest request)
        {
            var user = _userRepository.getUser(id);
            if (user == null) throw new NotFoundException($"User with id = {id} not found");

            user.Username = request.Username;
            user.Password = request.Password;
            user.Email = request.Email;
            user.Phone = request.Phone;
            _userRepository.updateUser(user);
        }
    }
}
