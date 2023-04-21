using AutoMapper;
using DattingAppUpdate.Dtos;
using DattingAppUpdate.Entites;
using DattingAppUpdate.Extensions;
using DattingAppUpdate.Helpers;
using DattingAppUpdate.IService;
using DattingAppUpdate.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DattingAppUpdate.Controllers
{
    [Authorize]
    public class MessagesController : BaseApiController
    {
        private readonly IDatingRepository _userRepo;
        private readonly IMessageRepository _msRepo;
        private readonly IMapper _mapper;

        public MessagesController(IDatingRepository userRepo,
                                  IMessageRepository msRepo,
                                  IMapper mapper)
        {
            _userRepo = userRepo;
            _msRepo = msRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.GetUsername();

            if (username == createMessageDto.RecipientUsername.ToLower())
                return BadRequest("You cannot send messages to yourself");

            var sender = await _userRepo.GetUserToUpdateAsync(username);
            var recipient = await _userRepo.GetUserToUpdateAsync(createMessageDto.RecipientUsername);

            if (recipient == null) return NotFound();

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = username,
                RecipientUsername = recipient.UserName,
                Content = createMessageDto.Content
            };

            _msRepo.AddMessage(message);

            if (await _msRepo.SaveAllAsync()) return Ok(_mapper.Map<MessageDto>(message));

            return BadRequest("Failed to send message");

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery]
            MessageParams messageParams)
        {
            messageParams.Username = User.GetUsername();

            var messages = await _msRepo.GetMessagesForUser(messageParams);

            Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize,
                messages.TotalCount, messages.TotalPages);

            return messages;
        }

        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
        {
            var currentUsername = User.GetUsername();

            return Ok(await _msRepo.GetMessageThread(currentUsername, username));
        }
    }
}
