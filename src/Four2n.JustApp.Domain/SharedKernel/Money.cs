using System;

namespace Four2n.JustApp.Domain.SharedKernel
{
    public class Money
    {
        public static Money EUR(decimal amount)
        {
            return new Money(amount, "EUR");
        }

        public static Money USD(decimal amount)
        {
            return new Money(amount, "USD");
        }

        public static Money PLN(decimal amount)
        {
            return new Money(amount, "PLN");
        }


        private Money(decimal amount, string currency)
        {
            Currency = currency;
            Amount = amount;
        }

        [Obsolete("For EF only", true)]
        public Money()
        {
        }

        public string Currency { get; set; }

        public decimal Amount { get; set; }
    }
}
