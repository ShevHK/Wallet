using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wallet.DAL.Entities.Enum;

namespace Wallet.DAL.Entities
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }

        public TransactionType Type { get; set; }

        public decimal TotalSum { get; set; }
        public decimal MaxBalanceAvailable { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        public string Description { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Icon { get; set; }
        public int userWhoPayedId { get; set; }
        public int userId { get; set; }
        //[JsonIgnore]
        public User User { get; set; }
    }
}
