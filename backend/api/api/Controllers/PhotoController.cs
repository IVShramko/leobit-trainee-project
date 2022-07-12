using Dating.Logic.DTO;
using Dating.Logic.Facades.PhotoFacade;
using Dating.Logic.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Dating.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoFacade _photoFacade;
        private readonly ITokenManager _tokenManager;

        public PhotoController(IPhotoFacade photoFacade,
            ITokenManager tokenManager)
        {
            _photoFacade = photoFacade;
            _tokenManager = tokenManager;
        }

        [HttpGet("{albumId}")]
        public async Task<IActionResult> GetAllPhotos(Guid albumId)
        {
            Guid userId = _tokenManager.ReadProfileId();

            var photos = await _photoFacade.GetAllPhotosAsync(userId, albumId);

            return Ok(photos);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhotoAsync(PhotoCreateDTO photo)
        {
            Guid userId = _tokenManager.ReadProfileId();

            bool result = await _photoFacade.CreatePhotoAsync(userId, photo);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePhoto(Guid id)
        {
            Guid userId = _tokenManager.ReadProfileId();

            bool result = _photoFacade.DeletePhoto(id, userId);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
