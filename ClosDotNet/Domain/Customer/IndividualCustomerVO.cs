namespace ClosDotNet.Domain.Customer
{
    using ClosDotNet.Domain.CodeSet;
    using System;

    public interface IIndividualCustomerVO : ICustomerVO
    {
        DateTime? WithBankSinceDate { get; set; }

        DateTime? FirstCreditGrantedDate { get; set; }

        ICodeValueVO Salutation { get; set; }

        ICodeValueVO PreviousSalutation { get; set; }

        ICodeValueVO Gender { get; set; }

        ICodeValueVO MaritalStatus { get; set; }

        DateTime? BirthDate { get; set; }

        int DependentCount { get; set; }

        ICountryVO Nationality { get; set; }

        ICountryVO ResidentCountry { get; set; }

        string PermanentResidentInd { get; set; }

        ICodeValueVO Race { get; set; }

        ICodeValueVO HighestEducation { get; set; }
    }

    [Serializable]
    public class IndividualCustomerVO : CustomerVO, IIndividualCustomerVO
    {
        public IndividualCustomerVO()
            : base()
        {
        }

        public virtual DateTime? WithBankSinceDate { get; set; }

        public virtual DateTime? FirstCreditGrantedDate { get; set; }

        public virtual ICodeValueVO Salutation { get; set; }

        public virtual ICodeValueVO PreviousSalutation { get; set; }

        public virtual ICodeValueVO Gender { get; set; }

        public virtual ICodeValueVO MaritalStatus { get; set; }

        public virtual DateTime? BirthDate { get; set; }

        public virtual int DependentCount { get; set; }

        public virtual ICountryVO Nationality { get; set; }

        public virtual ICountryVO ResidentCountry { get; set; }

        public virtual string PermanentResidentInd { get; set; }

        public virtual ICodeValueVO Race { get; set; }

        public virtual ICodeValueVO HighestEducation { get; set; }
    }
}