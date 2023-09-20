using CaloriesCatcher.UI.Service.IService;
using KitchenComfort.Web.Models;
using Newtonsoft.Json;
using System.Text;
using static KitchenComfort.Web.Models.Utility.StaticType;

namespace CaloriesCatcher.UI.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("CaloriesCatcherAPI");
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
                apiResponse = await client.SendAsync(message);
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
            catch
            {
                var dto = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return dto;
            }
        }
    }
}
