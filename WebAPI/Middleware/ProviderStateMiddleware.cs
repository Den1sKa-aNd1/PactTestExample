using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace WebAPI.Middleware
{
    public class ProviderStateMiddleware
    {
        private const string ConsumerName = "Values";
        private readonly RequestDelegate _next;
        private readonly IDictionary<string, Action> _providerStates;

        public ProviderStateMiddleware(RequestDelegate next)
        {
            _next = next;
            _providerStates = new Dictionary<string, Action>
            {
                {
                    "There is no data",
                    () => { }
                },
                {
                    "Some value",
                    () => { }
                }
            };
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value == "/provider-states")
            {
                this.HandleProviderStatesRequest(context);
            }
            else
            {
                await this._next(context);
            }
        }

        private void HandleProviderStatesRequest(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            if (context.Request.Method.ToUpper() == HttpMethod.Post.ToString().ToUpper() &&
                context.Request.Body != null)
            {
                try
                {
                    string jsonRequestBody = String.Empty;
                    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                    {
                        jsonRequestBody = reader.ReadToEndAsync().Result;
                    }

                    var providerState = JsonSerializer.Deserialize<ProviderState>(jsonRequestBody);

                    //A null or empty provider state key must be handled
                    if (providerState != null && !String.IsNullOrEmpty(providerState.State) &&
                        providerState.Consumer == ConsumerName)
                    {
                        _providerStates[providerState.State].Invoke();
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
