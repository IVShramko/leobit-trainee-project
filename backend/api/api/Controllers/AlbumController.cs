using Dating.Logic.DTO;
using Dating.Logic.Facades.AlbumFacade;
using Dating.Logic.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumFacade _albumFacade;
        private readonly ITokenManager _tokenManager;

        public AlbumController(IAlbumFacade albumFacade, ITokenManager tokenManager)
        {
            _albumFacade = albumFacade;
            _tokenManager = tokenManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlbumsAsync()
        {
            Guid id = _tokenManager.ReadProfileId();

            ICollection<AlbumMainDTO> albums = await _albumFacade.GetAllAlbumsAsync(id);

            return Ok(albums);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbumByIdAsync(Guid id)
        {
            AlbumFullDTO album = await _albumFacade.GetAlbumByIdAsync(id);

            return Ok(album);
        }

        [HttpPost("check")]
        public IActionResult IsValidName(string name)
        {
            Guid id = _tokenManager.ReadProfileId();

            bool result = _albumFacade.IsValidName(id, name);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(AlbumCreateDTO album)
        {
            Guid id = _tokenManager.ReadProfileId();

            bool result = _albumFacade.CreateAlbum(id, album);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(AlbumFullDTO album)
        {
            Guid id = _tokenManager.ReadProfileId();

            bool result = await _albumFacade.UpdateAlbumAsync(id, album);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Guid userId = _tokenManager.ReadProfileId();

            bool result = await _albumFacade.DeleteAlbumAsync(userId, id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
