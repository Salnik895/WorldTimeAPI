using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WorldTimeAPI
{
    /// <summary>
    /// Client for <see href="http://worldtimeapi.org/api/">World Time API</see>
    /// </summary>
    public sealed partial class WorldTimeAPIClient : IDisposable
    {
        private bool disposed = false;
        private HttpClient httpClient = new HttpClient();

        public bool IsDisposed => disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                httpClient.Dispose();
            }

            disposed = true;
        }

        private async Task<WorldTime> GetTimeBaseAsync(string method, object path)
        {
            string response = await CallAsync(method, path);
            return new WorldTime(response);
        }

        private async Task<List<TimeZone>> GetTimeZonesBaseAsync(string area = null)
        {
            string response = await CallAsync("timezone", area);
            return JsonSerializer.Deserialize<string[]>(response).Select(x => TimeZone.Parse(x)).ToList();
        }

        private async Task<string> CallAsync(params object[] segments)
        {
            return await httpClient.GetStringAsync("http://worldtimeapi.org/api/" + string.Join("/", segments));
        }
    }
}