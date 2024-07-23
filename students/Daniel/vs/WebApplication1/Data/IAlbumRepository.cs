﻿using WebApplication1.Models;

namespace WebApplication1.Data
{
    public interface IAlbumRepository
    {
        public IEnumerable<Albums> GetAlbums();
        public Albums GetOneAlbum(int id);
        public bool SaveEdited(Albums album);
        public bool AddNewAlbum (Albums album);
        public bool DeleteAlbum (int id);
    }
}
