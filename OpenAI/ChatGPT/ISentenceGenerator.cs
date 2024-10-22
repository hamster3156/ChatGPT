using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Hamster.OpenAI.ChatGPT
{
    public interface ISentenceGenerator
    {
        /// <summary>
        /// GPT�̎w��������������
        /// </summary>
        public void InitializePromptMessage(string inputMessage);

        /// <summary>
        /// �R���e���c�𑗐M���āA�ԓ���string�Ŏ擾����
        /// </summary>
        /// <param name="image">���M����摜</param>
        /// <param name="message">���M���郁�b�Z�[�W</param>
        /// <returns>GPT����Ԃ��ė����ԓ���string</returns>
        UniTask<string> SendContentAsync(Texture2D image, string message, CancellationToken ct);
    }
}