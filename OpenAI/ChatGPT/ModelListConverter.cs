using System.Text;

namespace Hamster.OpenAI.ChatGPT
{
    sealed internal class ModelListConverter
    {
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