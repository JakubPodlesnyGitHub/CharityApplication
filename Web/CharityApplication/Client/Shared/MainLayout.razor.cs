namespace CharityApplication.Client.Shared
{
    public partial class MainLayout
    {
        protected override async Task OnInitializedAsync()
        {
            HttpInterceptorService.RegisterEvent();
            await base.OnInitializedAsync();
        }
    }
}