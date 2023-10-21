using CaloriesCatcher.UI.Model;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace CaloriesCatcher.UI.Service
{
    public class StripePaymentsService
    {
        private readonly StripeSettings _stripeSettings;
        public StripePaymentsService(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }
        public async Task<Customer> CreateCustomerAsync(string email)
        {
            var options = new CustomerCreateOptions
            {
                Email = email,
            };
            var service = new CustomerService();
            return await service.CreateAsync(options);
        }

        public async Task<Stripe.Subscription> CreateSubscriptionAsync(string customerId, string priceId)
        {
            try
            {
                var options = new SubscriptionCreateOptions
                {
                    Customer = customerId,
                    Items = new List<SubscriptionItemOptions>
            {
                new SubscriptionItemOptions
                {
                    Price = priceId,
                    Quantity = 1,
                },
            },
                };
                var service = new SubscriptionService();
                return await service.CreateAsync(options);
            }
            catch (StripeException ex)
            {
                throw ex;
            }
        }
        public async Task<string> CreateCheckoutSessionAsync(string priceId)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = priceId,
                        Quantity = 1,
                    },
                },
                Mode = "subscription",
                SuccessUrl = "https://localhost:7024/subscription?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://localhost:7024/subscription/cancel",
            };
            var service = new SessionService();
            var session = await service.CreateAsync(options);
            return session.Id;
        }

    }
}
