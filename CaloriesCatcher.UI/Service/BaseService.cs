﻿using CaloriesCatcher.UI.Model;
using CaloriesCatcher.UI.Service.IService;
using KitchenComfort.Web.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using CaloriesCatcher.UI.Model.Edamam;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using static KitchenComfort.Web.Models.Utility.StaticType;

namespace CaloriesCatcher.UI.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<ApiTokenOptions> _token;
        public BaseService(IHttpClientFactory httpClientFactory, IOptions<ApiTokenOptions> token)
        {
            _httpClientFactory = httpClientFactory;
            _token = token;
        }
        
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("CaloriesCatcherAPI");
               // requestDto.AccessToken = _token.ToString();
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    /* Converts the requestDto.Data into Json */
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }
                HttpResponseMessage? apiResponse = null;
                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        {
                            message.Method = HttpMethod.Post; break;
                        }
                    case ApiType.PUT:
                        {
                            message.Method = HttpMethod.Put; break;
                        }
                    case ApiType.DELETE:
                        {
                            message.Method = HttpMethod.Delete; break;
                        }
                    default:
                        {
                            message.Method = HttpMethod.Get; break;
                        }
                }
                apiResponse = await client.SendAsync(message).ConfigureAwait(false);
                switch (apiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        {
                            return new() { IsSuccess = false, Message = "Not Found" };
                        }
                    case System.Net.HttpStatusCode.Forbidden:
                        {
                            return new() { IsSuccess = false, Message = "Access Denied" };
                        }
                    case System.Net.HttpStatusCode.Unauthorized:
                        {
                            return new() { IsSuccess = false, Message = "Unauthorized" };
                        }
                    case System.Net.HttpStatusCode.InternalServerError:
                        {
                            return new() { IsSuccess = false, Message = "InternalServerError" };
                        }
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        /* Converting the response json back into the Response Data Transfer Object */
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }

            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return dto;
            }
        }
        public async Task<Nutrition?> SendAsyncEdamam(RequestDto requestDto)
        {
            try
            {
                using HttpClient client = _httpClientFactory.CreateClient("CalorieCatcherAPI");
                // Create HttpRequestMessage
                var message = new HttpRequestMessage
                {
                    Headers = { { "Accept", "application/json" } },
                    RequestUri = new Uri(requestDto.Url),
                    Method = requestDto.ApiType switch
                    {
                        ApiType.POST => HttpMethod.Post,
                        ApiType.PUT => HttpMethod.Put,
                        ApiType.DELETE => HttpMethod.Delete,
                        _ => HttpMethod.Get
                    }
                };
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }
                var apiResponse = await client.SendAsync(message);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    Nutrition nutrition = JsonConvert.DeserializeObject<Nutrition>(apiContent);
                    nutrition.IsSuccess = true;
                    return nutrition;
                }
                // Handle error response
                var errorResponse = new Nutrition
                {
                    IsSuccess = false,
                    Message = apiResponse.StatusCode switch
                    {
                        HttpStatusCode.NotFound => "Not Found",
                        HttpStatusCode.Forbidden => "Access Denied",
                        HttpStatusCode.Unauthorized => "Unauthorized",
                        HttpStatusCode.InternalServerError => "InternalServerError",
                        _ => "Unknown Error"
                    }
                };
                return errorResponse;
            }
            catch (Exception ex)
            {
                return new Nutrition
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }
        public async Task<RecipeModelEdamam> SendAsyncRecipeEdamam(RequestDto requestDto)
        {
           try
            {
                using HttpClient client = _httpClientFactory.CreateClient("CalorieCatcherAPI");
                // Create HttpRequestMessage
                var message = new HttpRequestMessage
                {
                    Headers = { { "Accept", "application/json" } },
                    RequestUri = new Uri(requestDto.Url),
                    Method = requestDto.ApiType switch
                    {
                        ApiType.POST => HttpMethod.Post,
                        ApiType.PUT => HttpMethod.Put,
                        ApiType.DELETE => HttpMethod.Delete,
                        _ => HttpMethod.Get
                    }
                };

                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }
                
                var apiResponse = await client.SendAsync(message);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    RecipeModelEdamam recipe = JsonConvert.DeserializeObject<RecipeModelEdamam>(apiContent);
                    recipe.isSuccess = true;
                    return recipe;
                }
                
                return new RecipeModelEdamam()
                {
                    
                };
            }
            catch (Exception ex)
            {
                return new RecipeModelEdamam()
                {
                   isSuccess = false
                };
            } 
        }
    }
}
