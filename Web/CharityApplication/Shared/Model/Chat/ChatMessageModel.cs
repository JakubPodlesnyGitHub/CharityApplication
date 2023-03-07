using Application.Dtos.BasicDtos.Responses;

namespace CharityApplication.Shared.Model.Chat
{
    public class ChatMessageModel
    {
        public BasicAccountDTO? Sender { get; set; } = null;
        public BasicAccountDTO? Recipient { get; set; } = null;
        public int IdSender { get; set; }
        public int IdRecipient { get; set; }
        public DateTime? SendDate { get; set; } = DateTime.Now;
        public string MessageText { get; set; } = string.Empty;
    }
}