using AutoMapper;
using Azure;
using Calories.API.Data;
using Calories.API.Models;
using Calories.API.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Calories.API.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class AspNetUsersDetailController : Controller
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        private readonly ILogger<AspNetUsersDetailController> _logger;

        public AspNetUsersDetailController(AppDbContext appDbContext, IMapper mapper, ILogger<AspNetUsersDetailController> logger)
        {
            _db = appDbContext;
            _mapper = mapper;
            _response = new ResponseDto();
            _logger = logger;
        }

        [HttpGet]
        [Route("{userId}")]
        public ResponseDto GetByUser(string userId)
        {
            try
            {
                _logger.LogInformation(message: $"Attempting to fetch profile for User: {userId}.");
                AspNetUsersDetail obj = _db.AspNetUsersDetails.FirstOrDefault(x => x.UserId == userId);
                _response.Result = _mapper.Map<AspNetUsersDetailDto>(obj);
                _logger.LogInformation(message: $"Successfully fetched profile for User:{userId}.");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogError(message: $"While getting User Details, there was an Error. {ex.Message}");

            }
            return _response;
        }
        [HttpPost]
        public ResponseDto Post([FromBody] AspNetUsersDetail aspNetUsersDetail)
        {
            try
            {
                _logger.LogInformation(message: $"Attempting to add new profile record.");
                var obj = _mapper.Map<AspNetUsersDetail>(aspNetUsersDetail);
                _db.AspNetUsersDetails.Add(obj);
                _db.SaveChanges();
                _response.Result = _mapper.Map<AspNetUsersDetail>(obj);
                _logger.LogInformation(message: $"New profile record added succesfully.");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogInformation(message: $"Error occurred while adding a new profile record. Error Message: {ex.Message}");
            }
            return _response;
        }
        [HttpPut]
        
        public ResponseDto Put([FromBody] AspNetUsersDetailDto aspNetUsersDetail)
        {
            try
            {
                _logger.LogInformation(message: $"Attempting to update profile.");
                var obj = _mapper.Map<AspNetUsersDetail>(aspNetUsersDetail);
                _db.Update(obj);
                _db.SaveChanges();
                _response.IsSuccess=true;
                _response.Message = $"Updated {aspNetUsersDetail.UserId}";
                _logger.LogInformation(message: $"Profile was updated successfully.");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogInformation(message: $"Error occurred while updating profile. Error Message: {ex.Message}");
            }
            return _response;
        }
    }
}
