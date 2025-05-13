using Challenge2.Data;
using Challenge2.DTOs.Responses;
using Microsoft.EntityFrameworkCore;

namespace Challenge2.Repositories.Impl
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly MyDbContext _context;

        public UserRepositoryImpl(MyDbContext context)
        {
            _context = context;
        }
        
        public int createUser(User user)
        {
           _context.Users.Add(user);
            return _context.SaveChanges();

        }

        public void deleteUser(int id)
        {
           var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public User getUser(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            return user;
        }

        public IQueryable<User> getUsers()
        {
            return _context.Users;
        }

        public IQueryable<UserWithAddressResponse> QueryUsersWithAddresses(int pageNo, int pageSize)
        {
            return _context.Users
             .AsNoTracking()                               
             .Include(u => u.Addresses)                    
             .OrderBy(u => u.Username)                      
             .Skip((pageNo - 1) * pageSize)                   
             .Take(pageSize)
             .Select(u => new UserWithAddressResponse          
             {
                 Id = u.Id,
                 Username = u.Username,
                 Email = u.Email,
                 Phone = u.Phone,
                 Addresses = u.Addresses
                              .Select(a => new AddressResponse
                              {
                                  District = a.District,
                                  Ward = a.Ward,
                                  Province = a.Province,
                                  Details = a.details
                              }).ToList()
             });
        }

        public void updateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
