using Common.Logging;

namespace ClosDotNet.Domain.User
{
    public interface IUserBO
    {
        IUserVO RetrieveUserByLoginId(string loginId);
    }

    public class UserBOImpl : IUserBO
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UserBOImpl));

        public IUserDAO UserDAO { private get; set; }

        public IUserVO RetrieveUserByLoginId(string loginId)
        {
            return UserDAO.RetrieveUserByLoginId(loginId);
        }
    }
}