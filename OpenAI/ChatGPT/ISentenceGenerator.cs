using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Hamster.OpenAI.ChatGPT
{
    public interface ISentenceGenerator
    {
        /// <summary>
        /// GPTの指示を初期化する
        /// </summary>
        public void InitializePromptMessage(string inputMessage);

        /// <summary>
        /// コンテンツを送信して、返答をstringで取得する
        /// </summary>
        /// <param name="image">送信する画像</param>
        /// <param name="message">送信するメッセージ</param>
        /// <returns>GPTから返って来た返答のstring</returns>
        UniTask<string> SendContentAsync(Texture2D image, string message, CancellationToken ct);
    }
}