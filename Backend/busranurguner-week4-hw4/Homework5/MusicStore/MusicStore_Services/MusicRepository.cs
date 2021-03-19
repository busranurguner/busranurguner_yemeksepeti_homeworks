using MusicStore_Data;
using MusicStore_Data.MusicFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore_Services
{
    public class MusicRepository : GenericRepository<Music>, IMusicRepository
    {
        public MusicRepository(DatabaseContext context) : base(context)
        {

        }



    }
}
