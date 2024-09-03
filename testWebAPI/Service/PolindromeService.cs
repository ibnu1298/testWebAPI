using Microsoft.EntityFrameworkCore;
using testWebAPI.DTO;
using testWebAPI.Model;

namespace testWebAPI.Service
{
    public interface IPolindrome
    {
        Task<BaseResponse> CheckPolindrome(PolindromeDTO req);
        Task<BaseResponse> DeletePol(DeletePolindromeDTO req);
        Task<BaseResponse> Save(PolindromeDTO req);
        Task<List<CheckPolindromeDTO>> getAll();
    }
    public class PolindromeService : IPolindrome
    {
        private readonly DataContext _context;
        public PolindromeService(DataContext context) { _context = context; }
        public async Task<BaseResponse> CheckPolindrome(PolindromeDTO req)
        {
            BaseResponse result = new();
            try
            {
                int left = 0;
                int right = req.InputString.Length - 1;
                while (left < right) 
                {
                    if (req.InputString[left] != req.InputString[right])
                    {
                        result.IsSucceeded = false;
                        result.Message = "Bukan Palindrome";
                        return result;
                    }
                    left++;
                    right--;
                }
                result.IsSucceeded = true;
                result.Message = "Palindrome";
            }
            catch (Exception e)
            {

            }
            return result;
        }

        public async Task<BaseResponse> DeletePol(DeletePolindromeDTO req)
        {
            try
            {
                BaseResponse response = new();
                var result = _context.DataPolindromies.Where(x => x.ID == req.ID).FirstOrDefault();
                if (result != null) 
                {
                    _context.DataPolindromies.Remove(result);
                    await _context.SaveChangesAsync();
                    response.IsSucceeded = true;
                    response.Message = "Data Berhasil di Hapus";
                }
                else
                {
                    response.IsSucceeded = false;
                    response.Message = $"Data dengan ID {req.ID} tidak ditemukan";
                }
                return response;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<CheckPolindromeDTO>> getAll()
        {
            List<CheckPolindromeDTO> response = [];
            try
            {
                var result = _context.DataPolindromies.ToList();
                foreach (var item in result) 
                {
                    CheckPolindromeDTO data = new CheckPolindromeDTO();
                    data.ID = item.ID;
                    data.InputString = item.Data;
                    data.Hasil = item.Result ? "Polindrome" : "Bukan Polindrome";
                    response.Add(data);
                }
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BaseResponse> Save(PolindromeDTO req)
        {
            try
            {
                BaseResponse response = new();
                DataPolindrome input = new();
                var result = _context.DataPolindromies.Where(x => x.Data == req.InputString).FirstOrDefault();
                if (result != null)
                {
                    response.IsSucceeded = false;
                    response.Message = $"{req.InputString} sudah terdaftar";
                    return response;
                }                
                BaseResponse data = await CheckPolindrome(req);
                input.Result = data.IsSucceeded;
                input.Data = req.InputString;
                response.IsSucceeded = true;
                response.Message = "Berhasil Simpan Data";
                _context.DataPolindromies.Add(input);
                await _context.SaveChangesAsync();
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
