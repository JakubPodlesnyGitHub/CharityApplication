using CharityApplication.Client.Model.Module;
using CharityApplication.Shared.Model.JsonWrappers.Module.BasicModules;

namespace CharityApplication.Shared.Model.JsonWrappers.Event
{
    public class EventWrapper
    {
        public LocationDataWrapper LocationModule { get; set; }
        public MoudlePresenceWrapper OnlineEventModule { get; set; }
        public FoundraizerWrapper FoundraizerModule { get; set; }
        public MoudlePresenceWrapper QrCodeModule { get; set; }
        public MoudlePresenceWrapper AssesmentFormModule { get; set; }
        public MoudlePresenceWrapper AttendanceListModule { get; set; }
        public List<ModuleModel> GenericModulesList { get; set; }
    }
}