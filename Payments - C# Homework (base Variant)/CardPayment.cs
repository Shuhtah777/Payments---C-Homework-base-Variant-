using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments___C__Homework__base_Variant_
{
    public class CardPayment : Payment, IRefundable
    {
        public string CardMasked { get; }
        public string? AuthCode { get; private set; }
        private decimal _refunded;


        public CardPayment(decimal amount, string currency, string cardMasked)
        : base(amount, currency)
        {
            CardMasked = cardMasked ?? throw new ArgumentNullException(nameof(cardMasked));
            _refunded = 0m;
        }


        protected override void OnBeforeProcess()
        {
            Console.WriteLine($"[Card] Before processing card {CardMasked}");
        }


        protected sealed override void DoProcess()
        {
            AuthCode = GenerateAuthCode();
            Console.WriteLine($"[Card] Charging {Amount} {Currency} on card {CardMasked}. Auth: {AuthCode}");
        }


        protected override void OnAfterProcess()
        {
            base.OnAfterProcess();
            Console.WriteLine($"[Card] After process: processed at {ProcessedUtc}");
        }


        private string GenerateAuthCode() => DateTime.UtcNow.Ticks.ToString().Substring(0, 6);


        public void Refund(decimal amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Refund amount must be > 0");
            if (amount > (Amount - _refunded)) throw new InvalidOperationException("Refund amount exceeds available paid amount");


            _refunded += amount;
            Console.WriteLine($"[Card] Refunded {amount} {Currency} to {CardMasked}. Total refunded: {_refunded}");
        }
    }
}
