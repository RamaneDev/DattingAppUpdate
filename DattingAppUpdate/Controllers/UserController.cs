using AutoMapper;
using DattingAppUpdate.Dtos;
using DattingAppUpdate.Entites;
using DattingAppUpdate.Errors;
using DattingAppUpdate.IRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DattingAppUpdate.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<LightUserToReturn>>> GetUsers()
        {
            var users = await _repo.GetUsers();

            var usersToReturn = _mapper.Map<IReadOnlyList<LightUserToReturn>>(users);

            return Ok(usersToReturn);
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<UserToReturn>> GetUser(int id)
        //{
        //    var user = await _repo.GetUser(id);

        //    var userToReturn = _mapper.Map<UserToReturn>(user);

        //    return Ok(userToReturn);
        //}

        [HttpGet("{username}")]
        public async Task<ActionResult<UserToReturn>> GetUserByUsername(string username)
        {
            var user = await _repo.GetUserByUsername(username);

            var userToReturn = _mapper.Map<UserToReturn>(user);

            return Ok(userToReturn);
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateUser(string username, UserToUpdateDto userForUpdateDto)
        {
            if (username != User.FindFirst(ClaimTypes.Name).Value)
                return Unauthorized(new ApiErrorResponse(403));
            var userFormRepo = await _repo.GetUserByUsername(username);

            _mapper.Map(userForUpdateDto, userFormRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating user ${username}$ failed on save");
        }

    }
}
