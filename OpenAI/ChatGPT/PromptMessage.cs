using UnityEngine;

namespace Hamster.OpenAI.ChatGPT
{
    [CreateAssetMenu(fileName = "PromptMessage", menuName = "Scriptable Objects/PromptMessage")]
    public class PromptMessage : ScriptableObject
    {
        [SerializeField, TextArea(3, 10), Header("GPT‚Ì•Ô“š•û–@‚ÌŽwŽ¦‚ð‘‚­")]
        private string _promptMessage;

        public string GetPromptMessage => _promptMessage;
    }
}
