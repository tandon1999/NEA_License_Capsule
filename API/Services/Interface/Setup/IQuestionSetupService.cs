using API.RequestModel.Setup;
using Shared.Wrapper;

namespace API.Services.Interface.Setup
{
    public interface IQuestionSetupService :IService
    {
        Task<IResponse> CreateQuestionAsync(QuestionSetupRequestModel question);
        Task<IResponse<QuestionSetupRequestModel>> GetQuestionByIdAsync(int id);
        Task<IResponse<List<QuestionSetupRequestModel>>> GetAllQuestionsAsync();
        Task<IResponse> DeleteQuestionAsync(int id);
    }
}
