using Microsoft.EntityFrameworkCore;
using RefreshToken_Hw8.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefreshToken_Hw8.Context
{
    public class HotelApiDbContext : DbContext
    {
        public HotelApiDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<RoomEntity> Rooms { get; set; }
        public DbSet<UserEntity> Users { get; set; }

    }
}
