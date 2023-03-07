using CharityApplication.Shared.Enums;

namespace Application.Dtos.ServiceDtos.Requests
{
    public class VerificationRequestDTO
    {
        public int IdAccount { get; set; }
        public DocumentType DocumentType { get; set; }
        public string FrontDocumentImage { get; set; }
        public string BackDocumentImage { get; set; }
        public string AccountProfileLink { get; set; }
    }
}