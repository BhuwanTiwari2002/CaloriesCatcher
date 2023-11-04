using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Calories.API.Models.Dto;
using Calories.API.Data;

namespace Recipe.API.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipeAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public RecipeAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Calories.API.Models.Recipe> objList = _db.Recipes.ToList();
                _response.Result = _mapper.Map<IEnumerable<RecipeDto>>(objList);
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
        public ResponseDto GetByUser(string userId)
        {
            try
            {
                List<Calories.API.Models.Recipe> obj = _db.Recipes.Where(x => x.UserId == userId).ToList();
                _response.Result = _mapper.Map<List<RecipeDto>>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPost]
        public ResponseDto Post([FromBody] RecipeDto recipeDto)
        {
            try
            {
                var obj = _mapper.Map<Calories.API.Models.Recipe> (recipeDto);
                _db.Recipes.Add(obj);
                _db.SaveChanges();
                _response.Result = _mapper.Map<RecipeDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] RecipeDto caloriesDto)
        {
            try
            {
                var obj = _mapper.Map<Calories.API.Models.Recipe>(caloriesDto);
                _db.Recipes.Update(obj);
                _db.SaveChanges();
                _response.Result = _mapper.Map<RecipeDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        public ResponseDto Delete(int userId)
        {
            try
            {
                var obj = _db.Recipes.Where(c => c.RecipeId == userId).FirstOrDefault();
                _db.Recipes.Remove(obj);
                _db.SaveChanges();
                _response.Result = $"The {userId} has been deleted";
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
