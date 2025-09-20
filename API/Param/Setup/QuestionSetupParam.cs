using API.RequestModel.Setup;

namespace API.Param.Setup
{
    public class QuestionSetupParam
    {
        public char Flag { get; set; }
        public int QID { get; set; }
        public string? Question { get; set; }
        public string? Optionslist { get; set; }
        public int CreatedBy { get; set; }
    }
}
