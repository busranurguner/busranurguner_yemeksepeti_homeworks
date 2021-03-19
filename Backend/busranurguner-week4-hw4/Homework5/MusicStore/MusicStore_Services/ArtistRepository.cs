using MusicStore_Data;
using MusicStore_Data.ArtistFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore_Services
{
    class CatalogueRepository : GenericRepository<Artist>, IArtistRepository
    {
        public CatalogueRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
