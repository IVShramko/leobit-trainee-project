using Dating.Logic.DTO;
using Dating.Logic.Facades.AlbumFacade;
using Dating.Logic.Managers.TokenManager;
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
            Guid profileId = _tokenManager.ReadProfileId();

            ICollection<AlbumMainDTO> albums = 
                await _albumFacade.GetAllAlbumsAsync(profileId);

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
            Guid profileId = _tokenManager.ReadProfileId();

            bool isValid = _albumFacade.IsValidName(profileId, name);

            return Ok(isValid);
        }

        [HttpPost]
        public IActionResult Create(AlbumCreateDTO album)
        {
            Guid profileId = _tokenManager.ReadProfileId();

            bool isCreated = _albumFacade.CreateAlbum(profileId, album);

            if (isCreated)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(AlbumFullDTO album)
        {
            Guid profileId = _tokenManager.ReadProfileId();

            bool isUpdated = await _albumFacade.UpdateAlbumAsync(profileId, album);

            if (isUpdated)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Guid profileId = _tokenManager.ReadProfileId();

            bool isDeleted = await _albumFacade.DeleteAlbumAsync(profileId, id);

            if (isDeleted)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
