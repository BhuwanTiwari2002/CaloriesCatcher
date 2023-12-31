﻿using AutoMapper;
using Calories.API.Data;
using Calories.API.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Calories.API.Controllers
{
    [Route("api/calories")]
    [ApiController]
    public class CaloriesAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ILogger<CaloriesAPIController> _logger;
        private readonly IMapper _mapper;
        private ResponseDto _response;

        public CaloriesAPIController(AppDbContext db, IMapper mapper, ILogger<CaloriesAPIController> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
            _response = new ResponseDto();
        }

        [HttpGet]
        public ActionResult<ResponseDto> Get()
        {
            try
            {
                _logger.LogInformation("Fetching all calories data.");
                IEnumerable<Models.Calories> objList = _db.Calories.ToList();
                _response.Result = _mapper.Map<IEnumerable<CaloriesDto>>(objList);
                _logger.LogInformation("Successfully fetched all calories data.");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogError(ex, "Error occurred while fetching all calories data.");
            }
            return Ok(_response);
        }

        [HttpGet("{userId}")]
        public ActionResult<ResponseDto> GetByUser(string userId)
        {
            try
            {
                _logger.LogInformation("Fetching calories data for user: {UserId}", userId);
                List<Models.Calories> obj = _db.Calories.Where(x => x.UserId == userId).ToList();
                _response.Result = _mapper.Map<List<CaloriesDto>>(obj);
                _logger.LogInformation("Successfully fetched calories data for user: {UserId}", userId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogError(ex, "Error occurred while fetching calories data for user: {UserId}", userId);
            }
            return Ok(_response);
        }

        [HttpGet("{userId}/{dateFilter:int}")]
        public ActionResult<ResponseDto> GetByUser(string userId, int dateFilter)
        {
            try
            {
                _logger.LogInformation("Fetching calories data for user: {UserId} with date filter: {DateFilter}", userId, dateFilter);

                // Set the end date to the end of today to include any entries made today.
                DateTime endDate = DateTime.Today.AddDays(1).AddTicks(-1);
                DateTime startDate = DateTime.Today;

                switch (dateFilter)
                {
                    case 0: // Daily
                        break;
                    case 1: // Weekly
                        startDate = startDate.AddDays(-7);
                        break;
                    case 2: // Monthly
                        startDate = startDate.AddDays(-30);
                        break;
                    default:
                        throw new ArgumentException("Invalid date filter");
                }

                var results = _db.Calories.Where(c => c.UserId == userId && c.Date >= startDate && c.Date < endDate).ToList();

                if (!results.Any())
                {
                    _logger.LogInformation("No calorie data found for user: {UserId} with date filter: {DateFilter}", userId, dateFilter);
                }

                _response.Result = _mapper.Map<List<CaloriesDto>>(results);
                _logger.LogInformation("Successfully fetched calories data for user: {UserId} with date filter: {DateFilter}. Found {Count} records.", userId, dateFilter, results.Count);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogError(ex, "Error occurred while fetching calories data for user: {UserId} with date filter: {DateFilter}", userId, dateFilter);
            }
            return Ok(_response);
        }


        [HttpPost]
        public ActionResult<ResponseDto> Post([FromBody] CaloriesDto caloriesDto)
        {
            try
            {
                _logger.LogInformation("Attempting to add new calorie record.");
                var obj = _mapper.Map<Models.Calories>(caloriesDto);
                _db.Calories.Add(obj);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CaloriesDto>(obj);
                _logger.LogInformation("New calorie record added successfully.");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogError(ex, "Error occurred while adding new calorie record.");
            }
            return Ok(_response);
        }

        [HttpPut]
        public ActionResult<ResponseDto> Put([FromBody] CaloriesDto caloriesDto)
        {
            try
            {
                _logger.LogInformation("Attempting to update calorie record.");
                var obj = _mapper.Map<Models.Calories>(caloriesDto);
                _db.Calories.Update(obj);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CaloriesDto>(obj);
                _logger.LogInformation("Calorie record updated successfully.");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogError(ex, "Error occurred while updating calorie record.");
            }
            return Ok(_response);
        }

        [HttpDelete("Delete/{userId:int}")]
        public ActionResult DeleteCalorieByUser(int userId)
        {
            try
            {
                _logger.LogInformation("Attempting to delete calorie record for user with ID: {UserId}", userId);
                var obj = _db.Calories.FirstOrDefault(c => c.Id == userId);
                if (obj == null)
                {
                    _logger.LogWarning("Calorie record not found for user with ID: {UserId}", userId);
                    return NotFound();
                }
                _db.Calories.Remove(obj);
                _db.SaveChanges();
                _logger.LogInformation("Calorie record for user with ID: {UserId} deleted successfully", userId);
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _logger.LogError(ex, "Error occurred while deleting calorie record for user with ID: {UserId}", userId);
                return StatusCode(500, "Internal Server Error");
            }
            return Ok(_response);
        }
    }
}
