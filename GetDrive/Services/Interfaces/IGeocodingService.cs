using System.Runtime.CompilerServices;

namespace GetDrive.Services
{
    public interface IGeocodingService
    {
        Task<Location> GetLocationAsync(string address);
    }
}
