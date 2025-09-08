using API.Param.Auth;
using API.RequestModel.Auth;
using API.ResponseModel.Auth;
using API.Services.Interface.Auth;
using Microsoft.IdentityModel.Tokens;
using Shared.DataAccess.GenericRepository;
using Shared.Encryptions;
using Shared.Wrapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services.Implementation.Auth
{
    public class TokenService : ITokenService
    {
        private IEncryptionService _encryptionService;
        private readonly IGenericServices _genericServices;
        public const int JWT_TOKEN_VALIDITY_MINS = 120;
        public const string JWT_SECURITY_KEY = "CBS5M8B1W63E511B232SAI9CE316R69V57@sdsU&";

        public TokenService(IGenericServices genericServices, IEncryptionService encryptionService)
        {
            _genericServices = genericServices ?? throw new ArgumentNullException(nameof(genericServices));
            _encryptionService = encryptionService ?? throw new ArgumentNullException();
        }
        public async Task<IResponse<LoginResponseModel>> CreateToken(LoginRequestModel loginRequest)
        {
            LoginParam param = new();
            param.Flag = "S";
            param.Username = loginRequest.Username;
            param.Password = loginRequest.Password;
            var User = await _genericServices.GetAsync<UserDataResponseModel>("AUTH.spLogin", param);
            if (User == null)
            {
                return await Response<LoginResponseModel>.FailAsync("Invalid Username or Password.");
            }
            string storedPassword = User.Password;
            byte[] storedSalt = Convert.FromBase64String(User.Salt);
            byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(loginRequest.Password);
            byte[] saltedPassword = new byte[enteredPasswordBytes.Length + storedSalt.Length];
            Buffer.BlockCopy(enteredPasswordBytes, 0, saltedPassword, 0, enteredPasswordBytes.Length);
            Buffer.BlockCopy(storedSalt, 0, saltedPassword, enteredPasswordBytes.Length, storedSalt.Length);
            string enteredPasswordHash = _encryptionService.HashingPassword(loginRequest.Password, storedSalt);
            if (enteredPasswordHash != storedPassword)
            {
                return await Response<LoginResponseModel>.FailAsync("Invalid Username or Password.");
            }
            else
            {
                var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
                LoginResponseModel response = new LoginResponseModel
                {
                    UserName = User.UserName,
                    RoleId = User.RoleId,
                    UserId = User.UserId,
                    ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                    Token = GenerateToken(User)
                };
                return await Response<LoginResponseModel>.SuccessAsync(response);
            }
        }
        private string GenerateToken(UserDataResponseModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("UserName",user.UserName),
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("Password",user.Password),
                    new Claim("FullName",user.FullName),
                    new Claim("Email",user.Email),
                    new Claim("Hash",user.Hash),
                    new Claim("Salt",user.Salt),
                    new Claim("RoleId",user.RoleId.ToString()),
                    new Claim("RoleName",user.RoleName),
                };
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_SECURITY_KEY)),
                SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                   claims: claims,
                   expires: DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS),
                   signingCredentials: signingCredentials);
            return tokenHandler.WriteToken(token);
        }
    }
}
