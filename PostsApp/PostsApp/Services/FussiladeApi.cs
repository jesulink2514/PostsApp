using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Fusillade;
using Refit;

namespace PostsApp.Services
{
    public class FussiladeApi<T> : IApi<T>
    {
        private readonly string _baseUrl;

        public FussiladeApi(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public T UserInitiated => ClientFor(NetCache.UserInitiated);

        public T Background => ClientFor(NetCache.Background);

        public T Speculative => ClientFor(NetCache.Speculative);

        private T ClientFor(HttpMessageHandler handler)
        {
            return RestService.For<T>(_baseUrl, new RefitSettings()
            {
                HttpMessageHandlerFactory = () => handler
            });
        }
    }
}
