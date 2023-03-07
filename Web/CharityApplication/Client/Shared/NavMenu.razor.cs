using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.Json;

namespace CharityApplication.Client.Shared
{
    public partial class NavMenu
    {
        public BasicAccountDTO Account { get; set; } = null!;

        [Inject]
        public IAuthRepository AuthRepository { get; set; }

        private int IMAGE_HEIGHT = 50;
        private int IMAGE_WIDTH = 50;

        private MudTheme Theme = new();
        private bool IsDarkMode = false;
        private string ModeIcon = Icons.Material.Filled.DarkMode;

        protected override async Task OnInitializedAsync()
        {
            var contains = await LocalStorageService.ContainKeyAsync("loggedUser");
            if (contains)
            {
                var deserializedAccount = await LocalStorageService.GetItemAsync<string>("loggedUser");
                Account = JsonSerializer.Deserialize<BasicAccountDTO>(deserializedAccount,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            StateHasChanged();
            await base.OnInitializedAsync();
        }

        private async Task Logout()
        {
            await AuthRepository.Logout();
            NavigationManager.NavigateTo("/");
        }

        private void ChangeDisplayMode()
        {
            if (IsDarkMode)
            {
                IsDarkMode = false;
                ModeIcon = Icons.Material.Filled.DarkMode;
            }
            else
            {
                IsDarkMode = true;
                ModeIcon = Icons.Material.Filled.LightMode;
            }
        }
    }
}