using Dating.Logic.Facades.GalleryFacade;
using Dating.Logic.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryFacade _galleryFacade;

        public GalleryController(IGalleryFacade galleryFacade)
        {
            _galleryFacade = galleryFacade;
        }

        [HttpGet]
        public async Task<IActionResult> Albums(Guid userId)
        {
            var albums = await _galleryFacade.GetAllAlbumsAsync(userId);

            return Ok(albums);
        }

        [HttpGet]
        public async Task<IActionResult> Photos(Guid albumId)
        {

            return Ok();
        }

        [HttpPost]
        public IActionResult CreateAlbum()
        {
            return Ok();
        }
    }
}
