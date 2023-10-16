using KitchenComfort.Web.Models;

namespace CaloriesCatcher.UI.Model;

public class PayPalRequestDto: RequestDto
{
    public string PaymentId { get; set; } = default!;

}