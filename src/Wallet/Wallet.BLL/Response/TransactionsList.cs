namespace Wallet.BLL.Response
{
    public class TransactionsList
    {
        public string CardBalance { get; set; }
        public string NoPaymentDue { get; set; }
        public string DailyPoints { get; set; }
        public IList<TransactionView> LatesrTransactions { get; set; }

        //If you can store information from one call,
        //than use list of returned items,
        //if you want to create new call to backend in new page for one transaction,
        //than use list of Guids
        //public IList<Guid> LatesrTransactionsIds { get; set; }
    }
}
