using Wallet.BLL.Response;

namespace Wallet.BLL.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionView> GetTransaction(Guid TransactionId);
        Task<TransactionsList> GetTransactionsList(int UserId);
    }
}
