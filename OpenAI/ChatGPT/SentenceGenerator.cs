using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

namespace Hamster.OpenAI.ChatGPT
{
    sealed internal class SentenceGenerator : MonoBehaviour, ISentenceGenerator
    {
        [SerializeField, Header("OpenAIのAPIキー")]
        private string _apiKey;

        [SerializeField, TextArea(3, 10), Header("GPTの返答方法の指示を書く")]
        private string _promptMessage;

        [SerializeField, Header("チャット返答のモデル")]
        private ModelList _chatModel = ModelList.gpt_4o_mini;

        [SerializeField, Header("画像解析のモデル ※現状はGPT-4oが一番良い")]
        private ModelList _imageModel = ModelList.gpt_4o;

        [SerializeField, Header("チャットの最大トークン")]
        private int _maxChatToken = 300;

        [SerializeField, Header("画像解析の最大トークン")]
        private int _maxImageToken = 600;

        [SerializeField, Header("ログ出力フラグ")]
        private bool _isLogEnabled = true;

        [SerializeField]
        Texture2D image;

        private ModelListConverter _modelListConverter = new();
        private List<MessageModel> _messageLists = new();
        private int _usedTotalToken = 0;

        /// <summary>
        /// コンテンツを送信して、返答をstringで取得する
        /// </summary>
        public async UniTask<string> SendContentAsync(Texture2D image, string message, CancellationToken ct)
        {
            // メッセージリストにユーザーの入力を追加
            _messageLists.Add(new MessageModel
            {
                Role = "user",
                Content = message
            });

            // キーの設定や送信方法を設定する
            var headers = new Dictionary<string, string>
            {
                {
                    "Authorization", "Bearer " + _apiKey
                },

                {
                    "Content-type", "application/json"
                }
            };

            // 設定をJsonに変換
            string jsonOption = null;

            if (image == null)
            {
                jsonOption = JsonConvert.SerializeObject(CreateChatOptions(message));
            }
            else
            {
                jsonOption = JsonConvert.SerializeObject(CreateImageOptions(image, message));
            }

            // Webリクエストを作成
            using var reqest = new UnityWebRequest("https://api.openai.com/v1/chat/completions", "POST")
            {
                uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonOption)),
                downloadHandler = new DownloadHandlerBuffer(),
            };

            foreach (var header in headers)
            {
                reqest.SetRequestHeader(header.Key, header.Value);
            }

            // リクエストを送信
            await reqest.SendWebRequest().WithCancellation(ct);

            if (reqest.result == UnityWebRequest.Result.ConnectionError ||
                reqest.result == UnityWebRequest.Result.ProtocolError)
            {
                throw new Exception(reqest.error);
            }

            // 回答を取得
            var responseMessage = reqest.downloadHandler.text;

            // Jsonから回答を変換
            var responseObj = JsonConvert.DeserializeObject<ResponseModel>(responseMessage);

            // 使用したトークン数を加算
            _usedTotalToken += responseObj.UseUsage.TotalTokens;

            if (_isLogEnabled)
            {
                Debug.Log($"[自分]:{message}");
                Debug.Log($"[GPT]:{responseObj.Choices[0].Message.Content}");
                Debug.Log($"[今回の使用トークン数]:{responseObj.UseUsage.TotalTokens}");
                Debug.Log($"[使用したトークンの合計]:{_usedTotalToken}");
            }

            // メッセージリストに返答を追加
            _messageLists.Add(responseObj.Choices[0].Message);

            // 返答を返す
            return responseObj.Choices[0].Message.Content;
        }

        private async void Start()
        {
            InitializePromptMessage();

            await SendContentAsync(image, "これは何ですか", CancellationToken.None);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SendContentAsync(image, "これは何ですか", CancellationToken.None).Forget();
            }
        }

        /// <summary>
        /// GPTの指示を初期化する
        /// </summary>
        [Button("プロンプト初期化ボタン")]
        private void InitializePromptMessage()
        {
            // GPTの返答方法を設定する
            MessageModel messageModel = new()
            {
                Role = "system",
                Content = _promptMessage
            };

            if (_messageLists.Count > 0)
            {
                _messageLists.Clear();
            }

            _messageLists.Add(messageModel);

            if (_isLogEnabled)
            {
                Debug.Log($"[初期化されたプロンプト]:{_promptMessage}");
            }
        }

        /// <summary>
        /// チャットを送るオプション
        /// </summary>
        /// <param name="sendMessage">送るメッセージ</param>
        /// <returns>チャットを送るCompletionReqestModelを返す</returns>
        private CompletionReqestModel CreateChatOptions(string sendMessage)
        {
            // 応答するモデルの設定
            return new CompletionReqestModel
            {
                Model = _modelListConverter.GetConvertModel(_chatModel),
                Messages = _messageLists,
                MaxTokens = _maxChatToken,
            };
        }

        /// <summary>
        /// 画像を送るオプション
        /// </summary>
        /// <param name="sendImage">送る画像</param>
        /// <param name="sendMessage">送るメッセージ</param>
        /// <returns>画像解析とチャットを送るオプションを返す</returns>
        private object CreateImageOptions(Texture2D sendImage, string sendMessage)
        {
            string base64Image = ConvertTexture2DToBase64(sendImage);

            List<object> imageMessage = new();

            foreach (var message in _messageLists)
            {
                imageMessage.Add(new
                {
                    role = message.Role,
                    content = message.Content
                });
            }

            // 画像データを追加
            imageMessage.Add(new
            {
                role = "user",
                content = sendMessage
            });

            imageMessage.Add(new
            {
                role = "user",
                content = new object[]
                {
                    new
                    {
                        type = "text",
                        text = sendMessage
                    },

                    new
                    {
                        type = "image_url",
                        image_url = new
                        {
                            url = $"data:image/png;base64,{base64Image}"
                        }
                    }
                }
            });

            var options = new
            {
                model = _modelListConverter.GetConvertModel(_imageModel),
                messages = imageMessage,
                max_tokens = _maxImageToken,
            };

            return options;
        }

        /// <summary>
        /// Texture2DをBase64文字列に変換する
        /// </summary>
        /// <param name="texture">変換するTexture2D</param>
        /// <returns>Base64文字列</returns>
        private string ConvertTexture2DToBase64(Texture2D texture)
        {
            // Texture2Dをバイト配列に変換
            byte[] textureBytes = texture.EncodeToPNG();

            // バイト配列をBase64文字列に変換
            return Convert.ToBase64String(textureBytes);
        }
    }
}