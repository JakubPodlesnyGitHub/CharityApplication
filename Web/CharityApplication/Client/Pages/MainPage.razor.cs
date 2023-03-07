using Application.Dtos.BasicDtos.Responses;
using System.Text.Json;

namespace CharityApplication.Client.Pages
{
    public partial class MainPage
    {
        public BasicAccountDTO LoggedUser { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            if (await LocalStorageService.ContainKeyAsync("loggedUser"))
            {
                LoggedUser = JsonSerializer.Deserialize<BasicAccountDTO>(
                                    await LocalStorageService.GetItemAsync<string>("loggedUser"),
                                    new JsonSerializerOptions
                                    {
                                        PropertyNameCaseInsensitive = true
                                    });
            }
        }
    }
}