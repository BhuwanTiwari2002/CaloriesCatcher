using AutoMapper;
using Calories.API.Models;
using Calories.API.Models.Dto;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Calories.API
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                /* Source -> Destination */
                config.CreateMap<CaloriesDto, Calories.API.Models.Calories>();
                config.CreateMap<Calories.API.Models.Calories, CaloriesDto>();
                config.CreateMap<AspNetUsersDetailDto, AspNetUsersDetail>();
                config.CreateMap<AspNetUsersDetail, AspNetUsersDetailDto>();
                config.CreateMap<RecipeIngredientDto, RecipeIngredient>();
                config.CreateMap<RecipeIngredient, RecipeIngredientDto>();
                config.CreateMap<RecipeDto, Recipe>();
                config.CreateMap<Recipe, RecipeDto>();
            });
            return mappingConfig;
        }
    }
}
