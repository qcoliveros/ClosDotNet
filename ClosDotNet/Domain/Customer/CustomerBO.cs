using Common.Logging;

namespace ClosDotNet.Domain.Customer
{
    using ClosDotNet.Domain.Util;
    using System;
    using System.Collections.Generic;

    public interface ICustomerBO
    {
        IList<ICustomerVO> RetrieveCustomersByName(string customerName);

        ICustomerVO RetrieveCustomerByCifNumber(string cifNumber);

        ICustomerVO RetrieveCustomerByIdCombination(int idTypeId, string idNumber, string countryCode);

        bool CheckIfIdCombinationExists(int idTypeId, string idNumber, string countryCode, int excludeCustomerId);

        ICompanyCustomerVO CreateCompanyCustomer(ICompanyCustomerVO customer);

        ICompanyCustomerVO UpdateCompanyCustomer(
            ICompanyCustomerVO sessionCustomer, ICompanyCustomerVO formCustomer);

        ICompanyCustomerVO RetrieveCompanyCustomer(int customerId);

        IIndividualCustomerVO CreateIndividualCustomer(IIndividualCustomerVO customer);

        IIndividualCustomerVO UpdateIndividualCustomer(
            IIndividualCustomerVO sessionCustomer, IIndividualCustomerVO formCustomer);

        IIndividualCustomerVO RetrieveIndividualCustomer(int customerId);
    }

    public class CustomerBOImpl : ICustomerBO
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CustomerBOImpl));

        public ICustomerDAO CustomerDAO { private get; set; }

        public IList<ICustomerVO> RetrieveCustomersByName(string customerName)
        {
            return CustomerDAO.RetrieveCustomersByName(customerName);
        }

        public ICustomerVO RetrieveCustomerByCifNumber(string cifNumber)
        {
            return CustomerDAO.RetrieveCustomerByCifNumber(cifNumber);
        }

        public ICustomerVO RetrieveCustomerByIdCombination(int idTypeId, string idNumber, string countryCode)
        {
            return CustomerDAO.RetrieveCustomerByIdCombination(idTypeId, idNumber, countryCode);
        }

        public bool CheckIfIdCombinationExists(int idTypeId, string idNumber, string countryCode, int excludeCustomerId)
        {
            return CustomerDAO.CheckIfIdCombinationExists(idTypeId, idNumber, countryCode, excludeCustomerId);
        }

        public ICompanyCustomerVO CreateCompanyCustomer(ICompanyCustomerVO customer)
        {
            Logger.Debug("CreateCompanyCustomer|Create a new customer.");

            MapAddressList(customer);

            // Should be message call to host system.
            customer.CifNumber = "90000" + customer.IdNumber;

            return CustomerDAO.SaveCompanyCustomer(customer);
        }

        public ICompanyCustomerVO UpdateCompanyCustomer(
            ICompanyCustomerVO sessionCustomer, ICompanyCustomerVO formCustomer)
        {
            Logger.Debug("UpdateCompanyCustomer|Update an existing customer.");

            AccessorUtil.copyValue(formCustomer, sessionCustomer, CustomerVO.EXCLUDE_COPY);
            MapAddressList(sessionCustomer, formCustomer);

            if (string.IsNullOrEmpty(sessionCustomer.CifNumber))
            {
                // Should be message call to host system.
                sessionCustomer.CifNumber = "90000" + sessionCustomer.IdNumber;
            }

            return CustomerDAO.SaveCompanyCustomer(sessionCustomer);
        }

        public ICompanyCustomerVO RetrieveCompanyCustomer(int customerId)
        {
            return CustomerDAO.RetrieveCompanyCustomer(customerId);
        }

        public IIndividualCustomerVO CreateIndividualCustomer(IIndividualCustomerVO customer)
        {
            Logger.Debug("CreateIndividualCustomer|Create a new customer.");

            MapAddressList(customer);

            // Should be message call to host system.
            customer.CifNumber = "90000" + customer.IdNumber;

            return CustomerDAO.SaveIndividualCustomer(customer);
        }

        public IIndividualCustomerVO UpdateIndividualCustomer(
            IIndividualCustomerVO sessionCustomer, IIndividualCustomerVO formCustomer)
        {
            Logger.Debug("UpdateIndividualCustomer|Update an existing customer.");

            AccessorUtil.copyValue(formCustomer, sessionCustomer, CustomerVO.EXCLUDE_COPY);
            MapAddressList(sessionCustomer, formCustomer);

            if (string.IsNullOrEmpty(sessionCustomer.CifNumber))
            {
                // Should be message call to host system.
                sessionCustomer.CifNumber = "90000" + sessionCustomer.IdNumber;
            }            

            return CustomerDAO.SaveIndividualCustomer(sessionCustomer);
        }

        public IIndividualCustomerVO RetrieveIndividualCustomer(int customerId)
        {
            return CustomerDAO.RetrieveIndividualCustomer(customerId);
        }

        private void MapAddressList(ICustomerVO sessionCustomer)
        {
            if (sessionCustomer.AddressList.Count > 0)
            {
                foreach (IAddressVO address in sessionCustomer.AddressList)
                {
                    address.Customer = sessionCustomer;
                }
            }
        }

        private void MapAddressList(ICustomerVO sessionCustomer, ICustomerVO formCustomer)
        {
            if (formCustomer.AddressList.Count > 0)
            {
                foreach (IAddressVO formAddress in formCustomer.AddressList)
                {
                    if (formAddress.Id == 0)
                    {
                        formAddress.Customer = sessionCustomer;
                        sessionCustomer.AddressList.Add(formAddress);
                    }
                    else
                    {
                        foreach (IAddressVO sessionAddress in sessionCustomer.AddressList)
                        {
                            if (formAddress.Id == sessionAddress.Id)
                            {
                                AccessorUtil.copyValue(formAddress, sessionAddress, AddressVO.EXCLUDE_COPY);
                                sessionAddress.LastUpdateBy = sessionCustomer.LastUpdateBy;
                                sessionAddress.LastUpdateDate = DateTime.Now;
                            }
                        }
                    }
                }
            }
        }
    }
}