using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace ConfigCentral.WebApi.Specs
{
    public static class ContentExtensions
    {
        public static Task<dynamic> ReadAsJsonAsync(this HttpContent content)
        {
            return content.ReadAsJsonAsync<dynamic>();
        }

        public static Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            return content.ReadAsStringAsync()
                .ContinueWith(t => JsonConvert.DeserializeObject<T>(t.Result));
        }
    }
}