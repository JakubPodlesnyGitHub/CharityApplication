using Microsoft.AspNetCore.Components;
using System.Web;

namespace CharityApplication.Client.Shared.Modules
{
    public partial class GoogleMapComponent
    {
        [Parameter]
        public string OriginAddress { get; set; } = null!;

        [Parameter]
        public string? DestinationAddress { get; set; }

        private string GoogleMapURL { get; set; } = null!;
        private string TravelMode = "walking";
        private string Key = "AIzaSyCp4IFnESR8hfUFQbdHwDy-xU_n-dHa6qw";
        private int ZOOM = 11;

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(DestinationAddress))
            {
                GoogleMapURL = $"https://www.google.com/maps/embed/v1/directions?key={Key}&origin={HttpUtility.UrlEncode(OriginAddress)}&destination={HttpUtility.UrlEncode(DestinationAddress)}&mode={TravelMode}&zoom={ZOOM}&region=PL";
            }
            else
            {
                GoogleMapURL = $"https://www.google.com/maps/embed/v1/place?key={Key}&q={HttpUtility.UrlEncode(OriginAddress)}&zoom={ZOOM}&region=PL";
            }
            await base.OnInitializedAsync();
        }

        private void ChengedURL(string mode)
        {
            TravelMode = mode;
            GoogleMapURL = $"https://www.google.com/maps/embed/v1/directions?key={Key}&origin={HttpUtility.UrlEncode(OriginAddress)}&destination={HttpUtility.UrlEncode(DestinationAddress)}&mode={TravelMode}&zoom={ZOOM}&region=PL";
        }
    }
}