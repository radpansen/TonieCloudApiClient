using GraphQL.Common.Response;
using System;
using System.Linq;
using System.Net.Http;

namespace TonieCloudApiClient.Extensions
{
    public static class ResponseExtensions
    {
        public static GraphQLResponse ThrowIfNotSuccessful(this GraphQLResponse response)
        {
            if (response.Errors != null && response.Errors.Any())
            {
                throw new Exception(response.Errors.First().ToString());
            }

            return response;
        }

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
