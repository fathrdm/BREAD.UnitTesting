using Mahasiswa.Domain.Entities;
using Mahasiswa.Domain.Interface;

namespace Mahasiswa.Application.Services
{
    public class MahasiswaServices : IMahasiswaServices
    {
        private readonly IMahasiswaRepository _imahasiswaRepository;
        public MahasiswaServices (IMahasiswaRepository imahasiswaRepository)
        {
            _imahasiswaRepository = imahasiswaRepository;
        }
        public async Task<MahasiswaData> BrowseMahasiswaByNIM(string NIM)
        {
            return await _imahasiswaRepository.BrowseMahasiswaByNIM(NIM);
        }

        public async Task<int> DeleteMahasiswaByID(int id)
        {
            return await _imahasiswaRepository.DeleteMahasiswaByID(id);
        }

        public async Task<List<MahasiswaData>> ReadMahasiswa()
        {
            return await _imahasiswaRepository.ReadMahasiswa();
        }

        public async Task<int> UpdateMahasiswaByID(int id, MahasiswaData mahasiswaData)
        {
            return await _imahasiswaRepository.UpdateMahasiswaByID(id, mahasiswaData);
        }
        public async Task<int> AddMahasiswa(MahasiswaData mahasiswaData)
        {
            return await _imahasiswaRepository.AddMahasiswa(mahasiswaData);
        }
    }
}
