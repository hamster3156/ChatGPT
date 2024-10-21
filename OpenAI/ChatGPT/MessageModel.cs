using Newtonsoft.Json;
using System;

namespace Hamster.OpenAI.ChatGPT
{
    [Serializable]
    sealed internal class MessageModel
    {
        [JsonProperty("role")]
        public string Role;

        [JsonProperty("content")]
        public string Content;
    }
}