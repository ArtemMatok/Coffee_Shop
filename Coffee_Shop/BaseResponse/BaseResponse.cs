using Coffee_Shop.Data.Enum;
using Microsoft.AspNetCore.Http;

namespace Coffee_Shop.BaseResponse
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; }//описувать помилки якшо вони будуть

        public StatusCode StatusCode { get; set; }//код помилки
        public T Data { get; set; }
    }

    public interface IBaseResponse<T>
    {
        T Data { get; set; }
        public StatusCode StatusCode { get; set; }
    }
}
