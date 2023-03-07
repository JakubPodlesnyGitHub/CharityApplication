using CharityApplication.Client.Connection.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CharityApplication.Client.Pages.PageRedirection
{
    public partial class GoogleRedirectionComponent
    {
        [Inject]
        public IAuthRepository AuthRepository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var responseDTO = await AuthRepository.GoogleCallback();
            if (string.IsNullOrEmpty(responseDTO.Detail))
            {
                NavigationManager.NavigateTo("/mainpage");
                SnackBar.Add("Authenticated by Google", Severity.Success);
            }
            else
            {
                SnackBar.Add(responseDTO.Detail, Severity.Error);
                NavigationManager.NavigateTo("/login");
            }
            await base.OnInitializedAsync();
        }
    }
}