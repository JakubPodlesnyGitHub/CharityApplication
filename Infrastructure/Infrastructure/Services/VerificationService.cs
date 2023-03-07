using Application.Dtos.ServiceDtos.Requests;
using Application.Dtos.ServiceDtos.Responses;
using Application.Interfaces.Services;
using Application.Providers;
using CharityApplication.Shared.Model.JsonWrappers.Verification;
using Domain.Entities;
using Domain.Exceptions;
using Duende.IdentityServer.Extensions;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class VerificationService : IVerificationService
    {
        private readonly HttpClient httpClient = new HttpClient();

        private readonly UserManager<Account> _userManager;
        private readonly IEmailService _emailService;
        private readonly IOptions<EmailConfigurationModel> _configurationModel;

        public VerificationService(UserManager<Account> userManager, IEmailService emailService, IOptions<EmailConfigurationModel> configurationModel)
        {
            _userManager = userManager;
            _emailService = emailService;
            _configurationModel = configurationModel;
        }

        public async Task<VerifcationResponseDTO> VerifyAccount(VerificationRequestDTO request)
        {
            Account account = await _userManager.FindByIdAsync(request.IdAccount.ToString());
            if (account is null)
            {
                throw new NotFoundRecordException($"There is no account with given Id: {request.IdAccount}.");
            }
            if (account.CompanyAccountNavigation is not null)
                return await CheckCompany(request, account);
            if (account.PrivateAccountNavigation is not null)
                return await CheckAdulthood(request, account);

            return new VerifcationResponseDTO { IsVerificationSuccessful = false, Msg = "Verification was not successful" };
        }

        private async Task<VerifcationResponseDTO> CheckCompany(VerificationRequestDTO request, Account account)
        {
            CopyModel model = await GetHttpResponse(account.CompanyAccountNavigation.Krs);
            if (!model.Odpis.Dane.Dzial1.DanePodmiotu.Nazwa.ToLower().Contains(account.CompanyAccountNavigation.Name.Split(" ")[0].ToLower()))
            {
                return new VerifcationResponseDTO
                {
                    IsVerificationSuccessful = false,
                    ErrorMsg = "Company name is not included in the name returned from the krs register"
                };
            }
            if (!model.Odpis.Dane.Dzial1.DanePodmiotu.Identyfikatory.Nip.Equals(account.CompanyAccountNavigation.Nip))
            {
                return new VerifcationResponseDTO
                {
                    IsVerificationSuccessful = false,
                    ErrorMsg = "Company nip doesn't match to nip returned from the krs register"
                };
            }
            account.VerificationStatus = true;
            var result = await _userManager.UpdateAsync(account);
            if (result.Succeeded)
            {
                await _emailService.SendEmailAsync(HTMLProvider.ProvideHtmlEmailTemaplate(new EmailRequestDTO
                {
                    FirstName = account.CompanyAccountNavigation.Name,
                    Subject = "Verification Confirmation",
                    To = account.Email
                }, "VerificationClient"));
                await _emailService.SendEmailAsync(HTMLProvider.ProvideHtmlVerificationEmailTemplate(
                    request,
                    account,
                    _configurationModel.Value.EmailFrom,
                    "VerificationCompanyApplication"),
                    request.FrontDocumentImage,
                    request.BackDocumentImage);
            }
            return new VerifcationResponseDTO { IsVerificationSuccessful = true, Msg = "Verification was successful" };
        }

        private async Task<VerifcationResponseDTO> CheckAdulthood(VerificationRequestDTO request, Account account)
        {
            if (account.PrivateAccountNavigation.BirthDate.Value.AddYears(18) > DateTime.Now)
            {
                return new VerifcationResponseDTO
                {
                    IsVerificationSuccessful = false,
                    ErrorMsg = "The user must be of legal age - 18 years"
                };
            }
            account.VerificationStatus = true;
            var result = await _userManager.UpdateAsync(account);
            if (result.Succeeded)
            {
                await _emailService.SendEmailAsync(HTMLProvider.ProvideHtmlEmailTemaplate(new EmailRequestDTO
                {
                    FirstName = account.PrivateAccountNavigation.FirstName,
                    LastName = account.PrivateAccountNavigation.FirstName,
                    Subject = "Verification Confirmation",
                    To = account.Email
                }, "VerificationClient"));
                await _emailService.SendEmailAsync(HTMLProvider.ProvideHtmlVerificationEmailTemplate(
                    request,
                    account,
                    _configurationModel.Value.EmailFrom,
                    "VerificationPersonApplication"),
                    request.FrontDocumentImage,
                    request.BackDocumentImage);
            }
            return new VerifcationResponseDTO { IsVerificationSuccessful = true, Msg = "Verification was successful" };
        }

        private async Task<CopyModel> GetHttpResponse(string companyUserKrs)
        {
            string krsApiPath = $"https://api-krs.ms.gov.pl/api/krs/OdpisAktualny/{companyUserKrs}?rejestr=P&format=json";
            HttpResponseMessage response = await httpClient.GetAsync(krsApiPath);
            string copy = "";
            if (response.IsSuccessStatusCode)
            {
                copy = await response.Content.ReadAsStringAsync();
            }
            return await DeserializeJson(copy);
        }

        private async Task<CopyModel> DeserializeJson(string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            };

            if (json.IsNullOrEmpty())
            {
                return default;
            }
            return await Task.FromResult(JsonSerializer.Deserialize<CopyModel>(json, options));
        }
    }
}