using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Hamster.OpenAI.ChatGPT
{
    public interface ISentenceGenerator
    {
        UniTask<string> SendContentAsync(Texture2D image, string message, CancellationToken ct);
    }
}