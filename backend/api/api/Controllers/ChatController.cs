using Dating.Logic.DTO;
using Dating.Logic.Facades.ChatFacade;
using Dating.Logic.Managers.TokenManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ITokenManager _tokenManager;
        private readonly IChatFacade _chatFacade;

        public ChatController(ITokenManager tokenManager,
            IChatFacade chatFacade)
        {
            _tokenManager = tokenManager;
            _chatFacade = chatFacade;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserChatsAsync()
        {
            string id = _tokenManager.ReadAspNetUserId();

            ICollection<ChatShortDTO> chats = await _chatFacade.GetAllUserChatsAsync(id);

            return Ok(chats);
        }

        [HttpGet]
        [Route("{chatId}")]
        public async Task<IActionResult> GetChatByIdAsync(Guid chatId)
        {
            string senderId = _tokenManager.ReadAspNetUserId();

            ChatFullDTO chat = await _chatFacade.GetChatByIdAsync(chatId, senderId);

            return Ok(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChatAsync()
        {
            return Ok();
        }
    }
}
