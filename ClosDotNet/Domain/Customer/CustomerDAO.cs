using Common.Logging;

namespace ClosDotNet.Domain.Customer
{
    using ClosDotNet.Domain.Base.ORM;
    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Spring.Stereotype;
    using Spring.Transaction.Interceptor;
    using System;
    using System.Collections.Generic;

    public interface ICustomerDAO
    {
        IList<ICustomerVO> RetrieveCustomersByName(string customerName);

        ICustomerVO RetrieveCustomerByCifNumber(string cifNumber);

        ICustomerVO RetrieveCustomerByIdCombination(int idTypeId, string idNumber, string countryCode);

        bool CheckIfIdCombinationExists(int idTypeId, string idNumber, string countryCode, int excludeCustomerId);

        ICompanyCustomerVO RetrieveCompanyCustomer(int customerId);

        IIndividualCustomerVO RetrieveIndividualCustomer(int customerId);

        ICompanyCustomerVO SaveCompanyCustomer(ICompanyCustomerVO customer);

        IIndividualCustomerVO SaveIndividualCustomer(IIndividualCustomerVO customer);
    }

    [Repository]
    public class CustomerDAOImpl : HibernateDAOBase, ICustomerDAO
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CustomerDAOImpl));

        private static readonly string CHECK_ID_ALREADY_EXISTS = 
            @" SELECT 1 AS count FROM sml_customer sc "
                + " WHERE sc.id_type_id = :idTypeId AND sc.id_number = :IdNumber AND sc.id_country = :IdCountry "
                + " AND NOT (sc.id = :CustomerId) AND sc.status = :Status ";

        [Transaction(ReadOnly = true)]
        public virtual IList<ICustomerVO> RetrieveCustomersByName(string customerName)
        {
            if (!String.IsNullOrEmpty(customerName))
            {
                ICriteria criteria = CurrentSession.CreateCriteria<ICustomerVO>()
                    .Add(Restrictions.InsensitiveLike("CustomerName", (customerName + "%")))
                    .Add(Restrictions.Eq("BorrowerStatus", "ACTIVE"))
                    .SetProjection(Projections.ProjectionList()
                        .Add(Projections.Property("Id"), "Id")
                        .Add(Projections.Property("CustomerType"), "CustomerType")
                        .Add(Projections.Property("CustomerName"), "CustomerName")
                        .Add(Projections.Property("CifNumber"), "CifNumber")
                        .Add(Projections.Property("IdType"), "IdType")
                        .Add(Projections.Property("IdNumber"), "IdNumber")
                        .Add(Projections.Property("IdIssuedCountry"), "IdIssuedCountry")
                        .Add(Projections.Property("RelationshipManager"), "RelationshipManager"))
                    .SetResultTransformer(Transformers.AliasToBean<CustomerVO>());

                IList<ICustomerVO> customers = criteria.List<ICustomerVO>();
                Logger.Debug("RetrieveCustomersByName|Customer List: " + (customers != null ? customers.Count : 0));

                return customers;
            }

            return null;
        }

        [Transaction(ReadOnly = true)]
        public virtual ICustomerVO RetrieveCustomerByCifNumber(string cifNumber)
        {
            Logger.Debug("RetrieveCustomerByCifNumber|CIF Number: " + cifNumber);

            ICriteria criteria = CurrentSession.CreateCriteria<ICustomerVO>()
                .Add(Restrictions.Eq("CifNumber", cifNumber))
                .Add(Restrictions.Eq("BorrowerStatus", "ACTIVE"))
                .SetProjection(Projections.ProjectionList()
                    .Add(Projections.Property("Id"), "Id")
                    .Add(Projections.Property("CustomerType"), "CustomerType")
                    .Add(Projections.Property("CustomerName"), "CustomerName")
                    .Add(Projections.Property("CifNumber"), "CifNumber")
                    .Add(Projections.Property("IdType"), "IdType")
                    .Add(Projections.Property("IdNumber"), "IdNumber")
                    .Add(Projections.Property("IdIssuedCountry"), "IdIssuedCountry")
                    .Add(Projections.Property("RelationshipManager"), "RelationshipManager"))
                .SetResultTransformer(Transformers.AliasToBean<CustomerVO>())
                .SetMaxResults(1);

            IList<ICustomerVO> customers = criteria.List<ICustomerVO>();
            Logger.Debug("RetrieveCustomerByCifNumber|Customer List: " + (customers != null ? customers.Count : 0));
            if (customers != null && customers.Count > 0)
            {
                return customers[0];
            }

            return null;
        }

        [Transaction(ReadOnly = true)]
        public virtual ICustomerVO RetrieveCustomerByIdCombination(int idTypeId, string idNumber, string countryCode)
        {
            Logger.Debug("RetrieveCustomerByIdCombination|ID: " + idTypeId + "|" + idNumber + "|" + countryCode);

            ICriteria criteria = CurrentSession.CreateCriteria(typeof(ICustomerVO), "Customer")
                .Add(Restrictions.Eq("Customer.IdType.Id", idTypeId))
                .Add(Restrictions.Eq("Customer.IdNumber", idNumber))
                .Add(Restrictions.Eq("Customer.IdIssuedCountry.Code", countryCode))
                .Add(Restrictions.Eq("Customer.BorrowerStatus", "ACTIVE"))
                .SetProjection(Projections.ProjectionList()
                    .Add(Projections.Property("Id"), "Id")
                    .Add(Projections.Property("CustomerType"), "CustomerType")
                    .Add(Projections.Property("CustomerName"), "CustomerName")
                    .Add(Projections.Property("CifNumber"), "CifNumber")
                    .Add(Projections.Property("IdType"), "IdType")
                    .Add(Projections.Property("IdNumber"), "IdNumber")
                    .Add(Projections.Property("IdIssuedCountry"), "IdIssuedCountry")
                    .Add(Projections.Property("RelationshipManager"), "RelationshipManager"))
                .SetResultTransformer(Transformers.AliasToBean<CustomerVO>())
                .SetMaxResults(1);
            
            IList<ICustomerVO> customers = criteria.List<ICustomerVO>();
            Logger.Debug("RetrieveCustomerByIdCombination|Customer List: " 
                + (customers != null ? customers.Count : 0));
            if (customers != null && customers.Count > 0)
            {
                return customers[0];
            }

            return null;
        }

        [Transaction(ReadOnly = true)]
        public virtual bool CheckIfIdCombinationExists(
            int idTypeId, string idNumber, string countryCode, int excludeCustomerId)
        {
            Logger.Debug("CheckIfIdCombinationExists|ID: " + idTypeId + "|" + idNumber + "|" + countryCode);
            Logger.Debug("CheckIfIdCombinationExists|Exclude Customer: " + excludeCustomerId);

            int result = CurrentSession.CreateSQLQuery(CHECK_ID_ALREADY_EXISTS)
                .SetParameter("idTypeId", idTypeId)
                .SetParameter("IdNumber", idNumber)
                .SetParameter("IdCountry", countryCode)
                .SetParameter("CustomerId", excludeCustomerId)
                .SetParameter("Status", "ACTIVE")
                .UniqueResult<int>();

            if (result > 0)
            {
                return true;
            }

            return false;
        }

        [Transaction(ReadOnly = true)]
        public virtual ICompanyCustomerVO RetrieveCompanyCustomer(int customerId)
        {
            return CurrentSession.Get<ICompanyCustomerVO>(customerId);
        }

        [Transaction(ReadOnly = true)]
        public virtual IIndividualCustomerVO RetrieveIndividualCustomer(int customerId)
        {
            return CurrentSession.Get<IIndividualCustomerVO>(customerId);
        }

        [Transaction]
        public virtual ICompanyCustomerVO SaveCompanyCustomer(ICompanyCustomerVO customer)
        {
            if (customer.Id == 0)
            {
                return CreateCompanyCustomer(customer);
            }
            else
            {
                return UpdateCompanyCustomer(customer);
            }
        }

        [Transaction]
        public virtual IIndividualCustomerVO SaveIndividualCustomer(IIndividualCustomerVO customer)
        {
            if (customer.Id == 0)
            {
                return CreateIndividualCustomer(customer);
            }
            else
            {
                return UpdateIndividualCustomer(customer);
            }
        }

        #region Helper Methods
        private ICompanyCustomerVO CreateCompanyCustomer(ICompanyCustomerVO customer)
        {
            Logger.Debug("CreateCompanyCustomer|Create a new customer.");

            // LastUpdateBy will be set in the controller using the login user; hence, just copy.
            customer.CreateBy = customer.LastUpdateBy;
            customer.CreationDate = DateTime.Now;
            customer.LastUpdateDate = DateTime.Now;
            customer.BackendInd = "Y";
            customer.BorrowerStatus = "ACTIVE";
            
            CurrentSession.Save(customer);

            return customer;
        }

        private ICompanyCustomerVO UpdateCompanyCustomer(ICompanyCustomerVO customer)
        {
            Logger.Debug("UpdateCompanyCustomer|Update an existing customer.");

            customer.LastUpdateDate = DateTime.Now;
            CurrentSession.SaveOrUpdate(customer);

            return customer;
        }

        private IIndividualCustomerVO CreateIndividualCustomer(IIndividualCustomerVO customer)
        {
            Logger.Debug("CreateIndividualCustomer|Create a new customer.");

            // LastUpdateBy will be set in the controller using the login user; hence, just copy.
            customer.CreateBy = customer.LastUpdateBy;
            customer.CreationDate = DateTime.Now;
            customer.LastUpdateDate = DateTime.Now;
            customer.BackendInd = "Y";
            customer.BorrowerStatus = "ACTIVE";
            CurrentSession.Save(customer);

            return customer;
        }

        private IIndividualCustomerVO UpdateIndividualCustomer(IIndividualCustomerVO customer)
        {
            Logger.Debug("UpdateIndividualCustomer|Update an existing customer.");

            customer.LastUpdateDate = DateTime.Now;
            CurrentSession.SaveOrUpdate(customer);

            return customer;
        }
        #endregion Helper Methods
    }
}