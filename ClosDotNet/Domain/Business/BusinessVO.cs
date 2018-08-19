namespace ClosDotNet.Domain.Business
{
    using ClosDotNet.Domain.Base.Model;
    using ClosDotNet.Domain.CodeSet;
    using ClosDotNet.Domain.Customer;
    using System;

    public interface IBusinessVO : IValueObject
    {
        ICustomerVO Customer { get; set; }

        IAmountVO CurrentPaidUpCapital { get; set; }

        string BorrowerBackground { get; set; }

        string BusinessActivities { get; set; }

        string Management { get; set; }

        string AccountStrategy { get; set; }
    }

    [Serializable]
    public class BusinessVO : ValueObject, IBusinessVO
    {
        public static readonly string[] EXCLUDE_COPY = new string[] 
        {
            "Id",
            "VersionNumber",
            "CreateBy",
            "CreationDate",
            "Customer"
        };

        public BusinessVO()
            : base()
        {
            CurrentPaidUpCapital = new AmountVO();
        }

        public virtual ICustomerVO Customer { get; set; }
        
        public virtual IAmountVO CurrentPaidUpCapital { get; set; }

        public virtual string BorrowerBackground { get; set; }

        public virtual string BusinessActivities { get; set; }

        public virtual string Management { get; set; }

        public virtual string AccountStrategy { get; set; }
    }
}