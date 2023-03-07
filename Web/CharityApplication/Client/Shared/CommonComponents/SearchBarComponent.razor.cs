using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Text.Json;

namespace CharityApplication.Client.Shared.CommonComponents
{
    public partial class SearchBarComponent
    {
        public object SearchValue { get; set; } = "";
        public bool ResetValueOnEmptyText { get; set; } = true;
        public List<object> Entities { get; set; } = new List<object>();

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        [Inject]
        public IGroupRepository GroupRepository { get; set; }

        [Inject]
        public IEventRepository EventRepository { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Entities.AddRange(await AccountRepository.GetAccounts());
            if (await LocalStorageService.ContainKeyAsync("loggedUser"))
            {
                var loggedUser = JsonSerializer.Deserialize<BasicAccountDTO>(
                                    await LocalStorageService.GetItemAsync<string>("loggedUser"),
                                    new JsonSerializerOptions
                                    {
                                        PropertyNameCaseInsensitive = true
                                    });
                Entities.AddRange(await GroupRepository.GetPublicPrivateGroups(loggedUser.IdAccount));
            }
            else
            {
                Entities.AddRange(await GroupRepository.GetPublicGroups());
            }
            Entities.AddRange(await EventRepository.GetEventsList());
            await base.OnInitializedAsync();
        }

        private async Task<IEnumerable<object>> SearchFunc(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Entities;
            }
            return GetValues(value);
        }

        private IEnumerable<object> GetValues(string value)
        {
            List<object> searchValues = new List<object>();
            foreach (var obj in Entities)
            {
                if (obj.GetType().Equals(typeof(BasicEventDTO)))
                {
                    var basicEvent = obj as BasicEventDTO;
                    if (basicEvent.EventName.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                        searchValues.Add(basicEvent);
                }
                if (obj.GetType().Equals(typeof(BasicGroupDTO)))
                {
                    var group = obj as BasicGroupDTO;
                    if (group.GroupName.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                        searchValues.Add(group);
                }
                if (obj.GetType().Equals(typeof(BasicAccountDTO)))
                {
                    var account = obj as BasicAccountDTO;
                    if (account.PrivateAccount.FirstName.Contains(value, StringComparison.InvariantCultureIgnoreCase)
                        || account.PrivateAccount.LastName.Contains(value, StringComparison.InvariantCultureIgnoreCase)
                        || account.CompanyAccount.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                        searchValues.Add(account);
                }
            }
            return searchValues;
        }

        public string ToStringFunc(object obj)
        {
            if (obj.GetType().Equals(typeof(BasicEventDTO)))
            {
                var basicEvent = obj as BasicEventDTO;
                return basicEvent.EventName;
            }
            if (obj.GetType().Equals(typeof(BasicGroupDTO)))
            {
                var group = obj as BasicGroupDTO;
                return group.GroupName.Name;
            }
            if (obj.GetType().Equals(typeof(BasicAccountDTO)))
            {
                var account = obj as BasicAccountDTO;
                return account.PrivateAccount is not null ? $"{account.PrivateAccount.FirstName} {account.PrivateAccount.LastName}" : account.CompanyAccount.Name;
            }
            return "No Name";
        }

        private async Task NavigateToSpecificComponent(object obj)
        {
            var user = (await AuthenticationStateTask).User;
            if (user.Identity.IsAuthenticated)
            {
                if (obj.GetType().Equals(typeof(BasicEventDTO)))
                {
                    var basicEvent = obj as BasicEventDTO;
                    NavigationManager.NavigateTo($"/event/{basicEvent.IdEvent}");
                }
                else if (obj.GetType().Equals(typeof(BasicGroupDTO)))
                {
                    var group = obj as BasicGroupDTO;
                    NavigationManager.NavigateTo($"/group/{group.IdGroup}");
                }
                else if (obj.GetType().Equals(typeof(BasicAccountDTO)))
                {
                    var account = obj as BasicAccountDTO;
                    NavigationManager.NavigateTo($"/account/{account.IdAccount}");
                }
            }
        }
    }
}