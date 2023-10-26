﻿namespace CaloriesCatcher.UI.Model;

public class StripeSettings
{
    public string PublishableKey { get; set; }
    public string SecretKey { get; set; }
    public string SuccessUrl { get; set; }
    public string CancelUrl { get; set; }

}