using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiContigo.XaiClient.Configuration
{
    public class XaiClientOptions
    {
        public string ApiKey { get; set; }
        public string BaseUrl { get; set; }
        public string DefaultModel { get; set; }


        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(ApiKey))
                throw new InvalidOperationException("ApiKey must be specified in configuration.");
            if (string.IsNullOrWhiteSpace(BaseUrl))
                throw new InvalidOperationException("BaseUrl must be specified in configuration.");
            if (string.IsNullOrWhiteSpace(DefaultModel))
                throw new InvalidOperationException("DefaultModel must be specified in configuration.");
        }
    }
}
