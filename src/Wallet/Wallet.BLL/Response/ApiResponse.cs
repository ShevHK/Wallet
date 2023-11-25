using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.BLL.Response
{
    public class ApiResponse<T> where T : class
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        public ApiResponse(bool success, T data = default, string errorMessage = null)
        {
            Success = success;
            Data = data;
            ErrorMessage = errorMessage;
        }
    }
}
