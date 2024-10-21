using Newtonsoft.Json;
using System;

namespace Hamster.OpenAI.ChatGPT
{

    [Serializable]
    sealed internal class ResponseModel
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("@object")]
        public string @Object;

        [JsonProperty("created")]
        public int Created;

        [JsonProperty("choices")]
        public Choice[] Choices;

        [JsonProperty("usage")]
        public Usage UseUsage;

        [Serializable]
        sealed internal class Choice
        {
            [JsonProperty("index")]
            public int Index;

            [JsonProperty("message")]
            public MessageModel Message;

            [JsonProperty("finish_reason")]
            public string FinishReason;
        }

        [Serializable]
        sealed internal class Usage
        {
            [JsonProperty("prompt_tokens")]
            public int PromptTokens;

            [JsonProperty("completion_tokens")]
            public int CompletionTokens;

            [JsonProperty("total_tokens")]
            public int TotalTokens;
        }

        [Serializable]
        sealed internal class Message
        {
            [JsonProperty("content")]
            public string Content;
        }
    }
}