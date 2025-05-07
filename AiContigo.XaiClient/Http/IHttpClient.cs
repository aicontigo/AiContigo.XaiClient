

namespace AiContigo.XaiClient.Http
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value);
        Task<HttpResponseMessage> GetAsync(string requestUri);
        Uri BaseAddress { get; set; }
        void AddDefaultHeader(string name, string value);
    }
}
