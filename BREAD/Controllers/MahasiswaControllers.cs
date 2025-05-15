using Microsoft.AspNetCore.Mvc;
using Mahasiswa.Application.Services;
using Mahasiswa.Domain.Entities;
namespace BREAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MahasiswaControllers : ControllerBase
    {
        private readonly IMahasiswaServices _imahasiswaServices;

        public MahasiswaControllers (IMahasiswaServices mahasiswaServices)
        {
            _imahasiswaServices = mahasiswaServices;
        }

        [HttpGet]
        public async Task<IActionResult> ReadAllMahasiswa()
        {
            var mhs = await _imahasiswaServices.ReadMahasiswa();
            return Ok(mhs);
        }

        [HttpGet("{id}/{NIM}")]
        public async Task<IActionResult> BrowseByID (string NIM)
        {
            var mhs = await _imahasiswaServices.BrowseMahasiswaByNIM(NIM);
            if(mhs == null)
            {
                return NotFound();
            }
            return Ok(mhs);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> UpdateMahasiswa(int id, MahasiswaData mahasiswaData)
        {
            var mhs = await _imahasiswaServices.UpdateMahasiswaByID(id, mahasiswaData);
            if(mhs == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete ("{id}")] 
        public async Task<IActionResult> DeleteByID (int id)
        {
            var mhs = await _imahasiswaServices.DeleteMahasiswaByID(id);
            if(mhs == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> AddMahasiswa([FromBody] MahasiswaData mahasiswaData)
        {
            if (mahasiswaData == null)
            {
                return BadRequest();
            }

            var result = await _imahasiswaServices.AddMahasiswa(mahasiswaData);
            if (result == 0)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(BrowseByID), new { NIM = mahasiswaData.NIM }, mahasiswaData);
        }
    }
}
