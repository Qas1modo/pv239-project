using Microsoft.Maui.Controls.Maps;
using System.Threading.Tasks;
using Microsoft.Maui.Devices;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Maps;


// https://www.banditoth.net/2023/06/08/net-maui-android-bug-maps-movetoregion-method-not-updating-visible-region/
namespace GetDrive.Controls
{
    public class CustomMap : Microsoft.Maui.Controls.Maps.Map
    {
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsVisible))
            {
                if (IsVisible)
                {
                    Task.Run(async () =>
                    {
                        // Introduce a small delay before invoking MoveToRegion
                        await Task.Delay(1500);

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            // Assuming this is an example location, replace with actual coordinates
                            MoveToRegion(MapSpan.FromCenterAndRadius(new Location(0, 0), Distance.FromKilometers(1)));
                        });
                    });
                }
            }
        }
    }
}