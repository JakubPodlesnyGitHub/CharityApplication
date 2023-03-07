using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers;
using CharityApplication.Client.Model.EventModule;
using CharityApplication.Shared.Enums;
using CharityApplication.Shared.Model.JsonWrappers.Event;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace CharityApplication.Client.Shared.Modules
{
    public partial class EventModulesTableComponent
    {
        public string SearchValue { get; set; } = "";
        private bool LoadingBar = true;
        public bool DenseItems { get; set; } = false;
        public bool HoverItems { get; set; } = true;
        public string ButtonText { get; set; } = string.Empty;
        public string SnackBarText { get; set; } = string.Empty;

        public BasicModuleDTO SelectedElement { get; set; } = null!;
        public List<BasicModuleDTO> Modules { get; set; } = new List<BasicModuleDTO>();
        public HashSet<BasicModuleDTO> SelectedModules = new HashSet<BasicModuleDTO>();

        [Parameter]
        public BasicEventDTO? Event { get; set; } = null!;

        [Parameter]
        public int? IdEvent { get; set; }

        [Parameter]
        public EventWrapper EventWrapper { get; set; } = null!;

        [Parameter]
        public FormState FormState { get; set; }

        [Inject]
        public IModuleRepository ModuleRepository { get; set; }

        [Inject]
        public IEventModuleRepository EventModuleRepository { get; set; }

        [Inject]
        public IEventRepository EventRepository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (IdEvent is null)
            {
                Modules = await EventModuleRepository.GetEventModules(Event.IdEvent);
                LoadingBar = false;
            }
            if (Event is null)
            {
                Modules = await ModuleRepository.GetModules();
                LoadingBar = false;
            }
            if (EventWrapper is null && FormState == FormState.EDIT)
            {
                var eventDTO = await EventRepository.GetEvent(IdEvent);
                if (!string.IsNullOrEmpty(eventDTO.Detail))
                {
                    SnackBar.Add(eventDTO.Detail, MudBlazor.Severity.Error);
                }
                else
                {
                    EventWrapper = JsonSerializer.Deserialize<EventWrapper>(eventDTO.JsonEvent);
                    ButtonText = "Update Modules";
                    SnackBarText = "Modules has beeen updated to event";
                }
            }
            else if (EventWrapper is null && FormState != FormState.EDIT)
            {
                EventWrapper = new EventWrapper();
                ButtonText = "Add Modules";
                SnackBarText = "Modules has beeen added to event";
            }

            await base.OnInitializedAsync();
        }

        private bool FilterFuncShort(BasicModuleDTO e) => FilterFunc(e, SearchValue);

        private bool FilterFunc(BasicModuleDTO e, string SearchValue)
        {
            if (e.ModuleName.Contains(SearchValue, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (e.ModuleDesc.Contains(SearchValue, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (string.IsNullOrWhiteSpace(SearchValue))
            {
                return true;
            }
            return false;
        }

        private void ShowBtnSwipe(BasicModuleDTO selectedElement)
        {
            selectedElement.ShowModuleProperties = !selectedElement.ShowModuleProperties;
            selectedElement.ModuleWrapper = ModuleHelper.ProvideModuleJsonTemplate(selectedElement, EventWrapper);
            selectedElement.Properties = selectedElement.ModuleWrapper.GetType().GetProperties().Where(prop => !selectedElement.PropertiesToExclude.Contains(prop.Name)).ToArray();
            SelectedModules.Add(selectedElement);
        }

        private async Task AddModulesToDatabase()
        {
            BasicEventDTO Event = await EventRepository.GetEvent(IdEvent);
            if (!string.IsNullOrEmpty(Event.Detail))
            {
                SnackBar.Add(Event.Detail, MudBlazor.Severity.Error);
            }
            else
            {
                await CreateConnectionBetweenModuleEvent(Event);
                Event.JsonEvent = JsonSerializer.Serialize(EventWrapper, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                await UpdateEvent(Event);
            }
        }

        private async Task UpdateEvent(BasicEventDTO Event)
        {
            var eventDTO = await EventRepository.UpdateEvent(Event);
            if (string.IsNullOrEmpty(eventDTO.Detail))
            {
                SnackBar.Add(SnackBarText, MudBlazor.Severity.Success);
                NavigationManager.NavigateTo($"/event/{IdEvent}");
            }
            else
            {
                SnackBar.Add(eventDTO.Detail, MudBlazor.Severity.Error);
            }
        }

        private async Task CreateConnectionBetweenModuleEvent(BasicEventDTO eventDTO)
        {
            foreach (var module in SelectedModules)
            {
                await EventModuleRepository.CreateEventModule(new EventModuleModel { IdEvent = eventDTO.IdEvent, IdModule = module.IdModule });
            }
        }

        private string GetPropertyType(Type type)
        {
            return ModuleHelper.ProvideTypeNameOfProperty(type);
        }
    }
}