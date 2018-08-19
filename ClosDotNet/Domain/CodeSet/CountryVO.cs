namespace ClosDotNet.Domain.CodeSet
{
    using ClosDotNet.Domain.Base.Model;

    public interface ICountryVO : IValueObject
    {
        string Code { get; set; }

        string Description { get; set; }
    }

    public class CountryVO : ValueObject, ICountryVO
    {
        public CountryVO() 
            : base ()
        {
        }

        public virtual string Code { get; set; }

        public virtual string Description { get; set; }
    }
}