using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PostsApp.Services
{
    public class TraceHandler : DelegatingHandler
    {
        private readonly Stopwatch _stopWatch;

        public TraceHandler() : base(new HttpClientHandler())
        {
            _stopWatch = new Stopwatch();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _stopWatch.Reset();
            _stopWatch.Start();
            Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            Debug.WriteLine($"{request.Method} - {request.RequestUri}");
            var resp = await base.SendAsync(request, cancellationToken);
            _stopWatch.Stop();
            Debug.WriteLine($"{resp.StatusCode} - {_stopWatch.ElapsedMilliseconds}ms ellapsed");
            Debug.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            
            return resp;

        }
    }
}