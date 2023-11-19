using AutoMapper;
using Calories.API.Data;
using Calories.API.Models.Dto;
using static Azure.Core.HttpHeader;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Calories.API.Models;

namespace RecipeIngredients.API.Controllers
{
    [Route("api/recipeingredients")]
    [ApiController]
    public class RecipeIngredientsAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        private readonly ILogger<RecipeIngredientsAPIController> _logger; 
        public RecipeIngredientsAPIController(AppDbContext db, IMapper mapper, ILogger<RecipeIngredientsAPIController> logger)
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
                _logger.LogInformation(message: $"Attempting to fetch all recipe ingredients.");
                IEnumerable<Calories.API.Models.RecipeIngredient> objList = _db.ReceipeIngredients.ToList();
                _response.Result = _mapper.Map<IEnumerable<RecipeIngredientDto>>(objList);
                _logger.LogInformation(message: $"Successfully fetched all recipe ingredients.");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogInformation(message: $"Error occurred while fetching all recipe ingredients. Error Message: {ex.Message}");
            }
            return _response;
        }
        
        [HttpPost]
        public ResponseDto Post([FromBody] RecipeIngredientDto recipeIngredientDto)
        {
            try
            {
                _logger.LogInformation(message: $"Attempting to add new recipe ingredient for RecipeIngredient: {recipeIngredientDto}.");
                var obj = _mapper.Map<Calories.API.Models.RecipeIngredient>(recipeIngredientDto);
                _db.ReceipeIngredients.Add(obj);
                _db.SaveChanges();
                _response.Result = _mapper.Map<RecipeIngredientDto>(obj);
                _logger.LogInformation(message: $"Successfully added new recipe ingredient for RecipeIngredient: {recipeIngredientDto}.");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogInformation(message: $"Error occurred while adding new recipe ingredient for " +
                    $"RecipieIngredient: {recipeIngredientDto}. Error Message: {ex.Message}");
            }
            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] RecipeIngredientDto recipeIngredientDto)
        {
            try
            {
                _logger.LogInformation(message: $"Attempting to update recipe ingredient for RecipeIngredient: {recipeIngredientDto}.");
                var obj = _mapper.Map<RecipeIngredient>(recipeIngredientDto);
                _db.ReceipeIngredients.Update(obj);
                _db.SaveChanges();
                _response.Result = _mapper.Map<RecipeIngredientDto>(obj);
                _logger.LogInformation(message: $"Successfully updated recipe ingredient for RecipeIngredient: {recipeIngredientDto}.");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogInformation(message: $"Error occurred while updating recipe ingredient for " +
                    $"RecipeIngredient: {recipeIngredientDto}. Error Message: {ex.Message}");
            }
            return _response;
        }

        [HttpDelete]
        public ResponseDto Delete(int userId)
        {
            try
            {
                _logger.LogInformation(message: $"Attemping to delete recipe ingredient for UserId: {userId}.");
                var obj = _db.ReceipeIngredients.Where(c => c.RecipeId == userId).FirstOrDefault();
                _db.ReceipeIngredients.Remove(obj);
                _db.SaveChanges();
                _response.Result = $"The {userId} has been deleted";
                _logger.LogInformation(message: $"Successfully delete recipe ingredient for UserId: {userId}");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogInformation(message: $"Error occurred while deleting recipe ingredient for " +
                    $"UserId: {userId}. Error Message: {ex.Message}");
            }
            return _response;
        }
    }
}
