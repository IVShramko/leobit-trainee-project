using Dating.Logic.DTO;
using Dating.Logic.Facades.PhotoFacade;
using Dating.Logic.Managers.TokenManager;
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

        [HttpGet]
        [Route("{photoId}")]
        public async Task<IActionResult> GetPhotoById(Guid photoId)
        {
            Guid profileId = _tokenManager.ReadProfileId();

            PhotoMainDTO photo = await _photoFacade.GetPhotoByIdAsync(profileId, photoId);

            return Ok(photo);
        }

        [HttpGet]
        [Route("all/{albumId}")]
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

        [HttpPost]
        [Route("check")]
        public async Task<IActionResult> IsValidNameAsync(Guid albumId, string name)
        {
            bool isValid = await _photoFacade.IsValidName(albumId, name);

            return Ok(isValid);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePhotoAsync(PhotoMainDTO photo)
        {
            Guid profileId = _tokenManager.ReadProfileId();

            bool isUpdated = await _photoFacade.UpdatePhotoAsync(profileId, photo);

            if (isUpdated)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("data")]
        public async Task<IActionResult> UpdatePhotoDataUrlAsync(PhotoMainDTO photo)
        {
            Guid profileId = _tokenManager.ReadProfileId();

            bool isUpdated = await _photoFacade.UpdatePhotoDataUrlAsync(profileId, photo);

            if (isUpdated)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhotoAsync(Guid id)
        {
            Guid profileId = _tokenManager.ReadProfileId();

            bool isDeleted = await _photoFacade.DeletePhotoAsync(id, profileId);

            if (isDeleted)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
