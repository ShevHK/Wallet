using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Wallet.DAL.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [NotNull]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        public int DailyPoints { get; set; }

        //[JsonIgnore]
        public ICollection<Transaction> UsersTransactions { get; set; }

    }
}
