using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AiContigo.XaiClient.Builders
{
    public class ListLanguageModelsRequestBuilder
    {
        private readonly XaiClient _client;
        private readonly string _endpoint;
        private readonly ILogger _logger;

        public ListLanguageModelsRequestBuilder(XaiClient client, string endpoint, ILogger logger)
        {
            _client = client;
            _endpoint = endpoint;
            _logger = logger;
        }

        // Execute the request using GET
        public async Task<Models.LanguageModelResponse> ExecuteAsync()
        {
            _logger.LogInformation("Executing ListLanguageModels request to {Endpoint}", _endpoint);

            try
            {
                var response = await _client.GetHttpClient().GetAsync(_endpoint);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<Models.LanguageModelResponse>();
                _logger.LogInformation("Received response: {@Response}", result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to execute ListLanguageModels request");
                throw;
            }
        }
    }
}
