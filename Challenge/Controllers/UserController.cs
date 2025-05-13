using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAppDemo.Helpers.Document;
using MyAppDemo.Helpers;
using MyAppDemo.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using Microsoft.AspNetCore.Connections;

namespace MyAppDemo.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static List<User> users = new List<User>();
        /// <summary>
        /// Add new User
        /// </summary>
        /// <param name="userMV">user want to create</param>
        /// <response code="201">User added successfully</response>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<User>), 201)]
        [SwaggerRequestExample(typeof(UserMV), typeof(UserMVExample))]
        [SwaggerResponseExample(201, typeof(AddUserResponseExample))]
        public IActionResult createUser(UserMV userMV)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = userMV.Username,
                Email = userMV.Email,
                Phone = userMV.Phone,
                Password = userMV.Password,
            };
            users.Add(user);
            return Ok(new
            {
                status = HttpStatusCode.Created,
                success = true,
                message = "Created user successfully",
                data = users
            });
        }
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="id"> user id for update</param>
        /// <param name="userMV">new information for update</param>
        /// <response code="201">User updated successfully</response>
        /// <response code="400">Invalid user id format</response>
        /// <response code="403">User not found</response>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<User>), 201)]
        [ProducesResponseType(typeof(ApiResponse<User>), 400)]
        [ProducesResponseType(typeof(ApiResponse<User>), 404)]
        [SwaggerRequestExample(typeof(UserMV), typeof(UserMVExample))]
        [SwaggerResponseExample(201, typeof(UserUpdatedResponseExample))]
        [SwaggerResponseExample(400, typeof(BadrequestUserResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundUserResponseExample))]
        public IActionResult updateUser(String id, UserMV userMV)
        {
            var user = users.SingleOrDefault(u => u.Id == Guid.Parse(id));
            if (user == null)
            {
                return NotFound();
            }
            user.Username = userMV.Username;
            user.Email = userMV.Email;
            user.Password = userMV.Password;
            user.Phone = userMV.Phone;
            return Ok(new
            {
                status = HttpStatusCode.Created,
                success = true,
                message = "Updated user successfully",
                data = user
            });

        }
        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult deleteUser(String id) {
            var user = users.SingleOrDefault(u => u.Id == Guid.Parse(id));
            users.Remove(user);
            return Ok(new
            {
                status = HttpStatusCode.OK,
                success = true,
                message = "Deleted user successfully"
            });
        }
        /// <summary>
        /// Get list users
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///     GET /api/users
        /// </remarks>
        /// <response code="200">Get list user successfully</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<User>>), 200)]
        [SwaggerResponseExample(200, typeof(ListUsersResponseExample))]
        [HttpGet]
        public IActionResult getUsers()
        {
            return Ok(new
            {
                status = HttpStatusCode.OK,
                success = true,
                message = "Get users successfully!",
                data = users
            });
        }

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id">id for get users</param>
        /// <returns></returns>
        /// <response code="200">Get a user successfully</response>
        /// <response code ="400"> Invalid user id format </response>
        /// <response code ="403"> User not found </response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<User>), 200)]
        [ProducesResponseType(typeof(ApiResponse<User>), 400)]
        [ProducesResponseType(typeof(ApiResponse<User>), 404)]
        [SwaggerResponseExample(200, typeof(UserResponseExample))]
        [SwaggerResponseExample(400, typeof(BadrequestUserResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundUserResponseExample))]
        public IActionResult getUserById(String id)
        {
            try
            {
                var user = users.SingleOrDefault(u => u.Id == Guid.Parse(id));
                return Ok(user);
            } catch(Exception e)
            {
                return NotFound();
            }
        }

    }
}
