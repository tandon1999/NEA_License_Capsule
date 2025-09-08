namespace Shared.Model
{
    public class SystemResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Data { get; set; } = string.Empty;
        public string? Extras { get; set; } = string.Empty;
        public string? ExtrasSecond { get; set; } = string.Empty;
    }
}
