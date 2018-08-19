namespace ClosDotNet.Domain.Base.Model
{
    using ClosDotNet.Domain.CodeSet;
    using System;
    using System.Globalization;

    public interface IAmountVO
    {
        ICurrencyVO Currency { get; set; }

        decimal Value { get; set; }
    }

    public class AmountVO : IAmountVO
    {
        public AmountVO()
        {
        }

        public AmountVO(string currency, string amount)
        {
            Currency = new CurrencyVO() { Code = currency };
            if (!string.IsNullOrEmpty(amount))
            {
                Value = Convert.ToDecimal(amount, CultureInfo.InvariantCulture);
            }
        }

        public virtual ICurrencyVO Currency { get; set; }

        public virtual decimal Value { get; set; }
    }
}