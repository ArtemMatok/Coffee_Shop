using Coffee_Shop.BaseResponse;
using Coffee_Shop.ViewModels;
using System.Security.Claims;

namespace Coffee_Shop.Service.Interfaces
{
    public interface IAcountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);
        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
    }
}
