using AutoMapper;
using Calories.API.Data;
using Calories.API.Models;
using Calories.API.Models.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace RecipeIngredients.API.Controllers
{
    [Route("api/recipeingredients")]
    [ApiController]
    public class RecipeIngredientsAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public RecipeIngredientsAPIController(AppDbContext db, IMapper mapper)
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
                IEnumerable<Calories.API.Models.RecipeIngredient> objList = _db.ReceipeIngredients.ToList();
                _response.Result = _mapper.Map<IEnumerable<RecipeIngredientDto>>(objList);
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
                List<Calories.API.Models.RecipeIngredient> obj = _db.ReceipeIngredients.Where(x => x.UserId == userId).ToList();
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
        public ResponseDto Post([FromBody] RecipeIngredientDto recipeIngredientDto)
        {
            try
            {
                var obj = _mapper.Map<Calories.API.Models.Calories>(recipeIngredientDto);
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

        [HttpPut]
        public ResponseDto Put([FromBody] RecipeIngredientDto recipeIngredientDto)
        {
            try
            {
                var obj = _mapper.Map<RecipeIngredient>(recipeIngredientDto);
                _db.ReceipeIngredients.Update(obj);
                _db.SaveChanges();
                _response.Result = _mapper.Map<RecipeIngredientDto>(obj);
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
                var obj = _db.ReceipeIngredients.Where(c => c.RecipeId == userId).FirstOrDefault();
                _db.ReceipeIngredients.Remove(obj);
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
