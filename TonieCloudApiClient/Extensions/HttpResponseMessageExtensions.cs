using System;
using System.Net.Http;

namespace TonieCloudApiClient.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static HttpResponseMessage ThrowIfNotSuccessful(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }

            return response;
        }
    }
}
