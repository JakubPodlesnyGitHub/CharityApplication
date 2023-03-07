using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers;
using CharityApplication.Shared.Model.JsonWrappers.Event;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace CharityApplication.Client.Pages.Modules
{
    public partial class ModulesTablePage
    {
        public string SearchValue { get; set; } = "";
        private bool LoadingBar = true;
        public bool DenseItems { get; set; } = false;
        public List<BasicModuleDTO> Modules { get; set; } = new List<BasicModuleDTO>();
        public BasicModuleDTO SelectedElement { get; set; } = null;

        [Inject]
        public IModuleRepository ModuleRepository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Modules = await ModuleRepository.GetModules();
            LoadingBar = false;
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

        public object ModuleWrapper { get; set; } = null!;
        public PropertyInfo[] Properties { get; set; } = null!;
        public EventWrapper EventWrapper { get; set; } = new EventWrapper();

        private void ShowBtnSwipe(BasicModuleDTO selectedElement)
        {
            selectedElement.ShowModuleProperties = !selectedElement.ShowModuleProperties;
            ModuleWrapper = ModuleHelper.ProvideModuleJsonTemplate(selectedElement, EventWrapper);
            Properties = ModuleWrapper.GetType().GetProperties();
        }

        private string GetPropertyType(Type type)
        {
            return ModuleHelper.ProvideTypeNameOfProperty(type);
        }
    }
}