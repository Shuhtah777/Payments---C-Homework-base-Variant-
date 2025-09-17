using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments___C__Homework__base_Variant_
{
    public class CashPayment : Payment
    {
        public string ReceivedBy { get; }


        public CashPayment(decimal amount, string currency, string receivedBy = "")
        : base(amount, currency)
        {
            ReceivedBy = receivedBy;
        }


        protected override void DoProcess()
        {
            Console.WriteLine($"[Cash] Processed cash payment: {Amount} {Currency} (ReceivedBy: {ReceivedBy})");
        }
    }
}
