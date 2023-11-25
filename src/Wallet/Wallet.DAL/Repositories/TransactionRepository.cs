using Microsoft.EntityFrameworkCore;
using Wallet.DAL.Context;
using Wallet.DAL.Entities;

namespace Wallet.DAL.Repositories
{
    public class TransactionRepository : IRepository<Transaction>
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> CreateAsync(Transaction entity)
        {
            _context.Transactions.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Transaction entity)
        {
            _context.Transactions.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Func<Transaction, bool> func)
        {
            var transactionsToDelete = _context.Transactions.Where(func);
            _context.Transactions.RemoveRange(transactionsToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync(Func<Transaction, bool> func)
        {
            return _context.Transactions.Where(func).ToList();
        }

        public async Task<Transaction> ReadAsync(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task<Transaction> ReadAsync(Guid id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Transaction> UpdateAsync(Transaction entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<Transaction>> GetLast10(int userId)
        {
            return await _context.Transactions.Where(t => t.userId == userId).OrderByDescending(t => t.TransactionDate).Take(10).ToListAsync();
        }
    }
}
