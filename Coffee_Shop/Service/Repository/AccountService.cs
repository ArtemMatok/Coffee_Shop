using Coffee_Shop.BaseResponse;
using Coffee_Shop.Models;
using Coffee_Shop.Service.Interfaces;
using Coffee_Shop.ViewModels;
using System.Security.Claims;

namespace Coffee_Shop.Service.Repository
{
    public class AccountService : IAcountService
    {
        private readonly IUserRepository _userRepositpry;
        private readonly ILogger<AccountService> _logger;

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())

            };
            return new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
        public AccountService(ILogger<AccountService> logger, IUserRepository userRepositpry)
        {
            _logger = logger;
            _userRepositpry = userRepositpry;
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userRepositpry.GetByName(model.Name);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "User has not found"
                    };
                }
                if (user.Password != model.Password)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Wrong password"
                    };

                }
                var result = Authenticate(user);
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = Data.Enum.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Login]:{ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = Data.Enum.StatusCode.InternalServer
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await _userRepositpry.GetByName(model.Name);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = "This name is used",
                    };
                }
                user = new User()
                {
                    Name = model.Name,
                    Role = Data.Enum.Role.User,
                    Password = model.Password
                };
                _userRepositpry.Add(user);
                var result = Authenticate(user);
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Success",
                    StatusCode = Data.Enum.StatusCode.OK
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Register]:{ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = Data.Enum.StatusCode.InternalServer,
                };
            }
        }
    }
}
