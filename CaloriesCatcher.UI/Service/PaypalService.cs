using CaloriesCatcher.UI.Model;
using KitchenComfort.Web.Models;
using KitchenComfort.Web.Models.Utility;
using Microsoft.Extensions.Options;

namespace CaloriesCatcher.UI.Service;

public class PayPalService
{
    private readonly BaseService _baseService;
    private readonly PayPalSettings _payPalSettings;

    public PayPalService(BaseService baseService, IOptions<PayPalSettings> payPalSettings)
    {
        _baseService = baseService;
        _payPalSettings = payPalSettings.Value;
    }

    public async Task<ResponseDto?> CreatePayment(PayPalRequestDto payPalPaymentRequestDto)
    {
        if (string.IsNullOrWhiteSpace(_payPalSettings.ClientId) ||
            string.IsNullOrWhiteSpace(_payPalSettings.ClientSecret) ||
            string.IsNullOrWhiteSpace(_payPalSettings.Environment))
        {
            return new ResponseDto { Message = "The Client is not in settings "};
        }

        var url = _payPalSettings.Environment == "sandbox" 
            ? "https://api.sandbox.paypal.com/v1/payments/payment" 
            : "https://api.paypal.com/v1/payments/payment";
        try
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticType.ApiType.POST,
                Data = payPalPaymentRequestDto,
                Url = url
            });
        }
        catch(Exception ex)
        {
            return new ResponseDto() { IsSuccess = false, Message = ex.ToString() };
        }
       
    }

    
}

