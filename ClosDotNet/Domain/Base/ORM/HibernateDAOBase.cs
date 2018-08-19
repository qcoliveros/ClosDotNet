namespace ClosDotNet.Domain.Base.ORM
{
    using NHibernate;
    using System.Collections.Generic;

    public abstract class HibernateDAOBase
    {
        /// <summary>
        /// Session factory for sub-classes.
        /// </summary>
        public ISessionFactory SessionFactory { protected get; set; }

        /// <summary>
        /// Get's the current active session. Will retrieve session as managed by the 
        /// Open Session In View module if enabled.
        /// </summary>
        protected ISession CurrentSession
        {
            get  { return SessionFactory.GetCurrentSession(); }
        }

        protected IList<T> GetAll<T>() where T : class
        {
            ICriteria criteria = CurrentSession.CreateCriteria<T>();
            return criteria.List<T>();
        }
    }
}