using Dating.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.GalleryFacade
{
    public interface IGalleryFacade
    {
        public Task<ICollection<UserAlbumDTO>> GetAllAlbumsAsync(Guid userId);
    }
}
