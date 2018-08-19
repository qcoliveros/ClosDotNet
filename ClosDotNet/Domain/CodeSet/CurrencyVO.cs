namespace ClosDotNet.Domain.CodeSet
{
    using ClosDotNet.Domain.Base.Model;

    public interface ICurrencyVO : IValueObject
    {
        string Code { get; set; }

        string Description { get; set; }
    }

    public class CurrencyVO : ValueObject, ICurrencyVO
    {
        public CurrencyVO() 
            : base ()
        {
        }

        public virtual string Code { get; set; }

        public virtual string Description { get; set; }
    }
}