using CaloriesCatcher.UI.Model;
using CaloriesCatcher.UI.Service.IService;
using KitchenComfort.Web.Models;
using KitchenComfort.Web.Models.Utility;

namespace CaloriesCatcher.UI.Service;

public class UserDetailService : IUserDetailService
{
    private readonly IBaseService _baseService;
    public UserDetailService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> CreateUserDetailsAsync(UserDetail userDetail)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticType.ApiType.POST,
            Data = userDetail,
            Url = StaticType.CaloriesAPIBase + $"api/profile/"
        }).ConfigureAwait(false);
    }

    public async Task<ResponseDto> GetByUser(string userId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticType.ApiType.GET,
            Url = StaticType.CaloriesAPIBase + $"api/profile/{userId}"
        }).ConfigureAwait(false);

    }

    public async Task<ResponseDto> UpdateUserDetailsAsync(UserDetail userDetail)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticType.ApiType.PUT,
            Data = userDetail,
            Url = StaticType.CaloriesAPIBase + $"api/profile/"
        }).ConfigureAwait(false);
    }

}
