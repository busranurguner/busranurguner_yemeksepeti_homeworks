using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MusicStore_Data.ArtistFolder;
using MusicStore_Data.MusicFolder;
namespace MusicStore_Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }

        public DbSet<Music> Musics { get; set; }
        public DbSet<Artist> Artists { get; set; }

    }
}
