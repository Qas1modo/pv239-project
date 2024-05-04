using System.Runtime.CompilerServices;

namespace GetDrive.Services
{
    public interface IGlobalExceptionService
    {
        void HandleException(Exception exception, [CallerMemberName] string? source = null);
    }
}
