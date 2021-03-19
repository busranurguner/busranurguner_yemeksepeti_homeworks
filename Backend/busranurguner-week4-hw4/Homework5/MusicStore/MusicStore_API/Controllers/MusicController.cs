using Microsoft.AspNetCore.Mvc;
using MusicStore_Data;
using MusicStore_Data.ArtistFolder;
using MusicStore_Data.MusicFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Music>> Get()
        {
            return await _unitOfWork.Music.GetAll();
        }


     
        [HttpGet("{id}")]
        public async Task<Music> Get(int id)
        {
            return await _unitOfWork.Music.Get(id);
        }

        [HttpPost]
        public IActionResult Post()
        {
            var music = new Music
            {
                Id = 1,
                Name = "Blinding lights",
                Artist = "The weeknd",

            };

            var artist = new Artist
            {
                ArtistId = 1,
                Name = "The weeknd",

            };

            _unitOfWork.Music.Add(music);
            _unitOfWork.Artist.Add(artist);
            _unitOfWork.Complete();
            return Ok();
        }

    
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
