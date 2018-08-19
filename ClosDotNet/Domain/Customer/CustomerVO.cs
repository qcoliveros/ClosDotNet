namespace ClosDotNet.Domain.Customer
{
    using ClosDotNet.Domain.Base.Model;
    using ClosDotNet.Domain.CodeSet;
    using ClosDotNet.Domain.User;
    using System;
    using System.Collections.Generic;

    public interface ICustomerVO : IValueObject
    {
        string CustomerType { get; set; }

        string BorrowerStatus { get; set; }

        string BackendInd { get; set; }

        string CustomerName { get; set; }

        string PreviousCustomerName { get; set; }

        string CifNumber { get; set; }

        ICodeValueVO IdType { get; set; }

        string IdNumber { get; set; }

        ICountryVO IdIssuedCountry { get; set; }

        ICodeValueVO Classification { get; set; }

        DateTime? AccountOpenedDate { get; set; }

        int RelationshipSince { get; set; }

        DateTime? FacilityGrantedDate { get; set; }

        string HoldMailInd { get; set; }

        IList<IAddressVO> AddressList { get; set; }

        IUserVO RelationshipManager { get; set; }
    }

    [Serializable()]
    public class CustomerVO : ValueObject, ICustomerVO
    {
        public static readonly string[] EXCLUDE_COPY = new string[] 
        {
            "Id",
            "VersionNumber",
            "CreateBy",
            "CreationDate",
            "BorrowerStatus",
            "AddressList"
        };

        public CustomerVO() 
            : base ()
        {
            AddressList = new List<IAddressVO>();
        }

        public virtual string BorrowerStatus { get; set; }

        public virtual string BackendInd { get; set; }

        public virtual string CustomerType { get; set; }

        public virtual string CustomerName { get; set; }

        public virtual string PreviousCustomerName { get; set; }

        public virtual string CifNumber { get; set; }

        public virtual ICodeValueVO IdType { get; set; }

        public virtual string IdNumber { get; set; }

        public virtual ICountryVO IdIssuedCountry { get; set; }

        public virtual ICodeValueVO Classification { get; set; }

        public virtual DateTime? AccountOpenedDate { get; set; }

        public virtual int RelationshipSince { get; set; }

        public virtual DateTime? FacilityGrantedDate { get; set; }

        public virtual string HoldMailInd { get; set; }

        public virtual IList<IAddressVO> AddressList { get; set; }

        public virtual IUserVO RelationshipManager { get; set; }
    }
}