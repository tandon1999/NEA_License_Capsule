namespace API.RequestModel.Setup
{
    public class QuestionSetupRequestModel
    {
        public int QID { get; set; }
        public string? Question { get; set; }
        public List<McqOption> Optionslist { get; set; } = new List<McqOption>();
    }
    public class McqOption
    {
        public int OptionId { get; set; }
        public string Answer { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public bool IsSelected { get; set; }
    }
}
