using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fusillade;
using Refit;

namespace PostsApp.Services
{
    public class ServiceFactory<T>: IServiceFactory<T>
    {
        private readonly string _baseUrl;

        public ServiceFactory(string baseUrl)
        {
            _baseUrl = baseUrl;
            userInitiated = new Lazy<T>(() => BuildApi(NetCache.UserInitiated));
            speculative = new Lazy<T>(() => BuildApi(NetCache.Speculative));
            background = new Lazy<T>(() => BuildApi(NetCache.Background));
            offline = new Lazy<T>(() => BuildApi(NetCache.Offline));
        }

        protected T BuildApi(HttpMessageHandler messageHandler)
        {
            return RestService.For<T>(_baseUrl,new RefitSettings()
            {
              HttpMessageHandlerFactory  = ()=> messageHandler
            });
        }

        protected readonly Lazy<T> userInitiated;
        protected readonly Lazy<T> background;
        protected readonly Lazy<T> speculative;
        protected readonly Lazy<T> offline;

        public T UserInitiated => userInitiated.Value;
        public T Background => background.Value;
        public T Speculative => speculative.Value;
        public T Offline => offline.Value;
    }

    public class InMemoryCache : IRequestCache
    {
        private Dictionary<string,byte[]> _cache = new Dictionary<string, byte[]>();
        public async Task Save(HttpRequestMessage request, HttpResponseMessage response, string key, CancellationToken ct)
        {
            var data = await response.Content.ReadAsByteArrayAsync();
            _cache[key] = data;
        }

        public Task<byte[]> Fetch(HttpRequestMessage request, string key, CancellationToken ct)
        {
            return Task.FromResult(_cache[key]);
        }
    }
}
