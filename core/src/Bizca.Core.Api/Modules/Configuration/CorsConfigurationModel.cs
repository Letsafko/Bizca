﻿namespace Bizca.Core.Api.Modules.Configuration
{
    public sealed class CorsConfigurationModel
    {
        public PolicyConfigurationModel DefaultApiPolicy { get; set; }
    }

    public class PolicyConfigurationModel
    {
        public string[] Origins { get; set; }
        public string[] Methods { get; set; }
        public string[] Headers { get; set; }
    }
}