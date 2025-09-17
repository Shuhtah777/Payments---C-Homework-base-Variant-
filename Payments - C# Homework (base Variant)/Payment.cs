using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments___C__Homework__base_Variant_
{
    public abstract class Payment
    {
        public decimal Amount { get; }
        public string Currency { get; }
        public DateTime CreatedUtc { get; }
        public DateTime? ProcessedUtc { get; private set; }


        public event EventHandler<PaymentProcessedEventArgs>? Processed;


        protected Payment(decimal amount, string currency)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be > 0");
            if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency must be provided", nameof(currency));


            Amount = amount;
            Currency = currency;
            CreatedUtc = DateTime.UtcNow;
        }


        public void Process()
        {
            ValidateInvariants();
            OnBeforeProcess();
            DoProcess();
            OnAfterProcess();
            OnProcessed(new PaymentProcessedEventArgs(this, ProcessedUtc!.Value));
        }


        protected virtual void ValidateInvariants()
        {
            if (Amount <= 0) throw new InvalidOperationException("Invariant violated: Amount must be > 0");
            if (string.IsNullOrWhiteSpace(Currency)) throw new InvalidOperationException("Invariant violated: Currency required");
        }


        protected virtual void OnBeforeProcess() { }
        protected abstract void DoProcess();
        protected virtual void OnAfterProcess()
        {
            ProcessedUtc = DateTime.UtcNow;
        }
        protected virtual void OnProcessed(PaymentProcessedEventArgs e)
        {
            Processed?.Invoke(this, e);
        }
    }
}
