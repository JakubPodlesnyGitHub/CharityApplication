using Application.Dtos.ServiceDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Model.Auth;
using CharityApplication.Client.Validators.Auth;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CharityApplication.Client.Pages.Auth
{
    public partial class CompanyAccountRegisterPage
    {
        private IMask EmailMask = null!;
        private CompanyAccountAuthModel Model = new CompanyAccountAuthModel();
        private CompanyAccountAuthModelValidator Validator = null!;
        private MudForm? Form;

        private bool isPasswordShow = false,
            isReapetedPasswordShow = false;

        private InputType PasswordInput = InputType.Password,
            ReapetedPasswordInput = InputType.Password;

        private string PasswordInputIcon = Icons.Material.Filled.VisibilityOff,
            ReapetedPasswordIcon = Icons.Material.Filled.VisibilityOff;

        [Inject]
        public IAuthRepository AuthRepository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            EmailMask = RegexMask.Email();
            Validator = new CompanyAccountAuthModelValidator();
            await base.OnInitializedAsync();
        }

        private async Task RegisterUser()
        {
            AuthResponseDTO authResponse = await AuthRepository.RegisterCompanyUser(companyAccount: Model);
            if (!string.IsNullOrEmpty(authResponse.Detail) || !string.IsNullOrEmpty(authResponse.ErrorMsg))
            {
                SnackBar.Add(authResponse.Detail is null ? authResponse.ErrorMsg : authResponse.Detail, MudBlazor.Severity.Error);
            }
            else
            {
                NavigationManager.NavigateTo("/mainpage");
                SnackBar.Add("You have logged in", MudBlazor.Severity.Success);
                SnackBar.Add("If the avatar didn't load, please refresh the page", Severity.Info);
            }
        }

        public async Task Submit()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                await RegisterUser();
            }
        }

        private void DisplayPassoword()
        {
            if (isPasswordShow)
            {
                isPasswordShow = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                isPasswordShow = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }

        private void DisplayReapetedPassword()
        {
            if (isReapetedPasswordShow)
            {
                isReapetedPasswordShow = false;
                ReapetedPasswordIcon = Icons.Material.Filled.VisibilityOff;
                ReapetedPasswordInput = InputType.Password;
            }
            else
            {
                isReapetedPasswordShow = true;
                ReapetedPasswordIcon = Icons.Material.Filled.Visibility;
                ReapetedPasswordInput = InputType.Text;
            }
        }
    }
}