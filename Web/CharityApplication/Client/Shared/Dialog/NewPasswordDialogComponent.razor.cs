using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Model.Account;
using CharityApplication.Client.Validators.Account;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CharityApplication.Client.Shared.Dialog
{
    public partial class NewPasswordDialogComponent
    {
        [CascadingParameter]
        public MudDialogInstance? MudDialog { get; set; }

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        public AccountPasswordModel Model { get; set; } = new AccountPasswordModel();
        public AccountPasswordModelValidator Validator { get; set; } = new AccountPasswordModelValidator();

        private bool isPasswordShow = false,
            isReapetedPasswordShow = false;

        private InputType PasswordInput = InputType.Password,
            ReapetedPasswordInput = InputType.Password;

        private string PasswordInputIcon = Icons.Material.Filled.VisibilityOff,
            ReapetedPasswordIcon = Icons.Material.Filled.VisibilityOff;

        private bool IsVisable = false;
        private IMask EmailMask = RegexMask.Email();
        private MudForm? Form;

        //do poprawy
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        public async Task SubmitEmail()
        {
            var accountDTO = await AccountRepository.GetAccountByEmail(Model.Email);

            if (!string.IsNullOrEmpty(accountDTO.Detail))
                SnackBar.Add(accountDTO.Detail, Severity.Error);
            else
            {
                IsVisable = true;
            }
        }

        public async Task Submit()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                BasicAccountDTO accountDTO = await AccountRepository.UpdateAccountPassword(Model);
                if (string.IsNullOrEmpty(accountDTO.Detail))
                {
                    MudDialog.Close(DialogResult.Ok(true));
                    NavigationManager.NavigateTo("/login");
                    SnackBar.Add("Password has been updated successfully", Severity.Success);
                }
                else
                {
                    SnackBar.Add(accountDTO.Detail, Severity.Error);
                }
            }
        }

        private void Cancel() => MudDialog.Cancel();

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