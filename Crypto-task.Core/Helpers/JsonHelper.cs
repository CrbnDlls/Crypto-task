using Newtonsoft.Json;
using System.Threading.Tasks;


namespace Crypto_task.Core.Helpers
{
    public static class JsonHelper
    {
        public static async Task<T> ToObjectAsync<T>(string value)
        {
            return await Task.Run<T>(() =>
            {
                return JsonConvert.DeserializeObject<T>(value);
            });
        }
    }
}
