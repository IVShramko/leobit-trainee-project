﻿using System;

namespace Dating.Logic.Resourses.AlbumManager
{
    public interface IAlbumManager
    {
        void CreateAlbum(Guid userId, string name);

        void UpdateAlbum(Guid userId, string oldName, string newName);

        void DeleteAlbum(Guid userId, string name);
    }
}
