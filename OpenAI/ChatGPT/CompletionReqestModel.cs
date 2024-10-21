using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Hamster.OpenAI.ChatGPT
{
    [Serializable]
    sealed internal class CompletionReqestModel
    {
        [JsonProperty("model")]
        public string Model;

        [JsonProperty("messages")]
        public List<MessageModel> Messages;

        [JsonProperty("max_tokens")]
        public int MaxTokens;
    }
}