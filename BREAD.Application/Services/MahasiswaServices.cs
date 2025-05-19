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
            if (string.IsNullOrEmpty(NIM))
                throw new ArgumentException($"Silahkan Isi NIM");

            var data = await _imahasiswaRepository.BrowseMahasiswaByNIM(NIM);

            if (data == null)
                throw new FileNotFoundException($"Mahasiswa dengan NIM {NIM} tidak ditemukan");

            return data;
        }

        public async Task<int> DeleteMahasiswaByID(int id)
        {
            return await _imahasiswaRepository.DeleteMahasiswaByID(id);
        }

        public async Task<List<MahasiswaData>> ReadMahasiswa()
        {
            var data = await _imahasiswaRepository.ReadMahasiswa();
            if(data == null || !data.Any())
                throw new FileNotFoundException($"Tidak ada data mahasiswa");
            return data;
        }

        public async Task<int> UpdateMahasiswaByID(int id, MahasiswaData mahasiswaData)
        {
            return await _imahasiswaRepository.UpdateMahasiswaByID(id, mahasiswaData);
        }
        public async Task<int> AddMahasiswa(MahasiswaData mahasiswaData)
        {
            var data = await _imahasiswaRepository.AddMahasiswa(mahasiswaData);

            if (mahasiswaData is null ||
                string.IsNullOrEmpty(mahasiswaData.NIM) ||
                string.IsNullOrEmpty(mahasiswaData.Name))
                throw new ArgumentException("Semua data harus terisi NIM dan Nama");
            return data;
        }
    }
}
