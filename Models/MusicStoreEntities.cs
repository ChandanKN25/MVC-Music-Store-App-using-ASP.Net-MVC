using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class MusicStoreEntities
    {
        public DbSet<AlbumModel> Albums { get; set; }
        public DbSet<GenreModel> Genres { get; set; }
    }
}