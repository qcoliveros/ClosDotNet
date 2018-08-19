namespace ClosDotNet.Domain.Customer
{
    using ClosDotNet.Domain.Base.Model;
    using ClosDotNet.Domain.CodeSet;
    using System;

    public interface IAddressVO : IValueObject
    {
        ICustomerVO Customer { get; set; }

        string AddressType { get; set; }

        ICodeValueVO AddressFormat { get; set; }

        string StructuredBlock { get; set; }

        string StructuredStreet { get; set; }

        string StructuredStorey { get; set; }

        string StructuredUnit { get; set; }

        string StructuredBuilding { get; set; }

        string StructuredCity { get; set; }

        string StructuredPostalCode { get; set; }

        ICountryVO StructuredCountry { get; set; }

        string StructuredPoBox { get; set; }

        string UnstructuredAddress1 { get; set; }

        string UnstructuredAddress2 { get; set; }

        string UnstructuredAddress3 { get; set; }

        string UnstructuredAddress4 { get; set; }

        ICodeValueVO ResidentialOwnershipType { get; set; }

        ICodeValueVO ResidentialType { get; set; }

        int ResidentialStayStartYear { get; set; }
    }

    [Serializable()]
    public class AddressVO : ValueObject, IAddressVO
    {
        public static readonly string[] EXCLUDE_COPY = new string[] 
        {
            "Id",
            "VersionNumber",
            "CreateBy",
            "CreationDate",
            "Customer",
            "AddressType"
        };

        public AddressVO() 
            : base ()
        {
        }

        public virtual ICustomerVO Customer { get; set; }

        public virtual string AddressType { get; set; }

        public virtual ICodeValueVO AddressFormat { get; set; }

        public virtual string StructuredBlock { get; set; }

        public virtual string StructuredStreet { get; set; }

        public virtual string StructuredStorey { get; set; }

        public virtual string StructuredUnit { get; set; }

        public virtual string StructuredBuilding { get; set; }

        public virtual string StructuredCity { get; set; }

        public virtual string StructuredPostalCode { get; set; }

        public virtual ICountryVO StructuredCountry { get; set; }

        public virtual string StructuredPoBox { get; set; }

        public virtual string UnstructuredAddress1 { get; set; }

        public virtual string UnstructuredAddress2 { get; set; }

        public virtual string UnstructuredAddress3 { get; set; }

        public virtual string UnstructuredAddress4 { get; set; }

        public virtual ICodeValueVO ResidentialOwnershipType { get; set; }

        public virtual ICodeValueVO ResidentialType { get; set; }

        public virtual int ResidentialStayStartYear { get; set; }
    }
}