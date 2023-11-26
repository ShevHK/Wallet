using Wallet.BLL.Interfaces;
using Wallet.BLL.Response;
using Wallet.DAL.Entities;
using Wallet.DAL.Repositories;
using System.Linq;

namespace Wallet.BLL.Services
{
    public class TransactionService : ITransactionService
    {
        public readonly IRepository<Transaction> _transactionRepository;
        public readonly IRepository<User> _userRepository;

        public TransactionService(IRepository<Transaction> transactionRepository, IRepository<User> userRepository)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
        }

        public async Task<TransactionView> GetTransaction(Guid TransactionId)
        {
            var transaction = await _transactionRepository.ReadAsync(TransactionId);
            var result = new TransactionView
            {
                TransactionId = transaction.Id,
                Type = transaction.Type.ToString(),
                Total = transaction.TotalSum,
                Name = transaction.Name,
                Description = transaction.Description,
                Status = transaction.Status.ToString(),
                AuthUser = ""
            };
            if (transaction.userId == transaction.userWhoPayedId)
                result.AuthUser = (await _userRepository.ReadAsync(transaction.userWhoPayedId)).Name;
            return result;
        }

        public async Task<TransactionsList> GetTransactionsList(int userId)
        {
            var user = await _userRepository.ReadAsync(userId);
            var repo = _transactionRepository as TransactionRepository;
            var transactions = await repo.GetLast10(userId);
            var userIdsToLoad = transactions
                .Where(t => t.userId != t.userWhoPayedId)
                .Select(t => t.userId)
                .Distinct()
                .ToList();

            var users = await _userRepository.GetAllAsync(u => userIdsToLoad.Contains(u.Id));

            var result = await Task.WhenAll(transactions.Select(async t => new TransactionView
            {
                TransactionId = t.Id,
                Type = t.Type.ToString(),
                Total = t.TotalSum,
                Name = t.Name,
                Description = t.Description,
                Status = t.Status.ToString(),
                AuthUser = t.userId == t.userWhoPayedId ? "" : users.FirstOrDefault(u => u.Id == t.userId)?.Name
            }));

            DateTime firstDayOfCurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            var transactionsForLastMonth = transactions.Where(t => t.TransactionDate >= firstDayOfCurrentMonth);

            var CardBalance = transactionsForLastMonth.Select(t => t.TotalSum).Sum();

            var pointsToday = user.DailyPoints;

            int dayOfYear = DateTime.Now.DayOfYear;

            int season = (dayOfYear - 1) / 91 + 1;

            int points = pointsToday;
            for (int day = 2; day <= dayOfYear; day++)
            {
                if (day % 91 == 1) // Перший день кожної пори року
                {
                    points += 2;
                }
                else
                {
                    double previousDayPoints = points - pointsToday;
                    points += (int)(previousDayPoints + 0.6 * previousDayPoints);
                }
            }
            string pointString;
            if (points >= 1000)
            {
                pointString = $"{points / 1000}K";
            }
            else
            {
                pointString = points.ToString();
            }
            return new TransactionsList()
            {
                CardBalance = CardBalance.ToString("0.00"),
                NoPaymentDue = "You have paid your " + DateTime.Now.Month + " balance.",
                DailyPoints = pointString,
                LatesrTransactions = result
            };

        }

    }
}
