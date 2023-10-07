using AutoMapper;
using Calories.API.Data;
using Calories.API.Models.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Calories.API.Controllers
{
    [Route("api/calories")]
    [ApiController]
    public class CaloriesAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public CaloriesAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }
        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<Calories.API.Models.Calories> objList = await _db.Calories.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<CaloriesDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ResponseDto> GetByUser(string userId)
        {
            try
            {
                List<Calories.API.Models.Calories> obj = await _db.Calories.Where(x => x.UserId == userId).ToListAsync();
                _response.Result = _mapper.Map<List<CaloriesDto>>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPost]
        public ResponseDto Post([FromBody] CaloriesDto caloriesDto)
        {
            try
            {
                var obj = _mapper.Map< Calories.API.Models.Calories> (caloriesDto);
                _db.Calories.Add(obj);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CaloriesDto>(obj);
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
