using System.Text;

namespace Hamster.OpenAI.ChatGPT
{
    sealed internal class ModelListConverter
    {
        /// <summary>
        /// GPT�̃��f������ϊ�����
        /// </summary>
        /// <param name="modelList">���f���̃^�C�v</param>
        /// <returns>�ϊ����ꂽGPT�̃��f����</returns>
        public string GetConvertModel(ModelList modelList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder
                .Append(modelList.ToString())
                .Replace("_", "-")
                .Replace("3_5", "3.5");

            return stringBuilder.ToString();
        }
    }
}