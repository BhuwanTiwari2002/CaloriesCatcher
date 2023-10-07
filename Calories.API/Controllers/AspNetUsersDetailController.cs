using AutoMapper;
using Azure;
using Calories.API.Data;
using Calories.API.Models;
using Calories.API.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Calories.API.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class AspNetUsersDetailController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public AspNetUsersDetailController(AppDbContext appDbContext, IMapper mapper) {
            _db = appDbContext;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ResponseDto> GetByUser(string userId)
        {
            try
            {
                List<AspNetUsersDetail> obj = await _db.AspNetUsersDetails.Where(x => x.UserId == userId).ToListAsync();
                _response.Result = _mapper.Map<List<AspNetUsersDetailDto>>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPost]
        public ResponseDto Post([FromBody] AspNetUsersDetail aspNetUsersDetail)
        {
            try
            {
                var obj = _mapper.Map<AspNetUsersDetail>(aspNetUsersDetail);
                _db.AspNetUsersDetails.Add(obj);
                _db.SaveChanges();
                _response.Result = _mapper.Map<AspNetUsersDetail>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
