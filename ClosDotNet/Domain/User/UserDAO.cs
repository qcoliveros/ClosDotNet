namespace ClosDotNet.Domain.User
{
    using ClosDotNet.Domain.Base.ORM;
    using NHibernate;
    using NHibernate.Transform;
    using Spring.Stereotype;
    using Spring.Transaction.Interceptor;
    using System.Collections.Generic;
    using System.Linq;

    public interface IUserDAO
    {
        IUserVO RetrieveUserById(int id);

        IUserVO RetrieveUserByLoginId(string loginId);

        IList<IUserVO> RetrieveUserList(IList<string> excludedLoginIdList);
    }

    [Repository]
    public class UserDAOImpl : HibernateDAOBase, IUserDAO
    {
        private static readonly string RETRIEVE_USER = 
            @" SELECT su.id AS Id, su.login_id AS LoginId, su.full_name AS FullName FROM sml_user su "
                + " WHERE su.master_id IS NULL AND su.deprecated = 'N' AND su.status = 'ACTIVE' ";

        [Transaction(ReadOnly = true)]
        public virtual IUserVO RetrieveUserById(int id)
        {
            string query = RETRIEVE_USER + " AND su.id = :UserId ";

            return CurrentSession.CreateSQLQuery(query)
                .SetParameter("UserId", id)
                .SetResultTransformer(Transformers.AliasToBean<UserVO>())
                .UniqueResult<IUserVO>();
        }

        [Transaction(ReadOnly = true)]
        public virtual IUserVO RetrieveUserByLoginId(string loginId)
        {
            string query = RETRIEVE_USER + " AND su.login_id = :LoginId ";

            return CurrentSession.CreateSQLQuery(query)
                .SetParameter("LoginId", loginId)
                .SetResultTransformer(Transformers.AliasToBean<UserVO>())
                .UniqueResult<IUserVO>();
        }

        [Transaction(ReadOnly = true)]
        public virtual IList<IUserVO> RetrieveUserList(IList<string> excludedLoginIdList)
        {
            string query = RETRIEVE_USER;
            if (excludedLoginIdList != null && excludedLoginIdList.Count > 0)
            { 
                for (int counter = 0; counter < excludedLoginIdList.Count(); counter++)
                {
                    query += (" AND su.login_id <> :LoginId" + counter);
                }
            }

            ISQLQuery sqlQuery = CurrentSession.CreateSQLQuery(query);
            sqlQuery.SetResultTransformer(Transformers.AliasToBean<UserVO>());

            if (excludedLoginIdList != null && excludedLoginIdList.Count > 0)
            {
                for (int counter = 0; counter < excludedLoginIdList.Count(); counter++)
                {
                    sqlQuery.SetParameter(("LoginId" + counter), excludedLoginIdList.ElementAt(counter));
                }
            }

            return sqlQuery.List<IUserVO>();                
        }
    }
}