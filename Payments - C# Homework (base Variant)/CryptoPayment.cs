using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments___C__Homework__base_Variant_
{
    public class CryptoPayment : Payment
    {
        public string? TxHash { get; private set; }


        public CryptoPayment(decimal amount, string currency)
        : base(amount, currency)
        {
        }


        protected override void DoProcess()
        {
            TxHash = "0x" + DateTime.UtcNow.Ticks.ToString("x");
            Console.WriteLine($"[Crypto] Broadcast TX {TxHash} for {Amount} {Currency}");
        }
    }
}
