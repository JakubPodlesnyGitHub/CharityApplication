using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Shared.Model.JsonWrappers.Event;
using CharityApplication.Shared.Model.JsonWrappers.Module.BasicModules;
using System.Text.Json;

namespace CharityApplication.Client.Helpers
{
    public static class ModuleHelper
    {
        public static object ProvideModuleJsonTemplate(BasicModuleDTO module, EventWrapper eventWrapper)
        {
            switch (module.ModuleName)
            {
                case "Location":
                    if (eventWrapper.LocationModule is null)
                        eventWrapper.LocationModule = JsonSerializer.Deserialize<LocationDataWrapper>(module.ModuleJson);
                    return eventWrapper.LocationModule;

                case "Online Event":
                    if (eventWrapper.OnlineEventModule is null)
                        eventWrapper.OnlineEventModule = JsonSerializer.Deserialize<MoudlePresenceWrapper>(module.ModuleJson);
                    return eventWrapper.OnlineEventModule;

                case "Foundraiser":
                    if (eventWrapper.FoundraizerModule is null)
                        eventWrapper.FoundraizerModule = JsonSerializer.Deserialize<FoundraizerWrapper>(module.ModuleJson);
                    return eventWrapper.FoundraizerModule;

                case "QrCode":
                    if (eventWrapper.QrCodeModule is null)
                        eventWrapper.QrCodeModule = JsonSerializer.Deserialize<MoudlePresenceWrapper>(module.ModuleJson);
                    return eventWrapper.QrCodeModule;

                case "Assesment Form":
                    if (eventWrapper.AssesmentFormModule is null)
                        eventWrapper.AssesmentFormModule = JsonSerializer.Deserialize<MoudlePresenceWrapper>(module.ModuleJson);
                    return eventWrapper.AssesmentFormModule;

                case "Attendance List":
                    if (eventWrapper.AttendanceListModule is null)
                        eventWrapper.AttendanceListModule = JsonSerializer.Deserialize<MoudlePresenceWrapper>(module.ModuleJson);
                    return eventWrapper.AttendanceListModule;

                default:
                    return eventWrapper;
            }
        }

        public static string ProvideTypeNameOfProperty(Type propertyType)
        {
            if (propertyType == typeof(bool) || propertyType == typeof(bool?))
            {
                return "Bool";
            }
            else if (propertyType == typeof(int) || propertyType == typeof(int?))
            {
                return "Integer Number";
            }
            else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
            {
                return "DateTime";
            }
            else if (propertyType == typeof(string) || propertyType == typeof(string))
            {
                return "String";
            }
            else if (propertyType == typeof(double) || propertyType == typeof(double?))
            {
                return "Double Number";
            }
            return "No type name";
        }
    }
}