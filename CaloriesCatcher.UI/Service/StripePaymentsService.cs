using CaloriesCatcher.UI.Model;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesCatcher.UI.Service
{
    public class StripePaymentsService
    {
        private readonly StripeSettings _stripeSettings;
        private readonly CustomerService _customerService;
        private readonly SubscriptionService _subscriptionService;
        private readonly SessionService _sessionService;

        public StripePaymentsService(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            _customerService = new CustomerService();
            _subscriptionService = new SubscriptionService();
            _sessionService = new SessionService();
        }
        

        public async Task<string> CreateCheckoutSessionAsync(string priceId)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = priceId,
                        Quantity = 1
                    }
                },
                Mode = "subscription",
                SuccessUrl = _stripeSettings.SuccessUrl + "?session_id={CHECKOUT_SESSION_ID}$" + priceId,
                CancelUrl = _stripeSettings.CancelUrl
            };
            var service = new SessionService();
            var session = await service.CreateAsync(options).ConfigureAwait(false);
            return session.Id;
        }

    }
}
