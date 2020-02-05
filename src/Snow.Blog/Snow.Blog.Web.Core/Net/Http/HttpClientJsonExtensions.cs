using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Snow.Blog.Web.Core.Net.Http
{
    public static class HttpClientJsonExtensions
    {
        /// <summary>
        /// Sends a GET request to the specified URI, and parses the JSON response body
        /// to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="httpClient">The <see cref="HttpClient"/>.</param>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        public static async Task<T> GetJsonAsync<T>(this HttpClient httpClient, string requestUri)
        {
            var stringContent = await httpClient.GetStringAsync(requestUri);
            HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri);            
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<T>(stringContent);
            }
            // 这里如何解决返回服务器信息的问题
            return default;
        }
    }
}
