using System.Text;

namespace Hamster.OpenAI.ChatGPT
{
    sealed internal class ModelListConverter
    {
        /// <summary>
        /// GPTのモデル名を変換する
        /// </summary>
        /// <param name="modelList">モデルのタイプ</param>
        /// <returns>変換されたGPTのモデル名</returns>
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