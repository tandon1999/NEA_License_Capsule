using API.RequestModel.Setup;
using API.Services.Interface.Setup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Setup
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionSetupController : ControllerBase
    {
        private readonly IQuestionSetupService _questionSetupService;
        public QuestionSetupController(IQuestionSetupService questionSetupService)
        {
            _questionSetupService = questionSetupService ?? throw new ArgumentNullException(nameof(questionSetupService));
        }


        [HttpPost("CreateQuestion")]
        public async Task<IActionResult> CreateQuestion(QuestionSetupRequestModel question)
        {
            var response = await _questionSetupService.CreateQuestionAsync(question);
            return Ok(response);
        }


        [HttpGet("GetQuestionById")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var response = await _questionSetupService.GetQuestionByIdAsync(id);
            return Ok(response);
        }


        [HttpGet("GetAllQuestions")]
        public async Task<IActionResult> GetAllQuestions()
        {
            var response = await _questionSetupService.GetAllQuestionsAsync();
            return Ok(response);
        }


        [HttpDelete("DeleteQuestion")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var response = await _questionSetupService.DeleteQuestionAsync(id);
            return Ok(response);
        }
    }
}
