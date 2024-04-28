using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Api
{
    public partial class GetDriveClient
    {
        public async Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, StringBuilder urlBuilder, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }

        public async Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, string url, CancellationToken cancellationToken = default)
        {
            string token = await SecureStorage.GetAsync("Token") ?? "";
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task ProcessResponseAsync(HttpClient client, HttpResponseMessage request, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }

    }
}
