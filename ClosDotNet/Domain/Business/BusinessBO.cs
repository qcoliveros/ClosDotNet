namespace ClosDotNet.Domain.Business
{
    using ClosDotNet.Domain.Util;

    public interface IBusinessBO
    {
        IBusinessVO RetrieveBusiness(int businessId);

        IBusinessVO RetrieveBusinessByCustomerId(int customerId);

        IBusinessVO CreateBusiness(IBusinessVO business);

        IBusinessVO UpdateBusiness(IBusinessVO sessionBusiness, IBusinessVO formBusiness);
    }

    public class BusinessBOImpl : IBusinessBO
    {
        public IBusinessDAO BusinessDAO { private get; set; }

        public IBusinessVO RetrieveBusiness(int businessId)
        {
            return BusinessDAO.RetrieveBusiness(businessId);
        }

        public IBusinessVO RetrieveBusinessByCustomerId(int customerId)
        {
            return BusinessDAO.RetrieveBusinessByCustomerId(customerId);
        }

        public IBusinessVO CreateBusiness(IBusinessVO business)
        {
            return BusinessDAO.SaveBusiness(business);
        }

        public IBusinessVO UpdateBusiness(IBusinessVO sessionBusiness, IBusinessVO formBusiness)
        {
            AccessorUtil.copyValue(formBusiness, sessionBusiness, BusinessVO.EXCLUDE_COPY);

            return BusinessDAO.SaveBusiness(sessionBusiness);
        }
    }
}