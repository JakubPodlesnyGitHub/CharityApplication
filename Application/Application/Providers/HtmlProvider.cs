using Application.Dtos.BasicDtos.Responses;
using Application.Dtos.ServiceDtos.Requests;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Application.Providers
{
    public static class HTMLProvider
    {
        static Dictionary<string, string> HtmlTemplatesDictionary = new Dictionary<string, string> {
                { "AccountCreation", "HtmlTemplates\\AccountCreationConfirmation.html" },
                { "ContactForm", "HtmlTemplates\\ContactFormConfirmation.html" },
                { "PasswordRecovery", "HtmlTemplates\\PasswordRestoreConfirmation.html" },
                { "VerificationClient", "HtmlTemplates\\VerificationConfirmationClient.html" },
                { "VerificationCompanyApplication", "HtmlTemplates\\VerficationCompanyApplication.html" },
                { "VerificationPersonApplication", "HtmlTemplates\\VerficationPersonApplication.html" }
            };

        public static EmailRequestDTO ProvideHtmlEmailTemaplate(EmailRequestDTO request, string htmlTemplate)
        {
            var dictionary = new Dictionary<string, string>
            {
                { "{USER_NAME}",request.FirstName},
                { "{APPLICATION_REPRESENTATIVE}","Jacob"},
                { "{APPLICATION_NAME}","Charity Application"},
            };
            request.Message = ProvideMessageBody(htmlTemplate, dictionary);
            return request;
        }

        public static EmailRequestDTO ProvideHtmlVerificationEmailTemplate(VerificationRequestDTO verificationRequestDTO, Account account,String companyEmail, string htmlTemplate)
        {
            var dictionary = new Dictionary<string, string>
            {
                { "{KRS_LINK}", account.CompanyAccountNavigation is not null ? $"http://www.krs-online.com.pl/?p=6&look={account.CompanyAccountNavigation.Krs}" : string.Empty },
                { "{USER_WEBSITE}", verificationRequestDTO.AccountProfileLink},
                { "{ID_ACCOUNT}", verificationRequestDTO.IdAccount.ToString()},
                { "{DOCUMENT_TYPE}", verificationRequestDTO.DocumentType.ToString()},
                { "{COMPANY_KRS}", account.CompanyAccountNavigation is not null ? account.CompanyAccountNavigation.Krs : string.Empty},
                { "{COMPNY_NIP}", account.CompanyAccountNavigation is not null ? account.CompanyAccountNavigation.Nip : string.Empty},
                { "{COMPANY_NAME}", account.CompanyAccountNavigation is not null ? account.CompanyAccountNavigation.Name : string.Empty},
                { "{USER_FIRSTNAME}", account.PrivateAccountNavigation is not null ? account.PrivateAccountNavigation.FirstName : string.Empty},
                { "{USER_LASTNAME}",  account.PrivateAccountNavigation is not null ? account.PrivateAccountNavigation.LastName: string.Empty},
                { "{USER_BIRTHDATE}",  account.PrivateAccountNavigation is not null ? account.PrivateAccountNavigation.BirthDate.Value.ToLongDateString() : string.Empty}
            };
            return new EmailRequestDTO
            {
                FirstName = account.PrivateAccountNavigation is not null ? 
                $"{account.PrivateAccountNavigation.FirstName} {account.PrivateAccountNavigation.LastName}" : account.CompanyAccountNavigation.Name,
                LastName = account.PrivateAccountNavigation is not null ? account.PrivateAccountNavigation.LastName : null,
                To = companyEmail,
                Subject = account.PrivateAccountNavigation is not null ?
                $"{account.PrivateAccountNavigation.FirstName} {account.PrivateAccountNavigation.LastName} Verification" : $"{account.CompanyAccountNavigation.Name} Verification",
                Message = ProvideMessageBody(htmlTemplate, dictionary),
            };
        }

        private static string ProvideMessageBody(string htmlTemplate, Dictionary<string, string> keyValues)
        {
            var textFile = System.IO.File.ReadAllText(HtmlTemplatesDictionary[htmlTemplate]);
            foreach (var htmlWord in keyValues)
            {
                textFile = textFile.Replace(htmlWord.Key, htmlWord.Value);
            }
            return textFile;
        }
    }
}

