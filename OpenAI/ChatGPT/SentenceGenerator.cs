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
        [SerializeField, Header("OpenAI��API�L�[")]
        private string _apiKey;

        [SerializeField, TextArea(3, 10), Header("GPT�̕ԓ����@�̎w��������")]
        private string _promptMessage;

        [SerializeField, Header("�`���b�g�ԓ��̃��f��")]
        private ModelList _chatModel = ModelList.gpt_4o_mini;

        [SerializeField, Header("�摜��͂̃��f�� �������GPT-4o����ԗǂ�")]
        private ModelList _imageModel = ModelList.gpt_4o;

        [SerializeField, Header("�`���b�g�̍ő�g�[�N��")]
        private int _maxChatToken = 300;

        [SerializeField, Header("�摜��͂̍ő�g�[�N��")]
        private int _maxImageToken = 600;

        [SerializeField, Header("���O�o�̓t���O")]
        private bool _isLogEnabled = true;

        [SerializeField]
        Texture2D image;

        private ModelListConverter _modelListConverter = new();
        private List<MessageModel> _messageLists = new();
        private int _usedTotalToken = 0;

        /// <summary>
        /// �R���e���c�𑗐M���āA�ԓ���string�Ŏ擾����
        /// </summary>
        public async UniTask<string> SendContentAsync(Texture2D image, string message, CancellationToken ct)
        {
            // ���b�Z�[�W���X�g�Ƀ��[�U�[�̓��͂�ǉ�
            _messageLists.Add(new MessageModel
            {
                Role = "user",
                Content = message
            });

            // �L�[�̐ݒ�⑗�M���@��ݒ肷��
            var headers = new Dictionary<string, string>
            {
                {
                    "Authorization", "Bearer " + _apiKey
                },

                {
                    "Content-type", "application/json"
                }
            };

            // �ݒ��Json�ɕϊ�
            string jsonOption = null;

            if (image == null)
            {
                jsonOption = JsonConvert.SerializeObject(CreateChatOptions(message));
            }
            else
            {
                jsonOption = JsonConvert.SerializeObject(CreateImageOptions(image, message));
            }

            // Web���N�G�X�g���쐬
            using var reqest = new UnityWebRequest("https://api.openai.com/v1/chat/completions", "POST")
            {
                uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonOption)),
                downloadHandler = new DownloadHandlerBuffer(),
            };

            foreach (var header in headers)
            {
                reqest.SetRequestHeader(header.Key, header.Value);
            }

            // ���N�G�X�g�𑗐M
            await reqest.SendWebRequest().WithCancellation(ct);

            if (reqest.result == UnityWebRequest.Result.ConnectionError ||
                reqest.result == UnityWebRequest.Result.ProtocolError)
            {
                throw new Exception(reqest.error);
            }

            // �񓚂��擾
            var responseMessage = reqest.downloadHandler.text;

            // Json����񓚂�ϊ�
            var responseObj = JsonConvert.DeserializeObject<ResponseModel>(responseMessage);

            // �g�p�����g�[�N���������Z
            _usedTotalToken += responseObj.UseUsage.TotalTokens;

            if (_isLogEnabled)
            {
                Debug.Log($"[����]:{message}");
                Debug.Log($"[GPT]:{responseObj.Choices[0].Message.Content}");
                Debug.Log($"[����̎g�p�g�[�N����]:{responseObj.UseUsage.TotalTokens}");
                Debug.Log($"[�g�p�����g�[�N���̍��v]:{_usedTotalToken}");
            }

            // ���b�Z�[�W���X�g�ɕԓ���ǉ�
            _messageLists.Add(responseObj.Choices[0].Message);

            // �ԓ���Ԃ�
            return responseObj.Choices[0].Message.Content;
        }

        private async void Start()
        {
            InitializePromptMessage();

            await SendContentAsync(image, "����͉��ł���", CancellationToken.None);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SendContentAsync(image, "����͉��ł���", CancellationToken.None).Forget();
            }
        }

        /// <summary>
        /// GPT�̎w��������������
        /// </summary>
        [Button("�v�����v�g�������{�^��")]
        private void InitializePromptMessage()
        {
            // GPT�̕ԓ����@��ݒ肷��
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
                Debug.Log($"[���������ꂽ�v�����v�g]:{_promptMessage}");
            }
        }

        /// <summary>
        /// �`���b�g�𑗂�I�v�V����
        /// </summary>
        /// <param name="sendMessage">���郁�b�Z�[�W</param>
        /// <returns>�`���b�g�𑗂�CompletionReqestModel��Ԃ�</returns>
        private CompletionReqestModel CreateChatOptions(string sendMessage)
        {
            // �������郂�f���̐ݒ�
            return new CompletionReqestModel
            {
                Model = _modelListConverter.GetConvertModel(_chatModel),
                Messages = _messageLists,
                MaxTokens = _maxChatToken,
            };
        }

        /// <summary>
        /// �摜�𑗂�I�v�V����
        /// </summary>
        /// <param name="sendImage">����摜</param>
        /// <param name="sendMessage">���郁�b�Z�[�W</param>
        /// <returns>�摜��͂ƃ`���b�g�𑗂�I�v�V������Ԃ�</returns>
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

            // �摜�f�[�^��ǉ�
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
        /// Texture2D��Base64������ɕϊ�����
        /// </summary>
        /// <param name="texture">�ϊ�����Texture2D</param>
        /// <returns>Base64������</returns>
        private string ConvertTexture2DToBase64(Texture2D texture)
        {
            // Texture2D���o�C�g�z��ɕϊ�
            byte[] textureBytes = texture.EncodeToPNG();

            // �o�C�g�z���Base64������ɕϊ�
            return Convert.ToBase64String(textureBytes);
        }
    }
}