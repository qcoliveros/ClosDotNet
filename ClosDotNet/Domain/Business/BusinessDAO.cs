using Common.Logging;

namespace ClosDotNet.Domain.Business
{
    using ClosDotNet.Domain.Base.ORM;
    using NHibernate.Criterion;
    using Spring.Stereotype;
    using Spring.Transaction.Interceptor;
    using System;

    public interface IBusinessDAO
    {
        IBusinessVO RetrieveBusiness(int businessId);

        IBusinessVO RetrieveBusinessByCustomerId(int customerId);

        IBusinessVO SaveBusiness(IBusinessVO businessId);
    }

    [Repository]
    public class BusinessDAOImpl : HibernateDAOBase, IBusinessDAO
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BusinessDAOImpl));

        [Transaction(ReadOnly = true)]
        public virtual IBusinessVO RetrieveBusiness(int businessId)
        {
            return CurrentSession.Get<IBusinessVO>(businessId);
        }

        [Transaction(ReadOnly = true)]
        public IBusinessVO RetrieveBusinessByCustomerId(int customerId)
        {
            return CurrentSession.CreateCriteria<IBusinessVO>()
                .Add(Restrictions.Eq("Customer.Id", customerId))
                .UniqueResult<IBusinessVO>();
        }

        [Transaction]
        public virtual IBusinessVO SaveBusiness(IBusinessVO business)
        {
            if (business.Id == 0)
            {
                return CreateBusiness(business);
            }
            else
            {
                return UpdateBusiness(business);
            }
        }

        #region Helper Methods
        private IBusinessVO CreateBusiness(IBusinessVO business)
        {
            // LastUpdateBy will be set in the controller using the login user; hence, just copy.
            business.CreateBy = business.LastUpdateBy;
            business.CreationDate = DateTime.Now;
            business.LastUpdateDate = DateTime.Now;
            CurrentSession.Save(business);

            return business;
        }

        private IBusinessVO UpdateBusiness(IBusinessVO business)
        {
            business.LastUpdateDate = DateTime.Now;
            CurrentSession.SaveOrUpdate(business);

            return business;
        }
        #endregion  Helper Methods
    }
}