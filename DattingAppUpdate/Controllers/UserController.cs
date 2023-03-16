using AutoMapper;
using DattingAppUpdate.Dtos;
using DattingAppUpdate.Entites;
using DattingAppUpdate.IRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DattingAppUpdate.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
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

        [HttpGet("{id}")]
        public async Task<ActionResult<UserToReturn>> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserToReturn>(user);

            return Ok(userToReturn);
        }

    }
}
