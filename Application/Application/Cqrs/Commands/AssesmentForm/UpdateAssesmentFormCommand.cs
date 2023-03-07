using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.AssesmentForm
{
    public class UpdateAssesmentFormCommand : IRequest<BasicAssesmentFormDTO>
    {
        public int IdAssesmentForm { get; set; }
        public string Mail { get; set; } = null!;
        public int EventRate { get; set; }
        public string Subject { get; set; } = null!;
        public int AppRate { get; set; }
        public string Message { get; set; } = null!;
        public int Event { get; set; }
        public int IdOwner { get; set; }

        public UpdateAssesmentFormCommand(
            int idAssesmentForm,
            string mail,
            int eventRate,
            string subject,
            int appRate,
            string message,
            int @event,
            int idOwner)
        {
            IdAssesmentForm = idAssesmentForm;
            Mail = mail;
            EventRate = eventRate;
            Subject = subject;
            AppRate = appRate;
            Message = message;
            Event = @event;
            IdOwner = idOwner;
        }
    }
}