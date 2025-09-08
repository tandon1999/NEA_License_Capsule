using API.RequestModel.Auth;
using API.ResponseModel.Auth;
using Shared.Wrapper;

namespace API.Services.Interface.Auth
{
    public interface ITokenService
    {
        Task<IResponse<LoginResponseModel>> CreateToken(LoginRequestModel loginRequest);
    }
}
