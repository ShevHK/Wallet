namespace Wallet.BLL.Response
{
    public class TransactionView
    {
        public Guid TransactionId { get; set; }
        public string Type { get; set; }
        public decimal Total { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string AuthUser { get; set; }
    }
}
