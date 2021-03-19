using MusicStore_Data.ArtistFolder;
using MusicStore_Data.MusicFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore_Data
{
    public interface IUnitOfWork : IDisposable
    {
        IMusicRepository Music { get; }
        IArtistRepository Artist { get; }
        int Complete();
    }
}
