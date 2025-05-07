using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiContigo.XaiClient.Models
{
    public class LanguageModelResponse
    {
        public List<LanguageModel> Models { get; set; }

        // Nested class to represent each model
        public class LanguageModel
        {
            public string Id { get; set; }
            public string Fingerprint { get; set; }
            public long Created { get; set; } // UNIX timestamp in seconds
            public string Object { get; set; }
            public string OwnedBy { get; set; }
            public string Version { get; set; }
            public List<string> InputModalities { get; set; }
            public List<string> OutputModalities { get; set; }
            public long PromptTextTokenPrice { get; set; }
            public long CachedPromptTextTokenPrice { get; set; }
            public long PromptImageTokenPrice { get; set; }
            public long CompletionTextTokenPrice { get; set; }
            public List<string> Aliases { get; set; }
        }
    }
}
