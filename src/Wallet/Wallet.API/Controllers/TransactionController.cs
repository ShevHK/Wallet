using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wallet.BLL.Interfaces;
using Wallet.BLL.Response;

namespace Wallet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("GetTransactionDetails/{transactionId}")]
        public async Task<ActionResult<ApiResponse<TransactionView>>> GetTransaction(Guid transactionId)
        {
            try
            {
                var transaction = await _transactionService.GetTransaction(transactionId);
                return new ApiResponse<TransactionView>(true, transaction);
            }
            catch (Exception ex)
            {
                return new ApiResponse<TransactionView>(false, errorMessage: ex.Message);
            }
        }

        [HttpGet("GetUsersTransactions/{userId}")]
        public async Task<ActionResult<ApiResponse<TransactionsList>>> GetTransactionsList(int userId)
        {
            try
            {
                var transactionsList = await _transactionService.GetTransactionsList(userId);
                return new ApiResponse<TransactionsList>(true, transactionsList);
            }
            catch (Exception ex)
            {
                return new ApiResponse<TransactionsList>(false, errorMessage: ex.Message);
            }
        }
    }
}
