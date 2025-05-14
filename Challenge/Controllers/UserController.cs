using Challenge2.DTOs.Requests;
using Challenge2.Exceptions;
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
        public IActionResult CreateUser([FromBody] UserRequest request)
        {
            if (!ModelState.IsValid)
            {
                var messages = ModelState
                               .SelectMany(x => x.Value.Errors)  
                               .Select(e => e.ErrorMessage)     
                               .ToArray().GetValue(0);

                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    success = false,
                    message = "Validation failed",
                    errors = messages,
                });
            }
            try
            {
                var result = _userService.createUser(request);
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    success = true,
                    message = "Created user successfully!",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    success = false,
                    message = "Error creating user",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteUserById([FromRoute] int id)
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
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    success = false,
                    message = "Error creating user",
                    error = ex.Message
                });
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
            try
            {
                var user = _userService.getUser(id);

                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    success = true,
                    message = "Get user successfully",
                    data = user
                });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = HttpStatusCode.InternalServerError,
                    success = false,
                    message = "Unexpected error occurred",
                    error = ex.Message
                });
            }
        }



        [HttpPut("{id}")]
        public IActionResult updateUser(int id, UserRequest userRequest)
        {
            if (!ModelState.IsValid)
            {
                var messages = ModelState
                               .SelectMany(x => x.Value.Errors)
                               .Select(e => e.ErrorMessage)
                               .ToArray().GetValue(0);

                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    success = false,
                    message = "Validation failed",
                    errors = messages,
                });
            }
            try
            {
                _userService.updateUser(id, userRequest);
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    success = true,
                    message = "Update user successfully",
                });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = HttpStatusCode.InternalServerError,
                    success = false,
                    message = "Unexpected error occurred",
                    error = ex.Message
                });
            }
        }

    }
}
