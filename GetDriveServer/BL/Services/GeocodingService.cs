using BL.DTOs;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace GetDrive.Services
{
    public interface IGeocodingService
    {
        Task<LocationDTO?> GetLocationAsync(string address);
    }

    public class GeocodingService : IGeocodingService
    {
        private readonly HttpClient _httpClient;

        public GeocodingService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<LocationDTO?> GetLocationAsync(string address)
        {
            try
            {
                var url = $"https://nominatim.openstreetmap.org/search?q={address}&format=json&addressdetails=1";
                _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                var response = await _httpClient.GetAsync(url);

                using (JsonDocument document = JsonDocument.Parse(await response.Content.ReadAsStringAsync()))
                {
                    JsonElement root = document.RootElement;
                    if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
                    {
                        JsonElement firstResult = root[0];
                        if (firstResult.TryGetProperty("lat", out JsonElement latElement) &&
                            firstResult.TryGetProperty("lon", out JsonElement lonElement))
                        {
                            if (double.TryParse(latElement.GetString(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double lat) &&
                                double.TryParse(lonElement.GetString(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double lon))
                            {
                                return new LocationDTO(lat, lon);
                            }
                        }
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}