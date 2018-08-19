namespace ClosDotNet.Helper
{
    using System;
    using System.Web.Mvc;
    using ClosDotNet.Domain.Customer;

    public class UIUtility
    {
        public static string RenderRowNumber(string pageNumber, int pageSize, int rowNumber)
        {
            return (rowNumber + ((pageNumber != null ? Int32.Parse(pageNumber) : 1) - 1) * pageSize).ToString();
        }

        public static bool IsCompanyCustomer(ICustomerVO customer)
        {
            if (customer != null 
                && Constants.GetEnumDescription(CustomerType.Corporate).Equals(customer.CustomerType))
            {
                return true;
            }

            return false;
        }

        public static bool IsIndividualCustomer(ICustomerVO customer)
        {
            if (customer != null
                && Constants.GetEnumDescription(CustomerType.Individual).Equals(customer.CustomerType))
            {
                return true;
            }

            return false;
        }
    }
}