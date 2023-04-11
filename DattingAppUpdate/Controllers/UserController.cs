using AutoMapper;
using DattingAppUpdate.Dtos;
using DattingAppUpdate.Entites;
using DattingAppUpdate.Errors;
using DattingAppUpdate.Extensions;
using DattingAppUpdate.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DattingAppUpdate.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UsersController(IDatingRepository repo, IMapper mapper, IPhotoService photoService)
        {
            _repo = repo;
            _mapper = mapper;
            _photoService = photoService;
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

        [HttpGet("{username}", Name = "GetUser")]
        public async Task<ActionResult<UserToReturn>> GetUserByUsername(string username)
        {
            var user = await _repo.GetUserByUsername(username);

            var userToReturn = _mapper.Map<UserToReturn>(user);

            return Ok(userToReturn);
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateUser(string username, UserToUpdateDto userForUpdateDto)
        {
            if (username != User.GetUsername())
                return Unauthorized(new ApiErrorResponse(403));
            var userFormRepo = await _repo.GetUserByUsername(username);

            _mapper.Map(userForUpdateDto, userFormRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating user ${username}$ failed on save");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoToReturnDto>> AddPhoto(IFormFile file)
        {
            var user = await _repo.GetUserByUsername(User.GetUsername());

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            if (await _repo.SaveAll())
            {
                return CreatedAtRoute("GetUser", new { username = user.UserName }, _mapper.Map<PhotoToReturnDto>(photo));
            }

            return BadRequest("Problem adding photo");
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await _repo.GetUserByUsername(User.GetUsername());

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo.IsMain) return BadRequest("This is already your main photo");

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null) currentMain.IsMain = false;
            photo.IsMain = true;

            if (await _repo.SaveAll()) return NoContent();

            return BadRequest("Failed to set main photo");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var user = await _repo.GetUserByUsername(User.GetUsername());

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("You cannot delete your main photo");

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            user.Photos.Remove(photo);

            if (await _repo.SaveAll()) return Ok();

            return BadRequest("Failed to delete the photo");
        }

    }
}
