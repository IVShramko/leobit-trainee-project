using Dating.Logic.DTO;
using Dating.Logic.Facades.PhotoFacade;
using Dating.Logic.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            Guid profileId = _tokenManager.ReadProfileId();

            ICollection<PhotoMainDTO> photos = 
                await _photoFacade.GetAllPhotosAsync(profileId, albumId);

            return Ok(photos);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhotoAsync(PhotoCreateDTO photo)
        {
            Guid profileId = _tokenManager.ReadProfileId();

            bool isCreated = await _photoFacade.CreatePhotoAsync(profileId, photo);

            if (isCreated)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePhoto(Guid id)
        {
            Guid profileId = _tokenManager.ReadProfileId();

            bool isDeleted = _photoFacade.DeletePhoto(id, profileId);

            if (isDeleted)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
