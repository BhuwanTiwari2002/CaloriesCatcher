using AutoMapper;
using Azure;
using Calories.API.Data;
using Calories.API.Models;
using Calories.API.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;

namespace Calories.API.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class AspNetUsersDetailController : Controller
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
        public ResponseDto GetByUser(string userId)
        {
            try
            {
                AspNetUsersDetail obj = _db.AspNetUsersDetails.FirstOrDefault(x => x.UserId == userId);
                _response.Result = _mapper.Map<AspNetUsersDetailDto>(obj);
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
        [HttpPut]
        
        public ResponseDto Put([FromBody] AspNetUsersDetailDto aspNetUsersDetail)
        {
            try
            {
                //var user = _db.AspNetUsersDetails.FirstOrDefault(u => u.UserId == aspNetUsersDetail.UserId);
                var obj = _mapper.Map<AspNetUsersDetail>(aspNetUsersDetail);
                _db.Update(obj);
                _db.SaveChanges();
                _response.IsSuccess=true;
                _response.Message = $"Updated {aspNetUsersDetail.UserId}";
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
