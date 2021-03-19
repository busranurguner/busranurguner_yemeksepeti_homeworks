using MusicStore_Data;
using MusicStore_Data.ArtistFolder;
using MusicStore_Data.MusicFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore_Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public IMusicRepository Music { get; }
        public IArtistRepository Artist { get; }
       

        public UnitOfWork(DatabaseContext DbContext,
            IMusicRepository musicsRepository,
            IArtistRepository artistRepository)
        {
            this._context = DbContext;

            this.Music = musicsRepository;
            this.Artist = artistRepository;
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}

