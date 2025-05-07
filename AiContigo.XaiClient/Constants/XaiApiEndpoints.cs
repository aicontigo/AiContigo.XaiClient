namespace AiContigo.XaiClient.Constants;

// Constants for xAI API endpoints
public static class XaiApiEndpoints
{
    public const string ChatCompletions = "/v1/chat/completions";

    // Messages (Anthropic compatible)
    public const string Messages = "/v1/messages";

    public const string ImageGenerations = "/v1/images/generations";

    public const string ApiKey = "/v1/api-key";

    public const string ListModels = "/v1/models";

    public const string GetModel = "/v1/models/{model_id}";

    public const string ListLanguageModels = "/v1/language-models";

    public const string GetLanguageModel = "/v1/language-models/{model_id}";

    public const string ListImageGenerationModels = "/v1/image-generation-models";

    public const string GetImageGenerationModel = "/v1/image-generation-models/{model_id}";

    public const string TokenizeText = "/v1/tokenize-text";

    public const string GetDeferredChatCompletions = "/v1/chat/deferred-completion/{request_id}";

    // Completions (legacy)
    public const string CompletionsLegacy = "/v1/completions";

    // Completions (Anthropic compatible - legacy)
    public const string CompletionsAnthropicLegacy = "/v1/complete";
}