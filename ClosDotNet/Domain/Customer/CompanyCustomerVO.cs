namespace ClosDotNet.Domain.Customer
{
    using ClosDotNet.Domain.CodeSet;
    using System;

    public interface ICompanyCustomerVO : ICustomerVO
    {
        ICodeValueVO Constitution { get; set; }

        string MandatoryInfoNotAvailableInd { get; set; }

        ICountryVO IncorporationCountry { get; set; }

        DateTime? IncorporationDate { get; set; }

        ICountryVO BusinessOperationCountry { get; set; }

        string MoreThan50TurnoverInd { get; set; }
    }

    [Serializable]
    public class CompanyCustomerVO : CustomerVO, ICompanyCustomerVO
    {
        public CompanyCustomerVO() 
            : base ()
        {
        }

        public virtual ICodeValueVO Constitution { get; set; }

        public virtual string MandatoryInfoNotAvailableInd { get; set; }

        public virtual ICountryVO IncorporationCountry { get; set; }

        public virtual DateTime? IncorporationDate { get; set; }

        public virtual ICountryVO BusinessOperationCountry { get; set; }

        public virtual string MoreThan50TurnoverInd { get; set; }
    }
}