using Challenge2.DTOs.Requests;
using Challenge2.DTOs.Responses;

namespace Challenge2.Services
{
    public interface IUserService
    {
        int createUser(UserRequest request);

        void updateUser(int id, UserRequest request);

        void deleteUser(int id);

        UserResponse getUser(int id);

        IEnumerable<UserResponse> getUsers();
    }
}
