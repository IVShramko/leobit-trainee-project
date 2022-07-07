using Dating.Logic.Facades.PhotoFacade;
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

        public PhotoController(IPhotoFacade photoFacade)
        {
            _photoFacade = photoFacade;
        }

        [HttpGet("{albumId}")]
        public async Task<IActionResult> GetAllPhotos(Guid albumId)
        {
            var photos = await _photoFacade.GetAllPhotosAsync(albumId);

            return Ok(photos);
        }
    }
}
