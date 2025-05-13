using MyAppDemo.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace MyAppDemo.Helpers.Document
{
    public class UserExample
    {
    }
    public class UserMVExample : IExamplesProvider<UserMV>
    {
        public UserMV GetExamples()
        {
            return new UserMV
            {
                Username = "username123",
                Password = "password123",
                Email = "email@gmail.com",
                Phone = "0345671231"
            };
        }
    }
    public class AddUserResponseExample : IExamplesProvider<ApiResponse<User>>
    {
        public ApiResponse<User> GetExamples()
        {
            return new ApiResponse<User>
            {
                Status = HttpStatusCode.Created,
                Success = true,
                Message = "Added user successfully",
                Data = new User
                {   
                    Id = Guid.NewGuid(),
                    Username = "username123",
                    Password = "password123",
                    Email = "email@gmail.com",
                    Phone = "0345671231"
                }
            };
        }
    }
    public class UserUpdatedResponseExample : IExamplesProvider<ApiResponse<User>>
    {
        public ApiResponse<User> GetExamples()
        {
            return new ApiResponse<User>
            {
                Status = HttpStatusCode.Created,
                Success = true,
                Message = "Updated user successfully",
                Data = new User
                {
                    Id = Guid.NewGuid(),
                    Username = "username123",
                    Password = "password123",
                    Email = "email@gmail.com",
                    Phone = "0345671231"
                }
            };
        }
    }

    public class UserResponseExample : IExamplesProvider<ApiResponse<User>>
    {
        public ApiResponse<User> GetExamples()
        {
            return new ApiResponse<User>
            {
                Status = HttpStatusCode.OK,
                Success = true,
                Message = "Get list users successfully",
                Data = new User
                {
                    Id = Guid.NewGuid(),
                    Username = "username123",
                    Password = "password123",
                    Email = "email@gmail.com",
                    Phone = "0345671231"
                }
            };
        }
    }
    public class ListUsersResponseExample : IExamplesProvider<ApiResponse<List<User>>>
    {
        public ApiResponse<List<User>> GetExamples()
        {
            List<User> users = new List<User>();
            users.Add(new User
            {
                Id = Guid.NewGuid(),
                Username = "username123",
                Password = "password123",
                Email = "email@gmail.com",
                Phone = "0345671231"
            });
            users.Add(new User
            {
                Id = Guid.NewGuid(),
                Username = "username1234",
                Password = "password1234",
                Email = "email@gmail.com",
                Phone = "0345671231"
            });
            return new ApiResponse<List<User>>
            {
                Status = HttpStatusCode.OK,
                Success = true,
                Message = "Get list users successfully",
                Data = users
            };
        }
    }
    public class BadrequestUserResponseExample : IExamplesProvider<ApiResponse<User>>
    {
        public ApiResponse<User> GetExamples()
        {
            return new ApiResponse<User>
            {
                Status = HttpStatusCode.BadRequest,
                Success = false,
                Message = "Invalid user ID format",
                Data = null
            };
        }
    }
    public class NotFoundUserResponseExample : IExamplesProvider<ApiResponse<User>>
    {
        public ApiResponse<User> GetExamples()
        {
            return new ApiResponse<User>
            {
                Status = HttpStatusCode.NotFound,
                Success = false,
                Message = "User not found",
                Data = null
            };
        }
    }

}
