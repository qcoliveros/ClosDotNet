namespace ClosDotNet.Domain.CodeSet
{
    using ClosDotNet.Domain.Base.ORM;
    using Spring.Stereotype;
    using Spring.Transaction.Interceptor;
    using System.Collections.Generic;

    public interface ICountryDAO
    {
        IList<ICountryVO> RetrieveAllCountries();
    }

    [Repository]
    public class CountryDAOImpl : HibernateDAOBase, ICountryDAO
    {
        [Transaction(ReadOnly = true)]
        public virtual IList<ICountryVO> RetrieveAllCountries()
        {
            return GetAll<ICountryVO>();
        }
    }

}