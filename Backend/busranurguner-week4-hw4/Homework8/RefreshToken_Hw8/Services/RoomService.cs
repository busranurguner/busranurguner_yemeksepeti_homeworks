using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RefreshToken_Hw8.Context;
using RefreshToken_Hw8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefreshToken_Hw8.Services
{
    public class RoomService : IRoomService
    {
        private readonly HotelApiDbContext _dbContext;
        private readonly IMapper _mapper;

        public RoomService(HotelApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<Room>> GetRoomsAsync()
        {
            var roomEntities = await _dbContext.Rooms.ToListAsync();
            var result = roomEntities.Select(room => _mapper.Map<Room>(room))
                                     .ToList();

            return result;
        }
        public async Task<Room> GetRoomAsync(Guid id)
        {
            var roomEntity = await _dbContext.Rooms.SingleOrDefaultAsync(room => room.Id == id);
            if (roomEntity == null)
                return null;

            return _mapper.Map<Room>(roomEntity);


        }


    }
}
