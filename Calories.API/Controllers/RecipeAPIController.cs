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
        private readonly ILogger<RecipeAPIController> _logger;
        public RecipeAPIController(AppDbContext db, IMapper mapper, ILogger<RecipeAPIController> logger)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
            _logger = logger;
        }
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                _logger.LogInformation(message: $"Attempting to fetch all recipes.");
                IEnumerable<Calories.API.Models.Recipe> objList = _db.Recipes.ToList();
                _response.Result = _mapper.Map<IEnumerable<RecipeDto>>(objList);
                _logger.LogInformation(message: $"Successfully fetched all recipes.");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogInformation(message: $"Error occurred while fetching all recipes. Error Message: {ex.Message}");
            }
            return _response;
        }

        [HttpGet]
        [Route("{userId}")]
        public ResponseDto GetByUser(string userId)
        {
            try
            {
                _logger.LogInformation(message: $"Attempting to fetch recipes for User: {userId}");
                List<Calories.API.Models.Recipe> obj = _db.Recipes.Where(x => x.UserId == userId).ToList();
                _response.Result = _mapper.Map<List<RecipeDto>>(obj);
                _logger.LogInformation(message: $"Successfully fetched all recipes for User: {userId}");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogInformation(message: $"Error occurred while fetching recipes for " +
                    $"User: {userId}. Error Message: {ex.Message}");
            }
            return _response;
        }
        [HttpPost]
        public ResponseDto Post([FromBody] RecipeDto recipeDto)
        {
            try
            {
                _logger.LogInformation(message: $"Attempting to add new recipe.");
                var obj = _mapper.Map<Calories.API.Models.Recipe> (recipeDto);
                _db.Recipes.Add(obj);
                _db.SaveChanges();
                _response.Result = _mapper.Map<RecipeDto>(obj);
                _logger.LogInformation(message: $"Seccessfully added new recipe record.");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogInformation(message: $"Error occurred while adding a new recipe record. Error Message: {ex.Message}");
            }
            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] RecipeDto caloriesDto)
        {
            try
            {
                _logger.LogInformation(message: $"Attempting to update recipe.");
                var obj = _mapper.Map<Calories.API.Models.Recipe>(caloriesDto);
                _db.Recipes.Update(obj);
                _db.SaveChanges();
                _response.Result = _mapper.Map<RecipeDto>(obj);
                _logger.LogInformation(message: $"Successfully updated recipe.");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogInformation(message: $"Error occurred while updating recipe. Error Message: {ex.Message}");
            }
            return _response;
        }

        [HttpDelete]
        public ResponseDto Delete(int userId)
        {
            try
            {
                _logger.LogInformation(message: $"Attempting to delete recipe for User: {userId}");
                var obj = _db.Recipes.Where(c => c.RecipeId == userId).FirstOrDefault();
                _db.Recipes.Remove(obj);
                _db.SaveChanges();
                _response.Result = $"The {userId} has been deleted";
                _logger.LogInformation(message: $"Successfully deleted recipe for User: {userId}");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogInformation(message: $"Error occurred while deleting recipe for " +
                    $"User: {userId}. Error Message: {ex.Message}");
            }
            return _response;
        }
    
    }
}
