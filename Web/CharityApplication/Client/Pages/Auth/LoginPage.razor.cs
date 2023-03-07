using Application.Dtos.ServiceDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Model.Auth;
using CharityApplication.Client.Shared.Dialog;
using CharityApplication.Client.Validators.Auth;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CharityApplication.Client.Pages.Auth
{
    public partial class LoginPage
    {
        private LoginModel LoginModel = new LoginModel();
        private IMask EmailMask = null!;
        private LoginModelValidator Validator = null!;
        private MudForm? Form;

        private bool isPasswordShow = false;
        private InputType PasswordInput = InputType.Password;
        private string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        [Inject]
        public IAuthRepository AuthRepository { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            EmailMask = RegexMask.Email();
            Validator = new LoginModelValidator();
            await base.OnInitializedAsync();
        }

        public async Task Submit()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                await LoginUser();
            }
        }

        private async Task LoginUser()
        {
            AuthResponseDTO authResponse = await AuthRepository.LoginUser(login: LoginModel);
            if (!string.IsNullOrEmpty(authResponse.Detail) || !string.IsNullOrEmpty(authResponse.ErrorMsg))
            {
                SnackBar.Add(authResponse.Detail is null ? authResponse.ErrorMsg : authResponse.Detail, Severity.Error);
            }
            else
            {
                NavigationManager.NavigateTo("/mainpage");
                SnackBar.Add("You have logged in", Severity.Success);
                SnackBar.Add("If the avatar didn't load, please refresh the page", Severity.Info);
            }
        }

        private void OpenDialogInvitationLink()
        {
            DialogService.Show<NewPasswordDialogComponent>("Restoring Password");
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
    }
}