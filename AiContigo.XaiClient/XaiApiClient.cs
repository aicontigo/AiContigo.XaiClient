using AiContigo.XaiClient.Constants;
using Microsoft.Extensions.Logging;

namespace AiContigo.XaiClient
{
    public class XaiApiClient
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly string _defaultModel;
        private readonly Http.IHttpClient _httpClient;
        private readonly ILogger _logger;

        public XaiApiClient(string apiKey, string baseUrl, string defaultModel, Http.IHttpClient httpClient, ILogger logger)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
            _defaultModel = defaultModel ?? throw new ArgumentNullException(nameof(defaultModel));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.AddDefaultHeader("Authorization", $"Bearer {_apiKey}");
        }

        public string DefaultModel => _defaultModel;

        // Entry point for Chat Completions
        public Builders.XaiRequestBuilder ChatCompletions()
        {
            _logger.LogInformation("Initiating ChatCompletions request");
            return new Builders.XaiRequestBuilder(this, "chat/completions", _logger);
        }

        // New entry point for List language models
        public Builders.ListLanguageModelsRequestBuilder ListLanguageModels()
        {
            _logger.LogInformation("Initiating ListLanguageModels request");
            return new Builders.ListLanguageModelsRequestBuilder(this, XaiApiEndpoints.ListLanguageModels, _logger);
        }

        internal Http.IHttpClient GetHttpClient() => _httpClient;
    }
}
