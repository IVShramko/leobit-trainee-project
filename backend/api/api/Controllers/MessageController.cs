using Dating.Logic.DTO;
using Dating.Logic.Facades.MessageFacade;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageFacade _messageFacade;

        public MessageController(IMessageFacade messageFacade)
        {
            _messageFacade = messageFacade;
        }

        [HttpGet]
        [Route("{chatId}")]
        public async Task<IActionResult> GetAllChatMessages(Guid chatId)
        {
            ICollection<ChatMessageDTO> messages = 
                await _messageFacade.GetAllChatMessagesAsync(chatId);

            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(ChatMessageCreateDTO newMessage) 
        {
            bool result = await _messageFacade.CreateMessageAsync(newMessage);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
