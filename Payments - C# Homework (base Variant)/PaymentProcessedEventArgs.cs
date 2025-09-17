using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments___C__Homework__base_Variant_
{
    public class PaymentProcessedEventArgs : EventArgs
    {
        public Payment Payment { get; }
        public DateTime ProcessedUtc { get; }


        public PaymentProcessedEventArgs(Payment payment, DateTime processedUtc)
        {
            Payment = payment ?? throw new ArgumentNullException(nameof(payment));
            ProcessedUtc = processedUtc;
        }
    }
}
