using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akavache;
using Fusillade;

namespace PostsApp.Services
{
    public class AkavacheCache : IRequestCache
    {
        public async Task<byte[]> Fetch(HttpRequestMessage request, 
            string key, CancellationToken ct)
        {
            var resp = await BlobCache.LocalMachine.Get(key);
            return resp;
        }

        public async Task Save(HttpRequestMessage request, HttpResponseMessage response, string key, CancellationToken ct)
        {
            var responseBytes = await response.Content.ReadAsByteArrayAsync();
            await BlobCache.LocalMachine.Insert(key, responseBytes);
        }
    }
}
