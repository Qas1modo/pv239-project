using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Devices.Sensors;

namespace GetDrive.Services
{
    public class NominatimGeocodingService : IGeocodingService
    {
        private readonly HttpClient _httpClient;

        public NominatimGeocodingService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Location> GetLocationAsync(string address)
        {
            var url = $"https://nominatim.openstreetmap.org/search?q={address}&format=json&addressdetails=1";
            var response = await _httpClient.GetStringAsync(url);

            using (JsonDocument document = JsonDocument.Parse(response))
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
                            return new Location(lat, lon);
                        }
                    }
                }
            }

            return null;
        }
    }
}