using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AiContigo.XaiClient.Builders
{
    public class XaiRequestBuilder
    {
        private readonly XaiApiClient _client;
        private readonly string _endpoint;
        private readonly string _model;
        private readonly string _prompt;
        private readonly ILogger _logger;

        public XaiRequestBuilder(XaiApiClient client, string endpoint, ILogger logger)
            : this(client, endpoint, client.DefaultModel, null, logger)
        {
        }

        private XaiRequestBuilder(XaiApiClient client, string endpoint, string model, string prompt, ILogger logger)
        {
            _client = client;
            _endpoint = endpoint;
            _model = model;
            _prompt = prompt;
            _logger = logger;
        }

        public XaiRequestBuilder WithModel(string model) => new XaiRequestBuilder(_client, _endpoint, model, _prompt, _logger);
        public XaiRequestBuilder WithPrompt(string prompt) => new XaiRequestBuilder(_client, _endpoint, _model, prompt, _logger);
        public string GetModel() => _model; // Для тестов

        public async Task<Models.ChatCompletionResponse> ExecuteAsync()
        {
            if (string.IsNullOrEmpty(_prompt))
                throw new InvalidOperationException("Prompt is required for ChatCompletions");

            _logger.LogInformation("Executing ChatCompletions request to {Endpoint} with model {Model} and prompt {Prompt}", _endpoint, _model, _prompt);

            var request = new Models.ChatCompletionRequest { Model = _model, Prompt = _prompt };
            try
            {
                var response = await _client.GetHttpClient().PostAsJsonAsync(_endpoint, request);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<Models.ChatCompletionResponse>();
                _logger.LogInformation("Received response: {@Response}", result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to execute ChatCompletions request");
                throw;
            }
        }
    }
}
