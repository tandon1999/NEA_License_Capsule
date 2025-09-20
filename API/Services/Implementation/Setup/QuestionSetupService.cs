using API.Param.Setup;
using API.RequestModel.Setup;
using API.Services.Interface.Setup;
using AutoMapper;
using Microsoft.Extensions.Options;
using Shared.DataAccess.GenericRepository;
using Shared.Wrapper;
using System.Text.Json;

namespace API.Services.Implementation.Setup
{
    public class QuestionSetupService : IQuestionSetupService
    {
        public readonly IGenericServices _genericservice;
        public readonly IMapper _mapper;
        public string storedProcedure = "spQuestionSetup";
        public QuestionSetupService(IGenericServices genericservice, IMapper mapper)
        {
            _genericservice = genericservice ?? throw new ArgumentNullException(nameof(genericservice));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IResponse> CreateQuestionAsync(QuestionSetupRequestModel question)
        {
            try
            {
                QuestionSetupParam param = new();
                param = _mapper.Map<QuestionSetupParam>(question);
                param.Flag = 'C';
                param.Optionslist = JsonSerializer.Serialize(question.Optionslist);
                var response = await _genericservice.GetAsync<Response>(storedProcedure, param);
                if (!response.Succeeded)
                {
                    return await Response.FailAsync("Operation Failed");
                }
                else
                {
                    return await Response.SuccessAsync(response.Messages);
                }

            }
            catch (Exception ex)
            {
                return await Response.FailAsync(ex.Message);
            }

        }

        public async Task<IResponse> DeleteQuestionAsync(int id)
        {
            try
            {
                QuestionSetupParam param = new();
                param.Flag = 'D';
                param.QID = id;
                var response = await _genericservice.GetAsync<Response>(storedProcedure, param);
                if (!response.Succeeded)
                {
                    return await Response.FailAsync("Operation Failed");
                }
                else
                {
                    return await Response.SuccessAsync(response.Messages);
                }
            }
            catch (Exception ex)
            {
                return await Response.FailAsync(ex.Message);
            }
        }

        public async Task<IResponse<List<QuestionSetupRequestModel>>> GetAllQuestionsAsync()
        {
            try
            {
                QuestionSetupParam param = new();
                param.Flag = 'G';
                var response = await _genericservice.GetAllAsync<QuestionSetupRequestModel>(storedProcedure, param);
                return await Response<List<QuestionSetupRequestModel>>.SuccessAsync(response);
            }
            catch (Exception ex)
            {
                return await Response<List<QuestionSetupRequestModel>>.SuccessAsync(ex.Message);
            }
        }

        public async Task<IResponse<QuestionSetupRequestModel>> GetQuestionByIdAsync(int id)
        {
            try
            {
                QuestionSetupParam param = new();
                param.Flag = 'I';
                param.QID = id;
                var response = await _genericservice.GetAsync<QuestionSetupRequestModel>(storedProcedure, param);
                if (response != null)
                {
                    return await Response<QuestionSetupRequestModel>.SuccessAsync(response);
                }
                else
                {
                    return await Response<QuestionSetupRequestModel>.FailAsync("Operation Failed");
                }
            }
            catch (Exception ex)
            {
                return await Response<QuestionSetupRequestModel>.FailAsync(ex.Message);
            }
        }
    }
}
