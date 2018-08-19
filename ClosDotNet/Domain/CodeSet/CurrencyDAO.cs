namespace ClosDotNet.Domain.CodeSet
{
    using ClosDotNet.Domain.Base.ORM;
    using Spring.Stereotype;
    using Spring.Transaction.Interceptor;
    using System.Collections.Generic;

    public interface ICurrencyDAO
    {
        IList<ICurrencyVO> RetrieveAllCurrencies();
    }

    [Repository]
    public class CurrencyDAOImpl : HibernateDAOBase, ICurrencyDAO
    {
        [Transaction(ReadOnly = true)]
        public virtual IList<ICurrencyVO> RetrieveAllCurrencies()
        {
            return GetAll<ICurrencyVO>();
        }
    }
}