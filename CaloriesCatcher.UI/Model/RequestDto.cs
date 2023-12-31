﻿using Microsoft.AspNetCore.Components;
using static KitchenComfort.Web.Models.Utility.StaticType;

namespace KitchenComfort.Web.Models
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET; 
        public string Url { get; set; } = default!;
        public object Data { get; set; } = default!;
        public string AccessToken { get; set; } = default!;
        
    }
}
