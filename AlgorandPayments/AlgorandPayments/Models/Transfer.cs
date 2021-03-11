using SQLite;

namespace AlgorandPayments.Models
{
    public class Transfer
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiverAddress { get; set; }
        public int Amount { get; set; }
        public override string ToString()
        {
            return this.Amount + " algos to " + this.ReceiverAddress;
        }
    }
}
