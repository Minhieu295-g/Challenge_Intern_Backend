using Challenge2.DTOs.Requests;
using Challenge2.Repositories;
using Challenge2.Repositories.Impl;
using Challenge2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Challenge2.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult CreateUser(UserRequest request)
        {
            try
            {
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    success = true,
                    message = "Created user successfulyy!",
                    data = _userService.createUser(request)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUserById(int id)
        {
            try
            {
                _userService.deleteUser(id);
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    success = true,
                    message = "Deleted user successfulyy!",
                });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(new
            {
                status = HttpStatusCode.OK,
                success = true,
                message = "Get list users successfulyy!",
                data = _userService.getUsers()
            });
        }
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.getUser(id);

            if(user == null)
            {
                return NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    success = false,
                    message = "User not found!"
                });
            }

            return Ok(new
            {
                status = HttpStatusCode.OK,
                success = true,
                message = "Get user successfully",
                data = user
            });
        }


        [HttpPut("{id}")]
        public IActionResult updateUser(int id, UserRequest userRequest)
        {
            try
            {
                _userService.updateUser(id, userRequest);
                return Ok(new
                {
                    status = HttpStatusCode.NoContent,
                    success = true,
                    message = "Update user successfully",
                });
            }
            catch (Exception e)
            {
                return NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    success = false,
                    message = "User not found"
                });
            }
        }

    }
}
