using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using testWebAPI.DTO;
using testWebAPI.Service;

namespace testWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalindromiesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IPolindrome _polindrome;
        public PalindromiesController(DataContext context, IPolindrome polindrome)
        {
            _context = context;
            _polindrome = polindrome;
        }
        [HttpGet]
        public async Task<ActionResult<List<CheckPolindromeDTO>>> GETALL()
        {
            try
            {
                List<CheckPolindromeDTO> result = await _polindrome.getAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(DeletePolindromeDTO req)
        {
            try
            {
                BaseResponse response = new();
                response = await _polindrome.DeletePol(req);
                return response.IsSucceeded ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost("CheckPolindrome")]
        public async Task<ActionResult> CheckPolindrome(PolindromeDTO req)
        {
            try
            {
                BaseResponse response = new();
                response = await _polindrome.CheckPolindrome(req);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost("Save")]
        public async Task<ActionResult> Save(PolindromeDTO req)
        {
            try
            {
                BaseResponse response = new();
                response = await _polindrome.Save(req);
                return response.IsSucceeded?Ok(response):BadRequest(response);
            }
            catch (Exception ex) 
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
