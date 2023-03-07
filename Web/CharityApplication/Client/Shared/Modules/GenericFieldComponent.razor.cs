using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace CharityApplication.Client.Shared.Modules
{
    public partial class GenericFieldComponent
    {
        [Parameter]
        public Type PropertyType { get; set; }

        [Parameter]
        public string PropertyName { get; set; }

        [Parameter]
        public object ModuleWrapper { get; set; }

        [Parameter]
        public PropertyInfo Property { get; set; }

        [Parameter]
        public EventCallback<PropertyInfo> PropertyChanged { get; set; }
    }
}