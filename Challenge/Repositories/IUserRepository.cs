using Challenge2.Data;
using Challenge2.DTOs.Requests;
using Challenge2.DTOs.Responses;

namespace Challenge2.Repositories
{
    public interface IUserRepository
    {
        int createUser(User user);

        void updateUser(User user);

        void deleteUser(int id);

        User getUser(int id);

        IQueryable<User> getUsers();

        IQueryable<UserWithAddressResponse> QueryUsersWithAddresses(int pageNo, int pageSize);
    }
}
