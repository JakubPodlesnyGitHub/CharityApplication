namespace CharityApplication.Client.Model.AssessmentForm
{
    public class AssessmentFormModel
    {
        public int? IdAssesmentForm { get; set; }
        public string Mail { get; set; } = null!;
        public int EventRate { get; set; }
        public string Subject { get; set; } = null!;
        public int AppRate { get; set; }
        public string Message { get; set; } = null!;
        public int Event { get; set; }
        public int IdOwner { get; set; }
    }
}