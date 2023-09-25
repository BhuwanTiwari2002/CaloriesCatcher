﻿using KitchenComfort.Web.Models;

namespace CaloriesCatcher.UI.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto?> RegisterAsync(RegisterationRequestDto registerationRequestDto);
        Task<ResponseDto?> AssignRoleAsync(RegisterationRequestDto registerationRequestDto);
        public void StartGoogleLogin();
    }
}
